using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoRepository
{
    public class Developer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmployeeIDNumber { get; set; }
        public bool HasPluralsightAccess { get; set; }

        // Empty constructor
        public Developer() { }

        // Parameter constructor
        public Developer(string firstName, string lastName, int employeeID, bool hasPS)
        {
            FirstName = firstName;
            LastName = lastName;
            EmployeeIDNumber = employeeID;
            HasPluralsightAccess = hasPS; 
        }
    }

    public class DevTeam
    {
        public List<Developer> TeamMembers { get; set; }
        public string TeamName { get; set; }
        public int TeamIDNumber { get; set; }

        // Empty constructor
        public DevTeam() { }

        // Parameter constructor
        public DevTeam(List<Developer> teamMembers, string teamName, int teamID)
        {
            TeamMembers = teamMembers;
            TeamName = teamName;
            TeamIDNumber = teamID;
        }
    }
}
