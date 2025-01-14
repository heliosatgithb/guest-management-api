using GuestManagementApi.Models;
using GuestManagementApi.CQRS.Interfaces;

namespace GuestManagementApi.CQRS.Queries;

public class GetGuestByIdQuery : IQuery<Guest>
{
    public Guid GuestId { get; set; }
}
