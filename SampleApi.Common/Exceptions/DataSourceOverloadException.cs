using System;

namespace SampleApi.Common.Exceptions
{
    /// <summary>
    /// Thrown when the data source is getting too many requests
    /// </summary>
    public class DataSourceOverloadException : Exception
    {
    }
}
