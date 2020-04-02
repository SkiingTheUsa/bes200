using LibraryApi.Controllers;
using LibraryApi.Domain;
using System;
using System.Threading.Tasks;
using RabbitMqUtils;

namespace LibraryApi.Services
{
    public class RabbitMqReservationProcessor : IWriteToReservationQueue
    {
        private readonly IRabbitManager _manager;

        public RabbitMqReservationProcessor(IRabbitManager manager)
        {
            _manager = manager;
        }

        public async Task Write(Reservation reservation)
        {
            _manager.Publish(reservation, "", "direct", "Reservations");
        }
    }
}
