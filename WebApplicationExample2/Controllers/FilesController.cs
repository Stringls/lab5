using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using WebApplicationExample5.Helpers;
using WebApplicationExample5.Models;
using WebApplicationExample5.Services;

namespace WebApplicationExample5.Controllers
{
    public class FilesController : Controller
    {
        private readonly ILogger<MailController> _logger;
        private readonly IFileService _fileService;

        public FilesController(ILogger<MailController> logger, IFileService mailService)
        {
            _logger = logger;
            _fileService = mailService;
        }

        public IActionResult Index()
        {
            var files = _fileService.GetFileList();

            return View(new FileListModel()
            {
                Files = files
            });
        }

        public IActionResult ViewFile(string fileName)
        {
            var filePreview = _fileService.GetFilePreview(fileName);

            return View(new FilePreviewModel
            {
                FileContents = filePreview
            });
        }

        public IActionResult GetFile(string fileName)
        {
            var stream = _fileService.GetFileStream(fileName);

            if(stream != null)
            {
                return File(stream, ContentTypeHelper.GetContentType(fileName), fileName);
            }

            return NotFound();
        }

        public IActionResult SaveFile()
        {
            var model = new FileModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveFile(FileModel model)
        {
            if(model.File == null)
                return View(model);

            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension
                FileInfo fileInfo = new FileInfo(model.File.FileName);
                string fileName = Path.GetFileName(model.File.FileName);

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }

                model.Message = "File upload successfully";
                model.File = null;

                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                ModelState.AddModelError("", ex.Message);

                return View(model);
            }

            //return RedirectToAction("Index", "Home");
        }
    }
}