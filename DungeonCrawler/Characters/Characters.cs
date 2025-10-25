namespace DungeonCrawler.Characters
{
    public abstract class Character
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int AttackPower { get; protected set; }
        public int Level { get; protected set; }
        public bool IsAlive => Health > 0;

        protected Character(string name, int health, int attackPower, int level = 1)
        {
            Name = name;
            MaxHealth = health;
            Health = health;
            AttackPower = attackPower;
            Level = level;
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public virtual void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
        }

        public virtual int CalculateDamage()
        {
            return AttackPower;
        }

        public abstract void DisplayStatus();
    }
}