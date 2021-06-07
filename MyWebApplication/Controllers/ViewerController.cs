using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using GroupDocs.Viewer;
using MyWebApplication.Viewer.Common.Resources;

namespace MyWebApplication.Controllers
{
    public class ViewerController : Controller
    {
        private readonly string _filesDir;

        public ViewerController()
        {
            _filesDir = System.Web.Hosting.HostingEnvironment.MapPath("~/Files");
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Preview(string url)
        {
            var uri = new Uri(url);
            var client = new HttpClient();
            var response = await client.GetAsync(uri);
            var fileName = Path.GetFileName(url);
            var mediaType = response.Content.Headers.ContentType.MediaType;
            var bytes = await response.Content.ReadAsByteArrayAsync();

            var fileType = FileType.FromFilePath(fileName);
            if(fileType == FileType.Unknown)
                fileType = FileType.FromMediaType(mediaType);

            if (fileType != FileType.Unknown)
                fileName = Path.ChangeExtension(fileName, fileType.Extension);

            var filePath = Resources.GetFreeFileName(_filesDir, fileName);

            System.IO.File.WriteAllBytes(filePath, bytes);

            return RedirectToAction("Index", routeValues: new {f = Path.GetFileName(filePath) });
        }
    }
}
