using System;

namespace PF2
{
    enum D20Condition
    {
        Normal,
        Natural20,
        Natural1
    }

    public interface IIntegerSource
    {
        int Next(int minValue, int maxValue);
    }

    public struct DieSource
    {
        public static DieSource d20 = new DieSource(1, DieType.D20, 0);
        public DieSource(String equation)
        {
            equation.Replace(" ", "");
            string[] operands = equation.Split('+', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (operands.Length != 2) operands = equation.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            string[] die_info = operands[0].ToLower().Split('d', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (operands.Length == 2)
            {
                constantDamage = Int32.Parse(operands[1]);
            }
            else
            {
                constantDamage = 0;
            }
            if (die_info.Length != 2)
            {
                constantDamage = Int32.Parse(die_info[0]);
            }
            amountOfDice = Int32.Parse(die_info[0]);
            dieType = DieType.None;
        }
        public DieSource(int dice, DieType type, int constant)
        {
            constantDamage = constant;
            amountOfDice = dice;
            dieType = type;
        }
        public override string ToString()
        {
            string str = "";
            if (dieType != DieType.None) str += amountOfDice + "d" + (int)dieType;
            if (constantDamage != 0)
            {
                if (str.Length > 0 && (constantDamage > 0)) str += "+";
                str += constantDamage; //the - sign is embedded in constantDamage
            }
            return str;
        }
        public int constantDamage;
        public int amountOfDice;
        public DieType dieType;
    }

    class DefaultRandomIntegerSource: IIntegerSource
    {
        private static Random source = new Random();

        public int Next(int minValue, int maxValue)
        {
            return source.Next(minValue,maxValue);
        }
    }
    public class DieRoll
    {
        private static IIntegerSource random = new DefaultRandomIntegerSource();
        public static int Roll(DieSource src)
        {
            int result = src.constantDamage;
            for (int i = 0; i < src.amountOfDice; i++)
            {
                result += random.Next(1, (int)src.dieType + 1);
            }
            return result;
        }
        public static int RollFlat(DieType die)
        {
            return random.Next(1, (int)(die) + 1);
        }
        public static void SetIntegerSource(IIntegerSource ris)
        {
            random = ris;
        }
    }
}