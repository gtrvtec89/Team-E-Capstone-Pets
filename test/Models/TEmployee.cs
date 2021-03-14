using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace test.Models {
	public class TEmployee {



		[Display(Name = "First Name")]
		public string strFirstName { get; set; }

		[Display(Name = "Last Name")]
		public string strLastName { get; set; }

		[Display(Name = "Active")]
		public int IntJobTitleID { get; set; }

		[Display(Name = "User ID")]
		public int IntUserID { get; set; }

	}
}