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
    
    public partial class TUrogenitalInfo
    {
        public int intUrogenitalInfoID { get; set; }
        public bool isNormal { get; set; }
        public bool isAbnormalUrination { get; set; }
        public bool isGenitalDischarge { get; set; }
        public bool isAnalSacs { get; set; }
        public bool isRectal { get; set; }
        public bool isMammaryTumors { get; set; }
        public bool isAbnormalTesticles { get; set; }
        public bool isBloodSeen { get; set; }
        public int intHealthExamID { get; set; }
    
        public virtual THealthExam THealthExam { get; set; }
    }
}
