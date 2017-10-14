using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace aehyok.Web.Controllers
{
    public class BlogController : Controller
    {
        private IHostingEnvironment _hostingEnv;

        public BlogController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnv = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult TagIndex()
        {
            return View();
        }

        public IActionResult AddTag()
        {
            return View();
        }


        ///http://www.cnblogs.com/fishpro/p/how_to_using_httpclient_for_asp_net_core.html
        /// <summary>
        /// Http Get 同步方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpGet(string url, Encoding encoding = null)
        {
            HttpClient httpClient = new HttpClient();
            var t = httpClient.GetByteArrayAsync(url);
            t.Wait();
            var ret = encoding.GetString(t.Result);
            return ret;
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            const string savePath = "UploadImages";
            const string saveUrl = "UploadImages";
            const string fileTypes = "gif,jpg,jpeg,png,bmp,avi";
            const int maxSize = 1024 * 1024 * 50;

            Hashtable hash = new Hashtable();

            var file = Request.Form.Files["imgFile"];
            if (file == null)
            {
                hash = new Hashtable();
                hash["error"] = 1;
                hash["message"] = "请选择文件";
                return Json(hash);
            }

            var path=_hostingEnv.WebRootPath;
            string dirPath = path+"\\"+savePath;//System.Web.HttpContext.Current.Server.MapPath(savePath);
            
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string fileName = file.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));

            if (file.Length > maxSize)
            {
                hash = new Hashtable();
                hash["error"] = 1;
                hash["message"] = "上传文件大小超过限制";
                return Json(hash);
            }

            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                hash = new Hashtable();
                hash["error"] = 1;
                hash["message"] = "上传文件扩展名是不允许的扩展名";
                return Json(hash);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            string filePath = dirPath +"\\"+ newFileName;
            //file.SaveAs(filePath);
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            var qiniuUrl = "http://localhost:3000/qiniu/upload/"+newFileName;
            var result=BlogController.HttpGet(qiniuUrl, Encoding.GetEncoding("utf-8"));
            var jsonObject = JsonConvert.DeserializeObject<QiniuResult>(result);
            http://aehyok.qiniudn.com/Chrysanthemum.jpg
            string fileUrl = "http://aehyok.qiniudn.com/"+jsonObject.Key ;

            hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = fileUrl;

            return Json(hash);
        }

        public ActionResult ProcessRequest()
        {
            //根目录路径，相对路径
            String rootPath = "UploadImages";
            //根目录URL，可以指定绝对路径，
            String rootUrl = "UploadImages";
            //图片扩展名
            String fileTypes = "gif,jpg,jpeg,png,bmp";

            String currentPath = "";
            String currentUrl = "";
            String currentDirPath = "/";
            String moveupDirPath = "";

            //根据path参数，设置各路径和URL
            String path = Request.Query["path"];//Request.QueryString["path"];
            path = String.IsNullOrEmpty(path) ? "" : path;
            var tempPath = "";
            var webPath =@""+ _hostingEnv.WebRootPath;
            if (path == "")
            {
                var temp = _hostingEnv.WebRootPath + $@"\"+ rootPath;
                currentPath = webPath+"\\"+rootPath;// Server.MapPath(rootPath);
                currentUrl = rootUrl;
                currentDirPath = "http://aehyok.qiniudn.com/"; //rootPath +"\\";
                moveupDirPath = "";
            }
            else
            {
                tempPath = path.Replace("/", "");
                //path=path.Split
                currentPath =  webPath + "\\" + tempPath;// HttpContext.Current.Server.MapPath(rootPath) + path;
                currentUrl = tempPath;
                currentDirPath = tempPath + "\\";
                var array = tempPath.Split('\\');

                moveupDirPath = "UploadImages/";
                 //Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
            }

            //排序形式，name or size or type
            String order = Request.Query["order"];
            order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

            //不允许使用..移动到上一级目录
            if (Regex.IsMatch(path, @"\.\."))
            {
                return Content("Access is not allowed.");
            }
            //最后一个字符不是/
            if (path != "" && !path.EndsWith("/"))
            {
                return Content("Parameter is not valid.");
            }
            //目录不存在或不是目录
            if (!Directory.Exists(currentPath))
            {
                return Content("Directory does not exist.");
            }

            //遍历目录取得文件信息
            string[] dirList = Directory.GetDirectories(currentPath);
            string[] fileList = Directory.GetFiles(currentPath);

            switch (order)
            {
                case "size":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new SizeSorter());
                    break;
                case "type":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new TypeSorter());
                    break;
                case "name":
                default:
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new NameSorter());
                    break;
            }

            Hashtable result = new Hashtable();
            result["moveup_dir_path"] = moveupDirPath;     //上一级目录地址
            result["current_dir_path"] = currentDirPath;   //当前目录地址

            string fileUrl = "http://" + Request.Host.Value + "/" + currentUrl+"/";
            result["current_url"] = "http://aehyok.qiniudn.com/";
            result["total_count"] = dirList.Length + fileList.Length;
            List<Hashtable> dirFileList = new List<Hashtable>();
            result["file_list"] = dirFileList;
            for (int i = 0; i < dirList.Length; i++)
            {
                DirectoryInfo dir = new DirectoryInfo(dirList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = true;
                hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                hash["filesize"] = 0;
                hash["is_photo"] = false;
                hash["filetype"] = "";
                hash["filename"] = dir.Name;
                hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }

            var downLoadUrl = "http://localhost:3000/qiniu/download";
            var files = BlogController.HttpGet(downLoadUrl, Encoding.UTF8);

            List<QiniuFile> list = JsonConvert.DeserializeObject<List<QiniuFile>>(files);
            foreach(var item in list)
            {
                Hashtable hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = item.FSize;
                hash["is_photo"] = true;// (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                hash["filetype"] = item.MimeType;
                hash["filename"] = item.Key;
                hash["datetime"] = item.PutTime; //.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            for (int i = 0; i < fileList.Length; i++)
            {
                FileInfo file = new FileInfo(fileList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = file.Length;
                hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                hash["filetype"] = file.Extension.Substring(1);
                hash["filename"] = file.Name;
                hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            //Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            //context.Response.Write(JsonMapper.ToJson(result));
            //context.Response.End();
            return Json(result);
        }
    }

    public class QiniuResult
    {
        public string Hash { get; set; }
        public string Key { get; set; }

        public string GroupName { get; set; }

        public string FileType { get; set; }
    }

    public class QiniuFile
    {
        public string Hash { get; set; }

        public string Key { get; set; }

        public string FSize { get; set; }

        public string MimeType { get; set; }

        public string PutTime { get; set; }

        public string Type { get; set; }
    }


        public class NameSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return String.Compare(xInfo.FullName, yInfo.FullName, StringComparison.Ordinal);
            }
        }

        public class SizeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());
                return xInfo.Length.CompareTo(yInfo.Length);
            }
        }

        public class TypeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());
                return String.Compare(xInfo.Extension, yInfo.Extension, StringComparison.Ordinal);
            }
        }
}