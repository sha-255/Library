using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Data
{
    public class Reader
    {
        [Key]
        public int Id { get; set; }
        private string? name;
        public string Name 
        { 
            get => name?.Normalize().Trim(); 
            set => name = value;
        }
        public DateTime? Birthday { get; set; }
        private string? addressAndPhone;
        public string? AddressAndPhone 
        { 
            get => addressAndPhone?.Normalize().Trim(); 
            set => addressAndPhone = value; 
        }
        private string? group;
        public string? Group 
        { 
            get => group?.Normalize().Trim(); 
            set => group = value; 
        }
        public DateTime RecordingDate { get; set; }
        public string? books;
        public string? Books 
        { 
            get => books?.Normalize().Trim(); 
            set => books = value;
        }
    }
}
