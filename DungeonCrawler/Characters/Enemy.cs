namespace DungeonCrawler.Characters
{
    public class Enemy : Character
    {
        public int ExperienceReward { get; private set; }
        public int GoldReward { get; private set; }

        public Enemy(string name, int health, int attackPower, int expReward, int goldReward, int level = 1)
            : base(name, health, attackPower, level)
        {
            ExperienceReward = expReward;
            GoldReward = goldReward;
        }

        public override void DisplayStatus()
        {
            Console.WriteLine($"💀 {Name} - ❤️ {Health}/{MaxHealth} | ⚔️ {AttackPower}");
        }
    }
}