using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace Blackjack 
{
    public class BlackjackGame 
    {
        public static class Info //Bank for All Values Needed, so you can just call 'Info.'
        {
            public static int[] dealerHand = new int[10];
            public static int[] playerHand = new int[10];
            public static int playerTotal = 0;
            public static int dealerTotal = 0;
            public static string pInteract = "";
            public static string dInteract = "";
        }

        public static void StartGame() 
        {
            Console.Clear(); //Clear Console for clarity
            Console.WriteLine("Would you like to play?");
            string answer = Console.ReadLine()?.ToLower() ?? "null";
            if (answer == "y" || answer == "yes") 
            {
                Game(); //Runs Game if 'y' or 'yes'
            }
            else 
            {
                Console.WriteLine("Have a good day!");
                Casino.Select(); //Returns you to casino.cs
            }
        }
        static void Game() 
        {
            // Resets all Values For When you Replay
            Info.dealerHand = new int[10];
            Info.playerHand = new int[10];
            Info.playerTotal = 0;
            Info.dealerTotal = 0;

            Console.WriteLine("Let\'s Play!");
            Wait();

            Info.playerHand[0] = Cards(); //Sets your first card
            Info.playerHand[1] = Cards(); //Sets your second card
            Info.playerTotal = Info.playerHand[0] + Info.playerHand[1]; //Adds values up

            Console.WriteLine($"Your Hand is at {Info.playerTotal}");
            Wait();

            Info.dealerHand[0] = Cards(); //Sets dealer's first card
            Info.dealerHand[1] = Cards(); //Sets dealer's second card
            Info.dealerTotal = Info.dealerHand[0] + Info.dealerHand[1]; //Adds values up

            Console.WriteLine($"Dealer\'s Hand is {Info.dealerTotal}");
            Wait();
            PlaySwitch(); //Calls PlaySwitch
        }

        static int Cards() //Used to randomly generate a card for hand
        {
            int index = 0;
            int[] cards = {2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 11};
            Random rnd = new Random();
            index = rnd.Next(cards.Length);
            return cards[index];
        }
        static void PlaySwitch() 
        {
            if (Info.playerTotal == 21) { //Check for Blackjack
                Console.WriteLine("Blackjack!");
                DealSwitch(); //Calls DealSwitch
            }
            else 
            {
                Console.WriteLine("What would you like to do? \nHit \nStand \nFold");
                Info.pInteract = Console.ReadLine()?.ToLower() ?? "null"; //Read Players input for switch below
            }

            int plyr = 2; //Sets which part of the deck to start on (Already have 0 and 1 full)
            switch (Info.pInteract) 
            {
                case "hit":
                    Wait();
                    Info.playerHand[plyr] = Cards(); //Enters value into card spot in hand
                    Info.playerTotal += Info.playerHand[plyr]; //Adds value to total
                    plyr++; //Moves over 1 in deck
                    for (int i = 0; i < Info.playerHand.Length; i++) //Check to see what Ace needs to be valued at (1 or 11)
                    {
                        if (Info.playerHand[i] == 11 && Info.playerTotal > 21) {
                            Info.playerTotal -= 10; //Subtracts 10 from total
                            Info.playerHand[i] = 1; //Makes Ace(11) a 1
                        }
                    }
                    Console.WriteLine($"Your Hand is at {Info.playerTotal}");
                    if (Info.playerTotal > 21) //Checks if you are over 21
                    {
                        Console.WriteLine("You\'ve Gone Bust.");
                        Info.pInteract = "pBust";
                        Final(); //If so, push to final for loss, else, continues
                    }
                    else if (Info.playerTotal == 21) //Checks for Blackjack
                    {
                        Console.WriteLine("Blackjack!");
                        Info.pInteract = "pBlackjack";
                        DealSwitch(); //If yes, run DealSwitch to see if Dealer also gets Blackjack, else final for win
                    }
                    Wait();
                    PlaySwitch(); // If none true, call PlaySwitch again
                    break;
                case "stand":
                    DealSwitch(); //If stand, move to dealer's turn
                    break;
                case "fold":
                    Final(); //Move to final for loss
                    break;
                default:
                    PlaySwitch(); // Result back if values do not match
                    break;
            }
        }
        static void DealSwitch()
        {
            int deal = 2; //Sets which part of the deck to start on (Already have 0 and 1 full)
            Console.WriteLine("Dealer\'s Turn:");
            while (Info.dealerTotal <= 17) { //Dealer Must hit if total is less than or equal to 17
                Info.dealerHand[deal] = Cards();
                Info.dealerTotal += Info.dealerHand[deal];
                deal++;
                for (int i = 0; i < Info.playerHand.Length; i++) //Check for Aces and decrease if over 21
                {
                    if (Info.playerTotal > 21 && Info.playerHand[i] == 11) //Same as Player Hand
                    {
                        Info.playerTotal -= 10;
                        Info.playerHand[i] = 1;
                    }
                }
            Wait();
            Console.WriteLine("Dealer\'s Hand is at {0}", Info.dealerTotal);
            }
            if (Info.dealerTotal > 21) {
                Console.WriteLine("Dealer\'s Gone Bust");
                Info.dInteract = "dBust";
                Final();
            }
            else if (Info.dealerTotal == 21) {
                Console.WriteLine("Dealer Has Blackjack");
                Info.dInteract = "dBlackjack";
                Final();
            }
            else
            {
                Console.WriteLine("Dealer Stands on {0}", Info.dealerTotal);
                Info.dInteract = "dStand";
                Final();
            }

        }
        static void Final() {   
    Wait();

    switch (Info.pInteract) {
        case "fold":
            Console.WriteLine("Dealer Wins!"); //If player folds, Dealer wins
            break;

        case "pBust":
            if (Info.dInteract == "dBust") {
                Console.WriteLine("Draw"); //If both bust, draw
            } else {
                Console.WriteLine("Dealer Wins!"); //If player busts, dealer wins
            }
            break;

        case "pBlackjack":
            if (Info.dInteract == "dBlackjack") {
                Console.WriteLine("Draw"); //If both blackjack, draw
            } else {
                Console.WriteLine("Player Wins!");
            }
            break;
        case "dBust":
            if (Info.dInteract == "pBust") {
                Console.WriteLine("Draw"); //If both bust, draw
            } else {
                Console.WriteLine("Player Wins!");
            }
            break;
        default:
            switch (Info.dInteract) { //Activates if no Blackjacks, no folds, and no busts
                case "dStand":
                    if (Info.dealerTotal > Info.playerTotal) {
                        Console.WriteLine("Dealer Wins!"); //If dealer has higher value, they win
                    } else if (Info.playerTotal > Info.dealerTotal) {
                        Console.WriteLine("Player Wins!"); //If player has higher value, you win
                    } else {
                        Console.WriteLine("Draw"); //If they tie, draw
                    }
                    break;
            }
            break;
    }

    Wait();
    StartGame(); //Restart the game
}
        static void Wait() // Just for Pacing and Readability
        {
            int wait = 500;
            Thread.Sleep(wait);
        }
    }
}
