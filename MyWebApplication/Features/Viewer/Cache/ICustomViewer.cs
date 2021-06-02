using GroupDocs.Viewer.Results;

namespace MyWebApplication.Viewer.Cache
{
    interface ICustomViewer
    {
        GroupDocs.Viewer.Viewer GetViewer();

        void CreateCache();

        ViewInfo GetViewInfo();
    }
}