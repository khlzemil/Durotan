namespace Web.Areas.Admin.ViewModels.OurJournal
{
    public class OurJournalCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BlogName { get; set; }
        public DateTime BlogDate { get; set; }
        public IFormFile JournalPhoto { get; set; }
    }
}
