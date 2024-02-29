namespace Shared
{
    public class ResponseCodes
    {
        public const int Ok = 0;
        public const int Unknown = 1;
        public const int NotSupported = 2;
        public const int NotFound = 404;
        public const int ValidationFailed = 455;

        public const int CmsReleaseError = 700;

        public const int S3BucketCreateObject = 1000;
        public const int S3BucketDeleteObject = 1001;
        public const int S3BucketDownloadObject = 1002;
        public const int S3BucketDownloadResource = 1003;
        public const int S3BucketUploadObject = 1004;
        public const int S3BucketRequestExeption = 1005;
        public const int S3BucketCopySiteObjects = 1006;
        public const int S3BucketGetListObject = 1007;
        public const int S3BucketGetListObjects = 1008;
        public const int S3BucketUploadFolderObject = 1009;
        public const int S3BucketDeleteDBBackupFolder = 1010;
        public const int S3BucketResourceNotExists = 1011;
        public const int S3BucketDownloadFolder = 1012;
        public const int S3BucketFolderNotExists = 1013;
        public const int S3BucketFolderAlreadyExists = 1014;
        public const int S3BucketGetFileContent = 1015;
        public const int S3BucketUpdateFileContent = 1016;
        public const int S3BucketSetIdTagsToS3 = 1017;

        public const int SiteCreateExistDirectory = 1100;
        public const int SiteCreateEmptyTaskData = 1101;
        public const int SiteCreateDbScriptNotFound = 1102;
        public const int SiteCreateInvalidListeningPort = 1103;
        public const int SiteCopyS3FolderAlreadyExists = 1104;
        public const int SiteCopyS3ResourceNotExists = 1105;

        public const int UbuntuRestoreDatabaseBackupFolderNotExists = 1200;
        public const int UbuntuRestoreDatabaseBackupFolderIsEmpty = 1201;
        public const int UbuntuFailedExecuteScript = 1202;
        public const int UbuntuServiceFileAlreadyExists = 1203;
        public const int UbuntuInvalidNginxConfiruration = 1204;

        public const int AWSLambdaFunctionError = 1300;

        public const int DBBackupDirextoryNotFound = 1400;
        public const int SourceDBNotExists = 1401;

        public const int FileSistemDirectoryNotFoundOrNotEmpty = 1500;

        public const int RestRequestError = 1600;

        public const int JsonSerializationError = 1700;
        public const int JsonDeserializationError = 1701;

        public const int InvalidDataCenterId = 1800;
        public const int InvalidDeployAgentEntity = 1801;

        public const int BackupDBNotFound = 1900;
        public const int GetBackupResourse = 1901;
        public const int SiteBaseFolderNotExists = 1902;

        public const int DuplicateServerName = 2000;
        public const int ServerNotFound = 2001;

        public const int CreateCertificateException = 2100;
        public const int DeleteCertificateException = 2101;
        public const int InstallCertificateException = 2102;
        public const int CertificateNotExistsException = 2103;

        public const int BalancerFindTextException = 2200;

        public const int Ec2ClientErrorResponse = 2500;
        public const int CloudFlareErrorResponse = 2600;

        public const int SlaveNotAllowedIp = 2700;
        public const int BalancerNotAllowedIp = 2701;


    }
}
