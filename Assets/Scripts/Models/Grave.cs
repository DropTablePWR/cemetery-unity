using System;
using Backend;
using Models;

[Serializable]
public class Grave : AData
{
    public Guest guest;
    public int gridX;
    public int gridY;
    public int id;
    public Feature[] features;
    private int _cemeteryId;

    public void SetCemeteryId(int id)
    {
        _cemeteryId = id;
    }

    public int GetCemeteryId()
    {
        return _cemeteryId;
    }
    
    public Grave(Guest guest, int gridX, int gridY, int id)
    {
        this.guest = guest;
        this.gridX = gridX;
        this.gridY = gridY;
        this.id = id;
    }
}