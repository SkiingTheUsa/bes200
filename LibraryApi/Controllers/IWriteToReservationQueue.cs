using System.Threading.Tasks;
using LibraryApi.Domain;

namespace LibraryApi.Controllers
{
    public interface IWriteToReservationQueue
    {
        Task Write(Reservation reservation);
    }
}