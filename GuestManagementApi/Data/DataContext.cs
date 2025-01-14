using Microsoft.EntityFrameworkCore;
using GuestManagementApi.Models;

namespace GuestManagementApi.Data;
public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Guest> Guests { get; set; } = null!;
}