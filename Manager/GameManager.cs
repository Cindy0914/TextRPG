using System.Text;
using TextRPG.Stage;

namespace TextRPG.Manager;

public class GameManager(Warrior warrior)
{
    public Warrior? Player { get; private set; } = warrior;
}