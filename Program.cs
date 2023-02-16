using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new RhythmsContext();
            DisplayGreeting();

            bool keepGoing = true;

        }

        static void DisplayGreeting()
        {
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|  Welcome to Empire Records Music DB    |");
            Console.WriteLine("|                                        |");
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine();
            Console.WriteLine();
        }

    }
}
