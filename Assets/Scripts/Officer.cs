using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using UnityEngine;

[System.Serializable]
[BsonIgnoreExtraElements]
public class Officer
{
    [BsonElement(elementName: "OfficerName")]
    public string officerName;
    [BsonElement(elementName: "Race")]
    public string race;
    [BsonElement(elementName: "AttackStrength")]
    public int attackStrength;
    [BsonElement(elementName: "DefenceStrength")]
    public int defenceStrength;
    [BsonElement(elementName: "HealthStrength")]
    public int healthStrength;
    [BsonElement(elementName: "OverallStrength")]
    public int overallStrength;
    [BsonElement(elementName: "ShipSpecialty")]
    public Spaceship.SHIPCLASS shipSpecialty;
    [BsonElement(elementName: "HomePlanetSystem")]
    public string homePlanetSystem;
}
