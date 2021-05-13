using BikeRentalApi.Models;
using Services.Stores;
using System;

namespace Services.Reservations
{
    public class ReservationRequest
    {
        public Customer Customer { get; set; }
        public int RequestedBikeId { get; set; }
        public int DaysRequested { get; set; }

        public Reservation BuildReservation(PricingInfo pricingInfo)
        {
            Reservation reservation = new Reservation
            {
                BikeId = RequestedBikeId,
                CustomerId = Customer.Id,
                DateReserved = DateTime.Now,
                DateDue = DateTime.Now.AddDays(DaysRequested),
                GrandTotal = pricingInfo.Cost
            };

            return reservation;
        }
    }
}
