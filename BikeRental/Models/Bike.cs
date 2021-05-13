using System.Collections.Generic;

namespace BikeRentalApi.Models
{
    public class Bike : BaseEntity
    {
        public Bike()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int OwningStoreId { get; set; }

        public int FrameSize { get; set; }
        public bool? ElectricMotor { get; set; }
        public bool? AllTerrainSuspension { get; set; }
        public string BikeStyle { get; set; }
        public bool Available { get; set; }
        public decimal Price { get; set; }

        public virtual BikeStore OwningStore { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
