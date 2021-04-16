using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class CreateVisit
    {
        public int intPetID { get; set; }
        public DateTime dtmDateOfVisit { get; set; }
        public int intVisitReasonID { get; set; }
        public int intEmployeeID { get; set; }
    }
} 