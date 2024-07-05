using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ModelLayer;
using RepoLayer.Context;
using RepoLayer.CustomException;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class NoteRL : INoteRL
    {
        private readonly ProjectContext projectContext;
        private readonly KafkaProducer _kafkaProducer;


        public NoteRL(ProjectContext projectContext, KafkaProducer kafkaProducer)
        {
            this.projectContext = projectContext;
            _kafkaProducer = kafkaProducer;
        }

        public NoteEntity AddNote(NoteML note)
        {
            NoteEntity note1 = new NoteEntity();
            note1.Title= note.Title;
            note1.Description= note.Description;
            projectContext.Notes.Add(note1);
            projectContext.SaveChanges();
            int partition = note1.Id % 2 == 0 ? 0 : 1;
            _kafkaProducer.ProduceMessageAsync(note, partition);
            return note1;
        }

        public List<NoteEntity> GetAllNotes()
        {
            if (projectContext.Notes == null)
            {
                throw new CustomException1("No notes added");
            }
            List<NoteEntity> notes = new List<NoteEntity>();    
            foreach (NoteEntity note in projectContext.Notes)
            {
                if(note.isTrashed==false && note.isArchived == false)
                {
                    notes.Add(note);
                }
            }
            return notes.ToList();
        }

        public NoteEntity GetNoteById(int id)
        {
            var note = projectContext.Notes.FirstOrDefault(x => x.Id == id);
            if(note == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            return note;
        }

        public NoteEntity RemoveNote(int id)
        {
            var note = projectContext.Notes.FirstOrDefault(x => x.Id == id);
            if (note == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            projectContext.Notes.Remove(note);
            projectContext.SaveChanges() ;
            return note;
        }

        public NoteEntity UpdateNote(int id,NoteML updatedNote)
        {
            var note = projectContext.Notes.FirstOrDefault(x => x.Id == id);
            if (note == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            note.Title = updatedNote.Title;
            note.Description = updatedNote.Description;
            projectContext.Notes.Update(note);
            projectContext.SaveChanges();
            return note;
        }
        public NoteEntity Archive(int id)
        {
            var noteToArchive=projectContext.Notes.FirstOrDefault(x=>x.Id== id);
            if(noteToArchive == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            noteToArchive.isArchived = !noteToArchive.isArchived;
            if(noteToArchive.isTrashed) 
            {
                throw new CustomException1("Note in trash cannot be sent in archived");
            }
            projectContext.Notes.Update(noteToArchive);
            projectContext.SaveChanges();
            return noteToArchive;
        }
        public NoteEntity Trash(int id)
        {
            var noteToTrash= projectContext.Notes.FirstOrDefault(x => x.Id == id);
            if (noteToTrash == null)
            {
                throw new CustomException1("Note doesn't exist");
            }
            noteToTrash.isTrashed = !noteToTrash.isTrashed;
            
            projectContext.Notes.Update(noteToTrash);
            projectContext.SaveChanges();
            return noteToTrash;
        }

        public List<NoteEntity> GetAllTrashNotes()
        {
            var result=projectContext.Notes.ToList();
            List<NoteEntity> list=new List<NoteEntity>();
            foreach(var note in result)
            {
                if (note.isTrashed)
                {
                    list.Add(note);
                }
            }
            return list;
        }

        public List<NoteEntity> GetAllArchiveNotes()
        {
            var result = projectContext.Notes.ToList();
            List<NoteEntity> list = new List<NoteEntity>();
            foreach (var note in result)
            {
                if (note.isArchived && note.isTrashed==false)
                {
                    list.Add(note);
                }
            }
            return list;
        }
    }
}
