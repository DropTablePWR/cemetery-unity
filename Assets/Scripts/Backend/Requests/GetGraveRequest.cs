using Privi.Backend.Engine;
using UnityEngine.Events;

namespace Backend
{
    class GetGraveRequest : ARequest
    {
        public GetGraveRequest(int graveId, int cemeteryId, UnityAction<ARequest> APICallSucceed, UnityAction<ARequest> APICallFailed)
        {
            this.url = Constants.LocalAddress + $"cemetery/{cemeteryId}/tombstone/{graveId}";
            this.httpRequestType = Enums.RequestType.GET;
            SetActionCode();
            SetSuccessAndOutputKeys();
            this.FetchSucceeded= APICallSucceed;
            this.FetchFailed = APICallFailed;
            this.retryOnFail = true;
        }

        public override void Push()
        {
            RequestSender.Instance.FetchObjectRequest<Grave>(this);
        }

        public override void SetActionCode()
        {
            this.actionCode = Enums.ActionType.GetGrave;
        }

        public override void SetSuccessAndOutputKeys() { }
    }
}