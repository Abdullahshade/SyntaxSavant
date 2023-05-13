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
            Console.WriteLine("[ID]  user name\t\t\t Role");
            Console.WriteLine("__________________________________________");


            foreach (User user in users)
            {
                Console.WriteLine("["+user.Id+"]   "+user.Username+"\t\t\t"+user.Role);
            }
            Console.WriteLine("__________________________________________");

        }

        public void SetRole(User userToChange, UserRole newRole)
        {
            userToChange.Role = newRole;
            Console.WriteLine(userToChange.Name + "'s role has been changed to " + newRole);

            // Update the user list and save it to file
        }
        public void DeleteUser(LibrarySystem lb,User userToDelete)
        {
            lb.DeleteUser(userToDelete);
        }




    }

}
