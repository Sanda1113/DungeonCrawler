using DungeonCrawler.Characters;

namespace DungeonCrawler.Items
{
    public abstract class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        protected Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract void Use(Player player);

        public override string ToString()
        {
            return $"{Name}: {Description}";
        }
    }

    public class Weapon : Item
    {
        public int DamageBonus { get; private set; }

        public Weapon(string name, string description, int damageBonus)
            : base(name, description)
        {
            DamageBonus = damageBonus;
        }

        public override void Use(Player player)
        {
            player.EquipWeapon(this);
        }

        public override string ToString()
        {
            return $"{base.ToString()} (+{DamageBonus} damage)";
        }
    }

    public class Potion : Item
    {
        public int HealAmount { get; private set; }

        public Potion(string name, string description, int healAmount)
            : base(name, description)
        {
            HealAmount = healAmount;
        }

        public override void Use(Player player)
        {
            player.Heal(HealAmount);
            Console.WriteLine($"Drank {Name} and healed {HealAmount} health!");
        }
    }

    public class Treasure : Item
    {
        public int GoldValue { get; private set; }

        public Treasure(string name, string description, int goldValue)
            : base(name, description)
        {
            GoldValue = goldValue;
        }

        public override void Use(Player player)
        {
            player.AddGold(GoldValue);
            Console.WriteLine($"Sold {Name} for {GoldValue} gold!");
        }
    }
}