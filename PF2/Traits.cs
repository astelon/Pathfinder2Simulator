using System;
using System.Collections.Generic;

namespace PF2
{
    public enum Traits
    {
        Additive, Archetype, Attack, Aura, Cantrip, Charm, Companion, Complex, Concentrate, Consecration, Curse, Darkness, Death, Dedication, Detection,
        Disease, Downtime, Emotion, Environmental, Exploration, Extradimensional, Fear, Flourish, Focused, Fortune, General, Haunt, Healing,
        Incapacitation, Infused, Legacy, Light, Lineage, Linguistic, Magical, Manipulate, Mechanical, Mental, Metamagic, Minion, Misfortune,
        Morph, Move, Multiclass, Open, Polymorph, Possession, Precious, Prediction, Press, Reckless, Resonant,
        Revelation, Scrying, Secret, Skill, Sleep, Social, Splash, Stamina, Summoned, Telepathy, Teleportation, Trap, Vigilante, Virulent, Vocal,
        //Alignment
        Chaotic, Evil, Good, Lawful,
        //Ancestry
        Aasimar, Android, Aphorite, Azarketi, Beastkin, Catfolk, Changeling, Conrasu, Dhampir, Duskwalker, Dwarf, Elf, Fetchling, Fleshwarp, Ganzi,
        Geniekin, Gnome, Goblin, Half_Elf, Halfling, Half_Orc, Hobgoblin, Human, Ifrit, Kitsune, Kobold, Leshy, Lizardfolk, Orc, Oread, Ratfolk,
        Shoony, Sprite, Strix, Suli, Sylph, Tengu, Tiefling, Undine,
        //Armor
        Bulwark, Comfort, Flexible, Noisy,
        //Class
        Alchemist, Barbarian, Bard, Champion, Cleric, Druid, Fighter, Investigator, Monk, Oracle, Ranger, Rogue, Sorcerer, Swashbuckler, Witch, Wizard,
        //Class-Specific
        Composition, Cursebound, Finisher, Hex, Instinct, Litany, Oath, Rage, Stance,
        //Creature-Type
        Aberration, Animal, Astral, Beast, Celestial, Construct, Dragon, Dream, Elemental, Ethereal, Fey, Fiend, Fungus, Giant, Humanoid, Monitor,
        Negative, Ooze, Petitioner, Plant, Positive, Spirit, Time, Undead,
        //Elemental
        Air, Earth, Fire, Water,
        //Energy
        Acid, Cold, Electricity, Force, Sonic, //Negative, Positive
        //Equipment,
        Alchemical, Apex, Artifact, Bomb, Consumable, Contract, Cursed, Drug, Elixir, Intelligent, Invested, Mutagen, Oil, Potion, Saggorak, Scroll,
        Snare, Staff, Structure, Talisman, Tattoo, Wand,
        //Monster
        /*Aasimar, Acid, */Aeon, Aesir, Agathion, /*Air, Alchemical,*/ Amphibious, Anadi, Angel, Aquatic, Arcane, Archon, Asura, Azata, Boggard, Caligni,
        /*Catfolk, Changeling,*/ Charau_ka, Clockwork, /*Cold,*/ Couatl, Daemon, Demon, Dero, Devil, /*Dhampir,*/ Dinosaur, Div, Drow, Duergar, /*Duskwalker, 
        Earth, Electricity, Fetchling, Fire,*/ Genie, Ghoran, Ghost, Ghoul, Gnoll, Golem, Gremlin, Grioth, Grippli, Hag, Herald, /*Ifrit,*/ Illusion, 
        Incorporeal, Inevitable, Kami, /*Kobold,*/ Kovintus, /*Leshy, Lizardfolk,*/ Locathah, Merfolk, Mindless, Morlock, Mortic, Mummy, Munavri, Mutant, Nagaji,
        Nymph, Oni, /*Orc, Oread,*/ Paaridar, Phantom, Protean, Psychopomp, Qlippoth, Rakshasa, /*Ratfolk,*/ Sahkil, Samsaran, Sea, /*Devil,*/ Serpentfolk,
        Seugathi, Shabti, Siktempora, Skeleton, Skelm, Skulk, /*Sonic,*/ Soulbound, Spriggan, /*Sprite,*/ Stheno, /*Suli,*/ Swarm, /*Sylph,*/ Tane, Tanggal,
        /*Tengu, Tiefling,*/ Titan, Troll, Troop, /*Undine,*/ Urdefhan, Vampire, Vanara, Velstrac, Vishkanya, /*Water,*/ Wayang, Werecreature, Wight, Wraith,
        Wyrwood, Xulgath, Zombie,
        //Planar
        /*Air, Earth,*/ Erratic, Finite, /*Fire,*/ Flowing, High, Gravity, Immeasurable, Low, /*Gravity,*/ Metamorphic, Microgravity, /*Negative, Positive,*/
        Sentient, Shadow, Static, Strange, /*Gravity,*/ Subjective, /*Gravity,*/ Timeless, Unbounded, /*Water,*/
        //Poison
        Contact, Ingested, Inhaled, Injury, Poison,
        //Rarity
        Common, Rare, Uncommon, Unique,
        //School
        Abjuration, Conjuration, Divination, Enchantment, Evocation, /*Illusion,*/ Necromancy, Transmutation,
        //Sense
        Auditory, Olfactory, Visual,
        //Settlement,
        City, Metropolis, Town, Village,
        //Tradition,
        /*Arcane,*/ Divine, Occult, Primal,
        //Versatile-Heritage
        /*Aasimar, Changeling, Dhampir, Duskwalker, Tiefling,*/
        //Weapon
        Agile, Attached, /*Azarketi,*/ Backstabber, Backswing, Brutal, /*Catfolk,*/ Climbing, Concealable, /*Conrasu,*/ Deadly, Disarm, /*Dwarf, Elf,*/ Fatal,
        Finesse, Forceful, Free_Hand, /*Geniekin, Gnome, Goblin,*/ Grapple, /*Halfling,*/ Hampering, Jousting, /*Kobold,*/ Modular, /*Monk,*/ Nonlethal, Parry,
        Propulsive, Range, Ranged, Trip, Reach, Reload, Repeating, Shove, Sweep, Tethered, Thrown, /*Trip,*/ Twin, Two_Hand, Unarmed, Versatile, Volley,
        //Keep this element last
        TraitCount
    }

