using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SampleApi.Data.Entities
{
    /// <summary>
    /// A DTO to deserialize the response from TVMaze into
    /// </summary>
    public class ShowResponse
    {
        /// <summary>
        /// Gets or sets the serialization data
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "We need the setter here for serialization purposes")]
        [JsonExtensionData]
        public IDictionary<string, JToken> AdditionalData { get; set; }

        /// <summary>
        /// Gets or sets the Id of the show
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the show
        /// </summary>
        public string Name { get; set; }
    }
}
