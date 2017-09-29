using System.Collections.Generic;

namespace CodeValue.ScrumBoard.Service.DTOs
{
    public class GetUsersResponse
    {
        public int Count { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
        public string Error { get; internal set; }
    }
}
