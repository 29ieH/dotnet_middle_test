using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_login
{
    public class menu
    {
        public static void printMenu()
        {
            Console.WriteLine("----- Account Managers ------- ");
            Console.WriteLine("1.Get all records Account");
            Console.WriteLine("2.Add one Account");
            Console.WriteLine("3.Select Account by Username");
            Console.WriteLine("4.Delete Account by IdAccount");
            Console.WriteLine("5.Update Account by IdAccount");
            Console.WriteLine("6.Sort Account by Username");
            Console.WriteLine("7.Exit");
        }
    }
}