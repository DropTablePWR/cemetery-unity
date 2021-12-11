using Models;
using Privi.Backend.Engine;
using UnityEngine.Events;

namespace Backend
{
    class DeleteFeatureRequest : ARequest
    {
        public DeleteFeatureRequest(int cemeteryId, int tombstoneId, int featureId, UnityAction<ARequest> APICallSucceed, UnityAction<ARequest> APICallFailed)
        {
            this.url = Constants.LocalAddress + $"/cemetery/{cemeteryId}/tombstone/{tombstoneId}/feature/{featureId}";
            this.httpRequestType = Enums.RequestType.DELETE;
            SetActionCode();
            SetSuccessAndOutputKeys();
            this.FetchSucceeded= APICallSucceed;
            this.FetchFailed = APICallFailed;
            this.retryOnFail = true;
        }

        public override void Push()
        {
            RequestSender.Instance.FetchTextRequest(this);
        }

        public override void SetActionCode()
        {
            this.actionCode = Enums.ActionType.DeleteFeature;
        }

        public override void SetSuccessAndOutputKeys() { }
    }
}