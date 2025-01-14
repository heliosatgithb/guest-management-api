using GuestManagementApi.CQRS.Queries;
using GuestManagementApi.Data;
using GuestManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GuestManagementApi.Tests.Handlers
{
    [TestClass]
    public class GetAllGuestsQueryHandlerTests
    {
        private DataContext _context;
        private GetAllGuestsQueryHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "GuestTestDb")
                .Options;
            _context = new DataContext(options);

            _context.Guests.AddRange(new List<Guest>
            {
                new Guest { Id = System.Guid.NewGuid(), Firstname = "John", Lastname = "Cena", Email = "john.cena@example.com", PhoneNumbers = new List<string> { "123-456-7890" } },
                new Guest { Id = System.Guid.NewGuid(), Firstname = "Dwayne", Lastname = "Johnson", Email = "dwayne.johnson@example.com", PhoneNumbers = new List<string> { "987-654-3210" } }
            });
            _context.SaveChanges();

            _handler = new GetAllGuestsQueryHandler(_context);
        }

        [TestCleanup]
        public void TearDown()
        {
            // Clean up the in-memory database after each test
            _context.Database.EnsureDeleted();  // Deletes the in-memory database
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAllGuests_ShouldReturnAllGuests()
        {
            var query = new GetAllGuestsQuery();

            var result = await _handler.Handle(query);

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(g => g.Firstname == "John"));
            Assert.IsTrue(result.Any(g => g.Firstname == "Dwayne"));
        }
    }
}
