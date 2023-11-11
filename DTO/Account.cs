using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public  class Account
    {
        private int id;
        public int ID
        { get { return id; } 
            set { id = value; }
        }
        private string userName;
        public string UserName { get { return userName; } set { userName = value; } }
        private string password;
        public string Password { get { return password; } set { password = value; } }

        private string displayName;
        public string DisplayName { get { return displayName; } set { displayName = value; } }

        private int typeAccount;
        public int TypeAccount { get {  return typeAccount; } set {  typeAccount = value; } }   

        public Account (string userName, string password, string dis, int typeA )
        {
            this.userName = userName;
            this.password = password;
            this.displayName = dis;
            this.typeAccount = typeA;
        }
        public Account(int id, string userName, string password, string dis, int typeA)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.displayName = dis;
            this.typeAccount = typeA;
        }
        public Account()
        { }



    }
}
