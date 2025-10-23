using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BaseRegulations
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string photoPath { get; set; }
        public string filePath { get; set; }
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
        public string ContentType { get; set; }

        public BaseRegulations()
        {
            
        }

        public BaseRegulations(int id)
        {
            this.id = id;
        }

        public BaseRegulations(int id, string title, string description, string photoPath, string filePath, DateTime lastUpdated) : this(id)
        {
            this.title = title;
            this.description = description;
            this.photoPath = photoPath;
            this.filePath = filePath;
            this.lastUpdated = lastUpdated;
        }

        public BaseRegulations(int id, string title, string description, string photoPath, string filePath, DateTime lastUpdated, string fileBase64, string fileName, string contentType) : this(id, title, description, photoPath, filePath, lastUpdated)
        {
            FileBase64 = fileBase64;
            FileName = fileName;
            ContentType = contentType;
        }
    }
}
