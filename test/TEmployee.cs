//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace test
{
    using System;
    using System.Collections.Generic;
    
    public partial class TEmployee
    {
        public int intEmployeeID { get; set; }
        public string strFirstName { get; set; }
        public string strLastName { get; set; }
        public int intJobTitleID { get; set; }
        public bool isActive { get; set; }
        public int intUserID { get; set; }
        public int intDepartmentID { get; set; }
    
        public virtual TDepartment TDepartment { get; set; }
    }
}
