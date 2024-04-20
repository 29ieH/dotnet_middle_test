using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_login
{
    public class account
    {
        public String idAccount { set; get; }
        public String userName { set; get; }
        public String psw { set; get; }
        public String email { set; get; }
        public DateTime dateCreate { set; get; }
        public int idRole { set; get; }

        public override string ToString()
        {
            String role = (idRole == 1) ? "Admin" : "User";
            String dateCreateted = dateCreate.ToString("yyyy/MM/dd");
            return $"IDAccount: {idAccount}, UserName: {userName}, Password: {psw}, Email: {email}, DateCreated: {dateCreateted}, IDRole: {role}";
        }
    }
}