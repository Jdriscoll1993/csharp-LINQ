using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQed_list
{
    class Program
    {
        static void Main(string[] args)
        {
            //Restriction/Filtering Operations

            // Find the words in the collection that start with the letter 'L'
            List<string> fruits = new List<string>() { "Lemon", "Apple", "Orange", "Lime", "Watermelon", "Loganberry" };
            IEnumerable<string> LFruits = from fruit in fruits where fruit[0] == 'L' select fruit;
            LFruits.ToList().ForEach(fruit => Console.WriteLine(fruit));

            // Which of the following numbers are multiples of 4 or 6
            List<int> numbers = new List<int>()
            {
                15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
            };
            IEnumerable<int> fourSixMultiples = numbers.Where(number => number % 6 == 0 || number % 4 == 0);
            fourSixMultiples.ToList().ForEach(number => Console.WriteLine(number));


            //Ordering Operations

            // Order these student names alphabetically, in descending order (Z to A)
            List<string> names = new List<string>()
            {
                "Heather", "James", "Xavier", "Michelle", "Brian", "Nina",
                "Kathleen", "Sophia", "Amir", "Douglas", "Zarley", "Beatrice",
                "Theodora", "William", "Svetlana", "Charisse", "Yolanda",
                "Gregorio", "Jean-Paul", "Evangelina", "Viktor", "Jacqueline",
                "Francisco", "Tre"
            };

            IEnumerable<string> decendingNames = from name in names
                                                 orderby name descending
                                                 select name;

            decendingNames.ToList().ForEach(name => Console.WriteLine(name));

            // Build a collection of these numbers sorted in ascending order
            List<int> nums = new List<int>()
            {
                15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
            };

            IEnumerable<int> numbsAscending = from num in nums
                                              orderby num ascending
                                              select num;
            numbsAscending.ToList().ForEach(num => Console.WriteLine(num));


            //Aggregate Operations

            // Output how many numbers are in this list
            List<int> numbies = new List<int>()
            {
                15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
            };

            int amountOfNumbers = numbies.Count();
            Console.WriteLine(amountOfNumbers);

            // How much money have we made?
            List<double> purchases = new List<double>()
            {
                2340.29, 745.31, 21.76, 34.03, 4786.45, 879.45, 9442.85, 2454.63, 45.65
            };
            double total = purchases.Sum();
            Console.WriteLine(total);

            // What is our most expensive product?
            List<double> prices = new List<double>()
            {
                879.45, 9442.85, 2454.63, 45.65, 2340.29, 34.03, 4786.45, 745.31, 21.76
            };

            double mostExpensiveProduct = prices.Max();
            Console.WriteLine(mostExpensiveProduct);

            //Partitioning Operations
            /*
                Store each number in the following List until a perfect square
                is detected.
                Ref: https://msdn.microsoft.com/en-us/library/system.math.sqrt(v=vs.110).aspx
            */
            List<int> wheresSquaredo = new List<int>()
            {
                66, 12, 8, 27, 82, 34, 7, 50, 19, 46, 81, 23, 30, 4, 68, 14
            };

            IEnumerable<int> n = wheresSquaredo.TakeWhile(num =>
            {
                int number = Convert.ToInt32(Math.Sqrt(num));
                return number * number != num;

            });
            n.ToList().ForEach(num => Console.WriteLine(num));

            /*
                Given the same customer set, display how many millionaires per bank.
                Ref: https://stackoverflow.com/questions/7325278/group-by-in-linq

                Example Output:
                WF 2
                BOA 1
                FTB 1
                CITI 1
            */


            // create some banks and store them in a list
            List<Bank> banks = new List<Bank>() {
            new Bank(){ Name="First Tennessee", Symbol="FTB"},
            new Bank(){ Name="Wells Fargo", Symbol="WF"},
            new Bank(){ Name="Bank of America", Symbol="BOA"},
            new Bank(){ Name="Citibank", Symbol="CITI"},
        };

            // create some customers and store them in a list
            List<Customer> customers = new List<Customer>() {
            new Customer(){ Name="Bob Lesman", Balance=80345.66, Bank="FTB"},
            new Customer(){ Name="Joe Landy", Balance=9284756.21, Bank="WF"},
            new Customer(){ Name="Meg Ford", Balance=487233.01, Bank="BOA"},
            new Customer(){ Name="Peg Vale", Balance=7001449.92, Bank="BOA"},
            new Customer(){ Name="Mike Johnson", Balance=790872.12, Bank="WF"},
            new Customer(){ Name="Les Paul", Balance=8374892.54, Bank="WF"},
            new Customer(){ Name="Sid Crosby", Balance=957436.39, Bank="FTB"},
            new Customer(){ Name="Sarah Ng", Balance=56562389.85, Bank="FTB"},
            new Customer(){ Name="Tina Fey", Balance=1000000.00, Bank="CITI"},
            new Customer(){ Name="Sid Brown", Balance=49582.68, Bank="CITI"}


        };

            IEnumerable<Customer> millionaires = customers.Where(customer => customer.Balance >= 1000000);

            var Banks = millionaires.GroupBy(customer => customer.Bank);


            foreach (IGrouping<string, Customer> millionBoys in Banks)
            {
                Console.WriteLine($"{millionBoys.Key} {millionBoys.Count()}");
            }

            var millionaireReport = from millionaire in millionaires group millionaires by millionaire.Bank into mills orderby mills.Key select mills;


            // foreach (var item in millionaireReport)
            // {
            //     Console.WriteLine($"{item.CustomerName} at {item.BankName}");
            // }

        }
    }
}