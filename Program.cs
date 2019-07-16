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
            IEnumerable<string> LFruits = from fruit in fruits where fruit.StartsWith("L") select fruit;
            foreach (var item in LFruits)
            {
                Console.WriteLine(item);
            }

            // Which of the following numbers are multiples of 4 or 6
            List<int> numbers = new List<int>()
            {
                15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
            };
            IEnumerable<int> fourSixMultiples = numbers.Where(number => number % 6 == 0 || number % 4 == 0);

            foreach (int number in fourSixMultiples)
            {
                Console.WriteLine(number);
            }

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

            foreach (string name in decendingNames)
            {
                Console.WriteLine(name);
            }

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
            Console.WriteLine($"The Customers:");
            customers.ForEach(customer => Console.WriteLine(customer.Name));

            // build a collection of customers who are millionaires

            //LAMBDA SYNTAX
            // IEnumerable<Customer> MillionairesClub = customers.Where(customer => customer.Balance >= 1000000);
            // QUERY SYNTAX
            IEnumerable<Customer> MillionairesClub = from customer in customers
                                                     where customer.Balance >= 1000000
                                                     select customer;
            Console.WriteLine("The Millionaires Club:");
            foreach (Customer c in MillionairesClub)
            {
                Console.WriteLine(c.Name);
            }
            // GroupBy returns an IGrouping
            // LAMBDA SYNTAX
            // IEnumerable<IGrouping<string, Customer>> MillionairesPerBank = MillionairesClub.GroupBy(customer => customer.Bank);

            // QUERY SYNTAX
            IEnumerable<IGrouping<string, Customer>> MillionairesPerBank = from millionaire in MillionairesClub
                                                                           group millionaire by millionaire.Bank
                                                                           into bankGroup
                                                                           select bankGroup;
            // logic to output how many millionaires by each bank, and what customer banks at which bank                                                              
            foreach (IGrouping<string, Customer> m in MillionairesPerBank)
            {
                System.Console.WriteLine($"{m.Key} {m.Count()}");
                foreach (Customer c in m)
                {
                    System.Console.WriteLine(c.Name);
                }
            }

            /*
            TASK:
            As in the previous exercise, you're going to output the millionaires,
            but you will also display the full name of the bank. You also need
            to sort the millionaires' names, ascending by their LAST name.

            Example output:
            Tina Fey at Citibank
            Joe Landy at Wells Fargo
            Sarah Ng at First Tennessee
            Les Paul at Wells Fargo
            Peg Vale at Bank of America
            
            You will need to use the `Where()`
            and `Select()` methods to generate
            instances of the following class.

            public class ReportItem
            {
                public string CustomerName { get; set; }
                public string BankName { get; set; }
            }
            */

            List<ReportItem> millionaireReport = (from customer in customers
                                                  join bank in banks on customer.Bank equals bank.Symbol
                                                  where customer.Balance >= 1000000
                                                  orderby customer.Name.Split()[1]
                                                  select new ReportItem
                                                  {
                                                      CustomerName = customer.Name,
                                                      BankName = bank.Name
                                                  }).ToList();

            foreach (var item in millionaireReport)
            {
                Console.WriteLine($"{item.CustomerName} at {item.BankName}");
            }

        }
    }
}

