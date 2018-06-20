using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SampleApi.BLL.Managers;
using SampleApi.Common.Exceptions;
using SampleApi.Common.Logic;
using SampleApi.Data.DataProviders;

namespace SampleApi.API.Controllers
{
    /// <summary>
    /// Endpoints for getting information on a show
    /// </summary>
    [Route("api/shows")]
    [Produces("application/json")]
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
        /// Gets a paged collection of shows containing its castmembers
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/shows?page=9
        ///
        /// </remarks>
        /// <param name="page">The pagenumber to get starting from 0. Shows are paged by ID. Page 0 contains the IDs from 0 till 250, page 1 from 251 till 500, etcetera.</param>
        /// <returns>A collection of Shows</returns>
        /// <response code="200">Returns the collection of shows</response>
        /// <response code="400">If the page number is smaller than 0</response>
        /// <response code="404">If the page number is larger than the last page</response>
        /// <response code="500">If an unkown error occurs</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
            catch (PageNotFoundException)
            {
                return this.NotFound();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);

                return this.StatusCode(500, e.Message + e.StackTrace);
            }
        }
    }
}
