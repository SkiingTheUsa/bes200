namespace LibraryApi.Models
{
    public class PostReservationRequest
    {
        public string For { get; set; }
        public string[] Books { get; set; }
    }
}
