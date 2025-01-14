using GuestManagementApi.CQRS.Interfaces;
using GuestManagementApi.Data;
using GuestManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GuestManagementApi.CQRS.Queries;

public class GetAllGuestsQueryHandler(DataContext context) : IQueryHandler<GetAllGuestsQuery, List<Guest>>
{
    private readonly DataContext _context = context;

    public async Task<List<Guest>> Handle(GetAllGuestsQuery query)
    {
        return await _context.Guests.ToListAsync();
    }
}