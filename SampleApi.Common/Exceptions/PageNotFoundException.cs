using System;

namespace SampleApi.Common.Exceptions
{
    /// <summary>
    /// Thrown when the last page of the collection has been exceeded
    /// </summary>
    public class PageNotFoundException : Exception
    {
    }
}
