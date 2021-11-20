
using System;
using System.Collections.Generic;
using Backend;
using UnityEngine;
using UnityEngine.Events;

namespace Backend
{
    [Serializable]
    public abstract class ARequest
    {
        public Enums.RequestType httpRequestType { get; set; }
        public Enums.ActionType actionCode { get; set; }
        public string url { get; set; }
        public string data { get; set; }
        
        public string successKey = "";
        
        // If outputKey == "", returns the whole JSON
        public string outputKey = "";

        public bool retryOnFail { get; set; }

        public int retryCount = 0;

        public int retryLimit = 3;
        
        public Enums.ResultType resultType;

        public string _stringResult;
        public AData _objectResult;
        public List<AData> _arrayResult;

        public UnityAction<ARequest> FetchFailed;
        public UnityAction<ARequest> FetchSucceeded;
        
        public abstract void Push();
        public abstract void SetActionCode();
        public abstract void SetSuccessAndOutputKeys();
        
        
        public void Send()
        {
           Push();
        }

        public void OnSuccess()
        {
            FetchSucceeded(this);
        }
        
        public void OnFail()
        {
            FetchFailed(this);
        }

        public void SetResult(string result)
        {
            resultType = Enums.ResultType.Text;
            _stringResult = result;
        }

        public void SetResult(AData result)
        {
            resultType = Enums.ResultType.Object;
            _objectResult = result;
        }

        public void SetResult(List<AData> result)
        {
            resultType = Enums.ResultType.Array;
            _arrayResult = new List<AData>(result);
        }
    }
}