using Project_ASP.NET.Data.Entities;

namespace Project_ASP.NET.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile file);
        Task<List<ProductImageEntity>> SaveImagesAsync(List<IFormFile> files);
        Task<string> SaveImageFromUrlAsync(string imageUrl);
        Task DeleteImageAsync(string name);
    }
}
