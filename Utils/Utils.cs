namespace TextRPG.Utils;

public static class Util
{
    private const string wrongInputMessage = "[ERROR] 잘못된 입력입니다. 다시 입력해주세요.";
    private const string blankSpace = "                                              ";
    private const string newInput = ">> ";

    public const ConsoleColor error = ConsoleColor.Red;
    public const ConsoleColor success = ConsoleColor.Green;
    public const ConsoleColor system = ConsoleColor.Cyan;
    
    // 선택지의 개수를 매개변수로 받아 사용자 입력을 받는 메서드
    public static int GetUserInput(int max)
    {
        Console.Write(newInput);

        int inputNumber;
        while (true)
        {
            string? input = Console.ReadLine();
            if (int.TryParse(input, out inputNumber))
            {
                if (inputNumber >= 0 && inputNumber <= max)
                {
                    break;
                }
                else
                {
                    WrongInput();
                }
            }
            else if (input == null)
            {
                WrongInput();
            }
            else
            {
                WrongInput();
            }
        }

        return inputNumber;
    }
    
    private static void WrongInput()
    {
        PrintColorMessage(error, wrongInputMessage, false, true);
        Thread.Sleep(1000);
            
        int top = Console.CursorTop;
        Console.SetCursorPosition(0, top);
        Console.Write(newInput);
        Console.Write(blankSpace);
        Console.SetCursorPosition(newInput.Length, top);
    }

    // 색상과 함께 메시지를 출력하는 메서드
    public static void PrintColorMessage(ConsoleColor color, string message, bool isLineBreak = true, bool isInputError = false)
    {
        if (isInputError)
        {
            int top = Console.CursorTop;
            Console.SetCursorPosition(newInput.Length, top - 1);
        }
        
        Console.ForegroundColor = color;
        Console.Write(message);

        if (isLineBreak)
        {
            Console.WriteLine();
        }

        Console.ResetColor();
    }
}