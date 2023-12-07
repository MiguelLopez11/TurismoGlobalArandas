//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.EntityFrameworkCore;
//using TurismoGlobalArandas.Context;
//using TurismoGlobalArandas.Models;

//namespace TurismoGlobalArandas.Services
//{
//    public class NotificacionService : IHostedService, IDisposable
//    {
//        private readonly IServiceProvider _serviceProvider;
//        private Timer? _timer = null;
//        public NotificacionService(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }
//        public Task StartAsync(CancellationToken stoppingToken)
//        {

//            _timer = new Timer(async _ => await DoWorkAsync(), null, TimeSpan.Zero, TimeSpan.FromDays(1));

//            return Task.CompletedTask;
//        }
//        private async Task DoWorkAsync()
//        {
//            try
//            {
//                // Lógica para obtener reservaciones cercanas a la fecha de pago
//                var scope = _serviceProvider.CreateScope();
               
//                    var context = scope.ServiceProvider.GetRequiredService<TurismoGlobalContext>();
//                    var Reservations = await ObtenerReservacionesCercanas();

//                    // Generar notificaciones
//                    foreach (var reservacion in Reservations)
//                    {
//                        TimeSpan? DifferenceDays = reservacion.PaymentLimitDate - DateTime.UtcNow;
//                        int DaysFailing = (int)DifferenceDays?.TotalDays;

//                        var notificacion = new Notifications
//                        {
//                            Title = "Próxima fecha de pago: " + reservacion.PaymentLimitDate,
//                            Message = $"Te recordamos que el pago de la reservación con el folio: {reservacion.Invoice} está próximo a vencer.",
//                            Created = DateTime.UtcNow,
//                            IsReaded = false
//                        };

//                        context.Notifications.Add(notificacion);
//                        await context.SaveChangesAsync();
//                    }
                
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }
//        }
//        public Task StopAsync(CancellationToken stoppingToken)
//        {
//            _timer?.Change(Timeout.Infinite, 0);

//            return Task.CompletedTask;
//        }

//        public void Dispose()
//        {
//            _timer?.Dispose();
//            GC.SuppressFinalize(this);
//        }

//        private async Task<List<ReservationHotel>> ObtenerReservacionesCercanas()
//        {
//            var scope = _serviceProvider.CreateScope();
            
//                var context = scope.ServiceProvider.GetRequiredService<TurismoGlobalContext>();
//                var fechaLimite = DateTime.UtcNow.AddDays(3);
//                return await context.ReservationHotels
//                    .Where(r => r.PaymentLimitDate.HasValue && r.PaymentLimitDate <= fechaLimite)
//                    .ToListAsync();
            

//        }
//    }

//}
