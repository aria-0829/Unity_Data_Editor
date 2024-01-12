using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;

public class MongoDBManager
{
    private MongoClient dbClient = new MongoClient("mongodb+srv://arialian:h5mL9Nr5CbPoezda@cluster0.nc60pq6.mongodb.net/");

    public void MongoDBSave()
    {
        var database = dbClient.GetDatabase("PROG56693_Unity");

        // Save Spaceships
        List<Spaceship> spaceships = SpaceshipDataManager.Instance.spaceships;

        if (SpaceshipDataManager.Instance.spaceships.Any())
        {
            var spaceshipsCollection = database.GetCollection<Spaceship>("Spaceships");
            spaceshipsCollection.DeleteMany(new BsonDocument());
            spaceshipsCollection.InsertMany(spaceships);
        }
        else
        {
            Debug.Log("None spaceship uploaded!");
        }

        // Save Officers
        List<Officer> officers = OfficerDataManager.Instance.officers;

        if (OfficerDataManager.Instance.officers.Any())
        {
            var officersCollection = database.GetCollection<Officer>("Officers");
            officersCollection.DeleteMany(new BsonDocument());
            officersCollection.InsertMany(officers);
        }
        else
        {
            Debug.Log("None officer uploaded!");
        }

        // Save Planetary Systems
        List<PlanetarySystem> planetarySystems = PlanetarySystemDataManager.Instance.planetarySystems;

        if (PlanetarySystemDataManager.Instance.planetarySystems.Any())
        {
            var planetarySystemsCollection = database.GetCollection<PlanetarySystem>("PlanetarySystems");
            planetarySystemsCollection.DeleteMany(new BsonDocument());
            planetarySystemsCollection.InsertMany(planetarySystems);
        }
        else
        {
            Debug.Log("None planetary system uploaded!");
        }

        // Save Missions
        List<Mission> missions = MissionDataManager.Instance.missions;

        if (MissionDataManager.Instance.missions.Any())
        {
            var missionsCollection = database.GetCollection<Mission>("Missions");
            missionsCollection.DeleteMany(new BsonDocument());
            missionsCollection.InsertMany(missions);
        }
        else
        {
            Debug.Log("None mission uploaded!");
        }
    }

    async public void MongoDBLoad()
    {
        var database = dbClient.GetDatabase("PROG56693_Unity");

        List<Spaceship> spaceshipsCollection =
                await database.GetCollection<Spaceship>("Spaceships").Find(_ => true).As<Spaceship>().ToListAsync();
        SpaceshipDataManager.Instance.spaceships = spaceshipsCollection;
        Debug.Log("All spaceships retrived: " + spaceshipsCollection.Count);

        List<Officer> officersCollection =
                await database.GetCollection<Officer>("Officers").Find(_ => true).As<Officer>().ToListAsync();
        OfficerDataManager.Instance.officers = officersCollection;
        Debug.Log("All officers retrived: " + officersCollection.Count);

        List<PlanetarySystem> planetarySystemsCollection =
                await database.GetCollection<PlanetarySystem>("PlanetarySystems").Find(_ => true).As<PlanetarySystem>().ToListAsync();
        PlanetarySystemDataManager.Instance.planetarySystems = planetarySystemsCollection;
        Debug.Log("All planetary systems retrived: " + planetarySystemsCollection.Count);

        List<Mission> missionsCollection =
                await database.GetCollection<Mission>("Missions").Find(_ => true).As<Mission>().ToListAsync();
        MissionDataManager.Instance.missions = missionsCollection;
        Debug.Log("All missions retrived: " + missionsCollection.Count);

    }
}
