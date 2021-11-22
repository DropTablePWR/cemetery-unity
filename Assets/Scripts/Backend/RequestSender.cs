using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Backend;
using Newtonsoft.Json.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
namespace Privi.Backend.Engine
{
    public class RequestSender : PersistentSingleton<RequestSender>
    {
        public class Event : UnityEvent<ARequest>
        {
        }

        // This method is used to retrieve an object and serialize it
        public void FetchObjectRequest<T>(ARequest request)
        {
            StartCoroutine(RequestObjectCoroutine<T>(request));
        }

        // This method is used to retrieve an array of objects and serialize it
        public void FetchArrayRequest<T>(ARequest request)
        {
            StartCoroutine(RequestArrayCoroutine<T>(request));
        }

        // This method is used when we don't want to serialize an object and just retrieve the text response (POST/DELETE etc.)
        public void FetchTextRequest(ARequest request)
        {
            StartCoroutine(RequestTextCoroutine(request));
        }


        // This is a RequestCoroutine for getting a single object
        private IEnumerator RequestObjectCoroutine<T>(ARequest request)
        {
            UnityWebRequest www = CreateRequest(request);
            var _cert = new CertAccepter();
            www.certificateHandler = _cert;


            yield return www.SendWebRequest();

            if (HasAnyErrors(www))
            {
                if (request.retryOnFail && request.retryCount < request.retryLimit)
                {
                    request.retryCount++;
                    Debug.Log("Error with request, retrying " + www.error + " " + request.url);
                    request.Send();
                }
                else
                {
                    request.SetResult(www.error);
                    request.OnFail();
                }
            }
            else
            {
                request.SetResult(www.downloadHandler.text);
                ParseObjectResponse<T>(request);
            }
        }

        // This is a RequestCoroutine for getting an array of objects
        private IEnumerator RequestArrayCoroutine<T>(ARequest request)
        {
            UnityWebRequest www = CreateRequest(request);
            var _cert = new CertAccepter();
            www.certificateHandler = _cert;
            yield return www.SendWebRequest();

            if (HasAnyErrors(www))
            {
                if (request.retryOnFail && request.retryCount < request.retryLimit)
                {
                    request.retryCount++;
                    Debug.Log("Error with request, retrying " + www.error);
                    request.Send();
                }
                else
                {
                    request.SetResult(www.error);
                    request.OnFail();
                }
            }

            else
            {
                
                request.SetResult(www.downloadHandler.text);
                ParseArrayResponse<T>(request);
            }
        }

        // This is a RequestCoroutine for text requests
        private IEnumerator RequestTextCoroutine(ARequest request)
        {
            UnityWebRequest www = CreateRequest(request);
            var _cert = new CertAccepter();
            www.certificateHandler = _cert;
            yield return www.SendWebRequest();

            if (HasAnyErrors(www))
            {
                if (request.retryOnFail && request.retryCount < request.retryLimit)
                {
                    request.retryCount++;
                    Debug.Log(request.url);
                    Debug.Log("Error with request, retrying " + www.error);
                    request.Send();
                }
                else
                {
                    request.SetResult(www.error);
                    request.OnFail();
                }
            }
            else
            {
                
                request.SetResult(www.downloadHandler.text);
                ParseTextResponse(request);
            }
        }
        

        // This is a parser for getting a single object
        private void ParseObjectResponse<T>(ARequest request)
        {
            JObject response = JObject.Parse(request._stringResult);

            if (!string.IsNullOrEmpty(request.successKey) && !((bool) response[request.successKey]))
            {
                if (request.retryOnFail && request.retryCount < request.retryLimit)
                {
                    request.retryCount++;
                    request.Send();
                }
                else
                {
                    request.OnFail();
                }

                return;
            }
            JObject output = GetNestedJSONKey(response.ToString(), request.outputKey);
            Debug.Log(request.outputKey);
            Debug.Log(GetLastOutputKey(request.outputKey));
            Debug.Log(output);

            AData parsedData;

            if (string.IsNullOrEmpty(request.outputKey))
            {
                parsedData = JsonUtility.FromJson<T>(output.ToString()) as AData;
            }
            else
            {
                parsedData = JsonUtility.FromJson<T>(output[GetLastOutputKey(request.outputKey)].ToString()) as AData;
            }

            parsedData.Preprocess();
            request.SetResult(parsedData);
            request.OnSuccess();
        }

