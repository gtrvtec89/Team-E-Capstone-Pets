using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class OrderMedication
    {
        private DateTime _date = DateTime.Now;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "Order Date is required")]
        public DateTime dtmDateOfOrder { get { return _date; } set { _date = value; } }
        public int intMedicationId { get; set; }
        public string strMedicationName { get; set; }
        public decimal dblUnitCost { get; set; }
        public int intCurrentQuantity { get; set; }
        public int intOrderQuantity { get; set; }
        public decimal dblTotal { get; set; }
        public string strNotes { get; set; }

    }
}