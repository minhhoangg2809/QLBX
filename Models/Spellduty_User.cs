//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLBX.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Spellduty_User
    {
        public int id { get; set; }
        public Nullable<int> spelldutyId { get; set; }
        public Nullable<int> usersId { get; set; }
    
        public virtual Spellduty Spellduty { get; set; }
        public virtual User User { get; set; }
    }
}