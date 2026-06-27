namespace Matger.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Massege { get; set; }

        public ApiResponse(int statusCode, string? massege = null)
        {
            StatusCode = statusCode;
            Massege = massege ?? GetDefaultMassegeForStatusCode(StatusCode);
        }

        private string? GetDefaultMassegeForStatusCode(int? statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "You are not Authorized",
                404 => "Resource not Found",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
