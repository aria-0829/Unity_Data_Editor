using System.Collections.Generic;

[System.Serializable]
public class MissionList
{
    public List<Mission> missions;

    // Constructor to initialize the list
    public MissionList(List<Mission> Missions)
    {
        this.missions = Missions;
    }
}