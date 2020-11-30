using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoRepository
{
    class DeveloperRepo
    {
        // Create a field to store all the developers at the company
        private List<Developer> _listOfDevelopers = new List<Developer>();

        // Add a new developer
        public void AddNewDeveloper(Developer newDeveloper)
        {
            _listOfDevelopers.Add(newDeveloper);
        }

        // Read the list of developers
        public List<Developer> GetAllDevelopers()
        {
            return _listOfDevelopers;
        }

        // Update or edit an existing developer
        public bool EditExistingDeveloper(int employeeNumber, Developer newDeveloperInfo)
        {
            // Locate the developer by their ID number
            var oldDeveloper = GetDevByID(employeeNumber);

            // Update the developer's information, if the developer was found in the list
            if (oldDeveloper != null)
            {
                oldDeveloper.FirstName = newDeveloperInfo.FirstName;
                oldDeveloper.LastName = newDeveloperInfo.LastName;
                oldDeveloper.EmployeeIDNumber = newDeveloperInfo.EmployeeIDNumber;
                oldDeveloper.HasPluralsightAccess = newDeveloperInfo.HasPluralsightAccess;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Delete an existing developer, if the developer was found in the list
        public bool RemoveDeveloper(int employeeNumber)
        {
            var developer = GetDevByID(employeeNumber);

            if (developer == null)
            {
                return false;
            }

            int numberOfDevs = _listOfDevelopers.Count;
            _listOfDevelopers.Remove(developer);

            // Check if the entry was removed
            if (numberOfDevs > _listOfDevelopers.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Helper method to find employees by ID number
        private Developer GetDevByID(int employeeNumber)
        {
            foreach (var developer in _listOfDevelopers)
            {
                if (developer.EmployeeIDNumber == employeeNumber)
                {
                    return developer;
                }
            }

            return null;
        }
    }

    class DevTeamRepo
    {
        // Create a field to store all of the developer teams at the company
        private List<DevTeam> _listOfDeveloperTeams = new List<DevTeam>();

        // Add a new team
        public void AddNewTeam(DevTeam newTeam)
        {
            _listOfDeveloperTeams.Add(newTeam);
        }

        // Returns a list of all current teams
        public List<DevTeam> GetAllTeams()
        {
            return _listOfDeveloperTeams;
        }

        // Edit existing teams
        public bool EditExistingTeam(int teamNumber, DevTeam newTeamInfo)
        {
            // Find the team by ID
            var oldTeam = GetTeamByID(teamNumber);

            // Update the team's info if the team was found in the list
            if (oldTeam != null)
            {
                oldTeam.TeamMembers = newTeamInfo.TeamMembers;
                oldTeam.TeamName = newTeamInfo.TeamName;
                oldTeam.TeamIDNumber = newTeamInfo.TeamIDNumber;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Delete an existing team
        public bool RemoveTeam(int teamNumber)
        {
            var team = GetTeamByID(teamNumber);

            if (team == null)
            {
                return false;
            }

            int count = _listOfDeveloperTeams.Count;
            _listOfDeveloperTeams.Remove(team);

            // Check if the entry was removed
            if (count > _listOfDeveloperTeams.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Helper method to find teams by team ID
        private DevTeam GetTeamByID(int teamNumber)
        {
            foreach (var devTeam in _listOfDeveloperTeams)
            {
                if (devTeam.TeamIDNumber == teamNumber)
                {
                    return devTeam;
                }
            }

            return null;
        }
    }
}
