using UnityEngine;

public class OfficerAttributes : MonoBehaviour
{
    [Header("Officer Attributes")]
    [SerializeField] private string officerName;
    [SerializeField] private string race;
    [SerializeField] private int attackStrength;
    [SerializeField] private int defenceStrength;
    [SerializeField] private int healthStrength;
    [SerializeField] private int overallStrength;
    [SerializeField] private Spaceship.SHIPCLASS shipSpecialty;
    [SerializeField] private string homePlanetSystem;

    public void SetAttributes(string officerName, string race, int attackStrength, int defenceStrength, int healthStrength,int overallStrength, Spaceship.SHIPCLASS shipSpecialty, string homePlanetSystem)
    {
        this.officerName = officerName;
        this.race = race;
        this.attackStrength = attackStrength;
        this.defenceStrength = defenceStrength;
        this.healthStrength = healthStrength;
        this.overallStrength = overallStrength;
        this.shipSpecialty = shipSpecialty;
        this.homePlanetSystem = homePlanetSystem;
    }
        
}
