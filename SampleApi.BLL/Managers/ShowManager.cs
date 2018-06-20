using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using SampleApi.Common.Data;
using SampleApi.Common.Entities;
using SampleApi.Common.Exceptions;
using SampleApi.Common.Logic;

namespace SampleApi.BLL.Managers
{
    /// <summary>
    /// A service object to retrieve data from the backend
    /// </summary>
    public class ShowManager : IShowManager
    {
        // use same paging strategy
        // get all cast members, in parallel, watch time
        // not a performant solution by design
        private ITVMazeDataProvider dataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowManager"/> class.
        /// </summary>
        /// <param name="dataProvider">An implementation of ITVMazeDataProvider</param>
        public ShowManager(ITVMazeDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        /// <summary>
        /// Gets a page of Show objects from the data source
        /// </summary>
        /// <param name="pageId">The id of the page to get</param>
        /// <returns>A collection containing no more than 250 objects</returns>
        public IEnumerable<Show> GetShows(int pageId)
        {
            var shows = this.dataProvider.GetShows(pageId);

            var timer = Stopwatch.StartNew();

            foreach (var show in shows)
            {
                LoopStart:
                try
                {
                    this.GetCastMembersForShow(show);
                    if (timer.ElapsedMilliseconds > 10000)
                    {
                        timer.Restart();
                    }
                }
                catch (DataSourceOverloadException ex)
                {
                    Trace.WriteLine(ex);

                    // rudimentary synchronous solution
                    var duration = (int)(10000 - timer.ElapsedMilliseconds + 1);
                    Thread.Sleep(duration);

                    timer.Reset();

                    // wow, first time use ever in c#
                    goto LoopStart;
                }
            }

            return shows;
        }

        /// <summary>
        /// Gets a page of Show objects from the data source
        /// </summary>
        /// <param name="showId">The id of the show to get</param>
        /// <returns>A collection containing no more than 250 objects</returns>
        public Show GetShow(int showId)
        {
            var show = this.dataProvider.GetShowDetails(showId);
            return show;
        }

        /// <summary>
        /// Returns a collection of CastMembers for a given show
        /// </summary>
        /// <param name="showId">The id of the show</param>
        /// <returns>A collection of CastMembers</returns>
        public IEnumerable<CastMember> GetCastMembers(int showId)
        {
            var members = this.dataProvider.GetCastMembers(showId);
            return members;
        }

        private void GetCastMembersForShow(Show show)
        {
            var castMembers = this.GetCastMembers(show.Id);
            var sorted = castMembers.OrderBy(c => c.Birthdate).ToList();
            show.Cast.AddRange(sorted);
        }
    }
}
