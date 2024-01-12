using UnityEngine;

public class SpaceshipAttributes : MonoBehaviour
{
    [Header("Spaceship Attributes")]
    [SerializeField] private string shipName;
    [SerializeField] private Spaceship.SHIPCLASS shipClass;
    [SerializeField] private int strength;
    [SerializeField] private string specialAbility;
    [SerializeField] private int warpRange;
    [SerializeField] private float warpSpeed;

    public void SetAttributes(string shipName, Spaceship.SHIPCLASS shipClass, int strength, string specialAbility, int warpRange, float warpSpeed)
    {
        this.shipName = shipName;
        this.shipClass = shipClass;
        this.strength = strength;
        this.specialAbility = specialAbility;
        this.warpRange = warpRange;
        this.warpSpeed = warpSpeed;
    }
}
