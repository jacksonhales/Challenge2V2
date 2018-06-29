using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2_v2.ClassLibrary
{
    public class Pet
    {
        private int petID;
        private string name;
        private string type;
        private int ownerID;

        public int OwnerID
        {
            get { return ownerID; }
            set { ownerID = value; }
        }


        public string Type
        {
            get { return type; }
            set { type = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public int PetID
        {
            get { return petID; }
            set { petID = value; }
        }

    }
}
