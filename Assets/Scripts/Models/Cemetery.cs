using System;
using System.Collections.Generic;
using Backend;

namespace Models
{
    [Serializable]
    public class Cemetery : AData
    {
        public int id;
        public string name;
        public string description;
        public int type;
        public int maxGridX;
        public int maxGridY;
        public List<Grave> tombstones;


        public Cemetery(int id, string name, string description, int type, int maxGridX, int maxGridY,
            List<Grave> tombstones)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.type = type;
            this.maxGridX = maxGridX;
            this.maxGridY = maxGridY;
            this.tombstones = tombstones;
        }
    }
}