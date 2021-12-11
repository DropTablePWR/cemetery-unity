using UnityEngine;

namespace Backend
{
    public class ItemSlot : BaseMonoBehaviour
    {
        private GameObject spawned;
        private string slotId;
        public GraveManager _manager;
        private int currentFeatureId = -1;

        public bool IsEmpty()
        {
            return spawned == null;
        }

        public void SetSlotId(string slot)
        {
            this.slotId = slot;
        }

        public string GetSlotId()
        {
            return slotId;
        }

        public void InstantiateItem(Item item, int id)
        {
            spawned  = Instantiate(item.gameObject, transform);
            currentFeatureId = id;
        }
        
        public void DestroyItem()
        {
            Destroy(spawned);
            spawned = null;
        }

        private void PlacingFeatureFailed(ARequest request)
        {
            Debug.Log("Failed to place feature");
            SetNone();
        }
        
        private void DeletingFeatureFailed(ARequest request)
        {
            Debug.Log("Failed tod elete");
            SetNone();
        }
        
        private void PlacingFeatureSuccess(Feature feature)
        {
            _manager.SpawnItem(feature);
            SetNone();
        }
        
        private void DeletingFeatureSuccess(ARequest request)
        {
            SetNone();
            DestroyItem();
        }

        private void SetNone()
        {
            App.Instance.GetLookAt().SetMode(LookAtManager.Mode.NONE);
        }

        private void SetPending()
        {
            App.Instance.GetLookAt().SetMode(LookAtManager.Mode.PENDING);
        }

        public void RequestAddingFeature(int featureId)
        {
            SetPending();
            App.Instance.GetBackend().PutFeature(this, featureId, PlacingFeatureSuccess, PlacingFeatureFailed);
        }
        
        public void RequestDeletingFeature()
        {
            SetPending();
            App.Instance.GetBackend().DeleteFeature(this, currentFeatureId, DeletingFeatureSuccess, DeletingFeatureFailed);
        }
    }
}