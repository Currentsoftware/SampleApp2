using Microsoft.AspNetCore.Mvc;
using SampleApi.BLL.Managers;
using SampleApi.Common.Exceptions;
using SampleApi.Common.Logic;
using SampleApi.Data.DataProviders;
using System;
using System.Diagnostics;

namespace SampleApi.API.Controllers
{
    /// <summary>
    /// Endpoints for getting information on a show
    /// </summary>
    [Route("api/shows")]
    public class ShowsController : Controller
    {
        private IShowManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowsController"/> class.
        /// </summary>
        public ShowsController()
        {
            // skip the DI here for now
            this.manager = new ShowManager(new TVMazeRESTDataProvider());
        }

        /// <summary>
        /// Gets a collection of shows containing its castmembers
        /// </summary>
        /// <param name="page">The pagenumber to get</param>
        /// <returns>A collection of Shows</returns>
        [HttpGet]
        public IActionResult GetShows(int page)
        {
            if (page < 0)
            {
                return this.BadRequest();
            }

            try
            {
                var timer = Stopwatch.StartNew();

                var members = this.manager.GetShows(page);

                var duration = timer.ElapsedMilliseconds;
                return this.Ok(members);
            }
            catch(PageNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);

                return this.StatusCode(500);
            }
        }
    }
}
