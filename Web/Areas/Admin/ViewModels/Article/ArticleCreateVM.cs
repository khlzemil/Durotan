namespace Web.Areas.Admin.ViewModels.Article
{
    public class ArticleCreateVM
    {
        public int Id { get; set; }
        public DateTime ArticleDate { get; set; }
        public string Title { get; set; }
        public string ArticleName { get; set; }
        public string Author { get; set; }
        public IFormFile ArticlePhoto { get; set; }

    }
}
