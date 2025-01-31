using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace Utils
{
    public partial class Startup
    {
        public static void UseAmazonS3(WebApplicationBuilder builder)
        {
            // Register the IAmazonS3 client with the configuration from appsettings
            builder.Services.AddSingleton<IAmazonS3>(provider =>
            {
                var accessKey = builder.Configuration["AWS:AccessKey"];
                var secretKey = builder.Configuration["AWS:SecretKey"];
                var region = builder.Configuration["AWS:Region"];

                if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(region))
                {
                    throw new InvalidOperationException("AWS S3 configuration is missing or incomplete.");
                }

                return new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
            });

            // Register the S3Helper service
            builder.Services.AddScoped<AmazonS3Helper>();
        }
    }

    public class AmazonS3Helper
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public AmazonS3Helper(IConfiguration configuration, IAmazonS3 s3Client)
        {
            _bucketName = configuration["AWS:BucketName"] ?? throw new ArgumentNullException("AWS:BucketName");
            _s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File cannot be null or empty.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = memoryStream,
                Key = file.FileName,
                BucketName = _bucketName,
                ContentType = file.ContentType
            };

            var transferUtility = new TransferUtility(_s3Client);
            await transferUtility.UploadAsync(uploadRequest);

            return file.FileName;
        }

        public async Task<List<object>> UploadFilesAsync(List<IFormFile> files)
        {
            if (files == null || !files.Any())
                throw new ArgumentException("File list cannot be null or empty.");

            var results = new List<object>();
            var transferUtility = new TransferUtility(_s3Client);

            foreach (var file in files)
            {
                if (file.Length == 0)
                {
                    results.Add(new { FileName = file.FileName, Status = "Failed: Empty file." });
                    continue;
                }

                try
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = memoryStream,
                        Key = file.FileName,
                        BucketName = _bucketName,
                        ContentType = file.ContentType
                    };

                    await transferUtility.UploadAsync(uploadRequest);
                    results.Add(new { FileName = file.FileName, Status = "Success" });
                }
                catch (Exception ex)
                {
                    results.Add(new { FileName = file.FileName, Status = $"Failed: {ex.Message}" });
                }
            }

            return results;
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("File name cannot be null or empty.");

            try
            {
                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = fileName
                };

                var response = await _s3Client.DeleteObjectAsync(deleteRequest);
                return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch (AmazonS3Exception ex)
            {
                // Log Amazon S3-specific errors if needed
                throw new Exception($"Error deleting file from S3: {ex.Message}");
            }
        }

        public async Task<List<object>> DeleteFilesAsync(List<string> fileNames)
        {
            if (fileNames == null || !fileNames.Any())
                throw new ArgumentException("File name list cannot be null or empty.");

            var results = new List<object>();

            foreach (var fileName in fileNames)
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    results.Add(new { FileName = fileName, Status = "Failed: Empty or invalid file name." });
                    continue;
                }

                try
                {
                    var success = await DeleteFileAsync(fileName);
                    results.Add(new { FileName = fileName, Status = success ? "Success" : "Failed" });
                }
                catch (Exception ex)
                {
                    results.Add(new { FileName = fileName, Status = $"Failed: {ex.Message}" });
                }
            }

            return results;
        }


    }
}
