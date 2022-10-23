using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CsvHelper;
using System.Globalization;

// app based on tutorial from: https://www.youtube.com/watch?v=qBI7Qnz9Zho

public class cardHolder
{
    public String cardNum { get; set; }    // card number
    public int pin { get; set; }           // pin number
    public String firstName { get; set; }  // first name
    public String lastName { get; set; }   // last name
    public double balance { get; set; }    // balance

    //public cardHolder(string cardNum, int pin, string firstName, string lastName, double balance)
    //{
    //    this.cardNum = cardNum;
    //    this.pin = pin;
    //    this.firstName = firstName;
    //    this.lastName = lastName;
    //    this.balance = balance;
    //}

    // getters
    public String getNum()
    {
        return cardNum;
    }

    public int getPin()
    {
        return pin;
    }

    public String getFirstName()
    {
        return firstName;
    }
    
    public String getLastName()
    {
        return lastName;
    }

    public double getBalance()
    {
        return balance;
    }

    // setters
    public void setNum(String newCardNum)
    {
        cardNum = newCardNum;
    }
    public void setPin(int newPin)
    {
        pin = newPin;
    }
    public void setFirstName(String newFirstName)
    {
        firstName = newFirstName;
    }
    public void setLastName(String newLastName)
    {
        lastName = newLastName;
    }
    public void setBalance(double newBalance)
    {
        balance = newBalance;
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
            currentUser.setBalance(currentUser.getBalance() + deposit);
            Console.WriteLine("Thank you for your money. Your new balance is: " + currentUser.getBalance());
        }

        void withdraw(cardHolder currentUser)
        {
            Console.WriteLine("How much money would you like to withdraw: ");
            
            double withdrawal = Double.Parse(Console.ReadLine());
            // Check if the user has enough money
            if (currentUser.getBalance() < withdrawal)
            {
                Console.WriteLine("Insufficient balance :(");
            }
            else
            {
                currentUser.setBalance(currentUser.getBalance() - withdrawal);
                Console.WriteLine("You're good to go! Thank you :)");
            }
        }

        void balance(cardHolder currentUser)
        {
            Console.WriteLine("Current balance: " + currentUser.getBalance());
        }

        var cardHolders = new List<cardHolder>()
        {
            new cardHolder{ cardNum = "4532772818527395", pin = 1234, firstName = "John", lastName = "Griffith", balance = 150.31 },
            new cardHolder{ cardNum = "8645218942348435", pin = 4321, firstName = "Ashley", lastName = "Jones", balance = 321.13 },
            new cardHolder{ cardNum = "8726646943484746", pin = 9999, firstName = "Frida", lastName = "Dickerson", balance = 105.59 },
            new cardHolder{ cardNum = "1388546976325648", pin = 2468, firstName = "Muneeb", lastName = "Harding", balance = 851.84 },
            new cardHolder{ cardNum = "5135893462349531", pin = 4826, firstName = "Dawn", lastName = "Smith", balance = 54.27 }
        };
        // database
        //cardHolders.Add(new cardHolder("4532772818527395", 1234, "John", "Griffith", 150.31));
        //cardHolders.Add(new cardHolder("8645218942348435", 4321, "Ashley", "Jones", 321.13));
        //cardHolders.Add(new cardHolder("8726646943484746", 9999, "Frida", "Dickerson", 105.59));
        //cardHolders.Add(new cardHolder("1388546976325648", 2468, "Muneeb", "Harding", 851.84));
        //cardHolders.Add(new cardHolder("5135893462349531", 4826, "Dawn", "Smith", 54.27));

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
                if (currentUser.getPin() == userPin) { break; }
                else { Console.WriteLine("Incorrect pin. Please try again"); }
            }
            catch { Console.WriteLine("Incorrect pin. Please try again"); }
        }

        Console.WriteLine("Welcome " + currentUser.getFirstName() + " :)");
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