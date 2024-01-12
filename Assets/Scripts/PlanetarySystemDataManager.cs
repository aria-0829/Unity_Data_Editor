using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class PlanetarySystemDataManager
{
    private string fileLocation = Application.persistentDataPath + "/psData.json";
    public List<PlanetarySystem> planetarySystems = new List<PlanetarySystem>();

    // Singleton
    private static PlanetarySystemDataManager instance;
    public static PlanetarySystemDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlanetarySystemDataManager();
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

            PlanetarySystemList planetarySystemList = JsonUtility.FromJson<PlanetarySystemList>(jsonContent);
            planetarySystems = planetarySystemList.planetarySystems;
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
        dbCommandReadValues.CommandText = "SELECT * FROM PlanetarySystems";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

        while (dataReader.Read())
        {
            PlanetarySystem PlanetarySystem = new PlanetarySystem();

            PlanetarySystem.planetarySystemName = dataReader.GetString(1);
            PlanetarySystem.indigenousRace = dataReader.GetString(2);
            PlanetarySystem.numberOfPlanets = dataReader.GetInt32(3);

            planetarySystems.Add(PlanetarySystem);
            Debug.Log("PlanetarySystem Retrived: " + PlanetarySystem.planetarySystemName);
        }

        Debug.Log("All PlanetarySystems retrived: " + planetarySystems.Count);
        dbConnection.Close();
    }

    public void WriteJSON()
    {
        PlanetarySystemList planetarySystemList = new PlanetarySystemList(planetarySystems);
        string jsonString = JsonUtility.ToJson(planetarySystemList);
        Debug.Log(jsonString);

        File.WriteAllText(fileLocation, jsonString);
        Debug.Log("File saved to: " + fileLocation);
    }

    public void WriteSQLite()
    {
        // Insert a value into the table.
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand cmd = dbConnection.CreateCommand();

        cmd.CommandText = "DELETE FROM PlanetarySystems;";
        cmd.ExecuteNonQuery();

        foreach (PlanetarySystem psData in planetarySystems)
        {
            cmd.CommandText = $"INSERT OR REPLACE INTO PlanetarySystems VALUES (null, '{psData.planetarySystemName}', '{psData.indigenousRace}', {psData.numberOfPlanets});";

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
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOt EXISTS PlanetarySystems" +
                                            "(id INTEGER PRIMARY KEY, " +
                                            "planetarySystemName text, " +
                                            "indigenousRace text, " +
                                            "numberOfPlanets integer);";

        dbCommandCreateTable.ExecuteReader();
        return dbConnection;
    }
}
