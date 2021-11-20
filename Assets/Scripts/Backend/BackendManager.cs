using Models;
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
        
    }
}