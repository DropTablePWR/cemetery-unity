using System;
using Backend;

[Serializable]
public class Grave : AData
{
    public Guest guest;
    public int gridX;
    public int gridY;
    public int id;

    public Grave(Guest guest, int gridX, int gridY, int id)
    {
        this.guest = guest;
        this.gridX = gridX;
        this.gridY = gridY;
        this.id = id;
    }
}