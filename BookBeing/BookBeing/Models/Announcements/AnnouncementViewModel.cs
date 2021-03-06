
namespace BookBeing.Models.Announcements
{
    public class AnnouncementViewModel
    {
        public int Id { get; init; }
        public string Text { get; init; }
        public int LibraryId { get; set; }
        public string LibraryName { get; set; }
        public string LibraryZipCode { get; set; }
        public string LibraryCity { get; set; }
        public string LibraryAddress { get; set; }
        public string LibraryPhoneNumber { get; set; }
        public string LibraryEmail { get; set; }
        public bool IsByUser { get; set; }
    }
}
