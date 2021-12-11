using Models;
using Privi.Backend.Engine;
using UnityEngine.Events;

namespace Backend
{
    class AddFeatureRequest : ARequest
    {
        public AddFeatureRequest(int cemeteryId, int tombstoneId, int featureId, string place, UnityAction<ARequest> APICallSucceed, UnityAction<ARequest> APICallFailed)
        {
            this.url = Constants.LocalAddress + $"/cemetery/{cemeteryId}/tombstone/{tombstoneId}/feature";
            this.data = "{\"featureId\" : " + featureId + ", \"place\" : \"" + place + "\"}";
            this.httpRequestType = Enums.RequestType.POST;
            SetActionCode();
            SetSuccessAndOutputKeys();
            this.FetchSucceeded= APICallSucceed;
            this.FetchFailed = APICallFailed;
            this.retryOnFail = true;
        }

        public override void Push()
        {
            RequestSender.Instance.FetchObjectRequest<Feature>(this);
        }

        public override void SetActionCode()
        {
            this.actionCode = Enums.ActionType.PostFeature;
        }

        public override void SetSuccessAndOutputKeys() { }
    }
}