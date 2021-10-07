using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace KKFCoreEngine.Util
{
    public static class FileUtil
    {
        private static string pathImg = "imgs";
        private static string pathFile = "files";

        public static string GetImageBase64(string imgUrl)
        {
            string imageBase64 = "";
            using (WebClient client = new WebClient())
            {
                byte[] imageByteData = client.DownloadData(imgUrl);
                imageBase64 = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(imageByteData));
            }
            return imageBase64;
        }

        public static string SaveImage(string base64Image)
        {
            string Path = Environment.CurrentDirectory + @"\wwwroot\" + pathImg + @"\";
             
            try
            {
                Directory.CreateDirectory(Path);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if(!string.IsNullOrWhiteSpace(base64Image))
            {
                string filename = EncryptUtil.MD5(Guid.NewGuid().ToString());

                string type = Regex.Replace(base64Image, @"\b^data:image/([a-zA-Z]+).*$", "$1"); //"jpg";
                var tmp = base64Image.Split(',');
                if (tmp.Length == 2)
                {
                    Path += filename + "." + type;
                    var bytes = Convert.FromBase64String(tmp[tmp.Length - 1]);
                    using (var imageFile = new FileStream(Path, FileMode.Create))
                    {
                        imageFile.Write(bytes, 0, bytes.Length);
                        imageFile.Flush();
                    }
                    return "/" + pathImg + "/" + filename + "." + type;
                }
            }
            
            return base64Image;
        }

        public static string SaveImageFile(string base64Image, string pathImg, string filename)
        {

            string Path = Environment.CurrentDirectory + @"\wwwroot\" + pathImg + @"\";

            try
            {
                Directory.CreateDirectory(Path);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (!string.IsNullOrWhiteSpace(base64Image))
            {
                string type = Regex.Replace(base64Image, @"\b^data:image/([a-zA-Z]+).*$", "$1"); //"jpg";
                var tmp = base64Image.Split(',');
                if (tmp.Length == 2)
                {
                    Path += filename + "." + type;
                    var bytes = Convert.FromBase64String(tmp[tmp.Length - 1]);
                    using (var imageFile = new FileStream(Path, FileMode.Create))
                    {
                        imageFile.Write(bytes, 0, bytes.Length);
                        imageFile.Flush();
                    }
                    return "/" + pathImg + "/" + filename + "." + type;
                }
            }

            return base64Image;
        }


        public static string SaveFile(string base64File)
        {
            string Path = Environment.CurrentDirectory + @"\wwwroot\" + pathFile + @"\";

            try
            {
                Directory.CreateDirectory(Path);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (!string.IsNullOrWhiteSpace(base64File))
            {

                string type = Regex.Replace(base64File, @"\b^data:.*/([a-zA-Z]+);.*$", "$1");
                var tmp = base64File.Split(',');               
                if (tmp.Length == 2)
                {
                    string filename = EncryptUtil.MD5(Guid.NewGuid().ToString());
                    Path += filename + "." + type;
                    var bytes = Convert.FromBase64String(tmp[tmp.Length - 1]);
                    using (var file = new FileStream(Path, FileMode.Create))
                    {
                        file.Write(bytes, 0, bytes.Length);
                        file.Flush();
                    }
                    return filename + "." + type;
                }
            }

            return base64File;
        }

        public static string GetFileBase64(string fileName)
        {
            string Path = Environment.CurrentDirectory + @"\wwwroot\" + pathFile + @"\" + fileName;
            byte[] bytes = File.ReadAllBytes(Path);
            return Convert.ToBase64String(bytes);
        }

    }
}
