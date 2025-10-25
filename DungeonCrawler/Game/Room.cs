using DungeonCrawler.Characters;
using DungeonCrawler.Items;

namespace DungeonCrawler.Game
{
    public class Room
    {
        public string Description { get; private set; }
        public Enemy? Enemy { get; private set; }
        public Item? Item { get; set; }
        public bool IsExplored { get; private set; }

        public Room(string description, Enemy? enemy = null, Item? item = null)
        {
            Description = description;
            Enemy = enemy;
            Item = item;
            IsExplored = false;
        }

        public void Explore()
        {
            IsExplored = true;
            Console.WriteLine($"\nYou enter: {Description}");
        }

        public bool HasEnemy() => Enemy != null && Enemy.IsAlive;
        public bool HasItem() => Item != null;
    }
}