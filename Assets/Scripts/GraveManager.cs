using System;
using UnityEngine;

namespace Backend
{
    public class GraveManager : BaseMonoBehaviour
    {
        [SerializeField] private Item[] _items;
        [SerializeField] private ItemSlot[] _slots;
        private Grave _grave;

        public int GetCemeteryId()
        {
            if (_grave == null)
                return -1;

            return _grave.GetCemeteryId();
        }
        
        public int GetGraveId()
        {
            if (_grave == null)
                return -1;

            return _grave.id;
        }

        public Item GetItem(int index)
        {
            int ind = index - 1;

            if (ind > _items.Length)
            {
                Debug.Log("not enough items");
                return null;
            }
            
            return _items[ind];
        }

        public ItemSlot GetSlot(string place)
        {
            foreach (var slot in _slots)
            {
                if (slot.GetSlotId().Equals(place))
                    return slot;
            }

            return null;
        }

        protected override void Awake()
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i].SetSlotId(i.ToString());
            }   
        }

        public void SpawnItem(Feature feature)
        {
            ItemSlot slot = GetSlot(feature.place);
            if (slot == null)
            {
                Debug.LogWarning($"Slot with {feature.place} wasn't found");
                return;
            }
            
                    
            Item item = GetItem(feature.feature.id);

            if (item == null)
            {
                Debug.LogWarning($"Item with {feature.feature.id} wasn't found");
                return;
            }
            
            slot.InstantiateItem(item, feature.id);
        }

        public void InitializeGrave(Grave grave)
        {
            _grave = grave;
            Debug.Log(_grave.features);
            
            if (_grave.features != null)
            {
                foreach (var feature in _grave.features)
                {
                    SpawnItem(feature);
                }
            }
        }
    }
}