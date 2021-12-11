using System;
using UnityEngine;

namespace Backend
{
    [Serializable]
    public class Feature : AData
    {
        public int id;
        public FeatureData feature;
        public string place;
    }
}