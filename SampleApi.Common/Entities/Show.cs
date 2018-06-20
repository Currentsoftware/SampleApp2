using System.Collections.Generic;
using SampleApi.Common.Contracts;

namespace SampleApi.Common.Entities
{
    /// <summary>
    /// A DTO that holds show information
    /// </summary>
    public class Show : ContractBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Show"/> class.
        /// </summary>
        public Show()
        {
            this.Cast = new List<CastMember>();
        }

        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the cast
        /// </summary>
        public List<CastMember> Cast { get; }
    }
}
