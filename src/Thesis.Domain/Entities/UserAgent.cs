using System;
using Thesis.Domain.Commons;

namespace Thesis.Domain.Entities
{
    public class UserAgent : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

        public string Raw { get; set; }

        public string BrowserFamily { get; set; }
        public string BrowserMajorVersion { get; set; }
        public string BrowserMinorVersion { get; set; }

        public string OSFamily { get; set; }
        public string OSMajorVersion { get; set; }
        public string OSMinorVersion { get; set; }

        public string DeviceFamily { get; set; }
    }
}
