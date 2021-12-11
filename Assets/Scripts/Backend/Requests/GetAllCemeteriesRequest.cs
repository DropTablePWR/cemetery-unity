using Models;
using Privi.Backend.Engine;
using UnityEngine.Events;

namespace Backend
{
    class GetAllCemeteriesRequest : ARequest
    {
        public GetAllCemeteriesRequest(UnityAction<ARequest> APICallSucceed, UnityAction<ARequest> APICallFailed)
        {
            this.url = Constants.LocalAddress + $"/cemetery";
            this.httpRequestType = Enums.RequestType.GET;
            SetActionCode();
            SetSuccessAndOutputKeys();
            this.FetchSucceeded= APICallSucceed;
            this.FetchFailed = APICallFailed;
            this.retryOnFail = true;
        }

        public override void Push()
        {
            RequestSender.Instance.FetchArrayRequest<Cemetery>(this);
        }

        public override void SetActionCode()
        {
            this.actionCode = Enums.ActionType.GetAll;
        }

        public override void SetSuccessAndOutputKeys() { }
    }
}