using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using UnityEngine;

[System.Serializable]
[BsonIgnoreExtraElements]
public class Spaceship
{
    [BsonElement(elementName: "ShipName")]
    public string shipName;
    [BsonElement(elementName: "Strength")]
    public int strength;
    [BsonElement(elementName: "SpecialAbility")]
    public string specialAbility;
    [BsonElement(elementName: "WarpRange")]
    public int warpRange;
    [BsonElement(elementName: "WarpSpeed")]
    public float warpSpeed;

    public enum SHIPCLASS
    {
        Battleship = 0,
        Explorer = 1,
        Interceptor = 2,
        Miner = 3
    }

    [BsonElement(elementName: "ShipClass")]
    public SHIPCLASS shipClass;
}
