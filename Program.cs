using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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

    private String cardNum;
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

    private int pin;
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

    private String firstName;
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

    private String lastName;
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

    private double balance;
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

        // Load card holders from CSV file and store their data in cardHolders list
        string[] csvLines = System.IO.File.ReadAllLines("../../../filePersons.csv");
        for (int i = 1; i < csvLines.Length; i++)
        {
            string[] rowData = csvLines[i].Split(',');
            for (int j = 0; j < rowData.Length; j++)
            {
                cardHolders.Add(new cardHolder(rowData[0], int.Parse(rowData[1]), rowData[2], rowData[3], double.Parse(rowData[4], System.Globalization.CultureInfo.InvariantCulture)));
            }
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