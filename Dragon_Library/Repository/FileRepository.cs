using Dragon_Library.Helper.Image;
using Dragon_Library.Models.Interface;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace Dragon_Library.Repository
{
    /// <summary>
    /// 上傳檔案至Azure Storage
    /// </summary>
    public class FileRepository : IFileRepository
    {
        public string UploadAudio(string containerName, string groupName, byte[] file, string file_name)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("azure.blob.connectionstring"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container. 
            CloudBlobContainer container = blobClient.GetContainerReference(containerName.ToLower());

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            container.SetPermissions(
                     new BlobContainerPermissions
                     {
                         PublicAccess =
                             BlobContainerPublicAccessType.Blob
                     });

            var fileName = groupName + "/" + file_name.Replace("%3F", "?");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            blockBlob.Properties.ContentType = "audio/m4a";
            blockBlob.UploadFromByteArray(file, 0, file.Length);

            return blockBlob.Uri.AbsoluteUri;
        }

        public void DeleteImage(string containerName, string groupName, string file_name)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("azure.blob.connectionstring"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container. 
            CloudBlobContainer container = blobClient.GetContainerReference(containerName.ToLower());

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            container.SetPermissions(
                     new BlobContainerPermissions
                     {
                         PublicAccess =
                             BlobContainerPublicAccessType.Blob
                     });

            var fileName = groupName + "/" + file_name.Replace("%3F", "?");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            blockBlob.Delete();
        }
    }
}