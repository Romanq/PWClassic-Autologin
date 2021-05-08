using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace AutoLogin.Accounts
{
    [Serializable]
    public class AccountContainer
    {
        public List<Account> container { get; set; }
    }

    [Serializable]
    public class Account
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
