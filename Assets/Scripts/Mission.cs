using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using UnityEngine;

[System.Serializable]
[BsonIgnoreExtraElements]
public class Mission
{
    [BsonElement(elementName: "MissionName")]
    public string missionName;
    [BsonElement(elementName: "Rewards")]
    public string rewards;
    [BsonElement(elementName: "Description")]
    public string description;
    [BsonElement(elementName: "Location")]
    public string location;
}
