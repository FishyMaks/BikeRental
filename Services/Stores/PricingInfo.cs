using BikeRentalApi.Models;

namespace Services.Stores
{
    public class PricingInfo
    {
        decimal _totalBeforeDiscount;
        decimal _amountSaved;

        public decimal Cost => _totalBeforeDiscount - _amountSaved;

        public PricingInfo(BikeStore store, Bike bike, int daysRequested)
        {
            decimal storeRate = store.DailyRate * daysRequested;
            _totalBeforeDiscount = storeRate + bike.Price + store.Surcharge;
            _amountSaved = _totalBeforeDiscount * store.Discount;
        }
    }
}
