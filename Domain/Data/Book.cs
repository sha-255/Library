using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Data
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public int InventoryNumber { get; set; }
        private string nameAndAuthor;
        public string NameAndAuthor 
        { 
            get => nameAndAuthor.Normalize().Trim();
            set => nameAndAuthor = value; 
        }
    }
}
