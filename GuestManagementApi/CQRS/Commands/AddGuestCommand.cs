using GuestManagementApi.CQRS.Interfaces;
using GuestManagementApi.Models;

namespace GuestManagementApi.CQRS.Commands;

public class AddGuestCommand : ICommand
{
    public Guest Guest { get; set; } = null!;
}
