using System;
using System.Collections.Generic;
using System.Threading;

namespace Poker
{
    public class PokerGame
    {
        public static class Info //Bank for All Values Needed, so you can just call 'Info.'
        {
            public static int[] river = new int[5];
            public static int[] playerHand = new int[2];
            public static int[] opponentHand = new int[2];
            public static List<int> deck = new List<int>(); //List containing all cards in the deck and is cleared after each game played
        }

        public static void StartGame()
        {
            Console.Clear(); //Clear Console for clarity
            Console.WriteLine("Would you like to play?");
            string answer = Console.ReadLine()?.ToLower() ?? "y";
            if (answer == "y" || answer == "yes")
            {
                Game(); //Runs Game if 'y' or 'yes'
            }
            else
            {
                Console.WriteLine("Have a good day!");
                Casino.Select(); //Return to casino.cs
            }
        }

        public static void Game()
        {
            DealGame(); //Resets deck after every play, then populates the 52 cards for the deck with the standard cards

            Info.river = new int[5];
            Info.playerHand = new int[2];
            Info.opponentHand = new int[2];

            DealGame();
            DisplayHands();
            EvaluateHands();
        }

        public static void DealGame()
        {
            
            Info.deck.Clear(); //Clears previous deck
            for (int i = 0; i < 52; i++)
            {
                Info.deck.Add(i); //Populates deck with organized 52 cards
            }

            // Deal the player's hand (Only 2)
            Info.playerHand[0] = DrawCard();
            Info.playerHand[1] = DrawCard();

            // Deal the opponent's hand (Only 2)
            Info.opponentHand[0] = DrawCard();
            Info.opponentHand[1] = DrawCard();

            // Deal the river cards
            for (int i = 0; i < 5; i++) //Only 5
            {
                Info.river[i] = DrawCard();
            }
        }

        private static int DrawCard()
        {
            Random rnd = new Random(); //Chooses a random card from the deck
            int index = rnd.Next(Info.deck.Count);  // Randomly choose a card
            int drawnCard = Info.deck[index];  // Get the card at that value
            Info.deck.RemoveAt(index);  // Remove the card from the deck
            return drawnCard; //Send card to the value that requested it
}



        private static void DisplayHands()
        {
            Console.Clear(); //Clears console for organization
            Console.WriteLine("Your hand:");
            Console.WriteLine($"{Cards(Info.playerHand[0])}, {Cards(Info.playerHand[1])}"); //Prints hand including number and suit. (Does not Show Opponent's hand yet)
            Wait();
            Console.WriteLine("\nThe River:");
            foreach (int card in Info.river)
            {
                Console.Write($"{Cards(card)} "); //Prints each card in the river
            }
            Console.WriteLine(); //Creates a blank line for readability
            Wait();
        }

