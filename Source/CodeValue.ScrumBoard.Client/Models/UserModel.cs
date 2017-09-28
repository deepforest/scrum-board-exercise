namespace CodeValue.ScrumBoard.Client.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string Token { get; internal set; }
    }
}
