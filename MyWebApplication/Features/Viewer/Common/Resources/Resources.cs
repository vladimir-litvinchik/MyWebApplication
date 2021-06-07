using MyWebApplication.Viewer.Common.Entity;
using System.IO;

namespace MyWebApplication.Viewer.Common.Resources
{
    /// <summary>
    /// Provides methods for generating uploaded files names and exception entities.
    /// </summary>
    public static class Resources
    {
        /// <summary>
        /// Get free file name for uploaded file if such file already exists
        /// </summary>
        /// <param name="directory">Directory where to search files</param>
        /// <param name="fileName">Uploaded file name</param>
        /// <returns></returns>
        public static string GetFreeFileName(string directory, string fileName)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);

            var fileNumber = 1;
            var filePath = Path.Combine(directory, fileName);
            while (File.Exists(filePath))
            {
                var fileNameCandidate = $"{fileNameWithoutExtension}({fileNumber++}){extension}";
                filePath = Path.Combine(directory, fileNameCandidate);
            }

            return filePath;
        }

        /// <summary>
        /// Generate exception
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>ExceptionEntity</returns>
        public static ExceptionEntity GenerateException(System.Exception ex)
        {
            // Initiate Exception entity
            ExceptionEntity exceptionEntity = new ExceptionEntity();
            // set exception data
            exceptionEntity.message = ex.Message;
            exceptionEntity.exception = ex;
            return exceptionEntity;
        }

        /// <summary>
        /// Generate exception for password error
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="password">string</param>
        /// <returns>ExceptionEntity</returns>
        public static ExceptionEntity GenerateException(System.Exception ex, string password)
        {
            // Initiate exception
            ExceptionEntity exceptionEntity = new ExceptionEntity();
            // Check if exception message contains password and password is empty
            if (ex.Message.Contains("password") && string.IsNullOrEmpty(password))
            {
                exceptionEntity.message = "Password Required";
            }
            // Check if exception contains password and password is set
            else if (ex.Message.Contains("password") && !string.IsNullOrEmpty(password))
            {
                exceptionEntity.message = "Incorrect password";
            }
            else
            {
                exceptionEntity.message = ex.Message;
                exceptionEntity.exception = ex;
            }
            return exceptionEntity;
        }
    }
}