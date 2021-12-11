using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.Events;

namespace Backend
{
    public class BackendManager : BaseMonoBehaviour
    {
        public void GetGrave(int graveId, int cemeteryId, UnityAction<Grave> onSuccess, UnityAction<ARequest> onFail)
        {

            GetGraveRequest request = new GetGraveRequest(graveId, cemeteryId, OnSuccess, onFail);
            request.Push();
            
            void OnSuccess(ARequest request)
            {
                Grave grave = (Grave) request._objectResult;
                onSuccess.Invoke(grave);
            }            
        }
        
        public void GetCemetery(int cemeteryId, UnityAction<Cemetery> onSuccess, UnityAction<ARequest> onFail)
        {

            GetCemeteryRequest request = new GetCemeteryRequest(cemeteryId, OnSuccess, onFail);
            request.Push();
            
            void OnSuccess(ARequest request)
            {
                Cemetery cemetery = (Cemetery) request._objectResult;
                onSuccess.Invoke(cemetery);
            }            
        }

        public void PutFeature(ItemSlot slot, int itemId, UnityAction<Feature> onSuccess, UnityAction<ARequest> onFail)
        {
            AddFeatureRequest request = new AddFeatureRequest(slot._manager.GetCemeteryId(), slot._manager.GetGraveId(), itemId, slot.GetSlotId(), OnSuccess, onFail);
            request.Push();
            
            void OnSuccess(ARequest request)
            {
                Feature feature = (Feature) request._objectResult;
                onSuccess.Invoke(feature);
            }         
        }
        
        public void DeleteFeature(ItemSlot slot, int itemId, UnityAction<ARequest> onSuccess, UnityAction<ARequest> onFail)
        {
            DeleteFeatureRequest request = new DeleteFeatureRequest(slot._manager.GetCemeteryId(), slot._manager.GetGraveId(), itemId, onSuccess, onFail);
            request.Push();
        }
        
        public void GetAllCemeteries(UnityAction<List<Cemetery>> onSuccess, UnityAction<ARequest> onFail)
        {
            GetAllCemeteriesRequest request = new GetAllCemeteriesRequest(OnSuccess, onFail);
            request.Push();
            
            void OnSuccess(ARequest request)
            {
                List<Cemetery> list = new List<Cemetery>();
                foreach (AData data in request._arrayResult)
                {
                    list.Add((Cemetery)data);
                }
                onSuccess.Invoke(list);
            }         
        }
        
    }
}