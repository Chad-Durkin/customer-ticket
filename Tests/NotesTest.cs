using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Ticketizer
{
    public class NoteTest : IDisposable
    {

        public DateTime date1 = new DateTime(2008, 5, 1, 8, 30, 52);

        public NoteTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ticketizer_test;Integrated Security=SSPI;";
        }

        //Empty on load
        [Fact]
        public void Note_EmptyOnLoad()
        {
            Assert.Equal(0, Note.GetAll().Count);
        }

        //Equality Test
        [Fact]
        public void Equality_Test()
        {
            Note firstNote = new Note(date1, 1, 1, "Here is some note text!");
            Note secondNote = new Note(date1, 1, 1, "Here is some note text!");

            Assert.Equal(firstNote, secondNote);
        }

        //Save Test
        [Fact]
        public void SaveTest_NoteSave()
        {
            Note newNote = new Note(date1, 1, 1, "Here is some note text!");
            newNote.Save();

            Assert.Equal(newNote, Note.GetAll()[0]);
        }

        //Find Test
        [Fact]
        public void FindTest_FindNote()
        {
            Note newNote = new Note(date1, 1, 1, "Here is some note text!");
            newNote.Save();

            Assert.Equal(newNote, Note.Find(newNote.GetId()));
        }

        [Fact]
        public void DeleteSpecificTest()
        {
            Note newNote = new Note(date1, 1, 1, "Here is some note text!");
            newNote.Save();

            Note.Delete(newNote.GetId());

            List<Note> expected = new List<Note>();
            List<Note> actual = Note.GetAll();

            Assert.Equal(expected, actual);
        }

        //Update Note Test
        [Fact]
        public void UpdateNoteNameTest()
        {
            Note newNote = new Note(date1, 1, 1, "Here is some note text!");
            newNote.Save();

            Note.Update(newNote.GetId(), "some different text");


            Assert.Equal("some different text", Note.Find(newNote.GetId()).GetText());
        }


        public void Dispose()
        {
            Note.DeleteAll();
        }
    }
}
