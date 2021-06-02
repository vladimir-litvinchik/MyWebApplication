using MyWebApplication.Viewer.Config;

namespace MyWebApplication.Viewer.Common.Config
{
    /// <summary>
    /// Global configuration.
    /// </summary>
    public class GlobalConfiguration
    {
        public CommonConfiguration Common { get; set; }
        public ViewerConfiguration Viewer { get; set; }

        /// <summary>
        /// Get all configurations.
        /// </summary>
        public GlobalConfiguration()
        {
            this.Viewer = new ViewerConfiguration();
            this.Common = new CommonConfiguration();
        }
    }
}