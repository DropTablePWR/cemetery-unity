using System;
using System.Collections.Generic;

[Serializable]
public class Cemetery
{
    public int id;
    public string name;
    public string description;
    public int type;
    public List<List<Field>> area;

    public Cemetery(int id, string name, string description, int type, List<List<Field>> area)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.type = type;
        this.area = area;
    }
}