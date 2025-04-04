using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Bar;
using Blackjack;
using Poker;
using Roulette;
class Casino 
{
    static void Main() 
    {
        Select();
    }
    public static void Select() 
    {
        string[] Places = {"Blackjack", "Poker", "Roulette", "Bar", "Exit"};
        Console.WriteLine("Where would you like to go?");

        foreach (string place in Places)
            Console.Write(place + "\n"); //Prints all Options

        string myChoice = Console.ReadLine()?.ToLower() ?? "Null"; //Collects Entry and puts it in all Lowercase

        Switch(myChoice);//Calls Switch with string
    }
    static void Switch(string myChoice) 
    {
        switch (myChoice) 
        {
            case "blackjack": 
                BlackjackGame.StartGame(); //Runs from blackjack.cs
                break;
            case "poker":
                PokerGame.StartGame(); //Runs from poker.cs
                break;
            case "roulette":
                RouletteGame.StartGame(); //Runs from roulette.cs
                break;
            case "bar":
                BarDrink.StartDrink(); //Runs from bar.cs
                break;
            case "exit":
                Console.WriteLine("Have a Good Day!");
                Environment.Exit(0); //Closes Program
                break;
            default:
                Console.WriteLine("Not a Valid Entry");
                myChoice = Console.ReadLine() ?? "Null";
                Switch(myChoice); //Calls back on Switch to restart interaction
                break;
        }
    }
}