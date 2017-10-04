using System.Threading.Tasks;
using UPCItemDb;
using UPCItemDb.Requests;
using Xunit;

namespace UPCItemDB.Tests
{
    public class UPCItemDbTests
    {
        private readonly UPCItemDBClient _upcItemDbClient;
        public UPCItemDbTests()
        {
            _upcItemDbClient = new UPCItemDBClient();
        }

        [Fact]
        public async Task Get_Item_By_Get()
        {
            var items = await _upcItemDbClient.LookupByGetAsync("885909456017", "674785680773");

            Assert.NotNull(items);
        }

        [Fact]
        public async Task Get_Item_By_Post()
        {
            var items = await _upcItemDbClient.LookupByPostAsync("885909456017", "674785680773");

            Assert.NotNull(items);
        }

        [Fact]
        public async Task Search_Items_By_Keyword_By_Get()
        {
            var items = await _upcItemDbClient.SearchByGetAsync("iphone");

            Assert.NotNull(items);
        }

        [Fact]
        public async Task Search_Items_By_Brand_By_Get()
        {
            var items = await _upcItemDbClient.SearchByGetAsync(new SearchParameters("tablet")
            {
                Brand = "nokia"
            });

            Assert.NotNull(items);
        }

        [Fact]
        public async Task Search_Items_By_Keyword_By_Post()
        {
            var items = await _upcItemDbClient.SearchByPostAsync("iphone");

            Assert.NotNull(items);
        }

        [Fact]
        public async Task Search_Items_By_Brand_By_Post()
        {
            var items = await _upcItemDbClient.SearchByPostAsync(new SearchParameters("iphone")
            {
                Brand = "Apple"
            });

            Assert.NotNull(items);
        }
    }
}
