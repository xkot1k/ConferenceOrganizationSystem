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
    
    public partial class ActivityJurie
    {
        public int Id { get; set; }
        public int IdActivity { get; set; }
        public int IdJurie { get; set; }
    
        public virtual Activity Activity { get; set; }
        public virtual User User { get; set; }
    }
}
