using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
            HttpWebRequest.CreateHttp("http://localhost:8080/api/cemetery/" + cemeteryId + "/all");
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();
        StreamReader body = new StreamReader(response.GetResponseStream());
        String json = body.ReadToEnd();
        logger.Log(json);


        var cemetery = JsonUtility.FromJson<Cemetery>(json);
        List<List<Field>> x = new List<List<Field>>()
        {
            new List<Field>()
            {
                new Field("empty", new Grave(new Guest(0,
                    "Janek",
                    "dfasfa",
                    "2011-21312-21-31",
                    "13412412412"),),),
                // new Grave(new Guest(DateTime.Now, DateTime.Today, "Janek"), 0, 3),
                // new Grave(new Guest(DateTime.Now, DateTime.Today, "Dupa"), 3, 6),
                // new Grave(new Guest(DateTime.Now, DateTime.Today, "Chuj"), -3, 3)
            },
        };

        Cemetery cemetery = new Cemetery(1, "cmentarz", 'najlepszy cmentarz', 0,);

        return cemetery;
    }
}