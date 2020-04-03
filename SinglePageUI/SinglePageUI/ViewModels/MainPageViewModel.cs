using SinglePageUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace SinglePageUI.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            NotesCollection = new ObservableCollection<NoteModel>();

            SaveNoteCommand = new Command(() =>
            {
                var note = new NoteModel
                {
                    Text = NoteText
                };

                NotesCollection.Add(note);
                NoteText = string.Empty;
            },
            () => !string.IsNullOrEmpty(NoteText));

            EraseNotesCommand = new Command(() =>
            {
                NotesCollection.Clear();
            });

            NoteSelectedCommand = new Command(async() =>
            {
                if (SelectedNote is null)
                    return;

                var detailViewModel = new DetailPageViewModel
                {
                    NoteText = SelectedNote.Text
                };

                await Application.Current.MainPage.Navigation.PushAsync(new DetailPage());

                SelectedNote = null;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        string noteText;
        public string NoteText
        {
            get => noteText;
            set
            {
                noteText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(noteText));
                SaveNoteCommand.ChangeCanExecute();
            }
        }

        NoteModel selectedNote;
        public NoteModel SelectedNote
        {
            get => selectedNote;
            set
            {
                selectedNote = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedNote)));
            }
        }

        public ObservableCollection<NoteModel> NotesCollection
        {
            get;
        }

        public Command SaveNoteCommand
        {
            get;
        }

        public Command EraseNotesCommand
        {
            get;
        }

        public Command NoteSelectedCommand
        {
            get;
        }
    }
}
