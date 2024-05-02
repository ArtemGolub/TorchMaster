using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LoadingScreenController))]
public class LoadingScreenView : MonoBehaviour
{
    public Image mainImage;
    public TextMeshProUGUI hintText;
    public Slider loadingBar;

    public void UpdateLoadingBar(float value)
    {
        loadingBar.value = value;
    }
}
