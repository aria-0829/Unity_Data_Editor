using System.Collections.Generic;

[System.Serializable]
public class SpaceshipList
{
    public List<Spaceship> spaceships;

    // Constructor to initialize the list
    public SpaceshipList(List<Spaceship> spaceships)
    {
        this.spaceships = spaceships;
    }
}