using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;
using TurismoGlobalArandas.Models.Identity;

namespace TurismoGlobalArandas.Services
{
    public class NotificacionService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer? _timer = null;

        public NotificacionService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(
                async _ => await DoWorkAsync(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromHours(12)
            );

            return Task.CompletedTask;
        }

        private async Task DoWorkAsync()
        {
            try
            {
                // Lógica para obtener reservaciones cercanas a la fecha de pago
                var scope = _serviceProvider.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<TurismoGlobalContext>();
                var Reservations = await ObtenerReservacionesCercanas();

                // Generar notificaciones
                foreach (var reservacion in Reservations)
                {
                    var notificacion = new Notifications
                    {
                        Title =
                            $"La {reservacion.Origin} está proximo a vencer el pago del cliente.",
                        Message =
                            $"Te recordamos que el pago de la {reservacion.Origin} con el folio: {reservacion.Invoice} está próximo a vencer.",
                        Created = DateTime.UtcNow,  
                        IsReaded = false
                    };

                    context.Notifications.Add(notificacion);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }

        private async Task<List<ReservationsToExpire>> ObtenerReservacionesCercanas()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TurismoGlobalContext>();

                return await context.ReservationsToExpire
                    .FromSqlRaw("SELECT * FROM ReservationsToExpire")
                    .ToListAsync(); ;
            }
        }
    }
}
