namespace TextRPG.Manager;

public static class Utils
{
    private const string wrongInputMessage = "[ERROR] 잘못된 입력입니다. 다시 입력해주세요.";
    private const string blankSpace = "                                              ";
    private const string newInput = ">> ";

    // 사용자의 입력을 받아오는 메서드
    public static int GetUserInput(int max)
    {
        Console.Write(newInput);

        int inputNumber;
        while (true)
        {
            string? input = Console.ReadLine();
            if (int.TryParse(input, out inputNumber))
            {
                if (inputNumber > 0 && inputNumber <= max)
                {
                    break;
                }
                else
                {
                    WrongInput();
                }
            }
            else if (input == null) // 사용자가 입력을 하지 않고 엔터를 눌렀을 때
            {
                WrongInput();
            }
            else // 사용자가 숫자가 아닌 다른 문자를 입력했을 때
            {
                WrongInput();
            }
        }

        return inputNumber;

        void WrongInput()
        {
            PrintColorMessage(ConsoleColor.Red, wrongInputMessage);
            Thread.Sleep(1000);
            
            int top = Console.CursorTop;
            Console.SetCursorPosition(0, top);
            Console.Write(blankSpace);
            Console.SetCursorPosition(0,top - 1);
            Console.Write(newInput);
            Console.Write(blankSpace);
            Console.SetCursorPosition(newInput.Length, top - 1);
        }
    }

    public static void PrintColorMessage(ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.Write(message);

        Console.ResetColor();
    }
}