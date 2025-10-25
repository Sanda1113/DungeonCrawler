using DungeonCrawler.Game;
using DungeonCrawler.Utilities;

namespace DungeonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Dungeon Crawler!");
            Console.WriteLine("What's your name, adventurer?");
            string? playerName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(playerName))
                playerName = "Hero";

            GameEngine game = new GameEngine(playerName);
            game.StartGame();
        }
    }
}