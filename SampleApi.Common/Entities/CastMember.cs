using System;
using SampleApi.Common.Contracts;

namespace SampleApi.Common.Entities
{
    /// <summary>
    /// A DTO that holds cast member information
    /// </summary>
    public class CastMember
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the birthdate
        /// </summary>
        public DateTime? Birthdate { get; set; }
    }
}
