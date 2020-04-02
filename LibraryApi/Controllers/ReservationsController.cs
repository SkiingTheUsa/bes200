using LibraryApi.Domain;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        [HttpPost("/reservations/approved")]
        public async Task<ActionResult> ApprovedReservation([FromBody] Reservation reservation)
        {
            var storedReservation = await _context.Reservations.SingleOrDefaultAsync(r => r.Id == reservation.Id);
            if (storedReservation == null)
            {
                return BadRequest("No Pending Reservation with that id");
                // decision time!
            }
            else
            {
                storedReservation.Status = ReservationStatus.Approved;
                // do other stuff, or write a message to the queue that other processes will handle.
                await _context.SaveChangesAsync();
                return Accepted();
            }
        }

        [HttpPost("/reservations/cancelled")]
        public async Task<ActionResult> CancelledReservation([FromBody] Reservation reservation)
        {
            var storedReservation = await _context.Reservations.SingleOrDefaultAsync(r => r.Id == reservation.Id);
            if (storedReservation == null)
            {
                return BadRequest("No Pending Reservation with that id");
                // decision time!
            }
            else
            {
                storedReservation.Status = ReservationStatus.Cancelled;
                // do other stuff, or write a message to the queue that other processes will handle.
                await _context.SaveChangesAsync();
                return Accepted();
            }
        }

        [HttpGet("/reservations/approved")]
        public async Task<ActionResult> GetApprovedReservations()
        {
            var response = await _context.Reservations
                .Where(r => r.Status == ReservationStatus.Approved)
                .ToListAsync();
            // TODO: Project these into models. This is not a great way to do it. Classroom only.
            return Ok(response);
        }
        [HttpGet("/reservations/cancelled")]
        public async Task<ActionResult> GetCancelledReservations()
        {
            var response = await _context.Reservations
                .Where(r => r.Status == ReservationStatus.Cancelled)
                .ToListAsync();
            // TODO: Project these into models. This is not a great way to do it. Classroom only.
            return Ok(response);
        }
        [HttpGet("/reservations/pending")]
        public async Task<ActionResult> GetPendingReservations()
        {
            var response = await _context.Reservations
                .Where(r => r.Status == ReservationStatus.Pending)
                .ToListAsync();
            // TODO: Project these into models. This is not a great way to do it. Classroom only.
            return Ok(response);
        }
    }
}
