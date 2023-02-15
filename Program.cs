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

            var bandCount = context.Bands.Count();
            Console.WriteLine($"There are {bandCount} bands in the database");

            var albumList = context.Albums.Include(album => album.Band);

            foreach (var album in albumList)
            {
                Console.WriteLine($"There's an Album named {album.Title}");

            }

        }
    }

}


