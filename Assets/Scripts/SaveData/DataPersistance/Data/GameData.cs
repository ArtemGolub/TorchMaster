[System.Serializable]
public class GameData
{
    public SerializableDictionary<string, LevelState> Levels;
    public GameData()
    {
        Levels = new SerializableDictionary<string, LevelState>();
        
    }
}
