﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CsvHelper;
using System.Globalization;

public class cardHolder
{
    public cardHolder(string cardNum, int pin, string firstName, string lastName, double balance)
    {
        CardNum = cardNum;
        Pin = pin;
        FirstName = firstName;
        LastName = lastName;
        Balance = balance;
    }

    private String cardNum;    // card number

    public String CardNum
    {
        get
        {
            return cardNum;
        }
        set
        {
            cardNum = value;
        }
    }

    private int pin;           // pin number

    public int Pin
    {
        get
        {
            return pin;
        }
        set
        {
            pin = value;
        }
    }

    private String firstName;  // first name

    public String FirstName
    {
        get
        {
            return firstName;
        }
        set
        {
            firstName = value;
        }
    }

    private String lastName;   // last name

    public String LastName
    {
        get
        {
            return lastName;
        }
        set
        {
            lastName = value;
        }
    }

    private double balance;    // balance

    public double Balance
    {
        get
        {
            return balance;
        }
        set
        {
            balance = value;
        }
    }

    public static void Main(String[] args)
    {
        void printOptions()
        {
            Console.WriteLine("Please choose from one of the following options...");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Show Balance");
            Console.WriteLine("4. Exit");
        }

        void deposit(cardHolder currentUser)
        {
            Console.WriteLine("How much money would you like to deposit: ");

            double deposit = Double.Parse(Console.ReadLine());
            currentUser.Balance = currentUser.Balance + deposit;
            Console.WriteLine("Thank you for your money. Your new balance is: " + currentUser.Balance);
        }

        void withdraw(cardHolder currentUser)
        {
            Console.WriteLine("How much money would you like to withdraw: ");
            
            double withdrawal = Double.Parse(Console.ReadLine());
            // Check if the user has enough money
            if (currentUser.Balance < withdrawal)
            {
                Console.WriteLine("Insufficient balance :(");
            }
            else
            {
                currentUser.Balance = currentUser.Balance - withdrawal;
                Console.WriteLine("You're good to go! Thank you :)");
            }
        }

        void balance(cardHolder currentUser)
        {
            Console.WriteLine("Current balance: " + currentUser.Balance);
        }

        // List of card holders
        var cardHolders = new List<cardHolder>();

        // Database
        cardHolders.Add(new cardHolder("4532772818527395", 1234, "John", "Griffith", 150.31));
        cardHolders.Add(new cardHolder("8645218942348435", 4321, "Ashley", "Jones", 321.13));
        cardHolders.Add(new cardHolder("8726646943484746", 9999, "Frida", "Dickerson", 105.59));
        cardHolders.Add(new cardHolder("1388546976325648", 2468, "Muneeb", "Harding", 851.84));
        cardHolders.Add(new cardHolder("5135893462349531", 4826, "Dawn", "Smith", 54.27));

        // CSV WRITER
        using (var writer = new StreamWriter("filePersons.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(cardHolders);
        }

        // Prompt user
        Console.WriteLine("Welcome to SimpleATM");
        Console.WriteLine("Please insert your debit card: ");
        String debitCardNum = "";
        cardHolder currentUser;

        while (true)
        {
            try
            {
                debitCardNum = Console.ReadLine();
                // Check against database
                currentUser = cardHolders.FirstOrDefault(a => a.cardNum == debitCardNum);
                if (currentUser != null) { break; }
                else { Console.WriteLine("Card not recognized. Please try again"); }
            }
            catch { Console.WriteLine("Card not recognized. Please try again"); }
        }

        Console.WriteLine("Please enter your pin: ");
        int userPin = 0;

        while (true)
        {
            try
            {
                userPin = int.Parse(Console.ReadLine());
                // Check against database
                if (currentUser.Pin == userPin) { break; }
                else { Console.WriteLine("Incorrect pin. Please try again"); }
            }
            catch { Console.WriteLine("Incorrect pin. Please try again"); }
        }

        Console.WriteLine("Welcome " + currentUser.FirstName + " :)");
        int option = 0;
        do
        {
            printOptions();
            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch { }
            if (option == 1) { deposit(currentUser); }
            else if (option == 2) { withdraw(currentUser); }
            else if (option == 3) { balance(currentUser); }
            else if (option == 4) { break; }
            else { option = 0; }
        }
        while (option != 4);
        Console.WriteLine("Thank you! Have a nice day :)");
    }
}

// TODO:
// move database to external file
// add possibility to add new users
// change directory of csv file
// add reading from file