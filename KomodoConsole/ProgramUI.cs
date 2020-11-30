using KomodoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoConsole
{
    class ProgramUI
    {
        private List<Developer> _developerRepository = new List<Developer>();

        public void Run()
        {
            Menu();
        }

        private void Menu()
        {
        MainMenu:
            Console.WriteLine("What would you like to do?\n" +
                "1. Add a new Developer or Team\n" +
                "2. View all existing Developers or Teams\n" +
                "3. Search for a Developer or Team\n" +
                "4. Edit an existing Developer or Team\n" +
                "5. Delete an existing Developer or Team\n" +
                "6. Exit");

            string input = Console.ReadLine();
            bool parsed = int.TryParse(input, out int whatToDo);

            if (parsed)
            {
                switch (whatToDo)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Press D to add a Developer or press T to create a Team.");
                        if (Console.ReadKey().Key == ConsoleKey.D)
                        {
                            CreateNewDeveloper();
                        }
                        else if (Console.ReadKey().Key == ConsoleKey.T)
                        {
                            CreateNewTeam();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.ReadKey();
                            goto case 1;
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Press D to view all Developers or press T to view all Teams.");
                        if (Console.ReadKey().Key == ConsoleKey.D)
                        {
                            ViewAllDevelopers();
                        }
                        else if (Console.ReadKey().Key == ConsoleKey.T)
                        {
                            ViewAllTeams();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.ReadKey();
                            goto case 2;
                        }
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Press D to search for a Developer or press T to search for a Team.");
                        if (Console.ReadKey().Key == ConsoleKey.D)
                        {
                            SearchForDeveloper();
                        }
                        else if (Console.ReadKey().Key == ConsoleKey.T)
                        {
                            SearchForDevTeam();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.ReadKey();
                            goto case 3;
                        }
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Press D to edit a Developer or press T to edit a Team.");
                        if (Console.ReadKey().Key == ConsoleKey.D)
                        {
                            EditDeveloper();
                        }
                        else if (Console.ReadKey().Key == ConsoleKey.T)
                        {
                            EditDevTeam();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.ReadKey();
                            goto case 4;
                        }
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Press D to delete a Developer or press T to delete a Team.");
                        if (Console.ReadKey().Key == ConsoleKey.D)
                        {
                            DeleteDeveloper();
                        }
                        else if (Console.ReadKey().Key == ConsoleKey.T)
                        {
                            DeleteDevTeam();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.ReadKey();
                            goto case 5;
                        }
                        break;
                    case 6:
                        Console.WriteLine("Press Enter to exit or press any other key to return to the main menu.");
                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                        else
                        {
                            goto MainMenu;
                        }
                    default:
                        Console.WriteLine("Invalid input. Press any key to continue.");
                        goto MainMenu;
                }
            }
        }

        // Create a new Developer from user input
        private void CreateNewDeveloper()
        {
            Console.WriteLine("Create Dev");
        }

        // Create a new DevTeam from user input
        private void CreateNewTeam()
        {
            Console.WriteLine("Create Team");
        }

        // List all Developers
        private void ViewAllDevelopers()
        {
            Console.WriteLine("View Dev");
        }

        // List all DevTeams
        private void ViewAllTeams()
        {
            Console.WriteLine("View Team");
        }

        // Search for a Developer
        private void SearchForDeveloper()
        {
            Console.WriteLine("Search Dev");
        }

        // Search for a DevTeam
        private void SearchForDevTeam()
        {
            Console.WriteLine("Search Team");
        }

        // Edit an existing Developer
        private void EditDeveloper()
        {
            Console.WriteLine("Edit Dev");
        }

        // Edit an existing DevTeam
        private void EditDevTeam()
        {
            Console.WriteLine("Edit Team");
        }

        // Delete an existing Developer
        private void DeleteDeveloper()
        {
            Console.WriteLine("Delete Dev");
        }

        // Delete an existing DevTeaam
        private void DeleteDevTeam()
        {
            Console.WriteLine("Delete Team");
        }
    }
}
