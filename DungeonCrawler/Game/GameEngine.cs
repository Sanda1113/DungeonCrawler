using DungeonCrawler.Characters;
using DungeonCrawler.Items;
using DungeonCrawler.Utilities;

namespace DungeonCrawler.Game
{
    public class GameEngine
    {
        private Player player;
        private List<Room> rooms;
        private int currentRoomIndex;
        private bool gameRunning;

        public GameEngine(string playerName)
        {
            player = new Player(playerName);
            rooms = GenerateDungeon();
            currentRoomIndex = 0;
            gameRunning = true;
        }

        public void StartGame()
        {
            Console.WriteLine($"\nGood luckk, {player.Name}! Your adventure begins nowww...");

            while (gameRunning && player.IsAlive)
            {
                DisplayGameState();
                HandleRoom();

                if (currentRoomIndex >= rooms.Count - 1 && !rooms[currentRoomIndex].HasEnemy())
                {
                    WinGame();
                    break;
                }

                if (currentRoomIndex < rooms.Count - 1)
                {
                    currentRoomIndex++;
                }
            }

            if (!player.IsAlive)
            {
                GameOver();
            }
        }

        private void DisplayGameState()
        {
            Console.WriteLine("\n" + new string('=', 40));
            player.DisplayStatus();
            Console.WriteLine($"Current Room: {currentRoomIndex + 1}/{rooms.Count}");
            Console.WriteLine(new string('=', 40));
        }

        private void HandleRoom()
        {
            Room currentRoom = rooms[currentRoomIndex];

            if (!currentRoom.IsExplored)
            {
                currentRoom.Explore();
            }

            // Handle item in room
            if (currentRoom.HasItem())
            {
                Console.WriteLine($"You found: {currentRoom.Item!.Name}");
                player.AddToInventory(currentRoom.Item);
                currentRoom.Item.Use(player);
                currentRoom.Item = null; // Remove item from room
            }

            // Handle enemy encounter
            if (currentRoom.HasEnemy())
            {
                Console.WriteLine($"\nA wild {currentRoom.Enemy!.Name} appearss!");
                Combat(currentRoom.Enemy);
            }

            if (!currentRoom.HasEnemy() && player.IsAlive)
            {
                Console.WriteLine("\nThe room is clear. Press any key to continue...");
                Console.ReadKey();
            }
        }

        private void Combat(Enemy enemy)
        {
            while (player.IsAlive && enemy.IsAlive)
            {
                Console.WriteLine("\n=== COMBAT ===");
                player.DisplayStatus();
                enemy.DisplayStatus();

                Console.WriteLine("\nChoose your action:");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Use Item");
                Console.WriteLine("3. Flee (50% chance)");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        PlayerAttack(enemy);
                        break;
                    case "2":
                        UseItemInCombat();
                        break;
                    case "3":
                        if (AttemptFlee())
                            return;
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        continue;
                }

                // Enemy attacks if still alive
                if (enemy.IsAlive)
                {
                    EnemyAttack(enemy);
                }
            }

            if (!enemy.IsAlive)
            {
                Console.WriteLine($"\nYou defeated the {enemy.Name}!");
                player.AddExperience(enemy.ExperienceReward);
                player.AddGold(enemy.GoldReward);
            }
        }

        private void PlayerAttack(Enemy enemy)
        {
            int damage = player.CalculateDamage();
            enemy.TakeDamage(damage);
            Console.WriteLine($"You attack {enemy.Name} for {damage} damage!");
        }

        private void EnemyAttack(Enemy enemy)
        {
            int damage = enemy.CalculateDamage();
            player.TakeDamage(damage);
            Console.WriteLine($"{enemy.Name} attacks you for {damage} damage!");
        }

        private void UseItemInCombat()
        {
            player.ShowInventory();
            var potions = player.Inventory.OfType<Potion>().ToList();

            if (potions.Count == 0)
            {
                Console.WriteLine("No usable items in inventory!");
                return;
            }

            Console.WriteLine("Choose a potion to use (number):");
            for (int i = 0; i < potions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {potions[i]}");
            }

            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= potions.Count)
            {
                potions[choice - 1].Use(player);
                player.Inventory.Remove(potions[choice - 1]);
            }
            else
            {
                Console.WriteLine("Invalid choice!");
            }
        }

        private bool AttemptFlee()
        {
            if (Dice.Roll(2) == 1) // 50% chance
            {
                Console.WriteLine("You successfully fled from combat!");
                return true;
            }
            else
            {
                Console.WriteLine("Failed to flee!");
                return false;
            }
        }

        private List<Room> GenerateDungeon()
        {
            return new List<Room>
            {
                new Room("A dusty corridor with cobwebs",
                    new Enemy("Goblin", 30, 5, 25, 10)),

                new Room("A library with ancient tomes",
                    item: new Potion("Health Potion", "Restores 30 health", 30)),

                new Room("A torture chamber with rusty instruments",
                    new Enemy("Skeleton", 40, 8, 35, 15)),

                new Room("A treasure vault",
                    item: new Weapon("Steel Sword", "A sharp steel sword", 8)),

                new Room("The throne room",
                    new Boss("Dragon", 100, 15, "Fire Breath"))
            };
        }

        private void WinGame()
        {
            Console.WriteLine("\n CONGRATULATIONSSSSSSSS !!!!");
            Console.WriteLine("You have cleared the dungeon and emerged victorious!");
            Console.WriteLine($"Final Score - Level: {player.Level} | Gold: {player.Gold} | Exp: {player.Experience}");
            gameRunning = false;
        }

        private void GameOver()
        {
            Console.WriteLine("\n GAME OVER ");
            Console.WriteLine("You have been defeated...");
            Console.WriteLine($"You reached level {player.Level} and collected {player.Gold} gold.");
        }
    }
}