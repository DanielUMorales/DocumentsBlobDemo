namespace DocumentsBlobDemo.Controllers
{
    using Microsoft.Azure;
    using Microsoft.WindowsAzure.Storage;

    public static class ConnectionString
    {
        private static readonly string Account = CloudConfigurationManager.GetSetting("StorageAccountName");
        private static readonly string Key = CloudConfigurationManager.GetSetting("StorageAccountKey");

        public static CloudStorageAccount GetConnectionString()
        {
            string connectionString = $"DefaultEndpointsProtocol=https;AccountName={Account};AccountKey={Key}";
            return CloudStorageAccount.Parse(connectionString);
        }
    }
}