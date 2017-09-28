namespace CodeValue.ScrumBoard.Service.DTOs
{
    public class GetUserResponse
    {
        public UserDto User { get; set; }
        public string Error { get; internal set; }
    }
}
