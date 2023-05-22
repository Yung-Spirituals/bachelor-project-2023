using System.Collections;
using System.Collections.Generic;
using SoData;
using UnityEngine.Networking;
using UnityEngine;

// Static class for the application to communicate with the backend.
public static class WebCommunicationUtil
{
    private const string BasePath = "http://178.232.172.125:8080";

    /*
     * Send a HTTP Get request to retrieve all subjects and their underlying levels and questions.
     * Store this data in the provided SO (scriptable object).
     */
    public static IEnumerator GetGameDataRequest(GameDataScriptableObject gameDataScriptableObject)
    {
        // This endpoint returns a list of all subjects contained in a custom object (See JsonUtil.cs for more info)
        const string fullPath = BasePath + "/subject/subjects";
        
        // Create a web Get request to the provided url.
        UnityWebRequest webRequest = UnityWebRequest.Get(fullPath);
        
        yield return webRequest.SendWebRequest();
        
        if (webRequest.result == UnityWebRequest.Result.Success)
            // Set local data to the game data retrieved from the backend.
            yield return gameDataScriptableObject.Subjects = 
                JsonUtil.SubjectsFromJson(webRequest.downloadHandler.text);
        
        // If the web request was not successful, set local data to a new empty list to prevent null exceptions.
        else yield return gameDataScriptableObject.Subjects = new List<Subject>();
        
        // Clean-up unused resources.
        webRequest.Dispose();
    }

    // Send a HTTP Put request to add a new entity to the backend storage.
    public static IEnumerator PutNewGameDataRequest(Subject subject, Level level, Question question, string path)
    {
        // If the request is successful, the endpoint will return the generated ID of the new entity.
        string fullPath = BasePath + path + "/add";
        
        // Create a web Put request with a json body containing the new entity to the provided url.
        UnityWebRequest webRequest = UnityWebRequest.Put(fullPath,
            JsonUtil.SubjectLevelQuestionToJson(subject, level, question));
        webRequest.SetRequestHeader("Content-Type", "application/json");
        
        // Send the web request.
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
            // Apply the ID generated in the backend to the local copy of the newly created entity.
            SetNewId(subject, level, question, webRequest.downloadHandler.text);
        
        // Clean-up unused resources.
        webRequest.Dispose();
        
        // Get the most up-to-date version of game data from the server.
        yield return GameDataManager.Instance.RefreshGameData();
    }
    
    // Send a HTTP Put request to update an entity in the backend storage.
    public static IEnumerator PutUpdateGameDataRequest(Subject subject, Level level, Question question, string path)
    {
        string fullPath = BasePath + path + "/update";
        
        // Create a web Put request with a json body containing the updated entity to the provided url.
        UnityWebRequest webRequest = UnityWebRequest.Put(fullPath,
            JsonUtil.SubjectLevelQuestionToJson(subject, level, question));
        webRequest.SetRequestHeader("Content-Type", "application/json");
        
        // Send the web request.
        yield return webRequest.SendWebRequest();

        // Clean-up unused resources.
        webRequest.Dispose();
        
        // Get the most up-to-date version of game data from the server.
        yield return GameDataManager.Instance.RefreshGameData();
    }
    
    // Send a HTTP Delete request to delete an entity form the backend storage.
    public static IEnumerator DeleteGameDataRequest(string path, long id)
    {
        string fullPath = BasePath + path + "/delete/" + id;
        
        // Create a web Delete request to the provided url.
        UnityWebRequest webRequest = UnityWebRequest.Delete(fullPath);

        // Send the web request.
        yield return webRequest.SendWebRequest();

        // Clean-up unused resources.
        webRequest.Dispose();
        
        // Get the most up-to-date version of game data from the server.
        yield return GameDataManager.Instance.RefreshGameData();
    }

    // Method suggested from unity, convenient for debugging.
    private static void DebugRequest(UnityWebRequest webRequest, string fullPath)
    {
        string[] pages = fullPath.Split('/');
        int page = pages.Length - 1;
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log("Connection error!");
                break;
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

    /*
     * When a new entity is added to the database, the ID that gets assigned to it will be returned.
     * This function will take this ID and apply it to the local copy of the entity.
     */
    private static void SetNewId(Subject subject, Level level, Question question, string newObject)
    {
        if (question != null) GameDataManager.Instance.GetGameData().ActiveQuestion.ID = long.Parse(newObject);
        else if (level != null) GameDataManager.Instance.GetGameData().ActiveLevel.ID = long.Parse(newObject);
        else if (subject != null) GameDataManager.Instance.GetGameData().ActiveSubject.ID = long.Parse(newObject);
    }

    /*
     * For each image url provided, add it as a sprite to the sprite list.
     * Adds a null to the sprite list if an entry in the url list is empty.
     */ 
    public static IEnumerator GetImagesFromUrls(List<Sprite> images, List<string> urls)
    {
        foreach (string url in urls)
        {
            if (url == "") images.Add(null);
            else
            {
                // Create a web Get request to the provided url.
                UnityWebRequest webRequest = UnityWebRequest.Get(url);
                
                // Send the web request.
                yield return webRequest.SendWebRequest();

                // Create a new 2D texture to hold the .
                Texture2D tex = new Texture2D(1,1);
                if (webRequest.result == UnityWebRequest.Result.Success)
                    tex.LoadImage(webRequest.downloadHandler.data);
                
                // Convert the texture into a sprite.
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height),
                    new Vector2(tex.width/2f, tex.height/2f));
                
                // Add to sprite list.
                images.Add(sprite);
                
                // Clean-up unused resources.
                webRequest.Dispose();
            }
        }
        yield return images;
    }
}
