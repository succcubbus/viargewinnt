﻿using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class HttpService{

    public static string gameId { get; set; }
    public static string player { get; set; }
    private const string baseUrl = "http://localhost:4567/";
    public static WebSocket w;

    public static string HttPost(string url, string content = "")
    {
        HttpWebRequest req = WebRequest.Create(baseUrl+url)
                                as HttpWebRequest;
        req.Method = "POST";
        req.ContentType = "appliaction/json";

        Debug.Log("request url: " + url);

        var newStream = req.GetRequestStream();
        var data = Encoding.ASCII.GetBytes(content);
        newStream.Write(data, 0, data.Length);
        newStream.Close();

        string result = null;
        using (HttpWebResponse resp = req.GetResponse()
                                        as HttpWebResponse)
        {
            StreamReader reader =
                new StreamReader(resp.GetResponseStream());
            result = reader.ReadToEnd();
            Debug.Log("result: " + result);
        }
        return result;
    }

    public static void startSocket()
    {
        w = new WebSocket(new Uri("ws://localhost:4567/state"));
        //StartCoutine
        var enumerator = w.Connect();
        while (enumerator.MoveNext());
        w.SendString(gameId);
    }


}
