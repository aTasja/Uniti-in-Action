using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class NetworkService : MonoBehaviour {

    private const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mode=xml&APPID=<your api key>";

    private bool IsResponseValid(WWW www)
    {
        if (www.error != null)
        {
            Debug.Log("bad connection");
            return false;
        }
        else if (string.IsNullOrEmpty(www.text))
        {
            Debug.Log("bad data");
            return false;
        }
        else
        {   // all good
            return true;
        }
    }

    private IEnumerator CallAPI(string url, WWWForm form, Action<string> callback) {
		using (UnityWebRequest request = (form == null) ?
			UnityWebRequest.Get(url) : UnityWebRequest.Post(url, form)) {

			yield return request.Send();

			if (request.isNetworkError) {
				Debug.LogError("network problem: " + request.error);
			} else if (request.responseCode != (long)System.Net.HttpStatusCode.OK) {
				Debug.LogError("response error: " + request.responseCode);
			} else {
				callback(request.downloadHandler.text);
			}
		}
	}
    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, null, callback);
    }
}
