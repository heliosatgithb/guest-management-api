using GuestManagementApi.CQRS.Interfaces;
using GuestManagementApi.Data;

namespace GuestManagementApi.CQRS.Commands;
public class AddPhoneCommandHandler(DataContext context, ILogger<AddPhoneCommandHandler> logger) : ICommandHandler<AddPhoneCommand>
{
    private readonly DataContext _context = context;
    private readonly ILogger<AddPhoneCommandHandler> _logger = logger;

    public async Task Handle(AddPhoneCommand command)
    {
        var guest = await _context.Guests.FindAsync(command.GuestId);

        if (guest == null)
        {
            _logger.LogWarning("AddPhoneCommandHandler: Guest not found");
            throw new ArgumentException("Guest not found.");
        }

        if (guest.PhoneNumbers.Contains(command.PhoneNumber))
        {
            _logger.LogWarning("AddPhoneCommandHandler: Phone number already exists for this guest.");
            throw new ArgumentException("Phone number already exists for this guest.");
        }

        guest.PhoneNumbers.Add(command.PhoneNumber);
        await _context.SaveChangesAsync();

        _logger.LogWarning("Successfully added phone number {PhoneNumber}", command.PhoneNumber);
    }
}