    public class TraitsBitmap
    {
        UInt64[] segments;
        public static int GetSegment(Traits trait)
        {
            return ((int)trait) / 64;
        }
        public static int GetOffset(Traits trait)
        {
            return ((int)trait) % 64;
        }
        public TraitsBitmap()
        {
            segments = new UInt64[(int)(((int)Traits.TraitCount) / 64) + 1];
        }
        public bool Test(Traits trait)
        {
            return ((segments[GetSegment(trait)] & (UInt64)(1 << GetOffset(trait))) != 0);
        }
        public void Add(Traits trait)
        {
            segments[GetSegment(trait)] |= (UInt64)(1 << GetOffset(trait));
        }
        public void Remove(Traits trait)
        {
            segments[GetSegment(trait)] &= ~((UInt64)(1 << GetOffset(trait)));
        }
        public IEnumerable<Traits> Iterate()
        {
            for (uint i = 0; i < (uint)Traits.TraitCount; i++)
            {
                if (Test((Traits)i))
                {
                    yield return (Traits)i;
                }
            }
        }
    }

}

namespace PF2.Extensions
{
    public static class TraitsExtensions
    {
        public static string Name(this Traits trait)
        {
            return trait.ToString().Replace('_', '-');
        }
        public static bool TryParseTrait(this string trait_name, out Traits trait)
        {
            //Capitalize
            string name = char.ToUpper(trait_name[0]) + trait_name.Substring(1);
            int index = name.IndexOf('-');
            if((index > 0)&&(index<name.Length-2))
            {
                name = name.Substring(0,index) + "_" + char.ToUpper(name[index+1])  + name.Substring(index + 2);
            }
            return Enum.TryParse<Traits>(name.Replace('-', '_'), out trait);
        }
    }
}