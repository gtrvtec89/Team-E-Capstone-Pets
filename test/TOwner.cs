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
    
    public partial class TOwner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TOwner()
        {
            this.TPets = new HashSet<TPet>();
        }
    
        public int intOwnerID { get; set; }
        public string strFirstName { get; set; }
        public string strLastName { get; set; }
        public int intGenderID { get; set; }
        public string strAddress { get; set; }
        public string strCity { get; set; }
        public int intStateID { get; set; }
        public string strZip { get; set; }
        public string strPhoneNumber { get; set; }
        public string strEmail { get; set; }
        public string strOwner2Name { get; set; }
        public string strOwner2PhoneNumber { get; set; }
        public string strOwner2Email { get; set; }
        public string strNotes { get; set; }
        public int intUserID { get; set; }
    
        public virtual TGender TGender { get; set; }
        public virtual TState TState { get; set; }
        public virtual TUser TUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TPet> TPets { get; set; }
    }
}
