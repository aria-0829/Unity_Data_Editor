using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OfficerWindow : EditorWindow
{
    private string officerName;
    private string race;
    private int attackStrength;
    private int defenceStrength;
    private int healthStrength;
    private int overallStrength;
    private Spaceship.SHIPCLASS shipSpecialty;

    private string homePlanetSystem = "";
    private int selectedIndex = 0;

    private List<Officer> loadedOfficers = new List<Officer>();
    private int selectedOfficerIndex = 0;

    [MenuItem("PROG56693 Tools Project/Officers")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(OfficerWindow));
    }

    private void OnEnable()
    {
        OfficerDataManager.Instance.ReadJSON();
        loadedOfficers = OfficerDataManager.Instance.officers;
    }

    private void OnGUI()
    {
        GUILayout.Label("Officers Editor", EditorStyles.boldLabel);

        officerName = EditorGUILayout.TextField("Officer's name: ", officerName);
        race = EditorGUILayout.TextField("Officer's Race: ", race);
        attackStrength = EditorGUILayout.IntSlider("Attack Strength: ", attackStrength, 0, 100);
        defenceStrength = EditorGUILayout.IntSlider("Defence Strength: ", defenceStrength, 0, 100);
        healthStrength = EditorGUILayout.IntSlider("Health Strength: ", healthStrength, 0, 100);
        overallStrength = EditorGUILayout.IntSlider("Overall Strength: ", overallStrength, 0, 100);
        shipSpecialty = (Spaceship.SHIPCLASS)EditorGUILayout.EnumPopup("Ship Specialty: ", shipSpecialty);

        if (PlanetarySystem.planetarySystemNames.Count > 0)
        {
            string[] homePlanetSystemArray = PlanetarySystem.planetarySystemNames.ToArray();
            selectedIndex = EditorGUILayout.Popup("Home Planet System: ", selectedIndex, homePlanetSystemArray);
            homePlanetSystem = PlanetarySystem.planetarySystemNames[selectedIndex];
        }
        else
        {
            EditorGUILayout.LabelField("No Home Planet System available, create Planetary Systems first!");
        }

        if (GUILayout.Button("Create Officer Object in Scene"))
        {
            GameObject officerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Officer.prefab");
            GameObject newofficer = PrefabUtility.InstantiatePrefab(officerPrefab) as GameObject;
            newofficer.transform.position = new Vector3(0, 0, 0);

            OfficerAttributes officerAttributes = newofficer.GetComponent<OfficerAttributes>();
            officerAttributes.SetAttributes(officerName, race, attackStrength, defenceStrength, healthStrength, overallStrength, shipSpecialty, homePlanetSystem);

            Undo.RegisterCreatedObjectUndo(newofficer, "Create " + newofficer.name);
            Selection.activeObject = newofficer;
            Debug.Log("Officer Created");
        }

        GUILayout.Label("Information", EditorStyles.boldLabel);
        GUILayout.Label("Officer's name: " + officerName);
        GUILayout.Label("Race: " + race);
        GUILayout.Label("Attack Strength: " + attackStrength);
        GUILayout.Label("Defence Strength: " + defenceStrength);
        GUILayout.Label("Health Strength: " + healthStrength);
        GUILayout.Label("Overall Strength: " + overallStrength);
        GUILayout.Label("Ship Specialty: " + shipSpecialty);
        GUILayout.Label("Home Planet System: " + homePlanetSystem);

        if (GUILayout.Button("Add Entry to JSON"))
        {
            Officer o = new Officer();

            o.officerName = officerName;
            o.race = race;
            o.attackStrength = attackStrength;
            o.defenceStrength = defenceStrength;
            o.healthStrength = healthStrength;
            o.overallStrength = overallStrength;
            o.shipSpecialty = shipSpecialty;
            o.homePlanetSystem = homePlanetSystem;

            loadedOfficers.Add(o);
            OfficerDataManager.Instance.officers = loadedOfficers;
            OfficerDataManager.Instance.WriteJSON();
        }

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        GUILayout.Label("Loaded Officers Data", EditorStyles.boldLabel);

        string[] officerNames = loadedOfficers.ConvertAll(s => s.officerName).ToArray();
        selectedOfficerIndex = EditorGUILayout.Popup("Select Officer", selectedOfficerIndex, officerNames);
        GUILayout.Space(5);

        if (selectedOfficerIndex >= 0 && selectedOfficerIndex < loadedOfficers.Count)
        {
            Officer selectedOfficer = loadedOfficers[selectedOfficerIndex];

            GUILayout.Label("Selected Officer Information", EditorStyles.boldLabel);
            GUILayout.Label("Officer's name: " + selectedOfficer.officerName);
            GUILayout.Label("Officer's race: " + selectedOfficer.race);
            GUILayout.Label("Officer's attack strength: " + selectedOfficer.attackStrength);
            GUILayout.Label("Officer's defence strength: " + selectedOfficer.defenceStrength);
            GUILayout.Label("Officer's health strength: " + selectedOfficer.healthStrength);
            GUILayout.Label("Officer's ship specialty: " + selectedOfficer.shipSpecialty);
            GUILayout.Label("Officer's home planet system: " + selectedOfficer.homePlanetSystem);
        }
    }
}
