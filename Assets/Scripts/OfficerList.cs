using System.Collections.Generic;

[System.Serializable]
public class OfficerList
{
    public List<Officer> officers;

    // Constructor to initialize the list
    public OfficerList(List<Officer> Officers)
    {
        this.officers = Officers;
    }
}