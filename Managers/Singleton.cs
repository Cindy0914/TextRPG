namespace TextRPG.Manager;

public abstract class Singleton<T> where T : class, new()
{
    // 하나만 존재하는 Manager들과 Stage들을 위한 Singleton
    private static T? _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }

    public abstract void Init();
}