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
    
    public partial class TPetType
    {
        public TPetType()
        {
            this.TPets = new HashSet<TPet>();
        }
    
        public int intPetTypeID { get; set; }
        public string strPetType { get; set; }
    
        public virtual ICollection<TPet> TPets { get; set; }
    }
}
