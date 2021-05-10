using CustomerSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.ViewComponents
{
    public class ReservationFormViewComponent : ViewComponent
    {
        public ReservationFormViewComponent()
        {
        }

        public IViewComponentResult Invoke(int bikeId)
        {
            var vm = new CustomerReservationViewModel
            {
                RequestedBikeId = bikeId
            };

            return View(vm);
        }
    }
}
