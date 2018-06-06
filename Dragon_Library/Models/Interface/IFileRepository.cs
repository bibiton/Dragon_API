using System.IO;
using System.Web;

namespace Dragon_Library.Models.Interface
{
    /// <summary>
    /// 新增IFileRepository介面，開放兩個參數作為分類依據
    /// </summary>
    public interface IFileRepository
    {
        string UploadAudio(string containerName, string groupName, byte[] file, string file_name);
        void DeleteImage(string containerName, string groupName, string file_name);
    }
}