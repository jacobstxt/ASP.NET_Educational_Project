using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using Project_ASP.NET.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using Project_ASP.NET.Data.Entities;


namespace Project_ASP.NET.Services
{
    public class ImageService(IConfiguration configuration):IImageService
    {
        public async Task DeleteImageAsync(string name)
        {
            var sizes = configuration.GetRequiredSection("ImageSizes").Get<List<int>>();
            var dir = Path.Combine(Directory.GetCurrentDirectory(), configuration["ImagesDir"]!);

            Task[] tasks = sizes
                .AsParallel()
                .Select(size =>
                {
                    return Task.Run(() =>
                    {
                        var path = Path.Combine(dir, $"{size}_{name}");
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    });
                })
                .ToArray();

            await Task.WhenAll(tasks);
        }

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            using MemoryStream ms = new();
            await file.CopyToAsync(ms);
            var bytes = ms.ToArray();

            var imageName = await SaveImageAsync(bytes);
            return imageName;
        }


        public async Task<List<ProductImageEntity>> SaveImagesAsync(List<IFormFile> files)
        {
            var result = new List<ProductImageEntity>(); 
            int priority = 0;
           
            foreach (var file in files)
            {
                using (var ms = new MemoryStream())
                {
               
                    await file.CopyToAsync(ms);
                    var bytes = ms.ToArray();

                    var imageName = await SaveImageAsync(bytes);

                    var productImage = new ProductImageEntity
                    {
                        FileName = imageName, // Присвоєння імені зображення
                        Priority = priority++  // Якщо треба задати пріоритет (можна зробити динамічним)
                    };


                    result.Add(productImage); 
                }
            }

            return result;
        }


        public async Task<string> SaveImageAsync(byte[] bytes)
        {
            string imageName = $"{Path.GetRandomFileName()}.webp";
            var sizes = configuration.GetRequiredSection("ImageSizes").Get<List<int>>();
            var tasks = sizes
                .AsParallel()
                .Select(s => SaveImageAsync(bytes, imageName, s))
                .ToArray();

            await Task.WhenAll(tasks);

            return imageName;
        }


        private async Task SaveImageAsync(byte[] bytes, string name, int size)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), configuration["ImagesDir"]!, $"{size}_{name}");
                using var image = SixLabors.ImageSharp.Image.Load(bytes);
                image.Mutate(async imgContext =>
                {
                    imgContext.Resize(new ResizeOptions
                    {
                        Size = new Size(size, size),
                        Mode = ResizeMode.Max
                    });
                    await image.SaveAsync(path, new WebpEncoder());
                });
            }


        


            public async Task<string> SaveImageFromUrlAsync(string imageUrl)
                {
                    using var httpClient = new HttpClient();
                    var ImageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                    return await SaveImageAsync(ImageBytes);
                }


    }
}