        private static (double score, string handName) GetHandScore(List<int> cards) //Made to calculate the score to see who wins (Long due to all the different hands)
        {
            int[] ranks = new int[13]; //Tracks the card ranks in hand
            int[] suits = new int[4]; //Tracks the card suits in hand
            List<int> uniqueRanks = new List<int>(); //Stores the ranks found

            foreach (int card in cards) //Calculates through each card
            {
                int rank = card % 13; //Calculates the rank
                int suit = card / 13; //Calculates the suit
                ranks[rank]++; //Adds to the arrays
                suits[suit]++;
            }
            // Booleans that will be checked true if the calculations above say they should
            bool hasPair = false;
            bool hasTwoPair = false;
            bool hasThree = false;
            bool hasFour = false;
            bool isFlush = false;
            bool isStraight = false;
            int highCard = 0; //Stores the highest value card
            int pairCount = 0; //Stores how many pairs each hand has

            foreach (int count in suits) //Checks for flush
            {
                if (count >= 5)
                {
                    isFlush = true;
                    break;
                }
            }

            for (int i = 12; i >= 0; i--)
            {
                if (ranks[i] > 0)
                    uniqueRanks.Add(i);

                if (ranks[i] == 4) hasFour = true; //Checks for 4 of a kind
                else if (ranks[i] == 3) hasThree = true; //Checks for 3 of a kind
                else if (ranks[i] == 2) //Checks for pair
                {
                    pairCount++; //Increases pair count
                    if (pairCount == 1) hasPair = true; //Checks how many and either flips bool of pair or two pair
                    if (pairCount == 2) hasTwoPair = true;
                }

                if (ranks[i] > 0 && highCard == 0)
                    highCard = i; // Highest value card
            }

            uniqueRanks.Sort(); //Sorts in descending order
            uniqueRanks.Reverse(); //Flips so we can see if a straight exists
            int straightCount = 1; //First number is instantly added
            for (int i = 1; i < uniqueRanks.Count; i++)
            {
                if (uniqueRanks[i - 1] - uniqueRanks[i] == 1)
                {
                    straightCount++; //If next card is exactly 1 different, increases the straight count
                    if (straightCount >= 5)
                    {
                        isStraight = true; //If straightCount reaches 5, you have a straight
                        break;
                    }
                }
                else if (uniqueRanks[i - 1] != uniqueRanks[i])
                {
                    straightCount = 1;
                }
            }

            if (!isStraight && uniqueRanks.Contains(12) && //Special check for aces
                uniqueRanks.Contains(0) &&
                uniqueRanks.Contains(1) &&
                uniqueRanks.Contains(2) &&
                uniqueRanks.Contains(3))
            {
                isStraight = true;
            }
            // Calculates the value of your hand, then goes to EvaluateHands
            if (hasFour) return (8.0, "Four of a Kind");
            if (hasThree && hasPair) return (7.0, "Full House");
            if (isFlush) return (6.0, "Flush");
            if (isStraight) return (5.0, "Straight");
            if (hasThree) return (4.0, "Three of a Kind");
            if (hasTwoPair) return (3.0, "Two Pair");
            if (hasPair) return (2.0, "Pair");
            return (1.0 + highCard * 0.01, $"High Card ({RankName(highCard)})");
        }
        private static void EvaluateHands()
        {
            List<int> playerCards = new List<int>(Info.playerHand);
            playerCards.AddRange(Info.river);

            List<int> opponentCards = new List<int>(Info.opponentHand);
            opponentCards.AddRange(Info.river);

            var (plyscore, playerHandName) = GetHandScore(playerCards); //Calculates score of Player
            var (oppscore, oppHandName) = GetHandScore(opponentCards); //Calculates score of Opponent

            Console.WriteLine("\nOpponent's hand:");
            Console.WriteLine($"{Cards(Info.opponentHand[0])}, {Cards(Info.opponentHand[1])}"); //Shows Opponent's hand

            Console.WriteLine($"\nYour score: {plyscore}"); //Shows scores
            Console.WriteLine($"Opponent's score: {oppscore}");

            if (plyscore > oppscore) //If statement to check who wins
                Console.WriteLine("\nYou win!");
            else if (plyscore < oppscore)
                Console.WriteLine("\nYou lose.");
            else
                Console.WriteLine("\nIt's a tie.");
            longWait();
            StartGame();
        }

        private static string Cards(int cardIndex) //Information of the cards to produce the deck
        {
            string[] suits = { "♠", "♥", "♦", "♣" };
            string[] values = {
                "2", "3", "4", "5", "6", "7", "8", "9", "10",
                "J", "Q", "K", "A"
            };

            int value = cardIndex % 13;
            int suit = cardIndex / 13;

            return $"{values[value]}{suits[suit]}";
        }
        private static string RankName(int rank) //Contains the values for each
    {
        string[] values = {
            "2", "3", "4", "5", "6", "7", "8", "9", "10",
            "Jack", "Queen", "King", "Ace"
        };
        return values[rank];
    }

        static void longWait() // Just for Pacing and Readability
        {
            int wait = 5000;
            Thread.Sleep(wait);
        }
        static void Wait() // Just for Pacing and Readability
        {
            int wait = 500;
            Thread.Sleep(wait);
        }
    }
}
