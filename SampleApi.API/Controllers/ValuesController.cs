using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SampleApi.BLL.Managers;
using SampleApi.Common.Entities;
using SampleApi.Common.Exceptions;
using SampleApi.Common.Logic;
using SampleApi.Data.DataProviders;

namespace SampleApi.API.Controllers
{
    /// <summary>
    /// Endpoints for getting information on a show
    /// </summary>
    [Route("api/values")]
    [Produces("application/json")]
    public class ValuesController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesController"/> class.
        /// </summary>
        public ValuesController()
        {
        }

        /// <summary>
        /// Gets values
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/values
        ///
        /// </remarks>
        /// <returns>A collection of Shows</returns>
        /// <response code="200">Returns the collection of shows</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            return this.Ok("Foobar");
        }
    }
}
