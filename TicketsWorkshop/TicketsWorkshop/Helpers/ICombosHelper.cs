using Microsoft.AspNetCore.Mvc.Rendering;

namespace TicketsWorkshop.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboEntrancesAsync();
    }
}
