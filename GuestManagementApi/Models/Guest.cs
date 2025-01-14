using GuestManagementApi.Models.Enums;

namespace GuestManagementApi.Models;

public class Guest
{
    public Guid Id { get; set; }
    public Title Title { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public DateTime BirthDate { get; set; }
    public required string Email { get; set; }
    public List<string> PhoneNumbers { get; set; } = [];
}
