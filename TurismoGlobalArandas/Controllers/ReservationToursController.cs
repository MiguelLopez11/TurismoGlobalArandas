using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationToursController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public ReservationToursController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ReservationTours>> getReservationsTours()
        {
            var Reservations = await _context.ReservationTours
                .Include(i => i.Destinations)
                .ToListAsync();
            return Ok(Reservations);
        }
        [HttpGet("{ReservationTourId}")]
        public async Task<ActionResult> getReservationTour(int ReservationTourId)
        {
            var Reservation = await _context.ReservationTours
                .Include(i => i.Destinations)
                .FirstOrDefaultAsync(f => f.ReservationTourId == ReservationTourId);
            if (Reservation == null)
            {
                return NotFound();
            }
            return Ok(Reservation);
        }
        [HttpPost]
        public async Task<ActionResult<ReservationTours>> PostReservationTour(ReservationTours reservation)
        {
            reservation.DateSale = DateTime.Now;
            reservation.Invoice = _context.GetInvoiceReservationTours();
            _context.ReservationTours.Add(reservation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "getReservationTour",
                new { ReservationTourId = reservation.ReservationTourId },
                reservation
            );

        }

        [HttpPut("{ReservationTourId}")]
        public async Task<ActionResult> PutReservationTour(int ReservationTourId, ReservationTours reservationTours)
        {
            if (reservationTours.ReservationTourId != ReservationTourId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ReservationOld = await _context.ReservationTours.FirstOrDefaultAsync(
                f => f.ReservationTourId == ReservationTourId
            );

            if (ReservationOld == null)
            {
                return BadRequest($"La reservación con el ID {ReservationTourId} no existe");
            }
            ReservationOld.ReservationTourId = reservationTours.ReservationTourId;
            ReservationOld.Invoice = reservationTours.Invoice;
            ReservationOld.TourName = reservationTours.TourName;
            ReservationOld.CustomerId = reservationTours.CustomerId;
            ReservationOld.DestinationId = reservationTours.DestinationId;
            ReservationOld.DateSale = reservationTours.DateSale;
            ReservationOld.PaymentLimitDate = reservationTours.PaymentLimitDate;
            ReservationOld.PaymentLimitDateProvider = reservationTours.PaymentLimitDateProvider;
            ReservationOld.DateActivity = reservationTours.DateActivity;
            ReservationOld.OwnerName = reservationTours.OwnerName;
            ReservationOld.Transportations = reservationTours.Transportations;
            ReservationOld.NumberAdults = reservationTours.NumberAdults;
            ReservationOld.NumberMinors = reservationTours.NumberMinors;
            ReservationOld.AdultCost = reservationTours.AdultCost;
            ReservationOld.MinorCost = reservationTours.MinorCost;
            ReservationOld.PublicRate = reservationTours.PublicRate;
            ReservationOld.NetPrice = reservationTours.NetPrice;
            ReservationOld.ExchangeRate = reservationTours.ExchangeRate;
            ReservationOld.IsInternational = reservationTours.IsInternational;
            ReservationOld.IsNational = reservationTours.IsNational;
            ReservationOld.IncludeTransportation = reservationTours.IncludeTransportation;
            ReservationOld.IsSoldOut = reservationTours.IsSoldOut;
            ReservationOld.EmployeeId = reservationTours.EmployeeId;
            ReservationOld.ProviderId = reservationTours.ProviderId;
            ReservationOld.IsDeleted = reservationTours.IsDeleted;
            var ReservationPaymentRelation = await _context.PaymentsRelationReservations
                .Include(i => i.ReservationTours)
                .FirstOrDefaultAsync(f => f.ReservationTourId == ReservationTourId);
            var PaymentProvider = await _context.PaymentProviders
                .Include(i => i.ReservationTours)
                .FirstOrDefaultAsync(f => f.ReservationTourId == ReservationTourId);
            if (ReservationPaymentRelation.AmountTotal == null && PaymentProvider.AmountTotal == null && reservationTours.NetPrice != null)
            {
                if (ReservationPaymentRelation.AmountMissing == null && PaymentProvider.AmountMissing == null)
                {
                    ReservationPaymentRelation.AmountMissing = reservationTours.NetPrice;
                    PaymentProvider.AmountMissing = reservationTours.NetPrice;
                }
                ReservationPaymentRelation.AmountTotal = reservationTours.NetPrice;
                PaymentProvider.AmountTotal = reservationTours.NetPrice;
                _context.PaymentsRelationReservations.Update(ReservationPaymentRelation);
                _context.PaymentProviders.Update(PaymentProvider);
                await _context.SaveChangesAsync();
            }
            _context.ReservationTours.Update(ReservationOld);
            await _context.SaveChangesAsync();
            return Ok("Actualización correcta");
        }

        [HttpDelete("{ReservationTourId}")]
        public async Task<IActionResult> DeleteReservationTour(int ReservationTourId)
        {
            var Reservation = await _context.ReservationTours.FirstOrDefaultAsync(
                f => f.ReservationTourId == ReservationTourId
            );
            if (Reservation == null)
            {
                return NotFound();
            }

            Reservation.IsDeleted = true;
            _context.ReservationTours.Update(Reservation);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }
        [HttpDelete("Delete/{ReservationTourId}")]
        public async Task<IActionResult> RemoveReservationTour(int ReservationTourId)
        {
            var Reservation = await _context.ReservationTours.FirstOrDefaultAsync(
                f => f.ReservationTourId == ReservationTourId
            );
            if (Reservation == null)
            {
                return NotFound();
            }
            var paymentRelation = await _context.PaymentsRelationReservations.FirstOrDefaultAsync(
                f => f.ReservationTourId == ReservationTourId
            );
            if (paymentRelation != null)
            {
                //LISTA DE PAGOS CLIENTE
                var paymentRelationList = await _context.PaymentRelationLists
                    .Where(w => w.PaymentReservationId == paymentRelation.PaymentReservationId)
                    .ToListAsync();
                foreach (var item in paymentRelationList)
                {
                    _context.PaymentRelationLists.Remove(item);
                    await _context.SaveChangesAsync();
                }
                _context.PaymentsRelationReservations.Remove(paymentRelation);
            }
            var PaymentProviders = await _context.PaymentProviders
                .Where(w => w.ReservationTourId == ReservationTourId)
                .ToListAsync();
            foreach (var item in PaymentProviders)
            {
                _context.PaymentProviders.Remove(item);
                await _context.SaveChangesAsync();
            }
            _context.ReservationTours.Remove(Reservation);
            await _context.SaveChangesAsync();
            return Ok("registro eliminado");
        }
        [HttpGet("DescargarDatosEnPDF/{ReservationTourId}")]
        public async Task<ActionResult> DescargarDatosEnPDF(int ReservationTourId)
        {
            await new BrowserFetcher().DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions { Headless = true }
            );
            var reservation = await _context.ReservationTours
                .Include(i => i.Employees)
                .Include(i => i.Customers)
                .Include(i => i.Destinations)
                .Include(i => i.Providers)
                .FirstOrDefaultAsync(f => f.ReservationTourId == ReservationTourId);
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            var htmlContent = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n  <head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n  " +
                $"  <link\r\n      rel=\"stylesheet\"\r\n      href=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css\"\r\n    />\r\n  </head>\r\n  <style>\r\n   " +
                $" #row_campos {{\r\n      gap: 15px;\r\n      border: 1px solid #000000;\r\n      padding: 10px;\r\n    }}\r\n    .custom-label {{\r\n      display: block;\r\n     " +
                $" margin-top: 5px; /* Ajusta el espacio entre p y label */\r\n    }}\r\n\r\n    .custom-border {{\r\n      border: 1px solid #ccc;\r\n      padding: 10px; /* Ajusta el espacio dentro de las columnas */\r\n    }}\r\n" +
                $"    p {{\r\n      font-size: 13px;\r\n      font-family: 'Franklin Gothic Medium', 'Arial', Arial, sans-serif;\r\n      text-align: justify;\r\n    }}\r\n    input {{\r\n      height: 35px;\r\n    }}\r\n  " +
                $"  #Reservation_Type {{\r\n      height: 35px;\r\n    }}\r\n    table {{\r\n      font-family: arial, sans-serif;\r\n      border-collapse: collapse;\r\n      width: 100%;\r\n    }}\r\n\r\n    td,\r\n    " +
                $"th {{\r\n      border: 1px solid #dddddd;\r\n      text-align: left;\r\n      padding: 8px;\r\n      font-size: 13px;\r\n    }}\r\n\r\n    tr:nth-child(even) {{\r\n      background-color: #dddddd;\r\n    }}\r\n" +
                $"    .container {{\r\n      max-width: 100%;\r\n    }}\r\n  </style>\r\n  <body>\r\n    <div class=\"container text-center\">\r\n      <div class=\"row p-1 justify-content-center\">\r\n      " +
                $"  <div class=\"col-sm border p-2 text-white bg-secondary ml-5\">\r\n          <h6>Confirmación de Reserva</h6>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n          <input\r\n           " +
                $" id=\"Reservation_Type\"\r\n            type=\"text\"\r\n            class=\"form-control custom-border\"\r\n style=\"height:50px\" \r\n           value=\"RESERVACIÓN DE TOURS\"\r\n          />\r\n        </div>\r\n      </div>\r\n      " +
                $"<div id=\"row_campos\" class=\"row mt-3\">\r\n        <div class=\"col-sm text-center\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n             " +
                $" <p class=\"b\">Nombre del tour:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n               " +
                $" class=\"form-control custom-border\"\r\n                value=\"{reservation.TourName}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n       " +
                $" <div class=\"col-sm text-center\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Nombre(s):</p>\r\n            </div>\r\n            " +
                $"<div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.Customers.Name}\"\r\n              />\r\n" +
                $"            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Apellidos:</p>\r\n" +
                $"            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n              " +
                $"  value=\"{reservation.Customers.Lastname}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n        <div class=\"col-sm\">\r\n         " +
                $" <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Destino:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n               " +
                $" type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.Destinations.Name}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n  " +
                $"     \r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n             " +
                $" <p class=\"b\">Fecha de viaje:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n " +
                $"               value=\"{reservation.DateActivity}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n      </div>\r\n      " +
                $"<div id=\"row_campos\" style=\"gap: 10px\" class=\"row mt-3\">\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n             " +
                $" <p class=\"b\">Nombre de titular:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n " +
                $"               value=\"{reservation.OwnerName}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n          " +
                $"  <div class=\"col-3\">\r\n              <p class=\"b\">Adultos:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n               " +
                $" class=\"form-control custom-border\"\r\n                value=\"{reservation.NumberAdults}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n  " +
                $"      <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">menores:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n        " +
                $"      <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.NumberMinors}\"\r\n              />\r\n            </div>\r\n          </div>\r\n " +
                $"       </div>\r\n\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-4\">\r\n              <p>Costo por menor:</p>\r\n            </div>\r\n           " +
                $" <div class=\"col-8\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{"$ " + reservation.MinorCost}\"\r\n              />\r\n " +
                $"           </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n        <div class=\"col-12\">\r\n          <div class=\"row\">\r\n            <div class=\"col-4\">\r\n             " +
                $" <p>Costo por adulto:</p>\r\n            </div>\r\n            <div class=\"col-8\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n              " +
                $"  value=\"{"$ " + reservation.AdultCost}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-12\">\r\n          <div class=\"row\">\r\n            <div class=\"col-1\">\r\n " +
                $"             <p>Transporte:</p>\r\n            </div>\r\n            <div class=\"col-11\">\r\n              <textarea\r\n                           rows=\"4\"\r\n               " +
                $" class=\"w-100 custom-border\"\r\n              >{reservation.Transportations} </textarea>\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </div>\r\n      <div id=\"row_campos\" style=\"gap: 10px\" class=\"row mt-3\">\r\n       " +
                $" <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-4\">\r\n              <p class=\"b\">Tarifa pública:</p>\r\n            </div>\r\n            <div class=\"col-8\">\r\n             " +
                $" <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{"$ " + reservation.PublicRate}\"\r\n              />\r\n            </div>\r\n          </div>\r\n       " +
                $" </div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-4\">\r\n              <p class=\"b\">Tarifa neto:</p>\r\n            </div>\r\n            <div class=\"col-8\">\r\n " +
                $"             <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{"$ " + reservation.NetPrice}\"\r\n              />\r\n            </div>\r\n         " +
                $" </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-4\">\r\n              <p class=\"b\">Tipo de tour:</p>\r\n  " +
                $"          </div>\r\n            <div class=\"col-8\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{(reservation.IsNational ? "NACIONAL" : "INTERNACIONAL")}\"\r\n  " +
                $"            />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-4\">\r\n              <p class=\"b\">Tipo de cambio:</p>\r\n" +
                $"            </div>\r\n            <div class=\"col-8\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.ExchangeRate}\"\r\n   " +
                $"           />\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </div>\r\n\r\n    <script src=\"https://code.jquery.com/jquery-3.2.1.slim.min.js\"></script>\r\n   " +
                $" <script src=\"https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js\"></script>\r\n    <script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js\"></script>\r\n  </body>\r\n</html>\r\n";

            await page.SetContentAsync(htmlContent);
            var pdfContent = await page.PdfStreamAsync(
                new PdfOptions
                {
                    Format = PaperFormat.Letter,
                    PrintBackground = true,
                    MarginOptions = new MarginOptions
                    {
                        Top = "5px",
                        Right = "5px",
                        Bottom = "5px",
                        Left = "5px"
                    }
                }
            );
            return File(pdfContent, "application/pdf", "converted.pdf");
        }
    }
}
