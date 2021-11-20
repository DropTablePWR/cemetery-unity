using Models;
using Privi.Backend.Engine;
using UnityEngine.Events;

namespace Backend
{
    class GetCemeteryRequest : ARequest
    {
        public GetCemeteryRequest(int cemeteryId, UnityAction<ARequest> APICallSucceed, UnityAction<ARequest> APICallFailed)
        {
            this.url = Constants.LocalAddress + $"cemetery/{cemeteryId}/list/";
            this.httpRequestType = Enums.RequestType.GET;
            SetActionCode();
            SetSuccessAndOutputKeys();
            this.FetchSucceeded= APICallSucceed;
            this.FetchFailed = APICallFailed;
            this.retryOnFail = true;
        }

        public override void Push()
        {
            RequestSender.Instance.FetchObjectRequest<Cemetery>(this);
        }

        public override void SetActionCode()
        {
            this.actionCode = Enums.ActionType.GetCemetery;
        }

        public override void SetSuccessAndOutputKeys() { }
    }
}