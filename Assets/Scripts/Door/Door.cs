using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    private bool isOpened;
    public void TryOpen(bool HasKey)
    {
        if(isOpened) return;
        if (!HasKey)
        {
            Debug.Log("Cannot Open Door:" + HasKey);
            return;
        }
        Debug.Log("Opening door: " + HasKey);
        isOpened = true;
    }
}