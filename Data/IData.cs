namespace TextRPG.Data;

public interface IData<T>
{
    Dictionary<int, T>? Dict { get; }
    
    void LoadData(string folderPath);
}