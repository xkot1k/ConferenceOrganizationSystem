//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConferenceOrganizationSystem.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Activity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Activity()
        {
            this.ActivityJuries = new HashSet<ActivityJurie>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int DayNumber { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public int IdModerator { get; set; }
        public int IdEvent { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivityJurie> ActivityJuries { get; set; }
    }
}
