using GuestManagementApi.CQRS.Interfaces;

namespace GuestManagementApi.CQRS.Commands;
public class AddPhoneCommand : ICommand
{
    public Guid GuestId { get; set; }
    public string PhoneNumber { get; set; } = null!;
}