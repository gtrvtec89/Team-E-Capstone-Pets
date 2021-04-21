using System;
using System.Collections.Generic;

namespace test.Models
{
    public class VisitServiceViewModel
    {
        public string strPetName { get; set; }
        public string strDoctor { get; set; }

        public DateTime dtmDateOfVisit { get; set; }

        public IEnumerable<TService> Services { get; set; }
        public IEnumerable<TVisitService> PetVisitServices { get; set; }

    }
}