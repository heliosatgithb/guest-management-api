using GuestManagementApi.CQRS.Commands;
using GuestManagementApi.Data;
using GuestManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace GuestManagementApi.Tests.Handlers
{
    [TestClass]
    public class AddGuestCommandHandlerTests
    {
        private DataContext _context = null!;
        private AddGuestCommandHandler _handler = null!;
        private Mock<ILogger<AddGuestCommandHandler>> _mockLogger = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "GuestTestDb")
                .Options;
            _context = new DataContext(options);
            _mockLogger = new Mock<ILogger<AddGuestCommandHandler>>();
            _handler = new AddGuestCommandHandler(_context, _mockLogger.Object);
        }

        [TestCleanup]
        public void TearDown()
        {
            // Clean up the in-memory database after each test
            _context.Database.EnsureDeleted();  // Deletes the in-memory database
            _context.Dispose();  
        }


        [TestMethod]
        public async Task AddGuest_ValidCommand_ShouldAddGuest()
        {
            var newGuest = new Guest
            {
                Firstname = "Suraj",
                Lastname = "Naik",
                Email = "suraj.naik@example.com",
                BirthDate = new System.DateTime(1982, 10, 11),
                PhoneNumbers = new System.Collections.Generic.List<string> { "123-456-7890" }
            };

            var command = new AddGuestCommand { Guest = newGuest };

            await _handler.Handle(command);

            Assert.AreEqual(1, _context.Guests.CountAsync().Result);
            var guest = await _context.Guests.FirstAsync();
            Assert.AreEqual("Suraj", guest.Firstname);
            Assert.AreEqual("123-456-7890", guest.PhoneNumbers[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public async Task AddGuest_InvalidCommand_ShouldThrowException()
        {
            var newGuest = new Guest
            {
                Firstname = "",
                Lastname = "",
                Email = "",
                BirthDate = new System.DateTime(1990, 1, 1),
                PhoneNumbers = []
            };
            
            var command = new AddGuestCommand { Guest = newGuest };

            await _handler.Handle(command);
        }
    }
}
