namespace test.Models
{
    public class Vaccination
    {

        public int intVaccinationID { get; set; }
        public int intVisitServiceID { get; set; }
        public System.DateTime dtmDateOfVaccination { get; set; }
        public System.DateTime dtmDateOfExpiration { get; set; }
        public string strVaccineDesc { get; set; }
        public string strRabiesNumber { get; set; }
    }
}