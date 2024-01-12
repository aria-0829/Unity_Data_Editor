using UnityEngine;

public class PlanetarySystemAttributes : MonoBehaviour
{
    [Header("Planetary System Attributes")]
    [SerializeField] private string planetarySystemName;
    [SerializeField] private string indigenousRace;
    [SerializeField] private int numberOfPlanets;

    public void SetAttributes(string planetarySystemName, string indigenousRace, int numberOfPlanets)
    {
        this.planetarySystemName = planetarySystemName;
        this.indigenousRace = indigenousRace;
        this.numberOfPlanets = numberOfPlanets;
    }
}
