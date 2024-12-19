
namespace DataTransferObject.ResponseDto
{
    public class ResponseTeam
    {
        public int TeamId { get; set; }
        public string? Name { get; set; }
        public List<ResponseUser>? AppUsers { get; set; }
        //  public bool IsActive { get; set; } = true;
        public List<ResponseProject>? Projects { get; set; }
    }
}
