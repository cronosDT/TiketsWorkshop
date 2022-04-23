using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsWorkshop.Data;
using TicketsWorkshop.Data.Entities;
using TicketsWorkshop.Helpers;
using TicketsWorkshop.Models;

namespace TicketsWorkshop.Controllers
{
    public class TicketController : Controller
    {
        private readonly ICombosHelper _combosHelper;
        private readonly DataContext _context;

        public TicketController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets.ToListAsync());
        }

        public IActionResult CheckTicket()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.Id == id);
            if (id < 0 || id > 5003)
            {
                TempData["Message"] = "Error de boleta, no existe";

                return RedirectToAction(nameof(CheckTicket));

            }
            if (ticket.WasUsed != false)
            {
                TempData["Message"] = "Ticket ya es usado.";
                TempData["Name"] = ticket.Name;
                TempData["Document"] = ticket.Document;
                TempData["Date"] = ticket.DateTime;
                //TODO : time and entrance
                return RedirectToAction(nameof(CheckTicket), new { Id = ticket.Id });
            }
            else
            {
                TempData["Message"] = "Ticket no ha sido usada.";
                return RedirectToAction(nameof(EditTicket), new { Id = ticket.Id });
            }
        }


        public async Task<IActionResult> EditTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            TicketViewModel model = new()
            {
                Document = ticket.Document,
                Id = ticket.Id,
                Name = ticket.Name,
                DateTime = (DateTime)ticket.DateTime,
                WasUsed = (Boolean)ticket.WasUsed,
                Entrances = await _combosHelper.GetComboEntrancesAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTicket(int id, TicketViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                Ticket ticket = await _context.Tickets.FindAsync(model.Id);
                ticket.Document = model.Document;
                ticket.Name = model.Name;
                ticket.DateTime = model.DateTime;
                ticket.WasUsed = model.WasUsed;


                ticket.TicketEntrances = new List<TicketEntrance>()
                {
                    new TicketEntrance
                    {
                        Entrance = await _context.Entrances.FindAsync(model.EntranceId)
                    }
                };

                try
                {
                    _context.Add(ticket);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un ticket con el mismo id.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            model.Entrances = await _combosHelper.GetComboEntrancesAsync();
            return View(model);
        }




        /*public async Task<IActionResult> EditTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            TicketViewModel model = new()
            {
                Document = ticket.Document,
                Id = ticket.Id,
                Name = ticket.Name,
                DateTime = (DateTime)ticket.DateTime,
                WasUsed = (Boolean)ticket.WasUsed,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            try
            {
                Ticket ticket = await _context.Tickets.FindAsync(model.Id);
                ticket.Document = model.Document;
                ticket.Name = model.Name;
                ticket.DateTime = model.DateTime;
                ticket.WasUsed = model.WasUsed;
                _context.Update(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Ya existe un producto con el mismo nombre.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return View(model);
        }*/
    }
}
