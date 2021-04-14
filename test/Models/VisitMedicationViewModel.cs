using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class VisitMedicationViewModel
    {
        public IEnumerable<TMedication> Medications { get; set; }
        public IEnumerable<TVisitMedication> PetVisitMedications { get; set; }
    }
}