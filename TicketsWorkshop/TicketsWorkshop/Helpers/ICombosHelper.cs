using Microsoft.AspNetCore.Mvc.Rendering;
using TicketsWorkshop.Data.Entities;

namespace TicketsWorkshop.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboEntrancesAsync();
        Task<IEnumerable<SelectListItem>> GetComboEntrancesAsync(IEnumerable<Entrance> filter);
    }
}
