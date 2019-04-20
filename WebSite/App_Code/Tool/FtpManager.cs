using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

    /// <summary>
    /// FTP管理类
    /// 本地目录\
    /// FTP目录/
    /// </summary>
    public class FtpManager
    {
        /// <summary>
        /// 文件上传或下载进度委托
        /// </summary>
        /// <param name="percent">进度</param>
        public delegate void ProgressHandler(double percent);
        /// <summary>
        /// 单个文件上传或下载进度
        /// </summary>
        public event ProgressHandler FileProgressEvent;
        /// <summary>
        /// 多个文件上传或下载进度
        /// </summary>
        public event ProgressHandler DirProgressEvent;
        /// <summary>
        /// 是否联机
        /// </summary>
        public static bool IsOnline
        {
            get
            {
                return _isOnline;
            }

            set
            {
                _isOnline = value;
            }
        }
        /// <summary>
        /// 脱机本地路径
        /// </summary>
        public static string LocalPath
        {
            get
            {
                return _localPath;
            }

            set
            {
                _localPath = value;
            }
        }

        private static bool _isOnline = true;//是否联机
        private static string _localPath = "";//脱机本地路径
        private string _ftpIp;//服务器地址
        private string _ftpUser;//用户名
        private string _ftpPassword;//密码
        private string _ftpPort;//端口号
        private FtpWebRequest _ftpRequest;//请求对象
        private FtpWebResponse _ftpResponse;//响应对象

        public FtpManager()
        {
            if (_isOnline)
            {
                _ftpIp = "114.215.62.34";
                _ftpUser = "qxw0080382";
                _ftpPassword = "zzc369963";
                _ftpPort = "21";
            }
        }

        /// <summary>
        /// 检测服务器上是否有重名文件夹
        /// </summary>
        public bool CheckDir(string dirPath)
        {
            if (_isOnline)
                return OperateFile(dirPath, WebRequestMethods.Ftp.ListDirectory);
            else
            {
                return Directory.Exists(_localPath + dirPath);
            }
        }
        /// <summary>
        /// 检测服务器上是否有重名文件
        /// </summary>
        public bool CheckFile(string filePath)
        {
            if (_isOnline)
                return OperateFile(filePath, WebRequestMethods.Ftp.GetFileSize);
            else
                return File.Exists(_localPath + filePath);
        }
        /// <summary>
        /// 创建文件夹
        /// 注意dirPath的后面要加一个斜杠/。例如：Project/，否则会出错
        /// </summary>
        public bool CreateDir(string dirPath)
        {
            if (CheckDir(dirPath)) return true;
            if (_isOnline)
                return OperateFile(dirPath, WebRequestMethods.Ftp.MakeDirectory);
            else
            {
                Directory.CreateDirectory(_localPath + dirPath);
                return CheckDir(dirPath);
            }
        }
        /// <summary>
        /// 删除目录并删除该目录下所有子目录和文件
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="recursive">若要移除 path 中的目录、子目录和文件，则为 true；否则为 false。</param>
        /// <returns></returns>
        public bool DeleteDir(string dirPath, bool recursive = true)
        {
            try
            {
                if (!recursive)
                {
                    return DeleteDir(dirPath);
                }
                if (_isOnline)
                {
                    string[] dirs = null;
                    string[] files = GetFiles(dirPath, out dirs);
                    if (files != null)
                    {
                        foreach (var item in files)
                        {
                            if (!DeleteFile(item))
                                return false;
                        }
                    }
                    if (dirs != null)
                    {
                        dirs = dirs.OrderByDescending(t => t.Length).ToArray();
                        foreach (var item in dirs)
                        {
                            if (!DeleteDir(item))
                                return false;
                        }
                    }
                    DeleteDir(dirPath);
                    return true;
                }
                else
                {
                    Directory.Delete(_localPath + dirPath, true);
                    return !CheckDir(dirPath);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool DeleteFile(string filePath)
        {
            if (_isOnline)
                return OperateFile(filePath, WebRequestMethods.Ftp.DeleteFile);
            else
            {
                File.Delete(_localPath + filePath);
                return !CheckFile(filePath);
            }
        }
        /// <summary>
        /// 重命名文件或者文件夹
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="newName"></param>
        /// <param name="isDir">是否重命名文件夹</param>
        /// <returns></returns>
        public bool Rename(string filePath, string newName, bool isDir)
        {
            if (_isOnline)
                return OperateFile(filePath, WebRequestMethods.Ftp.Rename, newName);
            else
            {
                string destFileName = _localPath + Path.GetDirectoryName(filePath) + @"\" + newName;
                if (isDir)
                    Directory.Move(_localPath + filePath, destFileName);
                else
                    File.Move(_localPath + filePath, destFileName);
                return File.Exists(destFileName);
            }
        }

        /// <summary>
        /// 上传
        /// </summary>
        public bool UploadFile(string filePath, string ftpPath)
        {
            if (!File.Exists(filePath)) return false;
            if (_isOnline)
            {
                FileStream fileStream = null;
                Stream uploadStream = null;
                try
                {
                    if (string.IsNullOrEmpty(Path.GetExtension(ftpPath)))
                    {
                        ftpPath = ftpPath + @"/" + Path.GetFileName(filePath);
                    }
                    if (!CreateDir(Path.GetDirectoryName(ftpPath))) return false;
                    if (!Connect(ftpPath))
                    {
                        return false;
                    }
                    fileStream = new FileStream(filePath, FileMode.Open);
                    _ftpRequest.KeepAlive = false;
                    _ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                    _ftpRequest.ContentLength = fileStream.Length;

                    int buffLength = 2048;
                    byte[] buffer = new byte[buffLength];

                    uploadStream = _ftpRequest.GetRequestStream();
                    fileStream.Position = 0;
                    int contentLen = fileStream.Read(buffer, 0, buffLength);
                    int progressLen = 0;
                    while (contentLen != 0)
                    {
                        Application.DoEvents();
                        uploadStream.Write(buffer, 0, contentLen);
                        progressLen += contentLen;
                        if (FileProgressEvent != null)
                            FileProgressEvent(contentLen * 100 / fileStream.Length);
                        contentLen = fileStream.Read(buffer, 0, buffLength);
                    }
                    if (FileProgressEvent != null)
                        FileProgressEvent(100);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    if (uploadStream != null)
                    {
                        uploadStream.Close();
                        uploadStream.Dispose();
                    }
                    if (fileStream != null)
                    {
                        fileStream.Close();
                        fileStream.Dispose();
                    }
                }
            }
            else
            {
                FileStream fileStream = null;
                FileStream uploadStream = null;
                try
                {
                    if (string.IsNullOrEmpty(Path.GetExtension(ftpPath)))
                    {
                        ftpPath = ftpPath + @"/" + Path.GetFileName(filePath);
                    }
                    if (!CreateDir(Path.GetDirectoryName(ftpPath))) return false;
                    fileStream = new FileStream(filePath, FileMode.Open);

                    int buffLength = 2048;
                    byte[] buffer = new byte[buffLength];
                    uploadStream = new FileStream(_localPath + ftpPath, FileMode.Create);
                    fileStream.Position = 0;
                    int contentLen = fileStream.Read(buffer, 0, buffLength);
                    int progressLen = 0;
                    while (contentLen != 0)
                    {
                        uploadStream.Write(buffer, 0, contentLen);
                        progressLen += contentLen;
                        if (FileProgressEvent != null)
                            FileProgressEvent(contentLen * 100 / fileStream.Length);
                        contentLen = fileStream.Read(buffer, 0, buffLength);
                    }
                    if (FileProgressEvent != null)
                        FileProgressEvent(100);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    if (uploadStream != null)
                    {
                        uploadStream.Close();
                        uploadStream.Dispose();
                    }
                    if (fileStream != null)
                    {
                        fileStream.Close();
                        fileStream.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        public bool DownloadFile(string filePath, string ftpPath)
        {
            if (_isOnline)
            {
                Stream ftpStream = null;
                FileStream fileStream = null;
                try
                {
                    if (!Connect(ftpPath))
                    {
                        return false;
                    }
                    if (string.IsNullOrEmpty(Path.GetExtension(filePath)))
                    {
                        filePath = filePath + @"\" + Path.GetFileName(ftpPath);
                    }
                    string dirName = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(dirName))
                        Directory.CreateDirectory(dirName);

                    _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();
                    ftpStream = _ftpResponse.GetResponseStream();

                    int buffLength = 2048;
                    byte[] buffer = new byte[buffLength];
                    int contentLen = ftpStream.Read(buffer, 0, buffLength);
                    int progressLen = 0;
                    fileStream = new FileStream(filePath, FileMode.Create);
                    while (contentLen > 0)
                    {
                        Application.DoEvents();
                        fileStream.Write(buffer, 0, contentLen);
                        progressLen += contentLen;
                        if (FileProgressEvent != null)
                            FileProgressEvent(contentLen * 100 / _ftpResponse.ContentLength);
                        contentLen = ftpStream.Read(buffer, 0, buffLength);
                    }
                    if (FileProgressEvent != null)
                        FileProgressEvent(100);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    if (_ftpResponse != null)
                    {
                        _ftpResponse.Close();
                        _ftpResponse = null;
                    }
                    if (ftpStream != null)
                    {
                        ftpStream.Close();
                        ftpStream.Dispose();
                    }
                    if (fileStream != null)
                    {
                        fileStream.Close();
                        fileStream.Dispose();
                    }
                }
            }
            else
            {
                FileStream ftpStream = null;
                FileStream fileStream = null;
                try
                {
                    if (!CheckFile(ftpPath)) return false;
                    if (string.IsNullOrEmpty(Path.GetExtension(filePath)))
                    {
                        filePath = filePath + @"\" + Path.GetFileName(ftpPath);
                    }
                    string dirName = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(dirName))
                        Directory.CreateDirectory(dirName);

                    ftpStream = new FileStream(_localPath + ftpPath, FileMode.Open);
                    int buffLength = 2048;
                    byte[] buffer = new byte[buffLength];
                    int contentLen = ftpStream.Read(buffer, 0, buffLength);
                    int progressLen = 0;
                    fileStream = new FileStream(filePath, FileMode.Create);
                    while (contentLen > 0)
                    {
                        fileStream.Write(buffer, 0, contentLen);
                        progressLen += contentLen;
                        if (FileProgressEvent != null)
                            FileProgressEvent(contentLen * 100 / _ftpResponse.ContentLength);
                        contentLen = ftpStream.Read(buffer, 0, buffLength);
                    }
                    if (FileProgressEvent != null)
                        FileProgressEvent(100);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    if (ftpStream != null)
                    {
                        ftpStream.Close();
                        ftpStream.Dispose();
                    }
                    if (fileStream != null)
                    {
                        fileStream.Close();
                        fileStream.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// 上传文件夹
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="ftpPath"></param>
        /// <returns></returns>
        public bool UploadDir(string dirPath, string ftpPath)
        {
            if (!CreateDir(ftpPath)) return false;

            DirectoryInfo di = new DirectoryInfo(dirPath);
            DirectoryInfo[] diArray = di.GetDirectories("*", SearchOption.AllDirectories);
            foreach (var item in diArray)
            {
                CreateDir(ftpPath + @"/" + item.FullName.Replace(dirPath + @"\", ""));
            }
            FileInfo[] fiArray = di.GetFiles("*", SearchOption.AllDirectories);
            int count = 0;
            foreach (var item in fiArray)
            {
                UploadFile(item.FullName, ftpPath + @"/" + item.FullName.Replace(dirPath + @"\", "").Replace(@"\", "/"));
                if (DirProgressEvent != null)
                    DirProgressEvent(++count * 100 / fiArray.Length);
            }
            return true;
        }
        /// <summary>
        /// 下载文件夹
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="ftpPath"></param>
        /// <returns></returns>
        public bool DownloadDir(string dirPath, string ftpPath)
        {
            if (Directory.Exists(dirPath))
            {
                try
                {
                    Directory.Delete(dirPath, true);
                }
                catch
                {
                    return false;
                }
            }
            try
            {
                Directory.CreateDirectory(dirPath);
                string[] dirs = null;
                string[] files = GetFiles(ftpPath, out dirs);
                if (dirs != null)
                {
                    foreach (var item in dirs)
                    {
                        Directory.CreateDirectory(dirPath + @"\" + item.Replace(ftpPath, ""));
                    }
                }
                int count = 0;
                if (files != null)
                {
                    foreach (var item in files)
                    {
                        DownloadFile(dirPath + @"\" + item.Replace(ftpPath, ""), item);
                        if (DirProgressEvent != null)
                            DirProgressEvent(++count * 100 / files.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 连接FTP路径
        /// </summary>
        /// <param name="filePath">FTP路径</param>
        /// <returns>是否连接成功</returns>
        private bool Connect(string filePath)
        {
            try
            {
                filePath = string.Format("ftp://{0}/{1}", _ftpIp, filePath);
                _ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(filePath));
                _ftpRequest.UseBinary = true;
                _ftpRequest.Proxy = null;
                _ftpRequest.UsePassive = false;
                _ftpRequest.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// FTP操作
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="type">操作类型</param>
        /// <param name="value">重命名时的新文件名称</param>
        /// <returns>是否操作成功</returns>
        private bool OperateFile(string filePath, string type, string value = "")
        {
            if (!Connect(filePath))
            {
                return false;
            }
            try
            {
                _ftpRequest.KeepAlive = false;
                _ftpRequest.Method = type;
                if (type == WebRequestMethods.Ftp.Rename)
                    _ftpRequest.RenameTo = value;
                _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();
                _ftpResponse.Close();
                return true;
            }
            catch
            {
                if (_ftpResponse != null)
                {
                    _ftpResponse.Close();
                }
                return false;
            }
        }
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dirPath">文件路径</param>
        /// <returns>是否删除成功</returns>
        private bool DeleteDir(string dirPath)
        {
            if (!CheckDir(dirPath)) return true;
            if (_isOnline)
                return OperateFile(dirPath, WebRequestMethods.Ftp.RemoveDirectory);
            else
            {
                Directory.Delete(_localPath + dirPath);
                return !CheckDir(dirPath);
            }
        }
        /// <summary>
        /// 获取该目录下的所有子目录和文件
        /// </summary>
        /// <param name="ftpPath">ftp路径</param>
        /// <param name="dirs">文件夹列表</param>
        /// <returns>文件列表</returns>
        public string[] GetFiles(string ftpPath, out string[] dirs)
        {
            dirs = null;
            List<string> fileList = new List<string>();
            List<string> dirList = new List<string>();
            if (_isOnline)
            {
                if (!ftpPath.EndsWith("/"))
                    ftpPath += "/";
                string[] fileName = null;
                StringBuilder result = new StringBuilder();
                try
                {
                    Connect(ftpPath);
                    _ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                    WebResponse response = _ftpRequest.GetResponse();
                    StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    string line = sr.ReadLine();
                    if (line == null) return null;
                    while (line != null)
                    {
                        result.Append(line);
                        result.Append("\n");
                        line = sr.ReadLine();
                    }
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);
                    sr.Close();
                    response.Close();
                    fileName = result.ToString().Split('\n');
                    for (int i = 0; i < fileName.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(fileName[i])) continue;
                        string fileFtpPath = string.Format(@"{0}{1}", ftpPath, fileName[i]);
                        if (!string.IsNullOrEmpty(Path.GetExtension(fileName[i])))
                        {
                            fileList.Add(fileFtpPath);
                        }
                        else
                        {
                            dirList.Add(fileFtpPath);
                            string[] tempDirs = null;
                            string[] resultArray = GetFiles(fileFtpPath, out tempDirs);
                            if (resultArray != null)
                                fileList.AddRange(resultArray);
                            if (tempDirs != null)
                                dirList.AddRange(tempDirs);
                        }
                    }
                    dirs = dirList.ToArray();
                    return fileList.ToArray();
                }
                catch
                {
                    if (_ftpResponse != null)
                    {
                        _ftpResponse.Close();
                        _ftpResponse = null;
                    }
                    return fileName;
                }
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(_localPath + ftpPath);
                DirectoryInfo[] diArray = di.GetDirectories("*", SearchOption.AllDirectories);
                foreach (var item in diArray)
                {
                    dirList.Add(item.FullName.Replace(_localPath, ""));
                }
                FileInfo[] fiArray = di.GetFiles("*", SearchOption.AllDirectories);
                foreach (var item in fiArray)
                {
                    fileList.Add(item.FullName.Replace(_localPath, ""));
                }
                dirs = dirList.ToArray();
                return fileList.ToArray();
            }
        }
    }
