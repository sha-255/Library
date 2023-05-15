using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Data
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public int InventoryNumber { get; set; }
        public string NameAndAuthor { get; set; }
        public DateTime Issue { get; set; }
        public DateTime Return { get; set; }
    }
}
