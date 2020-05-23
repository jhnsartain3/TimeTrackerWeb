namespace TimeTrackerWeb.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string UserFriendlyMessage { get; set; }
        public bool ShowUserFriendlyMessage => !string.IsNullOrEmpty(UserFriendlyMessage);

        public string InnerException { get; set; }
        public bool ShowInnerException => !string.IsNullOrEmpty(InnerException);

        public string StackTrace { get; set; }
        public bool ShowStackTrace => !string.IsNullOrEmpty(StackTrace);

        public string Path { get; set; }
        public bool ShowPath => !string.IsNullOrEmpty(Path);

        public string HttpMethod { get; set; }
        public bool ShowHttpMethod => !string.IsNullOrEmpty(HttpMethod);
    }
}