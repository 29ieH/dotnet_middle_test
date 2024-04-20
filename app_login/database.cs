using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_login
{
    public class database
    {
        private List<role> _TableRole;
        private List<account> _TableAccount;

        public List<role> TableRoles
        {
            get
            {
                return _TableRole;
            }
            private set { }
        }
        public List<account> TableAccounts
        {
            get
            {
                return _TableAccount;
            }
            private set { }
        }
        private static database _Instance;
        public static database Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new database();
                }
                return _Instance;
            }
            private set { }
        }
        public database()
        {
            _TableRole = until.Instance.getAllRecordRoles();
            _TableAccount = until.Instance.getAllRecordAccounts();
        }
        public int addAccount()
        {
            return until.Instance.addAccount();
        }
        public account getAccountByUsername()
        {
            return until.Instance.SearchAccountByUsername();
        }
        public int deleteAccountById()
        {
            return until.Instance.deleteAccountById();
        }
        public int updateAccount()
        {
            return until.Instance.updateAccount();
        }
        public void sortAccount(List<account> list)
        {
            until.Instance.sortAccountByUserName(list);
        }

    }
}