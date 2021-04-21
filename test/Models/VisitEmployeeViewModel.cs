using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class VisitEmployeeViewModel
    {
        public string strPetName { get; set; }
        public string strDoctor { get; set; }

        public DateTime dtmDateOfVisit { get; set; }

        public IEnumerable<TEmployee> Employees { get; set; }
        public IEnumerable<TVisitEmployee> PetVisitEmployees { get; set; }
    }
}