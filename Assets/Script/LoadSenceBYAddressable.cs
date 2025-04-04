using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UIElements;

public class LoadSenceBYAddressable : MonoBehaviour
{
    public static LoadSenceBYAddressable Instance;

    [SerializeField] private AssetReference vR_Game;
    [SerializeField] private AssetReference video;
    private AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> handleGame;
    private AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> handleVideo;

    private void Awake()
    {
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
     //   AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> handleVideo
    }
    public void LoadAddressableSceneGame()
    {
        handleGame = vR_Game.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Single);
        handleGame.Completed += OnGameSceneLoaded;
    }

    public void LoadAddressableScene360()
    {
        handleVideo = video.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Single);
        handleVideo.Completed += OnVideoSceneLoaded;
    }

    private void OnGameSceneLoaded(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("VR Game Scene Loaded Successfully!");
        }
        else
        {
            Debug.LogError("Failed to Load VR Game Scene");
        }
    }

    private void OnVideoSceneLoaded(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("360 Video Scene Loaded Successfully!");
        }
        else
        {
            Debug.LogError("Failed to Load 360 Video Scene");
        }
    }

    public void ReloadAddressableSceneGame()
    {
       
       // Addressables.UnloadSceneAsync();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        /*if (handleGame.IsValid())
        {
            StartCoroutine(ReloadScene(vR_Game, true));
        }
        else
        {
            Debug.LogError("Game Scene is not loaded yet!");
        }*/
    }

    public void ReloadAddressableScene360()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private IEnumerator ReloadScene(AssetReference sceneRef, bool isGameScene)
    {
        AsyncOperationHandle<SceneInstance> sceneHandle = isGameScene ? handleGame : handleVideo;

        // Ensure the handle is valid before proceeding
        if (!sceneHandle.IsValid())
        {
            Debug.LogError("Scene handle is invalid or already released!");
            yield break;
        }

        Debug.Log("Unloading Scene...");

        // Unload the existing scene
        AsyncOperationHandle<SceneInstance> unloadHandle = Addressables.UnloadSceneAsync(sceneHandle);
        yield return unloadHandle;

        if (unloadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Scene Unloaded Successfully!");
        }
        else
        {
            Debug.LogError("Failed to Unload Scene!");
            yield break;
        }

        // Release the old scene handle
        Addressables.Release(sceneHandle);

        Debug.Log("Reloading Scene...");

        // Load the scene again
        AsyncOperationHandle<SceneInstance> newSceneHandle = sceneRef.LoadSceneAsync(LoadSceneMode.Single);
        yield return newSceneHandle;

        if (newSceneHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"{sceneRef.SubObjectName} Scene Reloaded Successfully!");
            if (isGameScene)
                handleGame = newSceneHandle;
            else
                handleVideo = newSceneHandle;
        }
        else
        {
            Debug.LogError($"Failed to Reload {sceneRef.SubObjectName} Scene");
        }
    }


    private void OnDestroy()
    {
        if (handleGame.IsValid()) Addressables.Release(handleGame);
        if (handleVideo.IsValid()) Addressables.Release(handleVideo);
    }
}
