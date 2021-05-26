using System.Collections.Generic;

namespace PF2
{
    class Check
    {
        public static CheckResultType Compare(int value, int dc, D20Condition d20Condition = D20Condition.Normal)
        {
            int num_result = 0;
            if (value >= dc + 10)
            {
                num_result = 3;
            }
            else if (value >= dc)
            {
                num_result = 2;
            }
            else if (value <= dc - 10)
            {
                num_result = 0;
            }
            else
            {
                num_result = 1;
            }
            if ((num_result < 3) && (d20Condition == D20Condition.Natural20))
            {
                num_result++;
            }
            else if ((num_result > 0) && (d20Condition == D20Condition.Natural1))
            {
                num_result--;
            }
            return (CheckResultType)num_result;
        }
        public static bool Passed(int value, int dc)
        {
            CheckResultType re = Compare(value, dc);
            return ((re == CheckResultType.Success) || (re == CheckResultType.CriticalSuccess));
        }
        public static bool Passed(CheckResultType result)
        {
            return ((result == CheckResultType.Success) || (result == CheckResultType.CriticalSuccess));
        }

        public static bool Passed(TestResult result)
        {
            return Passed(result.type);
        }
        TraitsBitmap traits;
        private List<DieSource> dice;
        public Check()
        {
            traits = new TraitsBitmap();
            dice = new List<DieSource>();
        }
        public void AddTrait(Traits trait)
        {
            traits.Add(trait);
        }
        public void RemoveTrait(Traits trait)
        {
            traits.Remove(trait);
        }
        public TraitsBitmap GetTraitsBitmap()
        {
            return traits;
        }
        public bool HasTrait(Traits trait)
        {
            return traits.Test(trait);
        }

        public void AddDieSource(DieSource src)
        {
            dice.Add(src);
        }
        public struct TestResult
        {
            public TestResult(int v, CheckResultType t, D20Condition c)
            {
                value = v; type = t; d20Condition = c;
            }
            public int value;
            public CheckResultType type;
            public D20Condition d20Condition;

            public bool Succeeded()
            {
                return Check.Passed(type);
            }
            public bool IsCriticalSuccess()
            {
                return type == CheckResultType.CriticalSuccess;
            }
            public bool IsCriticalFailure()
            {
                return type == CheckResultType.CriticalFailure;
            }
        }
        public TestResult Test(int dc, int modifier = 0)
        {
            int result_num = 0;
            D20Condition condition = D20Condition.Normal;

            foreach (DieSource src in dice)
            {
                int roll = DieRoll.Roll(src);
                result_num += roll;
                if ((roll == 20) && (src.dieType == DieType.D20)) condition = D20Condition.Natural20;
                if ((roll == 1) && (src.dieType == DieType.D20)) condition = D20Condition.Natural1;
            }
            result_num += modifier;
            return new TestResult(result_num, Compare(result_num, dc, condition), condition);
        }
    }

    class FlatCheck
    {
        public struct TestResult
        {
            int value;
            CheckResultType type;
        }
        static bool Test(int dc)
        {
            return (DieRoll.RollFlat(DieType.D20) >= dc);
        }
    }
}