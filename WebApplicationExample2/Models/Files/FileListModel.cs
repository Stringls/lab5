using System.ComponentModel.DataAnnotations;
using System.Web;
using WebApplicationExample5.Services.Models;

namespace WebApplicationExample5.Models
{
    public class FileListModel
    {
        public List<FileListItemModel> Files { get; set; }
    }
}