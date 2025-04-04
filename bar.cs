using Blackjack;

namespace Bar 
{
    public class BarDrink 
    {
        public static class Info //Bank for All Values Needed, so you can just call 'Info.'
        {
            public static int drinksHad = 0;
        }
        public static void StartDrink() 
        {
            Console.Clear();//Clear Console for clarity
            Console.WriteLine("What\'ll You have?");
            string[] Drinks = //array containing a random group of drinks
            {
                "1. Guinness",
                "2. Heineken",
                "3. Cabernet Sauvignon",
                "4. Chardonnay",
                "5. Old Fashioned",
                "6. Jameson Irish Whiskey",
                "7. Margarita",
                "8. Mojito",
                "9. Gin & Tonic",
                "10. Vodka Martini",
                "0. Leave" 
            };
            foreach (string drink in Drinks) Console.Write(drink + "\n"); //Prints out the selections onto the terminal

            string myDrink = Console.ReadLine()?.ToLower() ?? "Null"; //User input, sets to lowercase (Expects the full name or the number associated with it)
            if (Info.drinksHad > 9) { //Checks if you are over the drinks limit, if so, throws you out of the program (A.K.A. Out of the Casino)
                Console.WriteLine("Alright, buddy, I think you\'ve had enough.");
                Wait();
                Console.WriteLine("You Have Been Thrown Out.");
                Environment.Exit(0); //Exit
            }
            DrinkEffect(myDrink); //Calls DrinkEffect
        }
            static void DrinkEffect(string myDrink)
        {
            switch (myDrink) //A Switch containing the list of drinks, with a unique message for all of them
            {
                case "Guinness Stout":
                case "1":
                    Console.WriteLine("You feel relaxed and content, savoring the rich, creamy flavor of the stout.");
                    break;
                    
                case "Heineken Lager":
                case "2":
                    Console.WriteLine("A crisp, refreshing feeling. You're ready to socialize and enjoy the moment.");
                    break;
                    
                case "Cabernet Sauvignon":
                case "3":
                    Console.WriteLine("You feel a touch of sophistication as the rich flavors of the red wine fill your senses.");
                    break;
                    
                case "Chardonnay":
                case "4":
                    Console.WriteLine("A light, refreshing experience. You're feeling calm and classy with every sip.");
                    break;
                    
                case "Old Fashioned":
                case "5":
                    Console.WriteLine("A bold, refined drink that makes you feel like you're in an old-school speakeasy.");
                    break;
                    
                case "Jameson Irish Whiskey":
                case "6":
                    Console.WriteLine("Warmth spreads through you as you enjoy the smooth, slightly spicy flavor of this whiskey.");
                    break;
                    
                case "Margarita":
                case "7":
                    Console.WriteLine("You feel energized and ready to party with the tangy, citrusy kick of the margarita.");
                    break;
                    
                case "Mojito":
                case "8":
                    Console.WriteLine("The fresh mint and lime give you a feeling of tropical relaxation. A cool breeze on a warm day.");
                    break;
                    
                case "Gin & Tonic":
                case "9":
                    Console.WriteLine("The crispness of the gin and tonic gives you a feeling of refreshment and sharp focus.");
                    break;
                    
                case "Vodka Martini":
                case "10":
                    Console.WriteLine("You feel sleek and elegant, ready for the high society vibes as the martini hits your taste buds.");
                    break;
                case "Leave":
                case "0":
                    Console.WriteLine("Thanks for stopping in.");
                    Casino.Select();
                    break;
                    
                default:
                    Console.WriteLine("This drink has no effect, but you're still having a good time!"); // Default is some random drink
                    break;
            }
            Info.drinksHad++; //Increments the drinksHad
            StartDrink(); //Recalls the function
        }
        static void Wait() // Just for Pacing and Readability
        {
            int wait = 500;
            Thread.Sleep(wait);
        }
    }
}
