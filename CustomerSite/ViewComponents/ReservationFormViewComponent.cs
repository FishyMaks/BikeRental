using Microsoft.AspNetCore.Mvc;
using Services.Reservations;

namespace CustomerSite.ViewComponents
{
    public class ReservationFormViewComponent : ViewComponent
    {
        public ReservationFormViewComponent()
        {
        }

        public IViewComponentResult Invoke(int bikeId)
        {
            var request = new ReservationRequest
            {
                RequestedBikeId = bikeId
            };

            return View(request);
        }
    }
}
