using System;

namespace DrWatson.Adfs.Metadata
{
    class MetadataServiceException
        : Exception
    {
        public enum ErrorCode
        {
            NotReady
        }

        public ErrorCode Code { get; private set; }

        public MetadataServiceException(ErrorCode code)
            : this(code, null)
        {
        }

        public MetadataServiceException(ErrorCode code, string message)
            : this(code, message, null)
        {
        }

        public MetadataServiceException(ErrorCode code, string message, Exception innerException)
            : base($"{code.ToString()}{ (string.IsNullOrWhiteSpace(message) ? "" : ": " + message) }", innerException)
        {
            Code = code;
        }
    }
}