using System;

namespace AutoLogin.Accounts
{
    [Serializable]
    public class Account
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string ClassImage { get; set; }
    }

}
