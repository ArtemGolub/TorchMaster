using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LoadingScreenView))]
public class LoadingScreenController : MonoBehaviour
{
    public static LoadingScreenController current;

    [SerializeField]private LoadingScreenSO LoadingScreenPreset;
    private LoadingScreenModel _loadingScreenModel;
    private LoadingScreenView _loadingScreenView;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        _loadingScreenModel = new LoadingScreenModel(LoadingScreenPreset, this);
        _loadingScreenView = GetComponent<LoadingScreenView>();
    }
    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetLoadingScreen();
        SceneEvents.EventLoadScene.AddListener(LoadNewScene);

        // Debug.Log(current + " " +LoadingScreenPreset +" "+ _loadingScreenModel+ " "+ _loadingScreenView);
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SceneEvents.EventLoadScene.RemoveListener(LoadNewScene);
        GetComponent<Canvas>().enabled = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void LoadNewScene(string LevelToLoad)
    {
        GetComponent<Canvas>().enabled = true;
        StartCoroutine(_loadingScreenModel.AsynkLevelLoad(LevelToLoad));
    }

    private void SetLoadingScreen()
    {
        _loadingScreenModel.SetRandomHint(_loadingScreenView.hintText);
        _loadingScreenModel.SetRandomImage(_loadingScreenView.mainImage);
    }

    public void UpdateSlider(float value)
    {
        _loadingScreenView.UpdateLoadingBar(value);
    }
}