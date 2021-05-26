using System.Collections.Generic;

namespace PF2
{
    public struct DamageSource
    {
        public DieSource dice;
        public DamageType type;
        public DamageSource(DieSource src, DamageType t)
        {
            dice = src;
            type = t;
        }
    }

    public struct Damage
    {
        public uint value;
        public DamageType type;
        public Damage(uint v, DamageType t)
        {
            value = v; type = t;
        }

        public override string ToString()
        {
            return value + " points of " + type.ToString() + " damage";
        }
    }

    public class DamageRoll
    {
        private List<DamageSource> damageSources;

        public DamageRoll()
        {
            damageSources = new List<DamageSource>();
        }
        public void AddSource(DamageSource src)
        {
            damageSources.Add(src);
        }
        public List<Damage> Roll()
        {
            List<Damage> damageList = new List<Damage>();
            foreach (DamageSource src in damageSources)
            {
                int r = DieRoll.Roll(src.dice);
                if (r < 0) r = 0;
                damageList.Add(new Damage((uint)r, src.type));
            }
            return damageList;
        }
    }
}