using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class VisitSummary
    {
        //Owner Information
        public string strOwnerName { get; set; }
        public string strAddress { get; set; }
        public string strPhoneNumber { get; set; }
        public int intOwnerNumber { get; set; }

        //Pet Information
        public int intPetID { get; set; }
        public string strPetName { get; set; }
        public string strPetNumber { get; set; }

        //Visit Information
        public DateTime dtmOfVisit { get; set; }
        public string strDoctor { get; set; }
        public IEnumerable<TVisitService> PetVisitServices { get; set; }
        public IEnumerable<TVisitMedication> PetVisitMedications { get; set; }

    }
}