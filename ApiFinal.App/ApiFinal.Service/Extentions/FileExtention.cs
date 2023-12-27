using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFinal.Service.Extentions
{
    public static class FileExtention
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }

        public static bool IsSizeOk(this IFormFile file, int mb)
        {
            return file.Length / 1024 / 1024 <= mb;
        }

        public static string SaveFile(this IFormFile file, string root, string path)
        {
            string FileName = Guid.NewGuid().ToString() + file.Name;
            string FullPath = Path.Combine(root, path, FileName);

            using (FileStream stream = new FileStream(FullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return FileName;
        }
    }
}
