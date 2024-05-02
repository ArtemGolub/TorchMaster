using UnityEngine;
using UnityEngine.Events;

public static  class SceneEvents
{
    public static UnityEvent<string> EventLoadScene = new UnityEvent<string>();
    public static void LoadScene(string sceneName)
    {
        EventLoadScene.Invoke(sceneName);
    }
    
    
}