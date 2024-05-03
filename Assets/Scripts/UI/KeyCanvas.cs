using UnityEngine;

public class KeyCanvas : MonoBehaviour
{
    public static KeyCanvas current;
    [SerializeField] private Canvas _keyCanvas;
    private void Start()
    {
        current = this;
        Enable(false);
    }

    public void Enable(bool isEnable)
    {
        _keyCanvas.enabled = isEnable;
    }
}