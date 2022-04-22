using Microsoft.AspNetCore.Mvc.Rendering;
using TicketsWorkshop.Data;
using Microsoft.EntityFrameworkCore;

namespace TicketsWorkshop.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;
        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboEntrancesAsync()
        {
            List<SelectListItem> list = await _context.Entrances.Select(e => new SelectListItem
            {
                Text = e.Description,
                Value = e.Id.ToString()
            })
                .OrderBy(e => e.Text);
                .ToListasync();

            list.Insert(0, new SelectListItem { Text = "Seleccione una entrada", Value = "0"});
            return list;
        }
    }
}
