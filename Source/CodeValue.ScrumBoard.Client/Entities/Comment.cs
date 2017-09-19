namespace CodeValue.ScrumBoard.Service.Entities
{
    public class Comment : Entity
    {
        public string Content { get; set; }

        public string CommentedBy { get; set; }
    }
}