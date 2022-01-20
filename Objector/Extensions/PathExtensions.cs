using Objector.Services;

namespace Objector.Extensions
{
    public static class PathExtensions
    {
        public static string GetAbsolutePath(string relativePath)
        {
            var _dataRoot = new FileInfo(typeof(ImageMLService).Assembly.Location);
            var assemblyFolderPath = _dataRoot.Directory.FullName;

            return Path.Combine(assemblyFolderPath, relativePath);
        }
    }
}
