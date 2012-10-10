using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;

namespace Rentler.Web.Controllers
{
    public class PhotoController : Controller
    {
        IPhotoAdapter imageAdapter;

        public PhotoController(IPhotoAdapter imageAdapter)
        {
            this.imageAdapter = imageAdapter;
        }

        public ActionResult Upload(long id, object[] qqfile)
        {
            // get the filenames and streams
            if (qqfile == null || qqfile.Length < 1)
                return Json(Status.NotFound<Photo[]>());

            // Hack to get the files out of qqfile uploading :/
            List<UploadFileStream> files = new List<UploadFileStream>();
            foreach (var item in qqfile)
            {
                if (item is HttpPostedFileWrapper)
                {
                    HttpPostedFileWrapper wrapper = item as HttpPostedFileWrapper;
                    files.Add(new UploadFileStream()
                    {
                        FileName = wrapper.FileName,
                        Stream = wrapper.InputStream
                    });
                }
                else
                {
                    files.Add(new UploadFileStream()
                    {
                        FileName = qqfile[0].ToString(),
                        Stream = Request.InputStream
                    });
                }
            }

            Status<Photo[]> result = imageAdapter.UploadPhotos(User.Identity.Name, id, files.ToArray());
            return Json(result, "text/html");
        }

        public ActionResult Remove(Guid id)
        {
            if (!Request.IsAjaxRequest() || !User.Identity.IsAuthenticated)
                return Json(Status.UnAuthorized<bool>());

            return Json(this.imageAdapter.RemovePhoto(User.Identity.Name, id));
        }

        public ActionResult SetSortOrder(Guid id, int sortOrder)
        {
            if (!Request.IsAjaxRequest() || !User.Identity.IsAuthenticated)
                return Json(Status.UnAuthorized<Photo>());

            return Json(this.imageAdapter.SetSortOrder(id, sortOrder));
        }

        public ActionResult Reorder(long buildingId, string[] photoIds)
        {
            List<Guid> ids = new List<Guid>();
            bool parseFailed = false;

            // parse photo ids in new requested order
            foreach (string id in photoIds)
            {
                Guid pId;

                // if parse fails we can't process the reorder
                if (!Guid.TryParse(id, out pId))
                {
                    parseFailed = true;
                    break;
                }

                ids.Add(pId);
            }

            // check for parsing failure
            if (parseFailed)
            {
                string msg = "Required information for requested re-order was incomplete";

                // get existing photo ids in order so we can restore
                var pfStatus = this.imageAdapter.GetOrderedPhotoIds(buildingId);
                if (pfStatus.StatusCode != 200)
                    msg = pfStatus.Message;

                return Json(Status.Error<string[]>(msg, pfStatus.Result));
            }

            var status = this.imageAdapter.ReorderPhotos(User.Identity.Name, ids.ToArray());
            return Json(status);
        }
    }
}
