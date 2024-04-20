using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace app_login
{
    public class until
    {
        private static until _Instance;
        public static until Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new until();
                }
                return _Instance;
            }
            private set { }
        }
        public List<role> getAllRecordRoles()
        {
            String query = "select * from role";
            List<role> result = new List<role>();
            foreach (DataRow i in helper.Instance.getRecords(query).Rows)
            {
                int id = Convert.ToInt32(i["idRole"]);
                result.Add(new role
                {
                    idRole = id,
                    nameRole = i["nameRole"].ToString()
                });
            }
            return result;
        }
        public List<account> getAllRecordAccounts()
        {
            String query = "select * from account";
            List<account> result = new List<account>();
            foreach (DataRow i in helper.Instance.getRecords(query).Rows)
            {
                DateTime dayCreated = Convert.ToDateTime(i["dateCreate"]);
                int id = Convert.ToInt32(i["idRole"]);
                result.Add(new account
                {
                    idAccount = i["idAccount"].ToString(),
                    userName = i["userName"].ToString(),
                    psw = i["psw"].ToString(),
                    email = i["email"].ToString(),
                    dateCreate = dayCreated,
                    idRole = id
                });
            }
            return result;
        }
        public bool validID(String idAccount)
        {
            foreach (account a in database.Instance.TableAccounts)
            {
                if (a.idAccount == idAccount)
                {
                    return true;
                }
            }
            return false;
        }
        public bool validEmail(String email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }
        public int addAccount()
        {
            int rs = 0;
            DateTime today = DateTime.Today;
            string query = "insert into account values (@idAccount,@userName,@psw,@email,@dateCreate,@idRole)";
            String idAccount;
            String userName;
            String psw;
            String email;
            DateTime dateCreate = today;
            int idRole;
            bool flag = false;
            do
            {
                Console.WriteLine("Enter id account: ");
                idAccount = Console.ReadLine();
                if (validID(idAccount))
                {
                    Console.WriteLine("Your IDAccount already exits");
                }
            } while (validID(idAccount));
            Console.WriteLine("Enter username: ");
            userName = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            psw = Console.ReadLine();
            do
            {
                Console.WriteLine("Enter email: ");
                email = Console.ReadLine();
                if (!validEmail(email)) Console.WriteLine("Email is invalid");
            } while (!validEmail(email));
            do
            {
                Console.WriteLine("Enter idRole: (0 or 1)");
                idRole = Convert.ToInt32(Console.ReadLine());
                if (idRole < 0 || idRole > 1)
                {
                    Console.WriteLine("Id role (0 or 1) !!!");
                }
            } while (idRole < 0 || idRole > 1);
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@idAccount",idAccount),
                new SqlParameter("@userName",userName),
                new SqlParameter("@psw",psw),
                new SqlParameter("@email",email),
                new SqlParameter("@dateCreate",dateCreate),
                new SqlParameter("@idRole",idRole),
            };
            rs = helper.Instance.excuteDB(query, param);
            if (rs > 0)
            {
                database.Instance.TableAccounts.Add(new account
                {
                    idAccount = idAccount,
                    userName = userName,
                    psw = psw,
                    email = email,
                    dateCreate = dateCreate,
                    idRole = idRole
                });
            }
            return rs;
        }
        public account SearchAccountByUsername()
        {
            String username;
            account rs = null;
            Console.WriteLine("Enter username: ");
            username = Console.ReadLine();
            foreach (account i in database.Instance.TableAccounts)
            {
                if (i.userName.ToLower().CompareTo(username.ToLower()) == 0)
                {

                    rs = new account
                    {
                        idAccount = i.idAccount,
                        userName = i.userName,
                        psw = i.psw,
                        email = i.email,
                        dateCreate = i.dateCreate,
                        idRole = i.idRole,
                    };
                    break;
                }
            }
            return rs;
        }
        public int deleteAccountById()
        {
            int check = 0;
            String idAccount;
            Console.WriteLine("Enter id Account you want delete: ");
            idAccount = Console.ReadLine();
            if (!validID(idAccount))
            {
                Console.WriteLine("Not found account by idAccount");
                return check;
            }
            String sql = "delete account where idAccount = @id";
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@id",idAccount)
            };
            check = helper.Instance.excuteDB(sql, param);
            if (check > 0)
            {
                database.Instance.TableAccounts.RemoveAll(item => item.idAccount == idAccount.Trim());
            }
            return check;
        }
        public int updateAccount()
        {
            int rs = 0;
            int index = 0;
            bool flag;
            String idAccount;
            DateTime dateCreate = Convert.ToDateTime("2003/4/29");
            Console.WriteLine("Enter idAccount you want Update: ");
            idAccount = Console.ReadLine();
            for (int i = 0; i < database.Instance.TableAccounts.Count; i++)
            {
                if (database.Instance.TableAccounts[i].idAccount == idAccount.Trim())
                {
                    dateCreate = database.Instance.TableAccounts[i].dateCreate;
                    Console.WriteLine(database.Instance.TableAccounts[i].ToString());
                    index = i;
                }
            }
            if (index == 0)
            {
                Console.WriteLine("Not found account by idAccount");
                return rs;
            }
            Console.WriteLine("Do you want update this Account? (0.No - 1.Yes)");
            flag = Convert.ToBoolean((Console.ReadLine() == "0" ? false : true));
            if (flag)
            {
                String username;
                String psw;
                String email;
                int idRole;
                Console.WriteLine("Enter username: ");
                username = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                psw = Console.ReadLine();
                do
                {
                    Console.WriteLine("Enter email: ");
                    email = Console.ReadLine();
                    if (!validEmail(email)) Console.WriteLine("Email is invalid");
                } while (!validEmail(email));
                do
                {
                    Console.WriteLine("Enter idRole: (0 or 1)");
                    idRole = Convert.ToInt32(Console.ReadLine());
                    if (idRole < 0 || idRole > 1)
                    {
                        Console.WriteLine("Id role (0 or 1) !!!");
                    }
                } while (idRole < 0 || idRole > 1);
                account accountUpdate = new account
                {
                    idAccount = idAccount,
                    userName = username,
                    psw = psw,
                    email = email,
                    dateCreate = dateCreate,
                    idRole = idRole,
                };
                String query = "update account set userName = @userName,psw =@psw,email =@email,dateCreate = @dateCreate,idRole= @idRole where idAccount = @idAccount";
                SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@userName",username),
                new SqlParameter("@psw",psw),
                new SqlParameter("@email",email),
                new SqlParameter("@dateCreate",dateCreate),
                new SqlParameter("@IdRole",idRole),
                new SqlParameter("@idAccount",idAccount),
            };
                rs = helper.Instance.excuteDB(query, param);
                if (rs > 0)
                {
                    database.Instance.TableAccounts[index] = accountUpdate;
                }
            }
            else
            {
                Console.WriteLine("Goodbye");
            }
            return rs;
        }
        public void sortAccountByUserName(List<account> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i].userName.CompareTo(list[j].userName) > 0)
                    {
                        account temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
            foreach (account a in list)
            {
                Console.WriteLine(a.ToString());
            }
        }
    }
}