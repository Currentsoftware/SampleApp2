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
        /// <param name="id">The id of the show</param>
        /// <returns>An IActionResult</returns>
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
                return this.Ok(members);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);

                return this.StatusCode(500);
            }
        }
    }
}
