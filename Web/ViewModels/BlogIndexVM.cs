using Core.Entities;

namespace Web.ViewModels
{
    public class BlogIndexVM
    {
        public List<OurJournal> OurJournals { get; set; }
        public List<Article> Articles { get; set; }
        public List<Article> TwoArticles { get; set; }

        public int Page { get; set; } = 1;

        public int Take { get; set; } = 5;

        public int PageCount { get; set; }
    }
}
