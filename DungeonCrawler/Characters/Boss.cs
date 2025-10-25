using DungeonCrawler.Utilities;

namespace DungeonCrawler.Characters
{
    public class Boss : Enemy
    {
        public string SpecialAbility { get; private set; }

        public Boss(string name, int health, int attackPower, string specialAbility)
            : base(name, health, attackPower, 200, 100, 5)
        {
            SpecialAbility = specialAbility;
        }

        public override int CalculateDamage()
        {
            // Boss has a chance to do double damage
            if (Dice.Roll(10) >= 8) // 30% chance
            {
                Console.WriteLine($"{Name} uses {SpecialAbility} for double damage!");
                return base.CalculateDamage() * 2;
            }
            return base.CalculateDamage();
        }

        public override void DisplayStatus()
        {
            Console.WriteLine($"👹 BOSS: {Name} - ❤️ {Health}/{MaxHealth} | ⚔️ {AttackPower}");
            Console.WriteLine($"Special Ability: {SpecialAbility}");
        }
    }
}