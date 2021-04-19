using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class OwnerHome
    {
        //Owner Information
        public string intOwnerID { get; set; }
        public string strOwnerName { get; set; }
        public string strAddress { get; set; }
        public string strPhoneNumber { get; set; }
        

        //Pet Information
        public int intPetID { get; set; }
        public string strPetName { get; set; }
        public string strPetNumber { get; set; }

        //Pet Images Information
        public int intPetImageID { get; set; }
        public string strFileName { get; set; }
        public string strContentType { get; set; }
        public byte[] imgContent { get; set; }
        public string strFileType { get; set; }
        public virtual ICollection<TPet> TPets { get; set; }
        //public IEnumerable<TVisitMedication> PetVisitMedications { get; set; }

    }
}