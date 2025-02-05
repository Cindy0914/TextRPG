using System.Text;
using TextRPG.Manager;
using TextRPG.Utils;

namespace TextRPG.Stage;

public class SaveStage : Singleton<SaveStage>
{
    private StringBuilder saveSb = new();
    private StringBuilder loadSb = new();
    private StringBuilder saveFilesSb = new();

    public override void Init()
    {
        saveSb.AppendLine(" == 저장하기 ==");
        saveSb.AppendLine();
        saveSb.AppendLine("1. 새로 저장하기");
        saveSb.AppendLine("2. 덮어씌우기");
        saveSb.AppendLine();
        saveSb.AppendLine("0. 나가기");
        saveSb.AppendLine();
    }

    public void Run()
    {
        Console.Clear();
        Console.Write(saveSb.ToString());
        int input = Util.GetUserInput(2);
        SaveAction(input);
    }

    private void SaveAction(int input)
    {
        switch (input)
        {
            case 0:
                Town.Instance.Run();
                break;
            case 1:
                Save();
                break;
            case 2:
                OverWrite();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(input), input, null);
        }
    }

    private void Save()
    {
        var saveFiles = DataManager.Instance.GetSaveFiles();
        if (saveFiles.Count != 0)
        {
            ShowSaveFiles(saveFiles);
        }

        DataManager.Instance.SaveData(saveFiles.Count);
        string message = $"[System] {saveFiles.Count}번째 슬롯에 저장되었습니다.";
        Util.PrintColorMessage(Util.system, message);
        Thread.Sleep(1000);

        Town.Instance.Run();
    }

    private void OverWrite()
    {
        var saveFiles = DataManager.Instance.GetSaveFiles();
        string message;
        if (saveFiles.Count == 0)
        {
            message = "[ERROR] 저장된 데이터가 없습니다.";
            Util.PrintColorMessage(Util.error, message, false, true);
            Thread.Sleep(1000);
            Run();
        }

        Console.Clear();
        ShowSaveFiles(saveFiles);
        
        saveFilesSb.Clear();
        saveFilesSb.AppendLine("0. 나가기");
        saveFilesSb.AppendLine();
        saveFilesSb.AppendLine("어느 슬롯에 덮어씌우시겠습니까?");
        Console.Write(saveFilesSb.ToString());

        int input = Util.GetUserInput(saveFiles.Count);
        if (input == 0)
        {
            Run();
        }
        
        DataManager.Instance.SaveData(input - 1);
        message = $"[System] {input}번째 슬롯에 덮어씌웠습니다.";
        Util.PrintColorMessage(Util.system, message, false);
        Thread.Sleep(1000);

        Town.Instance.Run();
    }

    public bool TryLoad()
    {
        var saveFiles = DataManager.Instance.GetSaveFiles();
        if (saveFiles.Count == 0)
        {
            string message = "[ERROR] 저장된 데이터가 없습니다.";
            Util.PrintColorMessage(Util.error, message, false, true);
            Thread.Sleep(1000);
            return false;
        }

        Console.Clear();
        ShowSaveFiles(saveFiles);
        
        loadSb.Clear();
        loadSb.AppendLine("0. 나가기");
        loadSb.AppendLine();
        loadSb.AppendLine("어느 슬롯을 불러오시겠습니까?");
        Console.Write(loadSb.ToString());

        int input = Util.GetUserInput(saveFiles.Count);
        if (input == 0)
        {
            return false;
        }

        GameManager.Instance.Init();
        var data = DataManager.Instance.LoadData(input - 1);
        data!.Load();
        Town.Instance.Run();
        return true;
    }

    private void ShowSaveFiles(List<string> saveFiles)
    {
        saveFilesSb.Clear();
        saveFilesSb.AppendLine(" == 저장 목록 ==");
        saveFilesSb.AppendLine();

        for (int i = 0; i < saveFiles.Count; i++)
        {
            var data = DataManager.Instance.LoadData(i);
            if (data == null) continue;
            saveFilesSb.AppendLine($"{i + 1}. Lv.{data.Level} {data.Name}");
        }

        saveFilesSb.AppendLine();
        Console.Write(saveFilesSb.ToString());
    }
}