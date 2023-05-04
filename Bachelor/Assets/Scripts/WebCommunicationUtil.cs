using System.Collections;
using SoData;
using UnityEngine.Networking;
using UnityEngine;

public static class WebCommunicationUtil
{
    private static readonly string basePath = "http://localhost:8080";

    public static IEnumerator GetGameDataRequest(GameDataScriptableObject gameDataScriptableObject)
    {
        string fullPath = basePath + "/game/stories";
        UnityWebRequest webRequest = UnityWebRequest.Get(fullPath);
        yield return webRequest.SendWebRequest();
        //DebugRequest(webRequest, fullPath);
        yield return gameDataScriptableObject.Stories = JsonUtil.StoriesFromJson(webRequest.downloadHandler.text);
    }

    public static IEnumerator PutNewGameDataRequest(Story story, Level level, Question question)
    {
        Debug.Log(JsonUtil.StoryLevelQuestionToJson(story, level, question));
        string fullPath = basePath + "/game/add";
        UnityWebRequest webRequest = UnityWebRequest.Put(fullPath,
            JsonUtil.StoryLevelQuestionToJson(story, level, question));
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();
        //DebugRequest(webRequest, fullPath);
    }
    
    public static IEnumerator PutUpdateGameDataRequest(Story story, Level level, Question question)
    {
        Debug.Log(JsonUtil.StoryLevelQuestionToJson(story, level, question));
        string fullPath = basePath + "/game/update";
        UnityWebRequest webRequest = UnityWebRequest.Post(fullPath,
            JsonUtil.StoryLevelQuestionToJson(story, level, question));
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();
        //DebugRequest(webRequest, fullPath);
    }
    
    public static IEnumerator DeleteGameDataRequest(Story story, Level level, Question question)
    {
        string fullPath = basePath + "/game/delete";
        UnityWebRequest webRequest = UnityWebRequest.Post(fullPath,
            JsonUtil.StoryLevelQuestionToJson(story, level, question));
        yield return webRequest.SendWebRequest();
        DebugRequest(webRequest, fullPath);
    }

    private static void DebugRequest(UnityWebRequest webRequest, string fullPath)
    {
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
                break;
        }
    }
}
