namespace DocumentsBlobDemo.Controllers
{
    using System;
    using System.IO;
    using System.Web;
    using Microsoft.WindowsAzure.Storage.Blob;


    public class DocumentService
    {
        public string UploadDocument(HttpPostedFileBase documentToUpload)
        {
            string fullPath = null;

            if (documentToUpload == null || documentToUpload.ContentLength == 0)
            {
                return null;
            }

            try
            {
                var cloudStorageAccount = ConnectionString.GetConnectionString();
                var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                var cloudBlobContainer = cloudBlobClient.GetContainerReference("documents");

                if (cloudBlobContainer.CreateIfNotExists())
                {
                    cloudBlobContainer.SetPermissions(
                        new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob
                        }
                    );
                }

                var documentName = Guid.NewGuid() + "-" + Path.GetExtension(documentToUpload.FileName);

                var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(documentName);
                cloudBlockBlob.Properties.ContentType = documentToUpload.ContentType;

                cloudBlockBlob.UploadFromStream(documentToUpload.InputStream);

                fullPath = cloudBlockBlob.Uri.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return fullPath;
        }
    }
}