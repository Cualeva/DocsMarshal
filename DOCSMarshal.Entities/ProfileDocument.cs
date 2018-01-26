using System;
namespace DocsMarshal.Entities
{
    public class ProfileDocument
    {
        public byte[] Content()
        {
            if (string.IsNullOrWhiteSpace(FileBase64Content)) return new byte[0];
            return Convert.FromBase64String(FileBase64Content);
        }

        public Guid FileStorageId { get; set; }
        public string FileName { get; set; }
        public string FileSha256 { get; set; }
        public string FileBase64Content { get; set; }
        public double FileSize { get; set; }
        public int FileVersion { get; set; }
        public DateTime LastUpdate { get; set; }
        public int Owner { get; set; }
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public string FieldExternalId { get; set; }
        public string FieldDescription { get; set; }
    }
}

