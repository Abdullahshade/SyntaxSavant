using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Project;

namespace UML_Project
{
    public class Admin : User
    {
        private string workschedule;

        public Admin(string name, string username, string password, int id) : base(name, username,password , UserRole.Admin , id)
        {
        }

        public void ManageUserAccounts(List<User> users)
        {
            Console.WriteLine("All users:");
            foreach (User user in users)
            {
                Console.WriteLine(user.Id+"]"+user.Name);
            }
        }

        public void SetRole(User userToChange, UserRole newRole)
        {
            userToChange.Role = newRole;
            Console.WriteLine(userToChange.Name + "'s role has been changed to " + newRole);

            // Update the user list and save it to file
        }




    }

}
