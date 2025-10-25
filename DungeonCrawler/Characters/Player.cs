using DungeonCrawler.Items;

namespace DungeonCrawler.Characters
{
    public class Player : Character
    {
        public int Experience { get; private set; }
        public int ExperienceToNextLevel => Level * 100;
        public int Gold { get; private set; }
        public List<Item> Inventory { get; private set; }
        public Weapon? EquippedWeapon { get; private set; }

        public Player(string name) : base(name, 100, 10, 1)
        {
            Experience = 0;
            Gold = 50;
            Inventory = new List<Item>();
        }

        public void AddExperience(int exp)
        {
            Experience += exp;
            Console.WriteLine($"Gained {exp} experience!");

            while (Experience >= ExperienceToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            MaxHealth += 20;
            Health = MaxHealth;
            AttackPower += 5;
            Experience -= (Level - 1) * 100; // Fixed experience calculation

            Console.WriteLine($"🎉 Level Up! You are now level {Level}!");
            Console.WriteLine($"❤️  Health: {MaxHealth} | ⚔️  Attack: {AttackPower}");
        }

        public void AddGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($"Found {amount} gold! Total: {Gold}");
        }

        public void AddToInventory(Item item)
        {
            Inventory.Add(item);
            Console.WriteLine($"Added {item.Name} to inventory!");
        }

        public void EquipWeapon(Weapon weapon)
        {
            EquippedWeapon = weapon;
            Console.WriteLine($"Equipped {weapon.Name}!");
        }

        public override int CalculateDamage()
        {
            int baseDamage = base.CalculateDamage();
            return EquippedWeapon != null ? baseDamage + EquippedWeapon.DamageBonus : baseDamage;
        }

        public override void DisplayStatus()
        {
            Console.WriteLine($"=== {Name} ===");
            Console.WriteLine($"Level: {Level} | Exp: {Experience}/{ExperienceToNextLevel}");
            Console.WriteLine($"Health: {Health}/{MaxHealth}");
            Console.WriteLine($"Attack: {CalculateDamage()}");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"Weapon: {(EquippedWeapon?.Name ?? "None")}");
            Console.WriteLine($"Inventory: {Inventory.Count} items");
        }

        public void ShowInventory()
        {
            Console.WriteLine("\n=== INVENTORY ===");
            if (Inventory.Count == 0)
            {
                Console.WriteLine("Inventory is empty!");
                return;
            }

            for (int i = 0; i < Inventory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Inventory[i]}");
            }
        }
    }
}