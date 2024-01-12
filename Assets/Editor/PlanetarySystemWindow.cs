using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlanetarySystemWindow : EditorWindow
{
    private string planetarySystemName;
    private string indigenousRace;
    private int numberOfPlanets;

    private List<PlanetarySystem> loadedPlanetarySystems = new List<PlanetarySystem>();
    private int selectedPlanetarySystemIndex = 0;

    [MenuItem("PROG56693 Tools Project/PlanetarySystems")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PlanetarySystemWindow));
    }

    private void OnEnable()
    {
        PlanetarySystemDataManager.Instance.ReadJSON();
        loadedPlanetarySystems = PlanetarySystemDataManager.Instance.planetarySystems;
    }

    private void OnGUI()
    {
        GUILayout.Label("Planetary Systems Editor", EditorStyles.boldLabel);

        planetarySystemName = EditorGUILayout.TextField("Planetary System's name: ", planetarySystemName);
        indigenousRace = EditorGUILayout.TextField("Indigenous Race: ", indigenousRace);
        numberOfPlanets = EditorGUILayout.IntSlider("Number of Planets: ", numberOfPlanets, 0, 100);

        if (GUILayout.Button("Create Planetary System Object in Scene"))
        {
            PlanetarySystem.AddEnumValue(planetarySystemName);

            GameObject planetarySystemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PlanetarySystem.prefab");
            GameObject newplanetarySystem = PrefabUtility.InstantiatePrefab(planetarySystemPrefab) as GameObject;
            newplanetarySystem.transform.position = new Vector3(0, 0, 0);

            PlanetarySystemAttributes planetarySystemAttributes = newplanetarySystem.GetComponent<PlanetarySystemAttributes>();
            planetarySystemAttributes.SetAttributes(planetarySystemName, indigenousRace, numberOfPlanets);

            Undo.RegisterCreatedObjectUndo(newplanetarySystem, "Create " + newplanetarySystem.name);
            Selection.activeObject = newplanetarySystem;
            Debug.Log("Planetary System Created");
        }

        GUILayout.Label("Information", EditorStyles.boldLabel);
        GUILayout.Label("Planetary System's name: " + planetarySystemName);
        GUILayout.Label("Indigenous Race: " + indigenousRace);
        GUILayout.Label("Number of Planets: " + numberOfPlanets);

        if (GUILayout.Button("Add Entry to JSON"))
        {
            PlanetarySystem ps = new PlanetarySystem();

            ps.planetarySystemName = planetarySystemName;
            ps.indigenousRace = indigenousRace;
            ps.numberOfPlanets = numberOfPlanets;

            loadedPlanetarySystems.Add(ps);
            PlanetarySystemDataManager.Instance.planetarySystems = loadedPlanetarySystems;
            PlanetarySystemDataManager.Instance.WriteJSON();
        }

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        GUILayout.Label("Loaded Planetary Systems Data", EditorStyles.boldLabel);

        string[] planetarySystemNames = loadedPlanetarySystems.ConvertAll(s => s.planetarySystemName).ToArray();
        selectedPlanetarySystemIndex = EditorGUILayout.Popup("Select Planetary System", selectedPlanetarySystemIndex, planetarySystemNames);
        GUILayout.Space(5);

        if (selectedPlanetarySystemIndex >= 0 && selectedPlanetarySystemIndex < loadedPlanetarySystems.Count)
        {
            PlanetarySystem selectedPlanetarySystem = loadedPlanetarySystems[selectedPlanetarySystemIndex];

            GUILayout.Label("Selected Planetary System Information", EditorStyles.boldLabel);
            GUILayout.Label("Planetary System's name: " + selectedPlanetarySystem.planetarySystemName);
            GUILayout.Label("Planetary System's indigenous race: " + selectedPlanetarySystem.indigenousRace);
            GUILayout.Label("Planetary System's number of planets: " + selectedPlanetarySystem.numberOfPlanets);
        }
    }
}
