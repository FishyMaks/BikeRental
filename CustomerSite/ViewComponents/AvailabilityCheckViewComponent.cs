using BikeRentalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Extensions;
using Services.Repositories;
using System.Threading.Tasks;

namespace CustomerSite.ViewComponents
{
    public class AvailabilityCheckViewComponent : ViewComponent
    {
        readonly IRepositoryAsync<Reservation> _reservationsRepo;

        public AvailabilityCheckViewComponent(IRepositoryAsync<Reservation> reservations)
        {
            _reservationsRepo = reservations;
        }

        public async Task<IViewComponentResult> InvokeAsync(int bikeId)
        {
            Reservation reservation = await _reservationsRepo.GetByBikeIdAsync(bikeId);
            return View(reservation);
        }
    }
}
