using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_login
{
    public class Program
    {
        public static void Main()
        {
            bool flag = true;
            menu until = new menu();
            String choose;
            do
            {
                menu.printMenu();
                Console.WriteLine("Enter your choose: ");
                choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        choose1();
                        break;
                    case "2":
                        choose2();
                        break;
                    case "3": choose3(); break;
                    case "4": choose4(); break;
                    case "5": choose5(); break;
                    case "6": choose6(); break;
                    case "7":
                        flag = false;
                        Console.WriteLine("Bye..");
                        break;
                };
            } while (flag);
        }
        public static void choose1()
        {
            foreach (account a in database.Instance.TableAccounts)
            {
                Console.WriteLine(a);
            }
        }
        public static void choose2()
        {
            if (database.Instance.addAccount() > 0)
            {
                Console.WriteLine("Add account successful");
            }
            else
            {
                Console.WriteLine("Add failed");
            }
        }
        public static void choose3()
        {
            account rs = until.Instance.SearchAccountByUsername();
            if (rs != null)
            {
                Console.WriteLine("Account your find: " + rs.ToString());
            }
            else
            {
                Console.WriteLine("Not found");
            }
        }
        public static void choose4()
        {
            int rs = database.Instance.deleteAccountById();
            if (rs > 0)
            {
                Console.WriteLine("Delete successful");
            }
            else
            {
                Console.WriteLine("Delete failed");
            }
        }
        public static void choose5()
        {
            int rs = database.Instance.updateAccount();
            String s = (rs > 0) ? "Update Succes" : "Update Failed";
            Console.WriteLine(s);
        }
        public static void choose6()
        {
            List<account> listSort = new List<account>();
            listSort = [.. database.Instance.TableAccounts];
            database.Instance.sortAccount(listSort);
        }
    }
}