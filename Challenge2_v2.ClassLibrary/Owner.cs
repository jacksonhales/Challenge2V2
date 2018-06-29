using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2_v2.ClassLibrary
{
    public class Owner
    {
        private int ownerID;
        private string surname;
        private string givenName;
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }


        public string GivenName
        {
            get { return givenName; }
            set { givenName = value; }
        }


        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }


        public int OwnerID
        {
            get { return ownerID; }
            set { ownerID = value; }
        }


    }
}
