using GuestManagementApi.CQRS.Commands;
using GuestManagementApi.CQRS.Queries;
using GuestManagementApi.CQRS.Interfaces;
using GuestManagementApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuestManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestsController : ControllerBase
{
    private readonly ICommandHandler<AddGuestCommand> _addGuestHandler;
    private readonly ICommandHandler<AddPhoneCommand> _addPhoneHandler;
    private readonly IQueryHandler<GetAllGuestsQuery, List<Guest>> _getAllGuestsHandler;
    private readonly IQueryHandler<GetGuestByIdQuery, Guest> _getGuestByIdHandler;

    public GuestsController(ICommandHandler<AddGuestCommand> addGuestHandler,
                            ICommandHandler<AddPhoneCommand> addPhoneHandler,
                            IQueryHandler<GetAllGuestsQuery, List<Guest>> getAllGuestsHandler,
                            IQueryHandler<GetGuestByIdQuery, Guest> getGuestByIdHandler)
    {
        _addGuestHandler = addGuestHandler;
        _addPhoneHandler = addPhoneHandler;
        _getAllGuestsHandler = getAllGuestsHandler;
        _getGuestByIdHandler = getGuestByIdHandler;
    }

    [HttpPost("AddGuest")]
    public async Task<IActionResult> AddGuest([FromBody] Guest guest)
    {
        var command = new AddGuestCommand { Guest = guest };
        await _addGuestHandler.Handle(command);
        return Ok(guest);
    }

    [HttpPost("{guestId}/AddPhone")]
    public async Task<IActionResult> AddPhone(Guid guestId, [FromBody] string phoneNumber)
    {
        var command = new AddPhoneCommand { GuestId = guestId, PhoneNumber = phoneNumber };
        await _addPhoneHandler.Handle(command);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGuests()
    {
        var query = new GetAllGuestsQuery();
        var result = await _getAllGuestsHandler.Handle(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGuestById(Guid id)
    {
        var query = new GetGuestByIdQuery { GuestId = id };
        var result = await _getGuestByIdHandler.Handle(query);
        return Ok(result);
    }
}
