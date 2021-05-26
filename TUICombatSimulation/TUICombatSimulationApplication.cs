using PF2;
using System;
using System.Collections.Generic;
using System.IO;

namespace TUI
{
    class TUIObserver : PF2.IObserver
    {
        public void Message(string message)
        {
            Console.WriteLine(message);
        }
    }

    class TUICombatSimulationApplication : PF2.ICombatSimulationUserInterface
    {
        private static TUIObserver observer = new TUIObserver();

        public TUICombatSimulationApplication()
        {
        }

        public PF2.IObserver GetObserver()
        {
            return observer;
        }

        public void LogTurn(uint turn)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*** Turn: " + turn + " ***");
            Console.ResetColor();
        }

        public void Run()
        {
            Console.Title = "Pathfinder 2e Combat Simulator";
            string jsonText = File.ReadAllText("monsters-pf2.json");

            Console.WriteLine("Loading Monster Database...");
            PF2.MonsterFactory.LoadData(jsonText);
            Console.WriteLine("Loaded " + PF2.MonsterFactory.GetCount() + " creatures!");

            List<string> monsterNames = new List<string>(PF2.MonsterFactory.GetNames());

            Console.ResetColor();

            string monster_name = TUI.ConsoleTypeSelector.SelectLine(monsterNames, "  Select Monster: ", ConsoleColor.Gray, ConsoleColor.Green, ConsoleColor.White, ConsoleColor.DarkBlue);

            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Selected: " + monster_name);

            if (MonsterFactory.Contains(monster_name) == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Invalid Monster Name!");
                Console.ResetColor();
                return;
            }

            PF2CombatSimulation simulation = new PF2CombatSimulation();
            simulation.Run(monster_name, this);
        }
    }
}