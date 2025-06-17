using Integration.Client;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Api
{
    public static readonly string apiUrl = "https://666982da2e964a6dfed57122.mockapi.io/";

    void Start()
    {
    }

    public static IEnumerator FetchLessons(Action<Lesson[]> callback)
    {
        using UnityWebRequest request = UnityWebRequest.Get($"{apiUrl}lessons");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            var lessons = JsonHelper.FromJson<Lesson[]>(json);
            callback(lessons);
        }
        else
        {
            Debug.LogError("Failed to fetch lessons: " + request.error);
        }
    }
}
