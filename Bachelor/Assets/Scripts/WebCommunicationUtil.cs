using System.Collections;
using System.Collections.Generic;
using SoData;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public static class WebCommunicationUtil
{
    private static readonly string basePath = "http://178.232.172.125:8080";
    public static bool busy;

    public static IEnumerator GetGameDataRequest(GameDataScriptableObject gameDataScriptableObject)
    {
        if (busy) yield break;
        busy = true;
        string fullPath = basePath + "/game/stories";
        UnityWebRequest webRequest = UnityWebRequest.Get(fullPath);
        yield return webRequest.SendWebRequest();
        //DebugRequest(webRequest, fullPath);
        yield return gameDataScriptableObject.Stories = JsonUtil.StoriesFromJson(webRequest.downloadHandler.text);
        webRequest.Dispose();
        busy = false;
    }

    public static IEnumerator PutNewGameDataRequest(Story story, Level level, Question question, string path)
    {
        if (busy) yield break;
        busy = true;
        string fullPath = basePath + "/game/add" + path;
        UnityWebRequest webRequest = UnityWebRequest.Put(fullPath,
            JsonUtil.StoryLevelQuestionToJson(story, level, question));
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();
        //DebugRequest(webRequest, fullPath);
        SetNewId(story, level, question, webRequest.downloadHandler.text);
        webRequest.Dispose();
        busy = false;
        yield return GameDataManager.Instance.RefreshGameData();
    }
    
    public static IEnumerator PutUpdateGameDataRequest(Story story, Level level, Question question, string path)
    {
        if (busy) yield break;
        busy = true;
        string fullPath = basePath + "/game/update" + path;
        UnityWebRequest webRequest = UnityWebRequest.Put(fullPath,
            JsonUtil.StoryLevelQuestionToJson(story, level, question));
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();
        //DebugRequest(webRequest, fullPath);
        webRequest.Dispose();
        busy = false;
        yield return GameDataManager.Instance.RefreshGameData();
    }
    
    public static IEnumerator DeleteGameDataRequest(Story story, Level level, Question question, string path)
    {
        if (busy) yield break;
        busy = true;
        string fullPath = basePath + "/game/delete" + path;
        UnityWebRequest webRequest = UnityWebRequest.Post(fullPath,
            JsonUtil.StoryLevelQuestionToJson(story, level, question));
        yield return webRequest.SendWebRequest();
        DebugRequest(webRequest, fullPath);
        webRequest.Dispose();
        busy = false;
        yield return GameDataManager.Instance.RefreshGameData();
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

    private static void SetNewId(Story story, Level level, Question question, string newObject)
    {
        if (question != null) GameDataManager.Instance.GetGameData().ActiveQuestion.SetId(long.Parse(newObject));
        else if (level != null) GameDataManager.Instance.GetGameData().ActiveLevel.ID = long.Parse(newObject);
        else if (story != null) GameDataManager.Instance.GetGameData().ActiveStory.ID = long.Parse(newObject);
    }

    public static IEnumerator GetImagesFromUrls(List<Sprite> images, List<string> urls)
    {
        foreach (string url in urls)
        {
            if (url == "") images.Add(null);
            else
            {
                UnityWebRequest webRequest = UnityWebRequest.Get(url);
                yield return webRequest.SendWebRequest();
                //DebugRequest(webRequest, fullPath);
                Texture2D tex = new Texture2D(1,1);
                tex.LoadImage(webRequest.downloadHandler.data);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height),
                    new Vector2(tex.width/2f, tex.height/2f));
                images.Add(sprite);
                webRequest.Dispose();
            }
        }
        yield return images;
    }
}
