using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2_v2.ClassLibrary
{
    public class PetTreatmentsView
    {
        private string name;
        private string type;
        private DateTime date;
        private string notes;
        private double price;

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


        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public string Type
        {
            get { return type; }
            set { type = value; }
        }


    }
}
