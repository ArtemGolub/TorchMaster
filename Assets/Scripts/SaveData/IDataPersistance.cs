public interface IDataPersistance
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
    void CreateNewData(ProgressionSO progress, GameData data);
}