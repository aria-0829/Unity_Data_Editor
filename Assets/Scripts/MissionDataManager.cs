using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class MissionDataManager
{
    private string fileLocation = Application.persistentDataPath + "/mData.json";
    public List<Mission> missions = new List<Mission>();

    // Singleton
    private static MissionDataManager instance;
    public static MissionDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MissionDataManager();
            }
            return instance;
        }
    }

    public void ReadJSON()
    {
        if (File.Exists(fileLocation))
        {
            string jsonContent = File.ReadAllText(fileLocation);
            Debug.Log("File loading from: " + fileLocation);

            MissionList missionList = JsonUtility.FromJson<MissionList>(jsonContent);
            missions = missionList.missions;
            Debug.Log("File contents retrived: " + jsonContent);
        }
        else
        {
            Debug.Log("File not found at: " + fileLocation);
        }
    }

    public void ReadSQLite()
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM Missions";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

        while (dataReader.Read())
        {
            Mission Mission = new Mission();

            Mission.missionName = dataReader.GetString(1);
            Mission.rewards = dataReader.GetString(2);
            Mission.description = dataReader.GetString(3);
            Mission.location = dataReader.GetString(4);

            missions.Add(Mission);
            Debug.Log("Mission Retrived: " + Mission.missionName);
        }

        Debug.Log("All Missions retrived: " + missions.Count);
        dbConnection.Close();
    }

    public void WriteJSON()
    {
        MissionList missionList = new MissionList(missions);
        string jsonString = JsonUtility.ToJson(missionList);
        Debug.Log(jsonString);

        File.WriteAllText(fileLocation, jsonString);
        Debug.Log("File saved to: " + fileLocation);
    }

    public void WriteSQLite()
    {
        // Insert a value into the table.
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand cmd = dbConnection.CreateCommand();

        cmd.CommandText = "DELETE FROM Missions;";
        cmd.ExecuteNonQuery();

        foreach (Mission mData in missions)
        {
            cmd.CommandText = $"INSERT OR REPLACE INTO Missions VALUES (null, '{mData.missionName}', '{mData.rewards}', '{mData.description}','{mData.location}');";

            cmd.ExecuteNonQuery();
        }

        dbConnection.Close();
    }

    private IDbConnection CreateAndOpenDatabase()
    {
        string dbUri = "URI=file: MyDatabase.sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();

        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand();
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOt EXISTS Missions" +
                                            "(id INTEGER PRIMARY KEY, " +
                                            "missionName text, " +
                                            "rewards text, " +
                                            "description text, " +
                                            "location text);";

        dbCommandCreateTable.ExecuteReader();
        return dbConnection;
    }
}
