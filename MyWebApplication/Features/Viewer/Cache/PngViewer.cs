﻿using GroupDocs.Viewer.Options;
using GroupDocs.Viewer.Results;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyWebApplication.Viewer.Cache
{
    class PngViewer : IDisposable, ICustomViewer
    {
        private readonly string filePath;
        private readonly IViewerCache cache;

        private readonly GroupDocs.Viewer.Viewer viewer;
        private readonly PngViewOptions pngViewOptions;
        private readonly ViewInfoOptions viewInfoOptions;
        private readonly Common.Config.GlobalConfiguration globalConfiguration;

        public PngViewer(Common.Config.GlobalConfiguration globalConfiguration, string filePath, IViewerCache cache, LoadOptions loadOptions, int pageNumber = -1, int newAngle = 0)
        {
            this.globalConfiguration = globalConfiguration;
            this.cache = cache;
            this.filePath = filePath;
            this.viewer = new GroupDocs.Viewer.Viewer(filePath, loadOptions);
            this.pngViewOptions = this.CreatePngViewOptions(pageNumber, newAngle);
            this.viewInfoOptions = ViewInfoOptions.FromPngViewOptions(this.pngViewOptions);
        }

        public GroupDocs.Viewer.Viewer GetViewer()
        {
            return this.viewer;
        }

        private PngViewOptions CreatePngViewOptions(int passedPageNumber = -1, int newAngle = 0)
        {
            PngViewOptions createdPngViewOptions = new PngViewOptions(pageNumber =>
            {
                string fileName = $"p{pageNumber}.png";
                string cacheFilePath = this.cache.GetCacheFilePath(fileName);

                return File.Create(cacheFilePath);
            });

            if (passedPageNumber >= 0 && newAngle != 0)
            {
                Rotation rotationAngle = GetRotationByAngle(newAngle);
                createdPngViewOptions.RotatePage(passedPageNumber, rotationAngle);
            }

            createdPngViewOptions.SpreadsheetOptions.RenderHeadings = true;

            SetWatermarkOptions(createdPngViewOptions);

            return createdPngViewOptions;
        }

        /// <summary>
        /// Gets enumeration member by rotation angle value.
        /// </summary>
        /// <param name="newAngle">New rotation angle value.</param>
        /// <returns>Rotation enumeration member.</returns>
        private static Rotation GetRotationByAngle(int newAngle)
        {
            switch (newAngle)
            {
                case 90:
                    return Rotation.On90Degree;
                case 180:
                    return Rotation.On180Degree;
                case 270:
                    return Rotation.On270Degree;
                default:
                    return Rotation.On90Degree;
            }
        }

        public GroupDocs.Viewer.Results.FileInfo GetFileInfo()
        {
            string cacheKey = "file_info.dat";

            var viewInfo = this.cache.GetValue(cacheKey, () => this.ReadFileInfo());

            return viewInfo;
        }

        public System.IO.FileInfo GetPageFile(int pageNumber)
        {
            this.CreateCache();

            string pageKey = $"p{pageNumber}.png";
            string cacheFilePath = this.cache.GetCacheFilePath(pageKey);

            return new System.IO.FileInfo(cacheFilePath);
        }

        public void CreateCache()
        {
            ViewInfo viewInfo = this.GetViewInfo();

            using (new CrossProcessLock(this.filePath))
            {
                int[] missingPages = this.GetPagesMissingFromCache(viewInfo.Pages);

                if (missingPages.Length > 0)
                {
                    this.viewer.View(this.pngViewOptions, missingPages);
                }
            }
        }

        public void Dispose()
        {
            this.viewer?.Dispose();
        }

        /// <summary>
        /// Adds watermark on document if its specified in configuration file.
        /// </summary>
        /// <param name="options"></param>
        private void SetWatermarkOptions(ViewOptions options)
        {
            Watermark watermark = null;

            if (!string.IsNullOrEmpty(globalConfiguration.Viewer.GetWatermarkText()))
            {
                // Set watermark properties
                watermark = new Watermark(globalConfiguration.Viewer.GetWatermarkText())
                {
                    Color = System.Drawing.Color.Blue,
                    Position = Position.Diagonal,
                };
            }

            if (watermark != null)
            {
                options.Watermark = watermark;
            }
        }

        private GroupDocs.Viewer.Results.FileInfo ReadFileInfo()
        {
            using (new CrossProcessLock(this.filePath))
            {
                var fileInfo = this.viewer.GetFileInfo();
                return fileInfo;
            }
        }

        public ViewInfo GetViewInfo()
        {
            string cacheKey = "view_info.dat";

            if (!this.cache.Contains(cacheKey))
            {
                using (new CrossProcessLock(this.filePath))
                {
                    if (!this.cache.Contains(cacheKey))
                    {
                        return this.cache.GetValue(cacheKey, () => this.ReadViewInfo());
                    }
                }
            }

            return this.cache.GetValue<ViewInfo>(cacheKey);
        }

        private ViewInfo ReadViewInfo()
        {
            ViewInfo viewInfo = this.viewer.GetViewInfo(this.viewInfoOptions);
            return viewInfo;
        }

        private int[] GetPagesMissingFromCache(IList<Page> pages)
        {
            List<int> missingPages = new List<int>();
            foreach (Page page in pages)
            {
                string pageKey = $"p{page.Number}.png";
                if (!this.cache.Contains(pageKey))
                {
                    missingPages.Add(page.Number);
                }
            }

            return missingPages.ToArray();
        }
    }
}