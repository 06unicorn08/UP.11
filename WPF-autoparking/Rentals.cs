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
    
    public partial class Rentals
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rentals()
        {
            this.Payments = new HashSet<Payments>();
            this.RentalHistory = new HashSet<RentalHistory>();
        }
    
        public int rental_id { get; set; }
        public int customer_id { get; set; }
        public int car_id { get; set; }
        public System.DateTime rental_start_date { get; set; }
        public System.DateTime rental_end_date { get; set; }
        public decimal total_cost { get; set; }
        public int employee_id { get; set; }
    
        public virtual Cars Cars { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual Employees Employees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payments> Payments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RentalHistory> RentalHistory { get; set; }
    }
}