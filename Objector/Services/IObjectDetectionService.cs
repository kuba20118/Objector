using Objector.ML.DataModels;
using Objector.Models;

namespace Objector.Services
{
    public interface IObjectDetectionService
    {
        void DetectObjectsUsingModel(ImageInputData imageInputData);
        ProcessedImage DrawBoundingBox(string imageFilePath);
    }
}
