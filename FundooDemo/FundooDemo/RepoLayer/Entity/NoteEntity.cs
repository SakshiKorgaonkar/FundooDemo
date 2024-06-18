using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace RepoLayer.Entity
{
    public class NoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool isArchived { get; set; }=false;
        [Required]
        public bool isTrashed { get; set; }=false ;
        [JsonIgnore]
        public ICollection<LabelNoteEntity> LabelNotes { get; set;}
        [JsonIgnore]
        public ICollection<CollaboratorEntity> Collaborators { get; set;}
    }
}
