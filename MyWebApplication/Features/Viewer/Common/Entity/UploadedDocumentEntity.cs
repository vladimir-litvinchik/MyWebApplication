namespace MyWebApplication.Viewer.Common.Entity
{
    /// <summary>
    /// DTO-class, represents uploaded document by its absolute path.
    /// </summary>
    public class UploadedDocumentEntity
    {
        /// <summary>
        /// Absolute path to the uploaded document.
        /// </summary>
        public string guid { get; set; }
    }
}