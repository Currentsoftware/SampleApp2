using System.Collections.Generic;
using SampleApi.Common.Entities;

namespace SampleApi.Common.Contracts
{
    /// <summary>
    /// A baseclass for API contracts
    /// </summary>
    public class ContractBase
    {
        private List<Link> links = new List<Link>();

        /// <summary>
        /// Gets the links collection
        /// </summary>
        public List<Link> Links
        {
            get
            {
                return this.links;
            }
         }
    }
}
