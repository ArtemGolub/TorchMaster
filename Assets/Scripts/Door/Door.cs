using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    private bool isOpened;
    public void TryOpen(Character _character)
    {
        if(isOpened) return;
        if (!_character.hasKey) return;
        
        isOpened = true;
        KeyCanvas.current.Enable(false);
        
        _character.AnimationEventHandler.HandleCollision(null, null, _character); // TODO Refactor null logic
        
        _character.SM.ChancgeState(CharacterStateType.Use);
        
        StartCoroutine(DoorDown());
    }
    
    // TODO Dotween refactor
    private IEnumerator DoorDown()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos - new Vector3(0, 5.5f, 0);

        float duration = 5f;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
    }
}