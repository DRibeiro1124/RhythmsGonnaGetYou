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
            Console.WriteLine("🎵🎵🎵🎵🎵🎵🎵🎵🎵🎵🎵🎵🎵🎵🎵🎵🎵🎵🎵🎵🎵🎵");
            Console.WriteLine("🎶                                        🎶");
            Console.WriteLine("🎶  Welcome to Empire Records Music DB    🎶");
            Console.WriteLine("🎶                                        🎶");
            Console.WriteLine("🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶🎶");
            Console.WriteLine();
            Console.WriteLine();
        }

        public static string PromptInput(string prompt)
        {
            Console.WriteLine(prompt);
            string response = Console.ReadLine();
            return response;
        }

        public static Band CreateBand()
        {
            var newBand = new Band
            {
                Name = PromptInput("New name for the band: "),
                CountryOfOrigin = PromptInput("New Country of origin for the band: "),
                NumberOfMembers = int.Parse(PromptInput("New number of band members for band: ")),
                Website = PromptInput("New website for band: "),
                Genre = PromptInput("New genre for the band: "),
                IsSigned = (PromptInput("Is this new band signed? ").ToUpper() == "YES" ? true : false),
                ContactName = PromptInput("Who is the new manager for the band: "),
            };
            return newBand;
        }

        public static Album CreateAlbum()
        {
            var newAlbum = new Album
            {
                Title = PromptInput("New album name: "),
                IsExplicit = (PromptInput("Is the new album explicit? ").ToUpper() == "YES" ? true : false),
                ReleaseDate = DateTime.Parse(PromptInput("What date was it released? (YYYY-MM-DD)")),
            };
            return newAlbum;
        }

        public static Song CreateSong()
        {
            Song newSong = new Song
            {
                TrackNumber = int.Parse(PromptInput("What is the track number? ")),
                Title = PromptInput("New song name: "),
                Duration = int.Parse(PromptInput("What is the song duration? ")),
            };

            return newSong;
        }



    }
}
