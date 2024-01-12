using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpaceshipWindow : EditorWindow
{
    private string shipName;
    private int strength;
    private string specialAbility;
    private int warpRange;
    private float warpSpeed;
    private Spaceship.SHIPCLASS shipClass;

    private GameObject spaceshipPrefab;

    private List<Spaceship> loadedSpaceships = new List<Spaceship>();
    private int selectedSpaceshipIndex = 0;


    [MenuItem("PROG56693 Tools Project/Spaceships")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow(typeof(SpaceshipWindow));
    }

    private void OnEnable()
    {
        SpaceshipDataManager.Instance.ReadJSON();
    }

    private void OnGUI()
    {
        loadedSpaceships = SpaceshipDataManager.Instance.spaceships;

        GUILayout.Label("Spaceships Editor", EditorStyles.boldLabel);

        shipName = EditorGUILayout.TextField("Ship's name: ", shipName);
        shipClass = (Spaceship.SHIPCLASS)EditorGUILayout.EnumPopup("Ship's class: ", shipClass);
        strength = EditorGUILayout.IntField("Ship's strength: ", strength);
        specialAbility = EditorGUILayout.TextField("Special ability: ", specialAbility);
        warpRange = EditorGUILayout.IntSlider("Warp range: ", warpRange, 0, 100);
        warpSpeed = EditorGUILayout.Slider("Warp speed: ", warpSpeed, 0.0f, 10.0f);

        if (GUILayout.Button("Create Spaceship Object in Scene"))
        {
            if (shipClass == Spaceship.SHIPCLASS.Battleship)
            {
                spaceshipPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Battleship.prefab");
            }
            else if (shipClass == Spaceship.SHIPCLASS.Explorer)
            {
                spaceshipPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Explorer.prefab");
            }
            else if (shipClass == Spaceship.SHIPCLASS.Interceptor)
            {
                spaceshipPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Interceptor.prefab");
            }
            else if (shipClass == Spaceship.SHIPCLASS.Miner)
            {
                spaceshipPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Miner.prefab");
            }

            GameObject newSpaceship = PrefabUtility.InstantiatePrefab(spaceshipPrefab) as GameObject;
            newSpaceship.transform.position = new Vector3(0, 0, 0);

            SpaceshipAttributes spaceshipAttributes = newSpaceship.GetComponent<SpaceshipAttributes>();
            spaceshipAttributes.SetAttributes(shipName, shipClass, strength, specialAbility, warpRange, warpSpeed);

            Undo.RegisterCreatedObjectUndo(newSpaceship, "Create " + newSpaceship.name);
            Selection.activeObject = newSpaceship;

            Debug.Log($"{shipClass} Created");
        }

        GUILayout.Label("Information", EditorStyles.boldLabel);
        GUILayout.Label("Ship's name: " + shipName);
        GUILayout.Label("Ship's class: " + shipClass);
        GUILayout.Label("Ship's strength: " + strength);
        GUILayout.Label("Ship's special ability: " + specialAbility);
        GUILayout.Label("Ship's warp range: " + warpRange);
        GUILayout.Label("Ship's warp speed: " + warpSpeed);

        if (GUILayout.Button("Add Entry to JSON"))
        {
            Spaceship sp = new Spaceship();

            sp.shipName = shipName;
            sp.shipClass = shipClass;
            sp.strength = strength;
            sp.specialAbility = specialAbility;
            sp.warpRange = warpRange;
            sp.warpSpeed = warpSpeed;

            loadedSpaceships.Add(sp);
            SpaceshipDataManager.Instance.spaceships = loadedSpaceships;
            SpaceshipDataManager.Instance.WriteJSON();
        }

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        GUILayout.Label("Loaded Spaceships Data", EditorStyles.boldLabel);

        string[] spaceshipNames = loadedSpaceships.ConvertAll(s => s.shipName).ToArray();
        selectedSpaceshipIndex = EditorGUILayout.Popup("Select Spaceship", selectedSpaceshipIndex, spaceshipNames);
        GUILayout.Space(5);

        if (selectedSpaceshipIndex >= 0 && selectedSpaceshipIndex < loadedSpaceships.Count)
        {
            Spaceship selectedSpaceship = loadedSpaceships[selectedSpaceshipIndex];

            GUILayout.Label("Selected Spaceship Information", EditorStyles.boldLabel);
            GUILayout.Label("Ship's name: " + selectedSpaceship.shipName);
            GUILayout.Label("Ship's class: " + selectedSpaceship.shipClass);
            GUILayout.Label("Ship's strength: " + selectedSpaceship.strength);
            GUILayout.Label("Ship's special ability: " + selectedSpaceship.specialAbility);
            GUILayout.Label("Ship's warp range: " + selectedSpaceship.warpRange);
            GUILayout.Label("Ship's warp speed: " + selectedSpaceship.warpSpeed);
        }
    }
}
