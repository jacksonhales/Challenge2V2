using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2_v2.ClassLibrary
{
    public class Treatment
    {
        private int treatmentID;
        private DateTime date;
        private string notes;
        private double price;
        private int petID;
        private int procedureID;

        public int ProcedureID
        {
            get { return procedureID; }
            set { procedureID = value; }
        }


        public int PetID
        {
            get { return petID; }
            set { petID = value; }
        }


        public double Price
        {
            get { return price; }
            set { price = value; }
        }


        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }


        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }


        public int TreatmentID
        {
            get { return treatmentID; }
            set { treatmentID = value; }
        }

    }
}
