using Library.Domain.Data;
using System;

namespace Library.Dtos
{
    public class BookDto : Book
    {
        public string IssueDate { get; set; } = DateTime.Now.ToString();
        public string? ReturnDate { get; set; }
    }
}