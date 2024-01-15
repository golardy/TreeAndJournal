using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TreeAndJournal.Domain
{
    public class JournalItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Path { get;set;}
        public string Params { get; set;}
        public string StackTrace { get; set;}
        public DateTime CreatedAt { get; set; }
    }
}
