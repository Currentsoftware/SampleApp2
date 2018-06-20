using System.Collections.Generic;
using SampleApi.Common.Entities;

namespace SampleApi.Common.Logic
{
    /// <summary>
    /// Describes the interface to a ShowManager
    /// </summary>
    public interface IShowManager
    {
        /// <summary>
        /// Gets a page of Show objects from the data source
        /// </summary>
        /// <param name="pageId">The id of the page to get</param>
        /// <returns>A collection of Show objects</returns>
        IEnumerable<Show> GetShows(int pageId);

        /// <summary>
        /// Gets a collection of CastMembers for a show
        /// </summary>
        /// <param name="showId">The id of the show to get the castmembers for</param>
        /// <returns>A collection of CastMember objects</returns>
        IEnumerable<CastMember> GetCastMembers(int showId);
    }
}
