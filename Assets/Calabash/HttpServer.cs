﻿using UnityEngine;
using System.Collections;
using System.Net;
using System;
using System.Text.RegularExpressions;
using System.IO;

namespace Calabash {
    
public class HttpServer {

    protected HttpListener listener;

    // Use this for initialization
    public void Start() {
        listener = new HttpListener();
        listener.Prefixes.Add("http://127.0.0.1:37265/");
        listener.Start();
        listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
    }

    public void Destroy () {
        if (listener != null) {
            listener.Close();
        }
    }

    private void ListenerCallback(IAsyncResult result) {
        var listener = (HttpListener)result.AsyncState;

        var context = listener.EndGetContext(result);
        var request = context.Request;
        var response = context.Response;

        string jsonResponse = null;

        if (request.Url.LocalPath == "/version") {
            jsonResponse = new Calabash.VersionRoute().HandleRequest(request);
        }

        if (request.Url.LocalPath == "/map") {
            jsonResponse = new Calabash.MapRoute().HandleRequest(request);
        }

		if (jsonResponse == null) {
			jsonResponse = "{ \"outcome\": \"FAILURE\", \"reason\": \"Unsupported calabash-unity-server route: \\\"" + request.Url.LocalPath + "\\\"\" }";
		}
		
		FileLog.Log(jsonResponse);
				
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonResponse);

        // Get a response stream and write the response to it.
        response.ContentLength64 = buffer.Length;

        System.IO.Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);

        // You must close the output stream.
        output.Close();

        listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);

        response.StatusCode = 200;
        response.Close();
    }
}

}
