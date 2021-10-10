using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Helpers
{
    public class FileHelper
    {
        static public async Task<string> SaveImageAsync(int blogId, IFormFile file, string path)
        {
            var fileExtension = Path.GetExtension(Path.GetFileName(file.FileName));

            var newFileName = String.Concat(Convert.ToString(blogId), fileExtension);
            string filePath = Path.Combine("Files/" + path, newFileName);

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {

                await file.CopyToAsync(fileStream);
            }
            return filePath;
        }


        public static async Task<string> SaveEditImageAsync(int id, int imagePosition, IFormFile file, string path)
        {
            var fileExtension = Path.GetExtension(Path.GetFileName(file.FileName));
            var newFileName = "";
            if (imagePosition == 0)
            {
                newFileName = String.Concat(Convert.ToString(id), fileExtension);
            }
            else
            {
                newFileName = id + "." + imagePosition + fileExtension;
            }
            string filePath = Path.Combine("Files/" + path, newFileName);

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {

                await file.CopyToAsync(fileStream);
            }
            return filePath;
        }

    }
}
