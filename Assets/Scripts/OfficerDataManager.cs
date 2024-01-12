using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class OfficerDataManager
{
    private string fileLocation = Application.persistentDataPath + "/oData.json";
    public List<Officer> officers = new List<Officer>();

    // Singleton
    private static OfficerDataManager instance;
    public static OfficerDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new OfficerDataManager();
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

            OfficerList officerList = JsonUtility.FromJson<OfficerList>(jsonContent);
            officers = officerList.officers;
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
        dbCommandReadValues.CommandText = "SELECT * FROM Officers";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

        while (dataReader.Read())
        {
            Officer Officer = new Officer();

            Officer.officerName = dataReader.GetString(1);
            Officer.race = dataReader.GetString(2);
            Officer.attackStrength = dataReader.GetInt32(3);
            Officer.defenceStrength = dataReader.GetInt32(4);
            Officer.healthStrength = dataReader.GetInt32(5);
            Officer.overallStrength = dataReader.GetInt32(6);
            Officer.shipSpecialty = (Spaceship.SHIPCLASS)dataReader.GetInt32(7);
            Officer.homePlanetSystem = dataReader.GetString(8);

            officers.Add(Officer);
            Debug.Log("Officer Retrived: " + Officer.officerName);
        }

        Debug.Log("All Officers retrived: " + officers.Count);
        dbConnection.Close();
    }

    public void WriteJSON()
    {
        OfficerList officerList = new OfficerList(officers);
        string jsonString = JsonUtility.ToJson(officerList);
        Debug.Log(jsonString);

        File.WriteAllText(fileLocation, jsonString);
        Debug.Log("File saved to: " + fileLocation);
    }

    public void WriteSQLite()
    {
        // Insert a value into the table.
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand cmd = dbConnection.CreateCommand();

        cmd.CommandText = "DELETE FROM Officers;";
        cmd.ExecuteNonQuery();

        foreach (Officer oData in officers)
        {
            cmd.CommandText = $"INSERT OR REPLACE INTO Officers VALUES (null, '{oData.officerName}', '{oData.race}', " +
            $"{oData.attackStrength}, {oData.defenceStrength}, {oData.healthStrength}, {oData.overallStrength}, {(int)oData.shipSpecialty}, '{oData.homePlanetSystem}');";

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
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOt EXISTS Officers" +
                                            "(id INTEGER PRIMARY KEY, " +
                                            "officerName text, " +
                                            "race text, " +
                                            "attackStrength integer, " +
                                            "defenceStrength integer, " +
                                            "healthStrength integer, " +
                                            "overallStrength integer, " +
                                            "shipSpecialty integer, " +
                                            "homePlanetSystem text);";

        dbCommandCreateTable.ExecuteReader();
        return dbConnection;
    }
}
