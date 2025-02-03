namespace TextRPG.Data;

public interface IData<T>
{
    Dictionary<int, T>? datas { get; }
    
    void LoadData(string folderPath);
}