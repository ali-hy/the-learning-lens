using Integration.Client;
using Integration.Dtos.UserAccount;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class Api
{
    public static readonly string apiUrl = "https://localhost:7254/api/";
    public static AuthInfo authInfo { get; private set; }

    private static void SetHeaders(UnityWebRequest request)
    {
        if (authInfo != null)
            request.SetRequestHeader("Authorization", authInfo.AccessToken);
    }

    public static UnityWebRequest PostRequest<TBody>(string endpoint, TBody body, string contentType = "application/json")
    {
        string json = JsonHelper.ToJson(body);
        UnityWebRequest request = UnityWebRequest.Post($"{apiUrl}{endpoint}", json, contentType);
        SetHeaders(request);

        return request;
    }

    public static UnityWebRequest GetRequest(string endpoint)
    {
        UnityWebRequest request = UnityWebRequest.Get($"{apiUrl}{endpoint}");
        SetHeaders(request);
        return request;
    }

    public static IEnumerator FetchLessons(Action<Lesson[]> callback)
    {
        using UnityWebRequest request = GetRequest($"{apiUrl}Lessons");
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

    public static IEnumerator Login(string email, string password, Action<AuthInfo> callback)
    {
        var user = new LoginRequest { Email = email, Password = password };
        string json = JsonHelper.ToJson(user);
        using UnityWebRequest request = PostRequest($"{apiUrl}User/Login", json, "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseJson = request.downloadHandler.text;
            var userLoginInfo = JsonHelper.FromJson<AuthInfo>(responseJson);
            callback?.Invoke(userLoginInfo);
        }
        else
        {
            Debug.LogError("Login failed: " + request.error);
        }
    }
}
