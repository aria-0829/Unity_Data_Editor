using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Reflection;
using UnityEditor.PackageManager;
using System.Collections.ObjectModel;

public class MenuItems : MonoBehaviour
{
    [MenuItem("PROG56693 Tools Project/Data_Save to SQLite")]
    public static void SQLiteSaveData ()
    {
        SpaceshipDataManager.Instance.WriteSQLite();
        SpaceshipDataManager.Instance.WriteJSON();

        OfficerDataManager.Instance.WriteSQLite();
        OfficerDataManager.Instance.WriteJSON();

        PlanetarySystemDataManager.Instance.WriteSQLite();
        PlanetarySystemDataManager.Instance.WriteJSON();

        MissionDataManager.Instance.WriteSQLite();
        MissionDataManager.Instance.WriteJSON();

        Debug.Log("SQLite and JSON save complete!");
    }

    [MenuItem("PROG56693 Tools Project/Data_Load from SQLite")]
    public static void SQLiteLoadData()
    {
        SpaceshipDataManager.Instance.ReadSQLite();
        OfficerDataManager.Instance.ReadSQLite();
        PlanetarySystemDataManager.Instance.ReadSQLite();
        MissionDataManager.Instance.ReadSQLite();

        Debug.Log("SQLite load complete!");
    }

    [MenuItem("PROG56693 Tools Project/Data_Save to MongoDB")]
    public static void MongoDBSaveData()
    {
        MongoDBManager mgdbm = new MongoDBManager();
        mgdbm.MongoDBSave();

        SpaceshipDataManager.Instance.WriteJSON();
        OfficerDataManager.Instance.WriteJSON();
        PlanetarySystemDataManager.Instance.WriteJSON();
        MissionDataManager.Instance.WriteJSON();

        Debug.Log("MongoDB and JSON save complete!");
    }

    [MenuItem("PROG56693 Tools Project/Data_Load from MongoDB")]
    public static void MongoDBLoadData()
    {
        MongoDBManager mgdbm = new MongoDBManager();
        mgdbm.MongoDBLoad();

        Debug.Log("MongoDB load complete!");
    }
}
