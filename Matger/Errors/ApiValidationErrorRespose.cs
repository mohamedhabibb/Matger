namespace Matger.Errors
{
    public class ApiValidationErrorRespose: ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorRespose() : base(400)
        {
            Errors = new List<string>();
        }

    }
}
