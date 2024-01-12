using System.Collections.Generic;

[System.Serializable]
public class PlanetarySystemList
{
    public List<PlanetarySystem> planetarySystems;

    // Constructor to initialize the list
    public PlanetarySystemList(List<PlanetarySystem> PlanetarySystems)
    {
        this.planetarySystems = PlanetarySystems;
    }
}