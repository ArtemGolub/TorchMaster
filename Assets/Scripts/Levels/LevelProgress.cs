using UnityEngine;

public class LevelProgress : MonoBehaviour, IDataPersistance
{
    [SerializeField]private string LevelId;
    public string GetLevelName()
    {
        return LevelId;
    }
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        LevelId = System.Guid.NewGuid().ToString();
    }
    [SerializeField]private LevelState state;
    public LevelState GetState()
    {
        return state;
    }
    public void LoadData(GameData data)
    {
        data.Levels.TryGetValue(LevelId, out state);
        if (state == LevelState.Locked)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.Levels.ContainsKey(LevelId))
        {
            data.Levels.Remove(LevelId);
        }
        data.Levels.Add(LevelId, state);
    }

    public void CreateNewData(ProgressionSO progress, GameData data)
    {
        if (data.Levels.ContainsKey(LevelId))
        {
            data.Levels.Remove(LevelId);
        }
        var levelIndex = progress.LevelName.FindIndex(name => name == LevelId);
        data.Levels.Add(LevelId, progress.LevelState[levelIndex]);
    }
}