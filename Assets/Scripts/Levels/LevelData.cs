using System;

[Serializable]
public class LevelData
{
    public string levelName; 
    public LevelState state;
    
    public LevelData(string name, LevelState initialState = LevelState.Locked)
    {
        levelName = name;
        state = initialState;
    }
}