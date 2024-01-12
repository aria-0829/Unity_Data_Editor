using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using UnityEngine;

[System.Serializable]
[BsonIgnoreExtraElements]
public class PlanetarySystem
{
    [BsonElement(elementName: "PlanetarySystemName")]
    public string planetarySystemName;
    [BsonElement(elementName: "IndigenousRace")]
    public string indigenousRace;
    [BsonElement(elementName: "NumberOfPlanets")]
    public int numberOfPlanets;

    public static List<string> planetarySystemNames { get; private set; } = new List<string>();

    public static void AddEnumValue(string value)
    {
        planetarySystemNames.Add(value);
    }
}
