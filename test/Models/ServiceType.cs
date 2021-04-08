using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace test.Models {

	public class ServiceType {


		public int intServiceTypeID { get; set; }

		[Required(ErrorMessage = "Service Type is Required!")]
		[Display(Name = "Service Type")]
		public string strServiceType { get; set; }
	}
}