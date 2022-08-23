using Newtonsoft.Json;

namespace Northwind.Api.GlobalErrorHanding
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
