using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour, IPause
{
    [SerializeField] private CharacterSO player;
   // private Game_SM SM = new Game_SM();
    private List<IInitialize> startables = new List<IInitialize>();
    private List<IPause> pauses = new List<IPause>();
    public bool isPause { get; set; }
    public List<AudioSource> AudioSources;
    
    private void Start()
    {
        CharacterFabric.current.SpawnCharacter(player);
        
        startables.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<IInitialize>());
        foreach (var startable in startables)
        {
            startable.Init();
        }
        pauses.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<IPause>());
        
        AudioManager.current.PlayMainTheme();
    }
    

  
    public void Pause()
    {
        if (isPause)
        {
            foreach (var source in AudioSources)
            {
                source.Play();
            }
            isPause = false;
        }
        else
        {
            foreach (var source in AudioSources)
            {
                source.Stop();
            }
            isPause = true;
        }
    }

    public void PauseGame()
    {
        foreach (var pause in pauses)
        {
            pause.Pause();
        }
    }
}