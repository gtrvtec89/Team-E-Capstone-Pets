//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace test
{
    using System;
    using System.Collections.Generic;
    
    public partial class TNeurologicalInfo
    {
        public int intNeurologicalInfoID { get; set; }
        public bool isNormal { get; set; }
        public bool isPLRL { get; set; }
        public bool isPLRR { get; set; }
        public bool isCPLF { get; set; }
        public bool isCPRF { get; set; }
        public bool isCPLR { get; set; }
        public bool isCPRR { get; set; }
        public bool isPalpebralL { get; set; }
        public bool isPalpebralR { get; set; }
        public int intHealthExamID { get; set; }
    
        public virtual THealthExam THealthExam { get; set; }
    }
}
