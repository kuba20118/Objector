using Objector.ML.DataModels;
using Objector.Models;

namespace Objector.Services.Interfaces
{
    public interface IObjectDetectionService
    {
        void DetectObjectsUsingModel(ImageInputData imageInputData);
        ProcessedImage DrawBoundingBox(string imageFilePath);
    }
}
