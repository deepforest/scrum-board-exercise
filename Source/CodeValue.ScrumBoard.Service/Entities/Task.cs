namespace CodeValue.ScrumBoard.Service.Entities
{
    public enum TaskStatus
    {
        Todo = 10,
        Doing = 20,
        Done = 30
    }

    public class Task : Entity
    {
        public string CreatedBy { get; set; }
        public string AssignedTo { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public int Priority { get; set; }
        public int RemainingWork { get; set; }
        public Comment[] Comments { get; set; }
    }
}