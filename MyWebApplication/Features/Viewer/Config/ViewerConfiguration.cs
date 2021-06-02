using MyWebApplication.Viewer.Common.Config;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace MyWebApplication.Viewer.Config
{
    /// <summary>
    /// ViewerConfiguration.
    /// </summary>
    public class ViewerConfiguration : CommonConfiguration
    {
        [JsonProperty]
        private string filesDirectory = "Files";

        [JsonProperty]
        private string fontsDirectory = string.Empty;

        [JsonProperty]
        private string defaultDocument = string.Empty;

        [JsonProperty]
        private string watermarkText = string.Empty;

        [JsonProperty]
        private int preloadPageCount;

        [JsonProperty]
        private bool zoom = true;

        [JsonProperty]
        private bool search = true;

        [JsonProperty]
        private bool thumbnails = true;

        [JsonProperty]
        private bool rotate = true;

        [JsonProperty]
        private bool htmlMode = true;

        [JsonProperty]
        private bool cache = true;

        [JsonProperty]
        private bool saveRotateState = true;

        [JsonProperty]
        private bool printAllowed = true;

        [JsonProperty]
        private bool showGridLines = true;

        [JsonProperty]
        private string cacheFolderName = "cache";

        private static bool IsFullPath(string path)
        {
            return !string.IsNullOrWhiteSpace(path)
                && path.IndexOfAny(System.IO.Path.GetInvalidPathChars().ToArray()) == -1
                && Path.IsPathRooted(path)
                && !Path.GetPathRoot(path).Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal);
        }

        public void SetFilesDirectory(string filesDirectory)
        {
            this.filesDirectory = filesDirectory;
        }

        public string GetFilesDirectory()
        {
            return this.filesDirectory;
        }

        public void SetCacheFolderName(string cacheFolderName)
        {
            this.cacheFolderName = cacheFolderName;
        }

        public string GetCacheFolderName()
        {
            return this.cacheFolderName;
        }

        public void SetFontsDirectory(string fontsDirectory)
        {
            this.fontsDirectory = fontsDirectory;
        }

        public string GetFontsDirectory()
        {
            return this.fontsDirectory;
        }

        public void SetDefaultDocument(string defaultDocument)
        {
            this.defaultDocument = defaultDocument;
        }

        public string GetDefaultDocument()
        {
            return this.defaultDocument;
        }

        public void SetPreloadPageCount(int preloadPageCount)
        {
            this.preloadPageCount = preloadPageCount;
        }

        public int GetPreloadPageCount()
        {
            return this.preloadPageCount;
        }

        public void SetIsZoom(bool isZoom)
        {
            this.zoom = isZoom;
        }

        public bool GetIsZoom()
        {
            return this.zoom;
        }

        public void SetIsSearch(bool isSearch)
        {
            this.search = isSearch;
        }

        public bool GetIsSearch()
        {
            return this.search;
        }

        public void SetIsThumbnails(bool isThumbnails)
        {
            this.thumbnails = isThumbnails;
        }

        public bool GetIsThumbnails()
        {
            return this.thumbnails;
        }

        public void SetIsRotate(bool isRotate)
        {
            this.rotate = isRotate;
        }

        public bool GetIsRotate()
        {
            return this.rotate;
        }

        public void SetIsHtmlMode(bool isHtmlMode)
        {
            this.htmlMode = isHtmlMode;
        }

        public bool GetIsHtmlMode()
        {
            return this.htmlMode;
        }

        public void SetCache(bool Cache)
        {
            this.cache = Cache;
        }

        public bool GetCache()
        {
            return this.cache;
        }

        public void SetSaveRotateState(bool saveRotateState)
        {
            this.saveRotateState = saveRotateState;
        }

        public bool GetSaveRotateState()
        {
            return this.saveRotateState;
        }

        public void SetWatermarkText(string watermarkText)
        {
            this.watermarkText = watermarkText;
        }

        public string GetWatermarkText()
        {
            return this.watermarkText;
        }

        public void SetPrintAllowed(bool printAllowed)
        {
            this.printAllowed = printAllowed;
        }

        public bool GetPrintAllowed()
        {
            return this.printAllowed;
        }

        public void SetShowGridLines(bool showGridLines)
        {
            this.showGridLines = showGridLines;
        }

        public bool GetShowGridLines()
        {
            return this.showGridLines;
        }
    }
}