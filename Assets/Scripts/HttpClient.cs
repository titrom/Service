using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public enum ErrorType
{
    Connection,
    Protocol

}
public static class HttpClient
{
    public static IEnumerator Get<T>(string path, Action<T> respones, Action<ErrorType, string> error, Action<string> debug)
    {
        UnityWebRequest request = UnityWebRequest.Get(path);
        request.SendWebRequest();

        while (!request.isDone)
        {
            yield return new WaitForSeconds(1f);


        }
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    {
                        error.Invoke(ErrorType.Connection, "");
                    }
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    {
                        var errorResponce = JsonUtility.FromJson<ErrorResponse>(request.downloadHandler.text);
                        var errorText = errorResponce.error.http_code < 500 ? errorResponce.error.text : "Ошибка сервера. Попробуйтё ещё раз позже.";
                        error.Invoke(ErrorType.Protocol, errorText);

                    }
                    break;
            }
        }
        if (request.result == UnityWebRequest.Result.Success) respones.Invoke(JsonUtility.FromJson<T>(request.downloadHandler.text));
    }


}