using UnityEngine;

public class MissionAttributes : MonoBehaviour
{
    [Header("Mission Attributes")]
    [SerializeField] private string missionName;
    [SerializeField] private string rewards;
    [SerializeField] private string description;
    [SerializeField] private string location;

    public void SetAttributes(string missionName, string rewards, string description, string location)
    {
        this.missionName = missionName;
        this.rewards = rewards;
        this.description = description;
        this.location = location;
    }
}
