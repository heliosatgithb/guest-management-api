using GuestManagementApi.CQRS.Interfaces;
using GuestManagementApi.Models;

namespace GuestManagementApi.CQRS.Queries
{
    public class GetAllGuestsQuery : IQuery<List<Guest>> { }
}