        // This is a parser for getting an array of objects
        private void ParseArrayResponse<T>(ARequest request)
        {
            JObject response = JObject.Parse(request._stringResult);

            if (!string.IsNullOrEmpty(request.successKey) && !((bool) response[request.successKey]))
            {
                if (request.retryOnFail && request.retryCount < request.retryLimit)
                {
                    request.retryCount++;
                    Debug.Log("Error with request, retrying " + request._stringResult + request.url);
                    request.Send();
                }
                else
                {
                    request.OnFail();
                }

                return;
            }

            List<AData> objectList = new List<AData>();

            JObject output = GetNestedJSONKey(response.ToString(), request.outputKey);

            foreach (var x in output[GetLastOutputKey(request.outputKey)])
            {
                var y = JsonUtility.FromJson<T>(x.ToString()) as AData;
                y.jsonData = x.ToString();
                y.Preprocess();
                objectList.Add(y);
            }

            request.SetResult(objectList);
            request.OnSuccess();
        }

        // This is a parser for text Requests
        private void ParseTextResponse(ARequest request)
        {
            JObject response;
            try
            {
                response = JObject.Parse(request._stringResult);
            }
            catch
            {
                request.SetResult("Error while parsing json" + request._stringResult);
                request.OnFail();
                return;
            }

            if (!string.IsNullOrEmpty(request.successKey) && !((bool) response[request.successKey]))
            {
                if (request.retryOnFail && request.retryCount < request.retryLimit)
                {
                    request.retryCount++;
                    Debug.Log("Error with request, retrying " + request._stringResult);
                    request.Send();
                }
                else
                {
                    request.SetResult(request._stringResult);
                    request.OnFail();
                }

                return;
            }

            string message;

            if (request.outputKey.Equals(""))
                message = response.ToString();
            else
            {
                JObject output = GetNestedJSONKey(response.ToString(), request.outputKey);

                if (string.IsNullOrEmpty(request.outputKey))
                {
                    message = output.ToString();
                }
                else
                {
                    message = output[GetLastOutputKey(request.outputKey)].ToString();
                }
            }


            request.SetResult(message);
            request.OnSuccess();
        }

        private UnityWebRequest CreateRequest(ARequest request)
        {
            UnityWebRequest www;
            byte[] bodyRaw;

            if (request.httpRequestType == Enums.RequestType.GET)
            {
                www = UnityWebRequest.Get(request.url);

                // This is needed so the server won't throw a no identity exception
                bodyRaw = Encoding.UTF8.GetBytes("{}");
            }
            else 
            {
                if (request.httpRequestType == Enums.RequestType.POST)
                {
                    www = UnityWebRequest.Post(request.url, request.data);
                    bodyRaw = Encoding.UTF8.GetBytes(request.data);
                }
                else
                {
                    Debug.Log("Other types not supported yet");
                    return null;
                }
            }

            www.timeout = Constants.webRequestTimeout;

            www.SetRequestHeader("Content-Type", "application/json");

            www.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);

            return www;
        }

        private bool HasAnyErrors(UnityWebRequest www)
        {
            return www.result == UnityWebRequest.Result.ConnectionError ||
                   www.result == UnityWebRequest.Result.ProtocolError;
        }

        public static JObject GetNestedJSONKey(string msg, string outputKey)
        {
            string[] keys = outputKey.Split(';');

            if (keys.Length == 1)
                return JObject.Parse(msg);


            for (int i = 0; i < keys.Length - 1; i++)
            {
                JObject obj = JObject.Parse(msg);

                if (obj[keys[i]] == null)
                    return null;

                msg = obj[keys[i]].ToString();
            }

            return JObject.Parse(msg);
        }

        public static string GetLastOutputKey(string outputKey)
        {
            string[] keys = outputKey.Split(';');
            return keys[keys.Length - 1];
        }
        
        
    }
}