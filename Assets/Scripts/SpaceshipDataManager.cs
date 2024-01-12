using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class SpaceshipDataManager
{
    private string fileLocation = Application.persistentDataPath + "/spData.json";
    public List<Spaceship> spaceships = new List<Spaceship>();

    // Singleton
    private static SpaceshipDataManager instance;
    public static SpaceshipDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SpaceshipDataManager();
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

            // Deserialize the JSON array into a list of spaceships
            SpaceshipList spaceshipList = JsonUtility.FromJson<SpaceshipList>(jsonContent);
            spaceships = spaceshipList.spaceships;

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
        dbCommandReadValues.CommandText = "SELECT * FROM Spaceships";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

        while (dataReader.Read())
        {
            Spaceship spaceship = new Spaceship();

            spaceship.shipName = dataReader.GetString(1);
            spaceship.shipClass = (Spaceship.SHIPCLASS)dataReader.GetInt32(2);
            spaceship.strength = dataReader.GetInt32(3);
            spaceship.specialAbility = dataReader.GetString(4);
            spaceship.warpRange = dataReader.GetInt32(5);
            spaceship.warpSpeed = dataReader.GetFloat(6);

            spaceships.Add(spaceship);
            Debug.Log("spaceship Retrived: " + spaceship.shipName);
        }

        Debug.Log("All spaceships retrived: " + spaceships.Count);
        dbConnection.Close();
    }

    public void WriteJSON()
    {
        // Serialize the list of spaceships into JSON
        SpaceshipList spaceshipList = new SpaceshipList(spaceships);
        string jsonString = JsonUtility.ToJson(spaceshipList);
        Debug.Log(jsonString);

        File.WriteAllText(fileLocation, jsonString);
        Debug.Log("File saved to: " + fileLocation);
    }

    public void WriteSQLite()
    {
        // Insert a value into the table
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand cmd = dbConnection.CreateCommand();

        cmd.CommandText = "DELETE FROM Spaceships;";
        cmd.ExecuteNonQuery();

        foreach (Spaceship spData in spaceships)
        {
            cmd.CommandText = $"INSERT OR REPLACE INTO Spaceships VALUES (null, '{spData.shipName}', {(int)spData.shipClass}, {spData.strength}, '{spData.specialAbility}', " +
            $"{spData.warpRange}, {spData.warpSpeed});";

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
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOt EXISTS Spaceships" +
                                            "(id INTEGER PRIMARY KEY, " +
                                            "shipName text, " +
                                            "shipClass integer, " +
                                            "strength integer, " +
                                            "specialAbility text, " +
                                            "warpRange integer, " +
                                            "warpSpeed real);";

        dbCommandCreateTable.ExecuteReader();
        return dbConnection;
    }
}
