using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Data
{
    public class Reader
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string? AddressAndPhone { get; set; }
        public string? Group { get; set; }
        public DateTime RecordingDate { get; set; }
        public string? Books { get; set; }
    }
}
