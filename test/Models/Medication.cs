using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class Medication
    {
        public int intVisitMedicationID { get; set; }
        public int intVisitID { get; set; }
        public int intMedicationID { get; set; }
        public System.DateTime dtmDatePrescribed { get; set; }
        public int intQuantity { get; set; }
    }
}