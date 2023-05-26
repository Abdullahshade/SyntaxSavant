using System;
using System.Collections.Generic;



namespace UML_Project
{
    public class Admin : User
    {
        private string workschedule;

        public Admin(string name, string username, string password, int id) : base(name, username, password, UserRole.Admin, id)
        {
        }

        public void ManageUserAccounts(List<User> users)
        {
            Console.WriteLine("[ID]  user name\t\t\t Role");
            Console.WriteLine("__________________________________________");


            foreach (User user in users)
            {
                Console.WriteLine("[" + user.Id + "]   " + user.Username + "\t\t\t" + user.Role);
            }
            Console.WriteLine("__________________________________________");

        }

        public void SetRole(User userToChange, UserRole newRole)
        {
            userToChange.Role = newRole;
            Console.WriteLine(userToChange.Name + "'s role has been changed to " + newRole);

            // Update the user list and save it to file
        }
        public void DeleteUser(LibrarySystem lb, User userToDelete)
        {
            lb.DeleteUser(userToDelete);
        }
        public void generateReports(LibrarySystem lb)
        {
            lb.generateReports();
        }

        public void modifyingUsers(LibrarySystem lb, User userToModify)
        {
            Console.WriteLine("[0]To change username\n[1]To change password\n[2]To change role");
            int readL = Convert.ToInt32(Console.ReadLine());
            if (readL == 0)
            {
                Console.WriteLine("Enter new username");
                string newUser = Console.ReadLine();
                userToModify.Username = newUser;

            }
            else if (readL == 1)
            {
                Console.WriteLine("Enter new password");
                string newPass = Console.ReadLine();
                userToModify.Password = newPass;

            }else if (readL == 2)
            {
               
                    Console.WriteLine("Enter the new role for the user:");
                    UserRole newRole = (UserRole)Enum.Parse(typeof(UserRole), Console.ReadLine());

                    SetRole(userToModify, newRole);
                    lb.SaveUsersToFile();

                
                
                lb.LoadUsersFromFile();
            }
            else
            {
                Console.WriteLine("wrong input");
            }
            lb.SaveUsersToFile();
        }
    }

}