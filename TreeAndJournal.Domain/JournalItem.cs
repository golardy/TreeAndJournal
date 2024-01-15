using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TreeAndJournal.Domain
{
    public class JournalItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long EventId { get; set; }
        public string Path { get;set;}
        public string RequestParams { get; set; }
        public string StackTrace { get; set;}
        public DateTime CreatedAt { get; set; }
    }
}
