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
    
    public partial class Moderator
    {
        public int Id { get; set; }
        public int IdDirection { get; set; }
        public int IdEvent { get; set; }
    
        public virtual Direction Direction { get; set; }
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}
