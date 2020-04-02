using LibraryApi.Domain;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly LibraryDataContext _context;
        private readonly IWriteToReservationQueue _reservationQueue;

        public ReservationsController(LibraryDataContext context, IWriteToReservationQueue reservationQueue)
        {
            _context = context;
            _reservationQueue = reservationQueue;
        }

        [HttpPost("/reservations")]
        public async Task<ActionResult> AddReservation([FromBody] PostReservationRequest request)
        {
            // validate
            // add it to the database
            var reservation = new Reservation
            {
                For = request.For,
                Books = string.Join(',', request.Books),
                ReservationCreated = DateTime.Now,
                Status = ReservationStatus.Pending
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            // write a message to the queue
            // TODO: RabbitMQ
            // Write the code you wish you had.
            await _reservationQueue.Write(reservation);
            // return a response (201)
            return Ok(reservation);
        }
    }
}
