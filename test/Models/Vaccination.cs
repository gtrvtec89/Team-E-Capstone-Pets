using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class Vaccination
    { 
    
        public int intVaccinationID { get; set; }
        public int intVisitServiceID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime dtmDateOfVaccination { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime dtmDateOfExpiration { get; set; }
        public string strVaccineDesc { get; set; }
        public string strRabiesNumber { get; set; }
    }
}