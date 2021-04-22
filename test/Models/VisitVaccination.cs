using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class VisitVaccination
    {
        public int intServiceId { get; set; }
        public int intVisitServiceId { get; set; }
        public string strServiceName { get; set; }
        public string dtmDateofVaccination { get; set; }
        public string dtmDateOfExpiration { get; set; }
        public string strVaccineNotes { get; set; }
        public string strRabiesNumber { get; set; }
    }
}