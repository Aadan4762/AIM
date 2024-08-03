using AIM.Models.Entities;

namespace AIM.Dtos.SchoolDtos
{
    public class SchoolResponse
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public School School { get; set; }
    }
}