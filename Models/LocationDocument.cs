using Microsoft.AspNetCore.Http;

namespace EaglePortal.Models
{
    public class LocationDocument
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
