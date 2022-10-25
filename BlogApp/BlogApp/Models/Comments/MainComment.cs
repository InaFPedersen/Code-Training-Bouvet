namespace BlogApp.Models.Comments
{
    public class MainComment : Comments
    {
        public List<SubComment> SubComments { get; set; }
    }
}
