using GuestManagementApi.CQRS.Interfaces;
using GuestManagementApi.Data;

namespace GuestManagementApi.CQRS.Commands;
public class AddGuestCommandHandler(DataContext context, ILogger<AddGuestCommandHandler> logger) : ICommandHandler<AddGuestCommand>
{
    private readonly DataContext _context = context;
    private readonly ILogger<AddGuestCommandHandler> _logger = logger;

    public async Task Handle(AddGuestCommand command)
    {
        // Basic validation before adding the guest
        if (string.IsNullOrWhiteSpace(command.Guest.Firstname) &&
            string.IsNullOrWhiteSpace(command.Guest.Lastname))
        {
            _logger.LogWarning("AddGuestCommand: Firstname or Lastname must be provided.");
            throw new ArgumentException("At least one name (Firstname or Lastname) must be provided.");
        }

        if (!command.Guest.PhoneNumbers.Any())
        {
            _logger.LogWarning("AddGuestCommand: At least one phone number must be provided.");
            throw new ArgumentException("At least one phone number must be provided.");
        }

        var guestExists = _context.Guests.Any(g => g.Email == command.Guest.Email);
        if (guestExists)
        {
            _logger.LogWarning("AddGuestCommand: Guest with same email already exist");
            throw new ArgumentException("Guest with same email already exist");
        }

        command.Guest.Id = Guid.NewGuid();
        _context.Guests.Add(command.Guest);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Successfully added guest with email {Email}", command.Guest.Email);
    }
}
