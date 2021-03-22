using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FileManager
    {
        public async Task<string> SaveImage(IFormFile image)
        {
            if (image.Length>0)
            {
                var directory = Directory.GetCurrentDirectory();

            
                if (!Directory.Exists(directory + "\\Images\\"))
                {
                    Directory.CreateDirectory(directory + "\\Images\\");
                }
                var Path = "\\Images\\" + DateTime.UtcNow.ToString("yymmssfff") + image.FileName;

                using (var filestream = File.Create(directory + Path))
                {
                    await image.CopyToAsync(filestream);
                    filestream.Flush();
                    return Path;
                }            
            }
            return null;
        }



        public void DeleteImage(string path)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + path))
            {
                File.Delete(Directory.GetCurrentDirectory() + path);
            }
        }









    }
}
