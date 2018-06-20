using SampleApi.Data.DataProviders;
using Xunit;

namespace SampleApi.Data.Tests.DataProviders
{
    public static class TVMazeRESTDataProviderTests
    {
        [Fact]
        public static void GetShowDetailsShouldReturnShow()
        {
            var dataProvider = new TVMazeRESTDataProvider();

            var show = dataProvider.GetShowDetails(1);

            Assert.NotNull(show);
        }

        [Fact]
        public static void GetCastDetailsShouldReturnShow()
        {
            var dataProvider = new TVMazeRESTDataProvider();

            var members = dataProvider.GetCastMembers(1);

            Assert.NotNull(members);
        }
    }
}
