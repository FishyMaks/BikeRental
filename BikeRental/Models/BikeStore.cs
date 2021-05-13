using System.Collections.Generic;

namespace BikeRentalApi.Models
{
    public class BikeStore : BaseEntity
    {
        public BikeStore()
        {
            Bikes = new HashSet<Bike>();
            Employees = new HashSet<Employee>();
            Reservations = new HashSet<Reservation>();
        }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public decimal DailyRate { get; set; }
        public decimal Surcharge { get; set; }
        public decimal Discount { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public virtual ICollection<Bike> Bikes { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
