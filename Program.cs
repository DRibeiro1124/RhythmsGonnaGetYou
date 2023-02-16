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

            while (keepGoing)
            {
                var input = PromptInput("Please select from the following menu options: \r\n(C)reate, (V)iew, (U)pdate a band or (Q)uit program ").ToUpper();

                switch (input)
                {
                    case "C":
                        input = PromptInput("Create a new (B)and, (A)lbum, or a (S)ong ").ToUpper();

                        switch (input)
                        {
                            case "B":
                                context.Bands.Add(CreateBand());
                                context.SaveChanges();
                                break;

                            case "A":
                                Album newAlbum = CreateAlbum();
                                input = PromptInput("What is the name of the band for this new album? ");
                                var otherBandByName = context.Bands.FirstOrDefault(band => band.Name.ToUpper().Contains(input.ToUpper()));

                                if (otherBandByName != null)
                                {
                                    Album existingBandAlbum = CreateAlbum();
                                    existingBandAlbum.BandId = otherBandByName.Id;
                                    context.Add(existingBandAlbum);
                                    context.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine($"This band does not exist in our database...");
                                }
                                break;

                            case "S":
                                input = PromptInput("What is the name of the album that contains new song? ");
                                Album AlbumByName = context.Albums.FirstOrDefault(album => album.Title.ToUpper().Contains(input.ToUpper()));

                                if (AlbumByName != null)
                                {
                                    Song newSong = CreateSong();
                                    newSong.AlbumId = AlbumByName.Id;
                                    context.Add(newSong);
                                    context.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine($"That albums does not exist in our database...");
                                }
                                break;
                        }

                        break;

                    case "V":

                        input = PromptInput("Please select from the following numeric menu: \r\n1- View all bands \r\n2- View all albums from a band \r\n3- View all signed bands \r\n4- View all bands unsigned \r\n5- View all albums in order by release date");

                        List<Band> bandsList = context.Bands.Include(band => band.Albums).ToList();

                        switch (input)
                        {
                            case "1":

                                Console.WriteLine(String.Format(("{0,20} | {1,-20} | {2,-8} | {3, -20}"), "Band Name", "Country of Origin", "Numbers", "Website"));
                                Console.WriteLine($"___________________________________________________________________");

                                foreach (Band bands in bandsList)
                                {
                                    Console.WriteLine(String.Format(("{0,-20} | {1,-20} | {2,-8} | {3,-20}"), bands.Name, bands.CountryOfOrigin, bands.NumberOfMembers, bands.Website));
                                }
                                break;

                            case "2":

                                input = PromptInput("Which band would you like to see albums from? ");

                                var newBandByName = context.Bands.FirstOrDefault(band => band.Name.ToUpper().Contains(input.ToUpper()));

                                if (newBandByName != null)
                                {
                                    Console.WriteLine($"\r\nAlbums by {newBandByName.Name} ");
                                    Console.WriteLine(String.Format(("{0,-20} | {1,-20} | {2,-20}"), "Album Title", "Explicit?", "Release Date"));
                                    Console.WriteLine($"________________________________________________________________");

                                    foreach (Album album in newBandByName.Albums)
                                    {
                                        Console.WriteLine(String.Format(("{0,-20} | {1,-20} | {2,-20}"), album.Title, album.IsExplicit, album.ReleaseDate));
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Band could not be found...");

                                }
                                break;

                            case "3":
                                bandsList = bandsList.Where(band => band.IsSigned).ToList();
                                Console.WriteLine($"These are the bands signed: ");

                                foreach (Band band in bandsList)
                                {
                                    Console.WriteLine(band.Name);
                                }
                                Console.WriteLine();
                                break;

                            case "4":
                                bandsList = bandsList.Where(band => !band.IsSigned).ToList();
                                Console.WriteLine($"These bands are left unsigned: ");

                                foreach (Band band in bandsList)
                                {
                                    Console.WriteLine(band.Name);
                                }
                                Console.WriteLine();
                                break;

                            case "5":
                                List<Album> albumsList = context.Albums.OrderBy(album => album.ReleaseDate).ToList();

                                foreach (Album album in albumsList)
                                {
                                    Console.WriteLine($"{album.Title} was released on {album.ReleaseDate}");
                                }
                                Console.WriteLine();
                                break;
                        }
                        break;

                    case "U":

                        input = PromptInput("What is the name of the band you want to sign or release? ");
                        var bandByName = context.Bands.FirstOrDefault(band => band.Name.ToUpper().Contains(input.ToUpper()));

                        if (bandByName != null)
                        {
                            var isSignedOrNotSigned = (bandByName.IsSigned ? "signed" : "not signed");
                            Console.WriteLine($"{bandByName} is currently {isSignedOrNotSigned} ");
                            input = PromptInput($"Would you like to (S)ign or (R)elease {bandByName.Name}");
                            bandByName.IsSigned = (input.ToUpper() == "S" ? true : false);
                            context.Update(bandByName);
                            context.SaveChanges();
                            isSignedOrNotSigned = (bandByName.IsSigned ? "signed" : "not signed");
                        }
                        else
                        {
                            Console.WriteLine($"Sorry, band not found...");
                        }

                        break;

                    default:
                        keepGoing = false;
                        break;
                }
            }


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
