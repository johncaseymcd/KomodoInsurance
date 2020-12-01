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
        private DeveloperRepo _developerRepo = new DeveloperRepo();
        private DevTeamRepo _devTeamRepo = new DevTeamRepo();

        public void Run()
        {
            SeedDeveloperList();
            Menu();
        }

        private void Menu()
        {
        MainMenu:
            Console.Clear();
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
                        Console.WriteLine("Press D to add a Developer or press T to add a Team.");
                        string addWhich = Console.ReadLine().ToLower();
                        if (addWhich == "d")
                        {
                            CreateNewDeveloper();
                            PressAnyKey();
                            goto MainMenu;
                        }
                        else if (addWhich == "t")
                        {
                            CreateNewTeam();
                            PressAnyKey();
                            goto MainMenu;
                        }
                        else
                        {
                            PressEnter();
                            goto case 1;
                        }
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Press D to view all Developers or press T to view all Teams. Press P to view all developers needing access to PluralSight.");
                        string viewWhich = Console.ReadLine().ToLower();
                        if (viewWhich == "d")
                        {
                            ViewAllDevelopers();
                            PressAnyKey();
                            goto MainMenu;
                        }
                        else if (viewWhich == "t")
                        {
                            ViewAllTeams();
                            PressAnyKey();
                            goto MainMenu;
                        }
                        else if (viewWhich == "p")
                        {
                            ViewDevsWithoutPluralSight();
                            PressAnyKey();
                            goto MainMenu;
                        }
                        else
                        {
                            PressEnter();
                            goto case 2;
                        }
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Press D to search for a Developer or press T to search for a Team.");
                        string searchWhich = Console.ReadLine().ToLower();
                        if (searchWhich == "d")
                        {
                            SearchForDeveloper();
                            PressAnyKey();
                            goto MainMenu;
                        }
                        else if (searchWhich == "t")
                        {
                            SearchForDevTeam();
                            PressAnyKey();
                            goto MainMenu;
                        }
                        else
                        {
                            PressEnter();
                            goto case 3;
                        }
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Press D to edit a Developer or press T to edit a Team.");
                        string editWhich = Console.ReadLine().ToLower();
                        if (editWhich == "d")
                        {
                            EditDeveloper();
                            PressAnyKey();
                            goto MainMenu;
                        }
                        else if (editWhich == "t")
                        {
                            EditDevTeam();
                            PressAnyKey();
                            goto MainMenu;
                        }
                        else
                        {
                            PressEnter();
                            goto case 4;
                        }
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Press D to delete a Developer or press T to delete a Team.");
                        string deleteWhich = Console.ReadLine().ToLower();
                        if (deleteWhich == "d")
                        {
                            DeleteDeveloper();
                            PressAnyKey();
                            goto MainMenu;
                        }
                        else if (deleteWhich == "t")
                        {
                            DeleteDevTeam();
                            PressAnyKey();
                            goto MainMenu;
                        }
                        else
                        {
                            PressEnter();
                            goto case 5;
                        }
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
                        PressEnter();
                        goto MainMenu;
                }
            }
            else
            {
                PressEnter();
                goto MainMenu;
            }
        }

        // Create a new Developer from user input
        private Developer CreateNewDeveloper()
        {
            // Setup
            Console.Clear();
            var newDeveloper = new Developer();

            // First name
            Console.WriteLine("Enter the developer's first name:");
            newDeveloper.FirstName = Console.ReadLine();

            // Last name
            Console.WriteLine("Enter the developer's last name:");
            newDeveloper.LastName = Console.ReadLine();

            // Employee ID
        EnterID:
            Console.WriteLine("Enter the developer's unique ID number:");
            bool parsed = int.TryParse(Console.ReadLine(), out int devID);
            if (parsed)
            {
                foreach (var developer in _developerRepo.GetAllDevelopers())
                {
                    if (developer.EmployeeIDNumber == devID)
                    {
                        Console.WriteLine("This developer ID number is already in use. Please enter a unique ID.");
                        goto EnterID;
                    }
                }

                newDeveloper.EmployeeIDNumber = devID;
            }
            else
            {
                PressEnter();
                goto EnterID;
            }

        // Has PluralSight license?
        HasAccess:
            Console.WriteLine($"Does {newDeveloper.FirstName} {newDeveloper.LastName} have access to PluralSight (y/n)?");
            string access = Console.ReadLine().ToLower();
            if (access == "y")
            {
                newDeveloper.HasPluralsightAccess = true;
            }
            else if (access == "n")
            {
                newDeveloper.HasPluralsightAccess = false;
            }
            else
            {
                PressEnter();
                goto HasAccess;
            }

            _developerRepo.AddNewDeveloper(newDeveloper);
            return newDeveloper;
        }

        // Create a new DevTeam from user input
        private void CreateNewTeam()
        {
            // Setup
            Console.Clear();
            var devTeam = new DevTeam();
            var devsToAdd = new List<Developer>();
            var developer = new Developer();

            // Team name
            Console.WriteLine("What is the team's name?");
            devTeam.TeamName = Console.ReadLine();

            // Team ID
        EnterTeamID:
            Console.WriteLine("Please enter a unique team ID number.");
            bool parsedTeamID = int.TryParse(Console.ReadLine(), out int teamID);
            if (parsedTeamID)
            {
                devTeam.TeamIDNumber = teamID;
            }
            else
            {
                PressEnter();
                goto EnterTeamID;
            }

        AddDevelopersByID:
            // Team size
            Console.WriteLine("How many developers would you like to add to this team?");
            bool parsedSize = int.TryParse(Console.ReadLine(), out int howManyDevs);

            // Add devs to team
            if (parsedSize)
            {
                for (int x = 1; x <= howManyDevs; x++)
                {
                    Console.WriteLine("Enter the employee ID to add to the team:");
                    bool parsedEmployeeID = int.TryParse(Console.ReadLine(), out int employeeID);
                    if (parsedEmployeeID)
                    {
                        developer = _developerRepo.GetDevByID(employeeID);
                        if (developer != null)
                        {
                            devsToAdd.Add(developer);
                        }
                        else
                        {
                            Console.WriteLine("Employee ID not found. Would you like to add a new employee to this team (y/n)?");
                            string addNew = Console.ReadLine().ToLower();
                            if (addNew == "y")
                            {
                                developer = CreateNewDeveloper();
                                devsToAdd.Add(developer);
                            }
                            else if (addNew == "n")
                            {
                                PressAnyKey();
                                goto AddDevelopersByID;
                            }
                            else
                            {
                                PressEnter();
                                goto AddDevelopersByID;
                            }
                        }
                    }
                    else
                    {
                        PressEnter();
                        goto AddDevelopersByID;
                    }
                }
            }
            else
            {
                PressEnter();
                goto AddDevelopersByID;
            }

            devTeam.TeamMembers = devsToAdd;
            _devTeamRepo.AddNewTeam(devTeam);
        }

        // List all Developers
        private void ViewAllDevelopers()
        {
            Console.Clear();
            foreach (var developer in _developerRepo.GetAllDevelopers())
            {
                if (developer.HasPluralsightAccess)
                {
                    Console.WriteLine($"Employee #{developer.EmployeeIDNumber}: {developer.LastName}, {developer.FirstName}. Has access to PluralSight.");
                }
                else
                {
                    Console.WriteLine($"Employee #{developer.EmployeeIDNumber}: {developer.LastName}, {developer.FirstName}. Does not have access to PluralSight.");
                }   
            }
        }

        // List Developers who do not have PluralSight access
        private void ViewDevsWithoutPluralSight()
        {
            foreach(var developer in _developerRepo.GetAllDevelopers())
            {
                if (!developer.HasPluralsightAccess)
                {
                    Console.WriteLine($"Employee #{developer.EmployeeIDNumber}: {developer.LastName}, {developer.FirstName}");
                }
            }
        }

        // List all DevTeams
        private void ViewAllTeams()
        {
            Console.Clear();
            var teams = _devTeamRepo.GetAllTeams();
            var teamMember = new Developer();

            foreach (var devTeam in teams)
            {
                Console.WriteLine($"Team ID: {devTeam.TeamIDNumber}\n" +
                    $"Team name: {devTeam.TeamName}\n" +
                    $"Team members:");
                foreach(var developer in devTeam.TeamMembers)
                {
                    Console.WriteLine($"Employee #{developer.EmployeeIDNumber}: {developer.LastName}, {developer.FirstName}");
                }
                Console.WriteLine("\n");
            }
        }

        // Search for a Developer
        private void SearchForDeveloper()
        {
            var developer = new Developer();
        EnterID:
            Console.WriteLine("Enter the employee's ID:");
            bool parsed = int.TryParse(Console.ReadLine(), out int employeeID);
            if (parsed)
            {
                developer = _developerRepo.GetDevByID(employeeID);
                if (developer != null && developer.HasPluralsightAccess)
                {
                    Console.WriteLine($"Employee #{developer.EmployeeIDNumber}: {developer.LastName}, {developer.FirstName}. Has PluralSight access.");
                }
                else if (developer != null && !developer.HasPluralsightAccess)
                {
                    Console.WriteLine($"Employee #{developer.EmployeeIDNumber}: {developer.LastName}, {developer.FirstName}. Does not have PluralSight access.");
                }
                else
                {
                    Console.WriteLine("No developers found with that ID.");
                    PressAnyKey();
                    goto EnterID;
                }
            }
            else
            {
                PressEnter();
                goto EnterID;
            }
        }

        // Search for a DevTeam
        private void SearchForDevTeam()
        {
            Console.Clear();
            var devTeam = new DevTeam();
        EnterID:
            Console.WriteLine("Enter the team ID:");
            bool parsed = int.TryParse(Console.ReadLine(), out int teamID);
            if (parsed)
            {
                devTeam = _devTeamRepo.GetTeamByID(teamID);
                if (devTeam != null)
                {
                    Console.WriteLine($"Team ID: {devTeam.TeamIDNumber}\n" +
                        $"Team name: {devTeam.TeamName}\n" +
                        $"Team members:");
                    foreach(var developer in devTeam.TeamMembers)
                    {
                        Console.WriteLine($"Employee #{developer.EmployeeIDNumber}: {developer.LastName}, {developer.FirstName}");
                    }
                }
                else
                {
                    Console.WriteLine("No teams found with that ID.");
                    PressAnyKey();
                    goto EnterID;
                }
            }
        }

        // Edit an existing Developer
        private void EditDeveloper()
        {
            var developer = new Developer();
            Console.Clear();
        EnterID:
            Console.WriteLine("Enter the employee ID to edit:");
            bool parsed = int.TryParse(Console.ReadLine(), out int employeeID);
            if (parsed)
            {
                developer = _developerRepo.GetDevByID(employeeID);
                // Set new first name
                Console.WriteLine("Enter the employee's first name:");
                developer.FirstName = Console.ReadLine();

                // Set new last name
                Console.WriteLine("Enter the employee's last name:");
                developer.LastName = Console.ReadLine();

                // Set PluralSight access
            TryAgain:
                Console.WriteLine($"Does {developer.FirstName} {developer.LastName} have access to PluralSight (y/n)?");
                string access = Console.ReadLine().ToLower();
                if (access == "y")
                {
                    developer.HasPluralsightAccess = true;
                }
                else if (access == "n")
                {
                    developer.HasPluralsightAccess = false;
                }
                else
                {
                    PressEnter();
                    goto TryAgain;
                }
            }
            else
            {
                PressEnter();
                goto EnterID;
            }
        }

        // Edit an existing DevTeam
        private void EditDevTeam()
        {
            Console.Clear();

            var devTeam = new DevTeam();
            var developerAdd = new Developer();
            var developerRemove = new Developer();
            var devsToEdit = new List<Developer>();

        EnterID:
            Console.WriteLine("Enter the team ID to edit:");
            bool parsed = int.TryParse(Console.ReadLine(), out int teamID);
            if (parsed)
            {
                devTeam = _devTeamRepo.GetTeamByID(teamID);

                // Set new team name
                Console.WriteLine("Enter the team name:");
                devTeam.TeamName = Console.ReadLine();

            AddOrRemoveDevs:
                Console.WriteLine("Press A to add a team member or press R to remove a team member.");
                string choice = Console.ReadLine().ToLower();
                if (choice == "a")
                {
                AddDevelopersByID:
                    // Team size
                    Console.WriteLine("How many developers would you like to add to this team?");
                    bool parsedAddSize = int.TryParse(Console.ReadLine(), out int howManyAdded);

                    // Add devs to team
                    if (parsedAddSize)
                    {
                        for (int x = 1; x <= howManyAdded; x++)
                        {
                            Console.WriteLine("Enter the employee ID to add to the team:");
                            bool parsedEmployeeAdd = int.TryParse(Console.ReadLine(), out int employeeToAdd);
                            if (parsedEmployeeAdd)
                            {
                                developerAdd = _developerRepo.GetDevByID(employeeToAdd);
                                if (developerAdd != null)
                                {
                                    devsToEdit.Add(developerAdd);
                                }
                                else
                                {
                                    Console.WriteLine("Employee ID not found. Would you like to add a new employee to this team (y/n)?");
                                    string addNew = Console.ReadLine().ToLower();
                                    if (addNew == "y")
                                    {
                                        developerAdd = CreateNewDeveloper();
                                        devsToEdit.Add(developerAdd);
                                    }
                                    else if (addNew == "n")
                                    {
                                        PressAnyKey();
                                        goto AddDevelopersByID;
                                    }
                                    else
                                    {
                                        PressEnter();
                                        goto AddDevelopersByID;
                                    }
                                }
                            }
                            else
                            {
                                PressEnter();
                                goto AddDevelopersByID;
                            }
                        }
                    }
                    else
                    {
                        PressEnter();
                        goto AddDevelopersByID;
                    }
                    devTeam.TeamMembers = devsToEdit;
                }
                else if (choice == "r")
                {
                RemoveDevsByID:
                    Console.WriteLine("How many developers would you like to remove from this team?");
                    bool parsedRemoveSize = int.TryParse(Console.ReadLine(), out int howManyRemoved);
                    if (parsedRemoveSize)
                    {
                        for (int y = 1; y <= howManyRemoved; y++)
                        {
                        TryAgain:
                            Console.WriteLine("Enter the employee ID to remove from the team:");
                            bool parsedEmployeeRemove = int.TryParse(Console.ReadLine(), out int employeeToRemove);
                            if (parsedEmployeeRemove)
                            {
                                developerRemove = _developerRepo.GetDevByID(employeeToRemove);
                                if (developerRemove != null)
                                {
                                    devsToEdit.Remove(developerRemove);
                                }
                                else
                                {
                                    Console.WriteLine("Developer ID not found. No removal necessary.");
                                }
                            }
                            else
                            {
                                PressEnter();
                                goto TryAgain;
                            }
                        }
                    }
                    else
                    {
                        PressEnter();
                        goto RemoveDevsByID;
                    }
                    devTeam.TeamMembers = devsToEdit;
                }
                else
                {
                    PressEnter();
                    goto AddOrRemoveDevs;
                }
            }
            else
            {
                PressEnter();
                goto EnterID;
            }
        }

        // Delete an existing Developer
        private void DeleteDeveloper()
        {
            Console.WriteLine("Delete Dev");
            Console.ReadLine();
        }

        // Delete an existing DevTeaam
        private void DeleteDevTeam()
        {
            Console.WriteLine("Delete Team");
            Console.ReadLine();
        }

        // Helper method to catch input errors
        private void PressEnter()
        {
            Console.WriteLine("Invalid input. Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        // Helper method to prompt the user to press a key to return to a previous menu
        private void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        // Setup method to create existing developers at startup
        private void SeedDeveloperList()
        {
            var cm = new Developer("Casey", "McDonough", 12408, true);
            var js = new Developer("Jimmy", "Sullivan", 6661, true);
            var bw = new Developer("Bill", "Ward", 13, false);
            var jg = new Developer("Jean-Paul", "Gaster", 1141, false);
            var dc = new Developer("Danny", "Carey", 112358, true);

            _developerRepo.AddNewDeveloper(cm);
            _developerRepo.AddNewDeveloper(js);
            _developerRepo.AddNewDeveloper(bw);
            _developerRepo.AddNewDeveloper(jg);
            _developerRepo.AddNewDeveloper(dc);
        }
    }
}
