using GuestManagementApi.CQRS.Interfaces;
using GuestManagementApi.Data;
using GuestManagementApi.Models;

namespace GuestManagementApi.CQRS.Queries;

public class GetGuestByIdQueryHandler(DataContext context) : IQueryHandler<GetGuestByIdQuery, Guest>
{
    private readonly DataContext _context = context;

    public async Task<Guest> Handle(GetGuestByIdQuery query)
    {
        var guest = await _context.Guests.FindAsync(query.GuestId) ?? 
                    throw new ArgumentException("Guest not found.");
        return guest;
    }
}