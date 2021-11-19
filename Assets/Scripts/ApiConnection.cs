using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Models;
using UnityEngine;

public static class ApiConnection
{
    private static Logger logger = new Logger(new MyLogHandler());


    public static Grave GetGrave(int graveId, int cemeteryId)
    {
        HttpWebRequest request =
            HttpWebRequest.CreateHttp("http://localhost:8080/api/cemetery/" + cemeteryId + "/tombstone/" + graveId);
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();
        StreamReader body = new StreamReader(response.GetResponseStream());
        String json = body.ReadToEnd();
        logger.Log(json);
        return JsonUtility.FromJson<Grave>(json);
    }

    public static Cemetery GetCemetery(int cemeteryId)
    {
        HttpWebRequest request =
            HttpWebRequest.CreateHttp("http://localhost:8081/api/cemetery/" + cemeteryId + "/list");
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();
        StreamReader body = new StreamReader(response.GetResponseStream());
        String json = body.ReadToEnd();

        Cemetery cemetery = JsonUtility.FromJson<Cemetery>(json);

        return cemetery;
    }
}