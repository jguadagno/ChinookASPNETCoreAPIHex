using Chinook.MockData.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class AlbumRepositoryTest
    {
        private readonly AlbumRepository _repo;

        public AlbumRepositoryTest()
        {
            _repo = new AlbumRepository();
        }

        [Fact]
        public async Task AlbumGetAllAsync()
        {
            // Act
            var albums = await _repo.GetAllAsync();

            // Assert
            Assert.Equal(1, albums.Count);
        }
    }
}
