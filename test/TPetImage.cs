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
    
    public partial class TPetImage
    {
        public int intPetImageID { get; set; }
        public int intPetID { get; set; }
        public string imgPrimaryImage { get; set; }
        public byte[] imgImage { get; set; }
        public string strFileName { get; set; }
        public int intImageSize { get; set; }
        public System.DateTime dtmDateAdded { get; set; }
    
        public virtual TPet TPet { get; set; }
    }
}
