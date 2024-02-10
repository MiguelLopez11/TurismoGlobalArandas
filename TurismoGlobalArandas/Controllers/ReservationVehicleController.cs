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
    public class ReservationVehicleController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public ReservationVehicleController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ReservationVehicle>> getReservationVehicles()
        {
            var reservationVehicles = await _context.ReservationVehicles
                .Include(i => i.Providers)
                .ToListAsync();
            return Ok(reservationVehicles);
        }
        [HttpGet("{ReservationVehicleId}")]
        public async Task<ActionResult> getReservationVehicle(int ReservationVehicleId)
        {
            var reservationVehicle = await _context.ReservationVehicles
                .Include(i => i.Providers)
                .FirstOrDefaultAsync(f => f.ReservationVehicleId == ReservationVehicleId);
            if (reservationVehicle == null)
            {
                return NotFound();
            }
            return Ok(reservationVehicle);
        }
        [HttpPost]
        public async Task<ActionResult<ReservationVehicle>> PostReservationVehicle(ReservationVehicle reservationVehicle)
        {
            reservationVehicle.Invoice = _context.GetInvoiceReservationVehicles();
            reservationVehicle.DateSale = DateTime.Now;
            _context.ReservationVehicles.Add(reservationVehicle);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "getReservationVehicle",
                new { ReservationVehicleId = reservationVehicle.ReservationVehicleId },
                reservationVehicle
            );

        }

        [HttpPut("{ReservationVehicleId}")]
        public async Task<ActionResult> PutReservationVehicle(int ReservationVehicleId, ReservationVehicle reservationVehicle)
        {
            if (reservationVehicle.ReservationVehicleId != ReservationVehicleId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ReservationOld = await _context.ReservationVehicles.FirstOrDefaultAsync(
                f => f.ReservationVehicleId == ReservationVehicleId
            );

            if (ReservationOld == null)
            {
                return BadRequest($"La reservación con el ID {ReservationVehicleId} no existe");
            }
            ReservationOld.ReservationVehicleId = reservationVehicle.ReservationVehicleId;
            ReservationOld.Invoice = reservationVehicle.Invoice;
            ReservationOld.CustomerId = reservationVehicle.CustomerId;
            ReservationOld.DateSale = reservationVehicle.DateSale;
            ReservationOld.DateTravel = reservationVehicle.DateTravel;
            ReservationOld.PaymentLimitDate = reservationVehicle.PaymentLimitDate;
            ReservationOld.PaymentLimitDateProvider = reservationVehicle.PaymentLimitDateProvider;
            ReservationOld.DepartureLocation = reservationVehicle.DepartureLocation;
            ReservationOld.ArrivalLocation = reservationVehicle.ArrivalLocation;
            ReservationOld.ProviderId = reservationVehicle.ProviderId;
            ReservationOld.PricePublic = reservationVehicle.PricePublic;
            ReservationOld.PriceNeto = reservationVehicle.PriceNeto;
            ReservationOld.Description = reservationVehicle.Description;
            ReservationOld.IsSoldOut = reservationVehicle.IsSoldOut;
            ReservationOld.IsDeleted = reservationVehicle.IsDeleted;
            var ReservationPaymentRelation = await _context.PaymentsRelationReservations
                .Include(i => i.ReservationVehicles)
                .FirstOrDefaultAsync(f => f.ReservationVehicleId == ReservationVehicleId);
            var PaymentProvider = await _context.PaymentProviders
                .Include(i => i.ReservationVehicles)
                .FirstOrDefaultAsync(f => f.ReservationVehicleId == ReservationVehicleId);
            if (ReservationPaymentRelation.AmountTotal == null && PaymentProvider.AmountTotal == null && reservationVehicle.PriceNeto != null)
            {
                if (ReservationPaymentRelation.AmountMissing == null && PaymentProvider.AmountMissing == null)
                {
                    ReservationPaymentRelation.AmountMissing = reservationVehicle.PriceNeto;
                    PaymentProvider.AmountMissing = reservationVehicle.PriceNeto;
                }
                ReservationPaymentRelation.AmountTotal = reservationVehicle.PriceNeto;
                PaymentProvider.AmountTotal = reservationVehicle.PriceNeto;
                _context.PaymentsRelationReservations.Update(ReservationPaymentRelation);
                _context.PaymentProviders.Update(PaymentProvider);
                await _context.SaveChangesAsync();
            }
            _context.ReservationVehicles.Update(ReservationOld);
            await _context.SaveChangesAsync();
            return Ok("Actualización correcta");
        }
        [HttpDelete("{ReservationVehicleId}")]
        public async Task<IActionResult> DeleteReservationVehicle(int ReservationVehicleId)
        {
            var Reservation = await _context.ReservationVehicles.FirstOrDefaultAsync(
                f => f.ReservationVehicleId == ReservationVehicleId
            );
            if (Reservation == null)
            {
                return NotFound();
            }

            Reservation.IsDeleted = true;
            _context.ReservationVehicles.Update(Reservation);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }
        [HttpDelete("Delete/{ReservationVehicleId}")]
        public async Task<IActionResult> removeReservationVehicle(int ReservationVehicleId)
        {
            var Reservation = await _context.ReservationVehicles.FirstOrDefaultAsync(
                f => f.ReservationVehicleId == ReservationVehicleId
            );
            if (Reservation == null)
            {
                return NotFound();
            }
            var paymentRelation = await _context.PaymentsRelationReservations.FirstOrDefaultAsync(
               f => f.ReservationVehicleId == ReservationVehicleId
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
                .Where(w => w.ReservationVehicleId == ReservationVehicleId)
                .ToListAsync();
            foreach (var item in PaymentProviders)
            {
                _context.PaymentProviders.Remove(item);
                await _context.SaveChangesAsync();
            }
            _context.ReservationVehicles.Remove(Reservation);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }
        [HttpGet("DescargarDatosEnPDF/{ReservationVehicleId}")]
        public async Task<ActionResult> DescargarDatosEnPDF(int ReservationVehicleId)
        {
            await new BrowserFetcher().DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions { Headless = true }
            );
            var reservation = await _context.ReservationVehicles
                .Include(i => i.Employees)
                .Include(i => i.Customers)
                .Include(i => i.Providers)
                .FirstOrDefaultAsync(f => f.ReservationVehicleId == ReservationVehicleId);
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            var htmlContent = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n  <head>\r\n    <meta charset=\"UTF-8\" />\r\n   " +
                $" <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n    <title>Bootstrap Grid Example</title>\r\n " +
                $"   <link\r\n      rel=\"stylesheet\"\r\n      href=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css\"\r\n    />\r\n " +
                $" </head>\r\n  <style>\r\n    #row_campos {{\r\n      gap: 5px;\r\n      border: 2px solid #000000;\r\n      padding: 5px;\r\n    }}\r\n" +
                $"    #row_campos2 {{\r\n      gap: 5px;\r\n      border: 1px solid #dddddd;\r\n      padding: 5px;\r\n    }}\r\n    .custom-label {{\r\n  " +
                $"    display: block;\r\n      margin-top: 5px; /* Ajusta el espacio entre p y label */\r\n    }}\r\n\r\n    .custom-border {{\r\n      " +
                $"border: 1px solid #ccc;\r\n      padding: 5px; /* Ajusta el espacio dentro de las columnas */\r\n    }}\r\n    p {{\r\n      font-size: 16px;\r\n " +
                $"     font-family: 'Franklin Gothic Medium', 'Arial', Arial, sans-serif;\r\n      text-align: justify;\r\n    }}\r\n    input {{\r\n     " +
                $" height: 50px;\r\n    }}\r\n    #Reservation_Type {{\r\n      height: 50px;\r\n    }}\r\n\r\n    tr:nth-child(even) {{\r\n     " +
                $" background-color: #dddddd;\r\n    }}\r\n    .container {{\r\n      max-width: 100%;\r\n    }}\r\n  </style>\r\n  <body>\r\n    " +
                $"<div class=\"container text-center\">\r\n      <div class=\"row p-1 justify-content-center\">\r\n        <div class=\"col-sm border p-2 text-white bg-secondary ml-5\">\r\n " +
                $"         <h6>Confirmación de Reserva</h6>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n          <input\r\n            id=\"Reservation_Type\"\r\n   " +
                $"         type=\"text\"\r\n            class=\"form-control custom-border\"\r\n            value=\"RESERVACIÓN TRANSLADO VEHICULAR\"\r\n          />\r\n        </div>\r\n      </div>\r\n   " +
                $"   <div id=\"row_campos\" class=\"row mt-3\">\r\n        <div class=\"col-sm text-center\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n" +
                $"              <p class=\"b\">Nombre(s):</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n " +
                $"               class=\"form-control custom-border\"\r\n                value=\"{reservation.Customers.Name}\"\r\n              />\r\n            </div>\r\n          </div>\r\n  " +
                $"      </div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Apellidos:</p>\r\n " +
                $"           </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n " +
                $"               value=\"{reservation.Customers.Lastname}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n " +
                $"       <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Folio:</p>\r\n            </div>\r\n   " +
                $"         <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n             " +
                $"   value=\"{reservation.Invoice}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n        " +
                $"  <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Precio público:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n " +
                $"             <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.PricePublic}\"\r\n  " +
                $"            />\r\n            </div>\r\n          </div>\r\n        </div>\r\n\r\n        <div class=\"w-100\"></div>\r\n        <div class=\"col-sm\">\r\n       " +
                $"   <div class=\"row\">\r\n            <div class=\"col-2\">\r\n              <p class=\"b\">Precio neto:</p>\r\n            </div>\r\n            <div class=\"col-4\">\r\n " +
                $"             <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.PriceNeto}\"\r\n    " +
                $"          />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n      </div>\r\n     " +
                $" <div id=\"row_campos\" style=\"gap: 10px\" class=\"row mt-3\">\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n " +
                $"             <p class=\"b\">Fecha de reservación:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n " +
                $"               class=\"form-control custom-border\"\r\n                value=\"{reservation.DateTravel}\"\r\n              />\r\n            </div>\r\n          </div>\r\n   " +
                $"     </div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Fecha compra:</p>\r\n " +
                $"           </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n " +
                $"               value=\"{reservation.DateSale}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n " +
                $"       <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Promotora:</p>\r\n            </div>\r\n " +
                $"           <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n             " +
                $"   value=\"{reservation.Providers.Name}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n        " +
                $"  <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Agente:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n  " +
                $"            <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.Employees.Name + " " + reservation.Employees.Lastname}\"\r\n     " +
                $"         />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n        <div class=\"col-sm\">\r\n         " +
                $" <div class=\"row\">\r\n            <div class=\"col-4\">\r\n              <p>Plazo de pago a cliente:</p>\r\n            </div>\r\n            <div class=\"col-8\">\r\n " +
                $"             <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.PaymentLimitDate}\"\r\n    " +
                $"          />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n           " +
                $" <div class=\"col-4\">\r\n              <p>Observaciones:</p>\r\n            </div>\r\n            <div class=\"col-8\">\r\n              <textarea\r\n               " +
                $" rows=\"4\"\r\n                class=\"w-100 custom-border\"\r\n              >{reservation.Description}</textarea>\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </div>\r\n" +
                $"    </div>\r\n\r\n    <script src=\"https://code.jquery.com/jquery-3.2.1.slim.min.js\"></script>\r\n    <script src=\"https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js\"></script>\r\n " +
                $"   <script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js\"></script>\r\n  </body>\r\n</html>\r\n";

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
