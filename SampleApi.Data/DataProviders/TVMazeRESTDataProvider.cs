using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using SampleApi.Common;
using SampleApi.Common.Data;
using SampleApi.Common.Entities;
using SampleApi.Common.Exceptions;
using SampleApi.Data.Entities;

namespace SampleApi.Data.DataProviders
{
    /// <summary>
    /// A REST data provider for TVMaze
    /// </summary>
    public class TVMazeRESTDataProvider : ITVMazeDataProvider
    {
        private const string baseUrl = "http://api.tvmaze.com";
        private const string userAgent = "SampleApi/1.0";
        private IRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeRESTDataProvider"/> class.
        /// </summary>
        public TVMazeRESTDataProvider()
        {
            this.restClient = new RestClient();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeRESTDataProvider"/> class.
        /// </summary>
        /// <param name="restClient">An implementation of IRestClient</param>
        public TVMazeRESTDataProvider(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        /// <summary>
        /// Gets one page of shows without castmembers
        /// </summary>
        /// <param name="page">The number of the page to get</param>
        /// <exception cref="PageNotFoundException">Thrown when the page number exceeds the last page</exception>
        /// <exception cref="DataSourceOverloadException">Thrown when the rate limiter of the service kicked in</exception>
        /// <returns>A List of Show objects</returns>
        public List<Show> GetShows(int page)
        {
            var shows = new List<Show>(250);

            this.restClient.BaseUrl = new System.Uri(baseUrl + "/shows?page=" + page.ToString(CultureInfo.InvariantCulture));

            var request = new RestRequest(Method.GET);

            request.AddHeader("User-Agent", userAgent);
            request.AddHeader("Cache-Control", "no-cache");

            IRestResponse response = this.restClient.Execute(request);

            switch ((int)response.StatusCode)
            {
                case 404:
                    throw new PageNotFoundException();
                case 429:
                    throw new DataSourceOverloadException();
                case 200:
                    // do nothing
                    break;
                default:
                    // better error handling required here
                    return null;
            }

            var showResponse = JsonConvert.DeserializeObject<IEnumerable<ShowResponse>>(response.Content);

            foreach (var s in showResponse)
            {
                var show = new Show();
                show.Id = s.Id;
                show.Name = s.Name;

                shows.Add(show);
            }

            return shows;
        }

        /// <summary>
        /// Gets the details of a single Show
        /// </summary>
        /// <param name="showId">The ID of the show</param>
        /// <returns>A Show object</returns>
        public Show GetShowDetails(int showId)
        {
            this.restClient.BaseUrl = new System.Uri(baseUrl + "/shows/" + showId.ToString(CultureInfo.InvariantCulture));

            var request = new RestRequest(Method.GET);

            request.AddHeader("User-Agent", userAgent);
            request.AddHeader("Cache-Control", "no-cache");

            IRestResponse response = this.restClient.Execute(request);

            switch ((int)response.StatusCode)
            {
                case 404:
                    return null;
                case 429:
                    throw new DataSourceOverloadException();
                case 200:
                    // do nothing
                    break;
                default:
                    // better error handling required here
                    return null;
            }

            var showResponse = JsonConvert.DeserializeObject<ShowResponse>(response.Content);

            var show = new Show();
            show.Id = showResponse.Id;
            show.Name = showResponse.Name;
            var sortedMembers = this.GetCastMembers(showId).OrderBy(m => m.Birthdate);
            show.Cast.AddRange(sortedMembers);

            return show;
        }

        /// <summary>
        /// Gets the cast members of a single Show
        /// </summary>
        /// <param name="showId">The ID of the show</param>
        /// <returns>A List of CastMembers</returns>
        public List<CastMember> GetCastMembers(int showId)
        {
            var castMembers = new List<CastMember>();

            this.restClient.BaseUrl = new System.Uri(baseUrl + "/shows/" + showId.ToString(CultureInfo.InvariantCulture) + "/cast");

            var request = new RestRequest(Method.GET);

            request.AddHeader("User-Agent", userAgent);
            request.AddHeader("Cache-Control", "no-cache");

            IRestResponse response = this.restClient.Execute(request);

            switch ((int)response.StatusCode)
            {
                case 404:
                    return null;
                case 429:
                    throw new DataSourceOverloadException();
                case 200:
                    // do nothing
                    break;
                default:
                    // better error handling required here
                    return null;
            }

            var castResponse = JsonConvert.DeserializeObject<IEnumerable<CastResponse>>(response.Content);

            foreach (var c in castResponse)
            {
                var member = new CastMember();
                member.Id = Convert.ToInt32(c.Person["id"].ToString(), CultureInfo.InvariantCulture);
                member.Name = c.Person["name"].ToString();

                if (c.Person["birthday"] != null)
                {
                    var birthDate = c.Person["birthday"].ToString();
                    DateTime parsedDate;

                    if (DateTime.TryParseExact(birthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                    {
                        member.Birthdate = parsedDate;
                    }
                }

                castMembers.Add(member);
            }

            return castMembers;
        }
    }
}
