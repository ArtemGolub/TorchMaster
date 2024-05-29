using UnityEngine;

public class AdditionalAudioSource : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        AudioManager.current.AddAdditionalAudioSource(source);
    }
}
