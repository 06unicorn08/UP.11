//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPF_autoparking
{
    using System;
    using System.Collections.Generic;
    
    public partial class InsurancePolicies
    {
        public int policy_id { get; set; }
        public int car_id { get; set; }
        public System.DateTime start_date { get; set; }
        public System.DateTime end_date { get; set; }
        public string coverage_details { get; set; }
    
        public virtual Cars Cars { get; set; }
    }
}
