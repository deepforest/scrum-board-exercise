namespace CodeValue.ScrumBoard.Service.Entities
{
    public class Board : Entity
    {
        public string CreatedBy { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string[] TeamMembers { get; set; }
    }
}