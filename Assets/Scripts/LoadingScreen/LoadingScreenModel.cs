using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadingScreenModel
{
    private LoadingScreenSO preset;
    private LoadingScreenController LoadingScreenController;
    public LoadingScreenModel(LoadingScreenSO scriptable, LoadingScreenController loadingScreenController)
    {
        preset = scriptable;
        LoadingScreenController = loadingScreenController;
    }
    
    public void SetRandomHint(TextMeshProUGUI text)
    {
        var textIndex = Random.Range(0, preset.Hints.Count);
        text.text = preset.Hints[textIndex];
    }

    public void SetRandomImage(Image target)
    {
        var imageIndex = Random.Range(0, preset.mainImages.Count);
        target.sprite = preset.mainImages[imageIndex];
    }
    
        
    public IEnumerator AsynkLevelLoad(string LevelToLoad)
    {
        yield return new WaitForSeconds(0.1f);
        
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(LevelToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            LoadingScreenController.UpdateSlider(progressValue);
            yield return null;
        }
    }
}
