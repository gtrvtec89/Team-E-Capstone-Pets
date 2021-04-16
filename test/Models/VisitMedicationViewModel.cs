using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class VisitMedicationViewModel
    {
        public string strPetName { get; set; }
        public string strDoctor { get; set; }
        public DateTime dtmDateOfVisit { get; set; }

        public IEnumerable<TMedication> Medications { get; set; }
        public IEnumerable<TVisitMedication> PetVisitMedications { get; set; }
    }
}