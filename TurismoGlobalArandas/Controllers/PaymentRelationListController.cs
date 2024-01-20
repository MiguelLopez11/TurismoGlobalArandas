using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp.Media;
using PuppeteerSharp;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;
using System;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentRelationListController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public PaymentRelationListController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaymentRelationList>> getPaymentsRelationList()
        {
            var payments = await _context.PaymentRelationLists
                .Where(w => !w.IsDeleted)
                .ToListAsync();

            return Ok(payments);
        }

        [HttpGet("{PaymentId}")]
        public async Task<ActionResult> getPaymentRelationList(int PaymentId)
        {
            var payment = await _context.PaymentRelationLists
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.PaymentId == PaymentId);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpGet("PaymentReservationHotel/{PaymentReservationId}")]
        public async Task<ActionResult> getPaymentRelationListByPaymentReservationHotel(
            int PaymentReservationId
        )
        {
            #region comprobar monto faltante en reservación
            var payments = await _context.PaymentRelationLists
                .Where(w => !w.IsDeleted)
                .Where(w => w.PaymentReservationId == PaymentReservationId)
                .ToListAsync();
            decimal? total = 0;
            foreach (var payment in payments)
            {
                total += payment.Amount;
            }
            var paymentRelation = await _context.PaymentsRelationReservations
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.PaymentReservationId == PaymentReservationId);
            if (paymentRelation == null)
            {
                return BadRequest();
            }
            #endregion
            #region tomar todas las reservaciones
            decimal? AmountMissing = paymentRelation.AmountTotal - total;
            paymentRelation.AmountMissing = AmountMissing;
            //RESERVACION HOTELERIA
            var reservationHotel = await _context.ReservationHotels.FirstOrDefaultAsync(
                f => f.ReservationHotelId == paymentRelation.ReservationHotelId
            );
            //RESERVACION VUELOS
            var reservationFlight = await _context.ReservationFlights.FirstOrDefaultAsync(
                f => f.FlightId == paymentRelation.ReservationFlightId
            );
            //RESERVACIONES TOURS
            var reservationTour = await _context.ReservationTours.FirstOrDefaultAsync(
                f => f.ReservationTourId == paymentRelation.ReservationTourId
            );
            //RESERVACIONES VEHICULOS
            var reservationVehicle = await _context.ReservationVehicles.FirstOrDefaultAsync(
                f => f.ReservationVehicleId == paymentRelation.ReservationVehicleId
            );
            #endregion
            if (paymentRelation.AmountMissing == 0)
            {
                if (paymentRelation.ReservationHotelId != null)
                {
                    reservationHotel.IsSoldOut = true;
                    _context.ReservationHotels.Update(reservationHotel);
                    await _context.SaveChangesAsync();
                }
                if (paymentRelation.ReservationFlightId != null)
                {
                    reservationFlight.IsSoldOut = true;
                    _context.ReservationFlights.Update(reservationFlight);
                    await _context.SaveChangesAsync();
                }
                if (paymentRelation.ReservationTourId != null)
                {
                    reservationTour.IsSoldOut = true;
                    _context.ReservationTours.Update(reservationTour);
                    await _context.SaveChangesAsync();
                }
                if (paymentRelation.ReservationVehicleId != null)
                {
                    reservationVehicle.IsSoldOut = true;
                    _context.ReservationVehicles.Update(reservationVehicle);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                if (paymentRelation.ReservationHotelId != null)
                {
                    reservationHotel.IsSoldOut = false;
                    _context.ReservationHotels.Update(reservationHotel);
                    await _context.SaveChangesAsync();
                }
                if (paymentRelation.ReservationFlightId != null)
                {
                    reservationFlight.IsSoldOut = false;
                    _context.ReservationFlights.Update(reservationFlight);
                    await _context.SaveChangesAsync();
                }
                if (paymentRelation.ReservationTourId != null)
                {
                    reservationTour.IsSoldOut = false;
                    _context.ReservationTours.Update(reservationTour);
                    await _context.SaveChangesAsync();
                }
                if (paymentRelation.ReservationVehicleId != null)
                {
                    reservationVehicle.IsSoldOut = false;
                    _context.ReservationVehicles.Update(reservationVehicle);
                    await _context.SaveChangesAsync();
                }
            }
            _context.PaymentsRelationReservations.Update(paymentRelation);
            await _context.SaveChangesAsync();
            return Ok(payments);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentRelationList>> PostPaymentRelationList(
            PaymentRelationList payment
        )
        {
            payment.Invoice = _context.GetInvoicePaymentRelationList();
            payment.PaymentDate = DateTime.Today;
            _context.PaymentRelationLists.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "getPaymentRelationList",
                new { PaymentId = payment.PaymentId },
                payment
            );
        }

        [HttpPut("{PaymentId}")]
        public async Task<ActionResult> PutPaymentRelationList(
            int PaymentId,
            PaymentRelationList payment
        )
        {
            if (payment.PaymentId != PaymentId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var paymentOld = await _context.PaymentRelationLists.FirstOrDefaultAsync(
                f => f.PaymentId == PaymentId
            );
            if (paymentOld == null)
            {
                return BadRequest($"El cliente con el ID {PaymentId} no existe");
            }
            paymentOld.PaymentId = payment.PaymentId;
            paymentOld.Invoice = payment.Invoice;
            paymentOld.Amount = payment.Amount;
            paymentOld.AmountReceivedClient = payment.AmountReceivedClient;
            paymentOld.AmountReturnedClient = payment.AmountReturnedClient;
            paymentOld.PaymentDate = payment.PaymentDate;
            paymentOld.Observations = payment.Observations;
            paymentOld.PaymentMethods = payment.PaymentMethods;
            paymentOld.DetailsPayment = payment.DetailsPayment;
            paymentOld.PaymentReservationId = payment.PaymentReservationId;
            paymentOld.IsDeleted = payment.IsDeleted;

            _context.PaymentRelationLists.Update(paymentOld);
            await _context.SaveChangesAsync();
            return Ok("El cliente se actualizo correctamente");
        }

        [HttpDelete("{PaymentId}")]
        public async Task<IActionResult> DeletePaymentRelationList(int PaymentId)
        {
            var payment = await _context.PaymentRelationLists.FirstOrDefaultAsync(
                f => f.PaymentId == PaymentId
            );
            if (payment == null)
            {
                return NotFound();
            }

            payment.IsDeleted = true;
            _context.PaymentRelationLists.Update(payment);
            await _context.SaveChangesAsync();
            return Ok("Registro archivado");
        }

        [HttpGet("DescargarReciboEnPDF/{PaymentId}")]
        public async Task<ActionResult> DescargarReciboEnPDF(int PaymentId)
        {
            var payment = await _context.PaymentRelationLists
                .Include(i => i.PaymentMethods)
                .FirstOrDefaultAsync(f => f.PaymentId == PaymentId);
            if (payment == null)
            {
                return BadRequest();
            }
            var paymentRelation = await _context.PaymentsRelationReservations
                .Include(i => i.ReservationVehicles)
                .Include(i => i.reservationFlight)
                .Include(i => i.ReservationHotels)
                .Include(i => i.ReservationTours)
                .FirstOrDefaultAsync(f => f.PaymentReservationId == payment.PaymentReservationId);
            string CustomerName = string.Empty;

            if (paymentRelation == null)
            {
                return BadRequest();
            }
            if (paymentRelation.ReservationHotelId != null)
            {
                var employee = await _context.Customers
                      .Select(
                                  s =>
                                      new
                                      {
                                          CustomerId = s.CustomerId,
                                          Name = s.Name,
                                          LastName = s.Lastname
                                      }
                              )
                      .FirstOrDefaultAsync(f => f.CustomerId == paymentRelation.ReservationHotels.CustomerId);
                CustomerName = employee.Name;
            }
            else if (paymentRelation.ReservationFlightId != null)
            {
                var employee = await _context.Customers
                     .Select(
                                 s =>
                                     new
                                     {
                                         CustomerId = s.CustomerId,
                                         Name = s.Name,
                                         LastName = s.Lastname
                                     }
                             )
                     .FirstOrDefaultAsync(f => f.CustomerId == paymentRelation.reservationFlight.CustomerId);
                CustomerName = employee.Name;

            }
            else if (paymentRelation.ReservationVehicleId != null)
            {
                var employee = await _context.Customers
                     .Select(
                                 s =>
                                     new
                                     {
                                         CustomerId = s.CustomerId,
                                         Name = s.Name,
                                         LastName = s.Lastname
                                     }
                             )
                     .FirstOrDefaultAsync(f => f.CustomerId == paymentRelation.ReservationVehicles.CustomerId);
                CustomerName = employee.Name;
            }
            else if (paymentRelation.ReservationTourId != null)
            {
                var employee = await _context.Customers
                     .Select(
                                 s =>
                                     new
                                     {
                                         CustomerId = s.CustomerId,
                                         Name = s.Name,
                                         LastName = s.Lastname
                                     }
                             )
                     .FirstOrDefaultAsync(f => f.CustomerId == paymentRelation.ReservationTours.CustomerId);
                CustomerName = employee.Name;
            }
            await new BrowserFetcher().DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions { Headless = false }
            );
            var htmlContent =
                $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    "
                + $"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <link\r\n      rel=\"stylesheet\"\r\n     "
                + $" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css\"\r\n    />\r\n</head>\r\n<style>\r\n    "
                + $"#row-container {{\r\n      border: 1px solid #3f3e3e;\r\n    }}\r\n    hr {{\r\n      color: #000000;\r\n      height: 0px;\r\n     "
                + $" border: 1px dashed;\r\n    }}\r\n    strong{{\r\n        font-size: small;\r\n    }}\r\n    span{{\r\n        font-size: small;\r\n    }}\r\n    "
                + $"label{{\r\n        font-size: small;\r\n    }}\r\n    p{{\r\n        font-size: smaller;\r\n   }}\r\n  </style>\r\n<body>\r\n    <div class=\"container\">\r\n        "
                + $"<div class=\"container-fluid text-center\">\r\n\r\n            <div id=\"row-container\" class=\"container-fluid\">\r\n              "
                + $"<div class=\"row p-3\">\r\n                <div class=\"col\">\r\n                  <img src=\"https://scontent-hou1-1.xx.fbcdn.net/v/t39.30808-6/305960510_498834395582315_2152758621421830000_n.jpg?_nc_cat=109&ccb=1-7&_nc_sid=efb6e6&_nc_eui2=AeFcJyei8WU0bW99jvE9q0tqSdOF4FJuDVdJ04XgUm4NV12gyUbzk4ARWAl3uMTWc-_5_l3E4DxDLDaK-nI2QTHw&_nc_ohc=g9vg-s9k_GkAX-EKWU9&_nc_ht=scontent-hou1-1.xx&oh=00_AfBSJBalADZKybJwEYgO_nclSkut8WcooWXgfWmxs7KOqQ&oe=65AEAAA0\" alt=\"imagen\" width=\"100\" height=\"100\" />\r\n               "
                + $" </div>\r\n                <div class=\"col\">\r\n                  <div class=\"row\">\r\n                    <div class=\"col-12\">\r\n                      "
                + $"<strong> RECIBO PROVISIONAL </strong>\r\n                    </div>\r\n                    <div class=\"col-12\">\r\n                     "
                + $" <label> Arandas, Jalisco a: {string.Format("{0:dd-MM-yyyy}", payment.PaymentDate)}</label>\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n                "
                + $"<div class=\"col\">\r\n                  <label>FOLIO: <strong>{payment.Invoice}</strong></label>\r\n                  <div class=\"col-12\">\r\n                    "
                + $"<strong>COPIA CLIENTE</strong>\r\n                  </div>\r\n                </div>\r\n              </div>\r\n              <div class=\"row p-5 border m-2\">\r\n                "
                + $"<div class=\"col-12\">\r\n                  <div class=\"row\">\r\n                    <div class=\"col-4\">\r\n                      <strong>Recibi de:</strong>\r\n                   "
                + $" </div>\r\n                    <div class=\"col-8 border-bottom border-black\">\r\n                      <span>{CustomerName}</span>\r\n                    </div>\r\n                 "
                + $" </div>\r\n                </div>\r\n                <div class=\"col-12\">\r\n                  <div class=\"row\">\r\n                    <div class=\"col-4\">\r\n                     "
                + $" <strong>La cantidad de:</strong>\r\n                    </div>\r\n                    <div class=\"col-8 border-bottom border-black\">\r\n                      <span>{payment.Amount} MXN </span>\r\n"
                + $"                    </div>\r\n                  </div>\r\n                 <div class=\"row border-bottom border-black mt-4\"></div>\r\n                </div>\r\n                <div class=\"col-12\">\r\n"
                + $"                  <div class=\"row\">\r\n                    <div class=\"col-4\">\r\n                      <strong>Concepto:</strong>\r\n                    </div>\r\n                    "
                + $"<div class=\"col-8 border-bottom border-black\">\r\n                      <span></span>\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n"
                + $"                <div class=\"col-12\">\r\n                  <div class=\"row\">\r\n                    <div class=\"col-4\">\r\n                      <strong>Observaciones:</strong>\r\n"
                + $"                    </div>\r\n                    <div class=\"col-8 border-bottom border-black\">\r\n                      <span>{payment.Observations}</span>\r\n                    </div>\r\n"
                + $"                  </div>\r\n                  <div class=\"row border-bottom border-black mt-4\"></div>\r\n                </div>\r\n                <div class=\"col-12\">\r\n  "
                + $"                <div class=\"row\">\r\n                    <div class=\"col-4\">\r\n                      <strong>Metodo de pago:</strong>\r\n                    </div>\r\n   "
                + $"                 <div class=\"col-8 border-bottom border-black\">\r\n                      <span>{payment.PaymentMethods.Name}</span>\r\n                    </div>\r\n                  </div>\r\n    "
                + $"             <div class=\"row border-bottom border-black mt-4\"></div>\r\n                </div>\r\n                <div class=\"col-12\">\r\n                  <div class=\"row\">\r\n "
                + $"                   <div class=\"col-4\">\r\n                      <strong>Referencia de pago:</strong>\r\n                    </div>\r\n                    <div class=\"col-8 border-bottom border-black\">\r\n "
                + $"                     <span>{payment.DetailsPayment}</span>\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n              </div>\r\n              <div class=\"row m-5 text-center\">\r\n  "
                + $"              <div class=\"col-12\">\r\n                  <div class=\"col-12 border-top border-black\">\r\n                    <span>Firma de recibido</span>\r\n                  </div>\r\n                </div>\r\n "
                + $"             </div>\r\n              <div class=\"row text-center\">\r\n                <div class=\"col\">\r\n                    <p class=\"text-center\"> Álvaro Obregón esq. Hernández, Colonia Centro, Arandas, Jalisco. TEL/FAX: (348) 783-1657</p>\r\n "
                + $"               </div>\r\n              </div>\r\n              <div class=\"row text-center\">\r\n                <div class=\"col\">\r\n                    <p class=\"text-center\"> E-mail: arandasturismog@hotmail.com</p>\r\n                </div>\r\n  "
                + $"            </div>\r\n              <hr />\r\n              <div class=\"row p-3\">\r\n                <div class=\"col\">\r\n                  <img src=\"https://scontent-hou1-1.xx.fbcdn.net/v/t39.30808-6/305960510_498834395582315_2152758621421830000_n.jpg?_nc_cat=109&ccb=1-7&_nc_sid=efb6e6&_nc_eui2=AeFcJyei8WU0bW99jvE9q0tqSdOF4FJuDVdJ04XgUm4NV12gyUbzk4ARWAl3uMTWc-_5_l3E4DxDLDaK-nI2QTHw&_nc_ohc=g9vg-s9k_GkAX-EKWU9&_nc_ht=scontent-hou1-1.xx&oh=00_AfBSJBalADZKybJwEYgO_nclSkut8WcooWXgfWmxs7KOqQ&oe=65AEAAA0\" alt=\"imagen\" width=\"80\" height=\"80\" />\r\n               "
                + $" </div>\r\n                <div class=\"col\">\r\n                  <div class=\"row\">\r\n                    <div class=\"col-12\">\r\n                      <strong> RECIBO PROVISIONAL </strong>\r\n                    </div>\r\n "
                + $"                   <div class=\"col-12\">\r\n                      <label> Arandas, Jalisco a: {string.Format("{0:dd-MM-yyyy}", payment.PaymentDate)}</label>\r\n                    </div>\r\n                  </div>\r\n                </div>\r\n                <div class=\"col\">\r\n  "
                + $"                <label>FOLIO: <strong>{payment.Invoice}</strong></label>\r\n                  <div class=\"col-12\">\r\n                    <strong>COPIA COMERCIO</strong>\r\n                  </div>\r\n                </div>\r\n           "
                + $"   </div>\r\n              <div class=\"row p-5 border m-2\">\r\n                <div class=\"col-12\">\r\n                  <div class=\"row\">\r\n                    <div class=\"col-4\">\r\n                     "
                + $" <strong>Recibi de:</strong>\r\n                    </div>\r\n                    <div class=\"col-8 border-bottom border-black\">\r\n                      <span>{CustomerName}</span>\r\n                    </div>\r\n "
                + $"                 </div>\r\n                </div>\r\n                <div class=\"col-12\">\r\n                  <div class=\"row\">\r\n                    <div class=\"col-4\">\r\n                      <strong>La cantidad de:</strong>\r\n "
                + $"                   </div>\r\n                    <div class=\"col-8 border-bottom border-black\">\r\n                      <span>{payment.Amount} MXN </span>\r\n                    </div>\r\n                  </div>\r\n                "
                + $" <div class=\"row border-bottom border-black mt-4\"></div>\r\n                </div>\r\n                <div class=\"col-12\">\r\n                  <div class=\"row\">\r\n                    <div class=\"col-4\">\r\n   "
                + $"                   <strong>Concepto:</strong>\r\n                    </div>\r\n                    <div class=\"col-8 border-bottom border-black\">\r\n                      <span></span>\r\n                  "
                + $"  </div>\r\n                  </div>\r\n                </div>\r\n                <div class=\"col-12\">\r\n                  <div class=\"row\">\r\n                    <div class=\"col-4\">\r\n                      <strong>Observaciones:</strong>\r\n "
                + $"                   </div>\r\n                    <div class=\"col-8 border-bottom border-black\">\r\n                      <span>{payment.Observations}</span>\r\n                    </div>\r\n                  </div>\r\n                 "
                + $" <div class=\"row border-bottom border-black mt-4\"></div>\r\n                </div>\r\n                <div class=\"col-12\">\r\n                  <div class=\"row\">\r\n                    <div class=\"col-4\">\r\n  "
                + $"                    <strong>Metodo de pago:</strong>\r\n                    </div>\r\n                    <div class=\"col-8 border-bottom border-black\">\r\n                      <span>{payment.PaymentMethods.Name}</span>\r\n                    </div>\r\n  "
                + $"                </div>\r\n                 <div class=\"row border-bottom border-black mt-4\"></div>\r\n                </div>\r\n                <div class=\"col-12\">\r\n                  <div class=\"row\">\r\n                    <div class=\"col-4\">\r\n "
                + $"                     <strong>Referencia de pago:</strong>\r\n                    </div>\r\n                    <div class=\"col-8 border-bottom border-black\">\r\n                      <span>{payment.DetailsPayment}</span>\r\n                    </div>\r\n  "
                + $"                </div>\r\n                </div>\r\n              </div>\r\n              <div class=\"row m-5 text-center\">\r\n                <div class=\"col-12\">\r\n                  <div class=\"col-12 border-top border-black\">\r\n "
                + $"                   <span>Firma de recibido</span>\r\n                  </div>\r\n                </div>\r\n              </div>\r\n              <div class=\"row text-center\">\r\n                <div class=\"col\">\r\n                    "
                + $"<p class=\"text-center\"> Álvaro Obregón esq. Hernández, Colonia Centro, Arandas, Jalisco. TEL/FAX: (348) 783-1657</p>\r\n                </div>\r\n              </div>\r\n              <div class=\"row text-center\">\r\n                <div class=\"col\">\r\n "
                + $"                   <p class=\"text-center\"> E-mail: arandasturismog@hotmail.com</p>\r\n                </div>\r\n              </div>\r\n            </div>\r\n        </div>\r\n        \r\n        \r\n    </div>\r\n    \r\n</body>\r\n</html>";
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            await page.SetContentAsync(htmlContent);
            var pdfContent = await page.PdfStreamAsync(
                new PdfOptions
                {
                    Format = PaperFormat.Legal,
                    PrintBackground = true,
                    MarginOptions = new MarginOptions
                    {
                        Top = "10px",
                        Right = "0px",
                        Bottom = "0px",
                        Left = "0px"
                    }
                }
            );
            return File(pdfContent, "application/pdf", $"Pago_{payment.Invoice}.pdf");
        }
    }
}
