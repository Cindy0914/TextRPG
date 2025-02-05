namespace TextRPG.Data;

// json 파일을 읽어오는 클래스들이 갖는 인터페이스
public interface IData<T>
{
    // 데이터에 접근하기 위한 프로퍼티 (private set)
    Dictionary<int, T>? Dict { get; }
    
    void LoadData(string folderPath);
}