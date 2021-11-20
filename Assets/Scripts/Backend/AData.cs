using UnityEngine;

namespace Backend
{
    [System.Serializable]
    public abstract class AData
    {
        public virtual void Preprocess()
        {
            jsonData = JsonUtility.ToJson(this);
        }

        public string jsonData;
    }
}