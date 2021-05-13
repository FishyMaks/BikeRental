namespace Services
{
    public class BikeRentalRoute
    {
        // API Endpoints
        public const string Bikes = "bikes";
        public const string Customers = "customers";
        public const string Reservations = "reservations";
        public const string BikeStores = "bikestores";
        public const string Employees = "employees";

        public const string CustomersByEmail = Customers + "/email";
        public const string ReservationsByBikeId = Bikes + "/reservation";
    }
}
