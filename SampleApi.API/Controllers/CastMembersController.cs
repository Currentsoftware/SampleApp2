using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SampleApi.BLL.Managers;
using SampleApi.Common.Logic;
using SampleApi.Data.DataProviders;

namespace SampleApi.API.Controllers
{
    /// <summary>
    /// Endpoints for getting CastMembers for a show
    /// </summary>
    [Route("api/shows/{id}/castmembers")]
    [Produces("application/json")]
    public class CastMembersController : Controller
    {
        private IShowManager manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CastMembersController"/> class.
        /// </summary>
        public CastMembersController()
        {
            // skip the DI here for now
            this.manager = new ShowManager(new TVMazeRESTDataProvider());
        }

        /// <summary>
        /// Gets the castmemebers for a given show
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/shows/1/castmembers
        ///
        /// </remarks>
        /// <param name="id">The id of the show</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the collection of shows</response>
        /// <response code="400">If the id is smaller than 0</response>
        /// <response code="404">If the show is not found</response>
        /// <response code="500">If an unkown error occurs</response>
        [HttpGet]
        public IActionResult GetCastMembers(int id)
        {
            if (id < 0)
            {
                return this.BadRequest();
            }

            try
            {
                var members = this.manager.GetCastMembers(id);

                if (members == null)
                {
                    return this.NotFound();
                }
                else
                {
                    return this.Ok(members);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);

                return this.StatusCode(500);
            }
        }
    }
}
