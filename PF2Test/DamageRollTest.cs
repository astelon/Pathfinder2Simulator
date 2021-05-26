using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;
using PF2;
using System.Collections.Generic;

namespace PF2Test
{
    public class ArrayIntegerSource : PF2.IIntegerSource
    {
        int[] numbers;
        int index = 0;
        public ArrayIntegerSource(int[] n)
        {
            numbers = n;
        }
        public int Next(int minValue, int maxValue)
        {
            int value = 1; //Default value is 1
            if (index < numbers.Length)
            {
                value = numbers[index];
                index++;
            }
            return value;
        }
    }

    struct TestData
    {
        public int dice;
        public DieType die;
        public DamageType type;
        public int constant;
        public int[] testDiceValues;
    }

    [TestClass]
    public class DamageRollTest
    {
        (int,int,DieType,int,DamageType,int[],int)[] testSettings = new (int,int, DieType, int, DamageType, int[],int)[] {
            // Repetitions  Dice   DiceType     Modifier   DamageType        DiceRolls                                            Result
            (1,             1,     DieType.D20, 3,         DamageType.Acid,  new int[] { 1 },                                     4),
        };

        [TestMethod]
        public void TestRollDamage()
        {
            foreach ( (int repetitions, int dice, DieType type, int constant, DamageType dtype, int[] numbers,int value) in testSettings)
            {
                ArrayIntegerSource arrayIntegerSource = new ArrayIntegerSource(numbers);
                PF2.DieRoll.SetIntegerSource(arrayIntegerSource);
                PF2.DamageRoll dr = new DamageRoll();
                dr.AddSource(new DamageSource(new DieSource(dice, type, constant), dtype));

                for(int r=0; r < repetitions; r++)
                {
                    List<Damage> results = dr.Roll();
                    Assert.IsTrue(results.Count == 1);
                    Assert.IsTrue(results[0].value == value);
                    Assert.IsTrue(results[0].type == dtype);
                }
            }
        }
    }
}
