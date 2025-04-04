namespace Roulette 
{
    public class RouletteGame 
    {
        public static class Info //Bank for All Values Needed, so you can just call 'Info.'
        {
            public static string playChoice = "null";
            public static string selectedNumber = "null";
            public static string selectedColor = "null";
        }
        public static void StartGame() 
        {
            Console.Clear(); //Clear Console for clarity
            Console.WriteLine("Would you like to play?");
            string answer = Console.ReadLine()?.ToLower() ?? "y";
            if (answer == "y" || answer == "yes") 
            {
                Game();//Runs Game if 'y' or 'yes'
            }
            else 
            {
                Console.WriteLine("Have a good day!");
                Casino.Select(); //Returns you to casino.cs
            }
        }
        public static void Game()
        {
            Console.WriteLine("Please Place Your Bets!");
            Console.WriteLine("Options: Numbers 1 - 35, 0, 00, Or Color (Red, Black, or Green)");
            Info.playChoice=Console.ReadLine()?.ToLower() ?? "null"; //Reads Input and sets it to lower case
            Wait();
            Console.WriteLine("Bets are in? Let\'s Begin.");
            Wait(); //Just for suspense and readability
            Console.Write("Spinning...  ");
            Wait();
            Console.Write("Spinning...  ");
            Wait();
            Console.Write("Spinning...  ");
            Wait();
            Wheel(); //Calls Wheel function
            Console.WriteLine("Number is {0} (Color: {1})", Info.selectedNumber, Info.selectedColor); //Prints the information collected from Wheel fuction
            if (Info.playChoice == Info.selectedNumber || Info.playChoice == Info.selectedColor) //Tests to see if your info was correct
            {
                Console.WriteLine("Congratulations! You Win!"); //If yes, you win
                longWait();
                StartGame();
            }
            else
            {
                Console.WriteLine("You Lose, Better Luck Next Time."); //If no, you lose
                longWait();
                StartGame();
            }
        }
        static void Wheel() {
            string[,] rouletteWheel = new string[38, 2] //Creates a new multidimensional array that contains the numbers on a roulette wheel, along with their color
            {
                { "0", "green" },
                { "00", "green" },
                { "1", "red" },
                { "2", "black" },
                { "3", "red" },
                { "4", "black" },
                { "5", "red" },
                { "6", "black" },
                { "7", "red" },
                { "8", "black" },
                { "9", "red" },
                { "10", "black" },
                { "11", "black" },
                { "12", "red" },
                { "13", "black" },
                { "14", "red" },
                { "15", "black" },
                { "16", "red" },
                { "17", "black" },
                { "18", "red" },
                { "19", "red" },
                { "20", "black" },
                { "21", "red" },
                { "22", "black" },
                { "23", "red" },
                { "24", "black" },
                { "25", "red" },
                { "26", "black" },
                { "27", "red" },
                { "28", "black" },
                { "29", "red" },
                { "30", "black" },
                { "31", "red" },
                { "32", "black" },
                { "33", "red" },
                { "34", "black" },
                { "35", "red" },
                { "36", "black" }
            };
            Random rnd = new Random(); //Randomly picks an item in the array and stores the number and color of the selection
            int randomIndex = rnd.Next(0, 38);
            Info.selectedNumber = rouletteWheel[randomIndex, 0];
            Info.selectedColor = rouletteWheel[randomIndex, 1];
        }
        static void Wait() // Just for Pacing and Readability
        {
            int wait = 500;
            Thread.Sleep(wait);
        }
        static void longWait() // Just for Pacing and Readability
        {
            int wait = 5000;
            Thread.Sleep(wait);
        }
    }
}
