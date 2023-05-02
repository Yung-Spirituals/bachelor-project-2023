using System.Collections;
using SoData;
using UnityEngine.Networking;
using UnityEngine;

public static class WebCommunicationManager
{
    private static readonly string basePath = "http://localhost:8080";

    public static IEnumerator GetGameDataRequest(GameDataScriptableObject gameDataScriptableObject)
    {
        string fullPath = basePath + "/story/stories";
        UnityWebRequest webRequest = UnityWebRequest.Get(fullPath);
        yield return webRequest.SendWebRequest();
        DebugRequest(webRequest, fullPath);
        gameDataScriptableObject.Stories = JsonUtil.StoriesFromJson(webRequest.downloadHandler.text);
    }

    private static bool DebugRequest(UnityWebRequest webRequest, string fullPath)
    {
        bool success = false;
        string[] pages = fullPath.Split('/');
        int page = pages.Length - 1;
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                success = true;
                break;
        }

        return success;
    }
}
