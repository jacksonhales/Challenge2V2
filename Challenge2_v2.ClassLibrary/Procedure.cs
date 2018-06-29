using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2_v2.ClassLibrary
{
    public class Procedure
    {
        private int procedureID;
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int ProcedureID
        {
            get { return procedureID; }
            set { procedureID = value; }
        }

    }
}
