namespace SampleApi.Common.Entities
{
    /// <summary>
    /// A DTO for a Link to another resource
    /// </summary>
    public class Link
    {
        /// <summary>
        /// Gets or sets the href of the link
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the rel of the link
        /// </summary>
        public string Rel { get; set; }

        /// <summary>
        /// Gets or sets the method
        /// </summary>
        public string Method { get; set; }
    }
}
