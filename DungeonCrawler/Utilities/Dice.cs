namespace DungeonCrawler.Utilities
{
    public static class Dice
    {
        private static Random random = new Random();

        public static int Roll(int sides)
        {
            return random.Next(1, sides + 1);
        }

        public static int Roll(int count, int sides)
        {
            int total = 0;
            for (int i = 0; i < count; i++)
            {
                total += Roll(sides);
            }
            return total;
        }
    }
}