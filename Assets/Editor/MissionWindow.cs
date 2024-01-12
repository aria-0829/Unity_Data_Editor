using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MissionWindow : EditorWindow
{
    private string missionName;
    private string rewards;
    private string description;

    private string location = "";
    private int selectedIndex = 0;

    private List<Mission> loadedMissions = new List<Mission>();
    private int selectedMissionIndex = 0;

    [MenuItem("PROG56693 Tools Project/Missions")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MissionWindow));
    }

    private void OnEnable()
    {
        MissionDataManager.Instance.ReadJSON();
        loadedMissions = MissionDataManager.Instance.missions;
    }

    private void OnGUI()
    {
        GUILayout.Label("Missions Editor", EditorStyles.boldLabel);

        missionName = EditorGUILayout.TextField("Mission's name: ", missionName);
        rewards = EditorGUILayout.TextField("Mission's rewards: ", rewards);
        description = EditorGUILayout.TextField("Mission's description: ", description);

        if (PlanetarySystem.planetarySystemNames.Count > 0)
        {
            string[] locationArray = PlanetarySystem.planetarySystemNames.ToArray();
            selectedIndex = EditorGUILayout.Popup("Mission's location: ", selectedIndex, locationArray);
            location = PlanetarySystem.planetarySystemNames[selectedIndex];
        }
        else
        {
            EditorGUILayout.LabelField("No Location available, create Planetary Systems first!");
        }

        if (GUILayout.Button("Create Mission Object in Scene"))
        {
            GameObject missionPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Mission.prefab");
            GameObject newMission = PrefabUtility.InstantiatePrefab(missionPrefab) as GameObject;
            newMission.transform.position = new Vector3(0, 0, 0);

            MissionAttributes missionAttributes = newMission.GetComponent<MissionAttributes>();
            missionAttributes.SetAttributes(missionName, rewards, description, location);

            Undo.RegisterCreatedObjectUndo(newMission, "Create " + newMission.name);
            Selection.activeObject = newMission;

            Debug.Log("Mission Created");
        }

        GUILayout.Label("Information", EditorStyles.boldLabel);
        GUILayout.Label("Mission's name: " + missionName);
        GUILayout.Label("Mission's rewards: " + rewards);
        GUILayout.Label("Mission's description: " + description);
        GUILayout.Label("Location: " + location);

        if (GUILayout.Button("Add Entry to JSON"))
        {
            Mission m = new Mission();

            m.missionName = missionName;
            m.rewards = rewards;
            m.description = description;
            m.location = location;

            loadedMissions.Add(m);
            MissionDataManager.Instance.missions = loadedMissions;
            MissionDataManager.Instance.WriteJSON();
        }

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        GUILayout.Label("Loaded Missions Data", EditorStyles.boldLabel);

        string[] missionNames = loadedMissions.ConvertAll(s => s.missionName).ToArray();
        selectedMissionIndex = EditorGUILayout.Popup("Select Mission", selectedMissionIndex, missionNames);
        GUILayout.Space(5);

        if (selectedMissionIndex >= 0 && selectedMissionIndex < loadedMissions.Count)
        {
            Mission selectedMission = loadedMissions[selectedMissionIndex];

            GUILayout.Label("Selected Mission Information", EditorStyles.boldLabel);
            GUILayout.Label("Mission's name: " + selectedMission.missionName);
            GUILayout.Label("Mission's rewards: " + selectedMission.rewards);
            GUILayout.Label("Mission's description: " + selectedMission.description);
            GUILayout.Label("Mission's location: " + selectedMission.location);
        }
    }
}
