using Objector.ML.DataModels;
using Objector.Services.Interfaces;
using System.Drawing.Imaging;
using System.Drawing;
using Objector.Models;

namespace Objector.Services
{
    public class ImageMLService : IImageMLService
    {
        private readonly string _imagesTmpFolder;
        private readonly IObjectDetectionService _objectDetectionService;
        private readonly IImagesService _imagesService;
        private string base64String = string.Empty;
        private long elapsedMs = 0;
        public ImageMLService(IObjectDetectionService objectDetectionService, IImagesService imagesService)
        {
            //_imageService = imageService;
            _objectDetectionService = objectDetectionService;
            _imagesService = imagesService;
            _imagesTmpFolder = Path.GetFullPath(@"ImagesTemp");
        }

        public async Task<Guid> IdentifyObjectsAsync(IFormFile imageFile)
        {
            try
            {
                MemoryStream imageMemoryStream = new MemoryStream();
                await imageFile.CopyToAsync(imageMemoryStream);
                //Check that the image is valid
                byte[] imageData = imageMemoryStream.ToArray();
                //Convert to Image
                System.Drawing.Image image = System.Drawing.Image.FromStream(imageMemoryStream);
                string fileName = string.Format("{0}.Jpeg", image.GetHashCode());
                string imageFilePath = Path.Combine(_imagesTmpFolder, fileName);
                ////
                bool exists = Directory.Exists(_imagesTmpFolder);
                if (!exists)
                    Directory.CreateDirectory(_imagesTmpFolder);
                //save image to a path
                image.Save(imageFilePath, ImageFormat.Jpeg);

                //Convert to Bitmap
                Bitmap bitmapImage = (Bitmap)image;

                //Measure execution time
                var watch = System.Diagnostics.Stopwatch.StartNew();

                //Set the specific image data into the ImageInputData type used in the DataView
                var imageInputData = new ImageInputData { Image = bitmapImage };

                //Detect the objects in the image                
                var result = DetectAndPaintImage(imageInputData, imageFilePath);

                //Stop measuring time
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;

                result.ElapsedTime = elapsedMs;
                result.ImageStringOriginal = imageData;

                return await _imagesService.AddImageAsync(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private Result DetectAndPaintImage(ImageInputData imageInputData, string imageFilePath)
        {
            //Predict the objects in the image
            _objectDetectionService.DetectObjectsUsingModel(imageInputData);
            var img = _objectDetectionService.DrawBoundingBox(imageFilePath);

            using MemoryStream m = new();
            img.Image.Save(m, img.Image.RawFormat);
            byte[] imageBytes = m.ToArray();

            return new Result
            {
                ImageStringProcessed = imageBytes,
                Description = img.Description,
                ElapsedTime = elapsedMs
            };
        }
    }
}
