using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Abstract;
using Microsoft.AspNetCore.Http;

namespace Entities
{
    [Table("Regulations")] // Esasname
    public class Regulations : IEntity
    {
        [Key]
        [Column("ID")]
        public int id { get; set; } = 1;
        [Column("TITLE")]
        public string title { get; set; }
        [Column("DESCRIPTION")]
        public string description { get; set; }
        [Column("PHOTO_PATH")]
        public string photoPath { get; set; }
        [Column("FILE_PATH")]
        public string filePath { get; set; }
        [Column("LAST_UPDATED")]
        public DateTime lastUpdated { get; set; }

        // Properties to display photo and file in the admin panel and web page
        // Not migrated

        [NotMapped]
        // The file content as Base64
        public string FileBase64 { get; set; }

        [NotMapped]
        // Optional: original file name
        public string FileName { get; set; }

        [NotMapped]
        // Optional: content type
        public string FileContentType { get; set; }

        [NotMapped]
        // The file content as Base64
        public string PhotoBase64 { get; set; }

        [NotMapped]
        // Optional: original file name
        public string PhotoName { get; set; }

        [NotMapped]
        // Optional: content type
        public string PhotoContentType { get; set; }

        public Regulations()
        {
             
        }

        public Regulations(string title, string description, string photoPath, string filePath, DateTime lastUpdated)
        {
            this.title = title;
            this.description = description;
            this.photoPath = photoPath;
            this.filePath = filePath;
            this.lastUpdated = lastUpdated;
        }

        public Regulations(int id, string title, string description, string photoPath, string filePath, DateTime lastUpdated, string fileBase64, string fileName, string fileContentType, string photoBase64, string photoName, string photoContentType)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.photoPath = photoPath;
            this.filePath = filePath;
            this.lastUpdated = lastUpdated;
            FileBase64 = fileBase64;
            FileName = fileName;
            FileContentType = fileContentType;
            PhotoBase64 = photoBase64;
            PhotoName = photoName;
            PhotoContentType = photoContentType;
        }
    }
}