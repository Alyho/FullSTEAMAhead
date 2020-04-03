using SinglePageUI.Models;
using SinglePageUI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SinglePageUI
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        
        Editor noteEditor;
        Image Tabbycatsleeping;
        CollectionView collectionView;
        Button saveButton, deleteButton;

        public MainPage()
        {

            BackgroundColor = Color.PowderBlue;

            BindingContext = new MainPageViewModel();

            Tabbycatsleeping = new Image
            {
                Source = "Tabbycatsleeping.jpg"
            };

            noteEditor = new Editor
            {
                Placeholder = "Enter Note",
                BackgroundColor = Color.White,
                Margin = new Thickness(10)
            };
            noteEditor.SetBinding(Editor.TextProperty, nameof(MainPageViewModel.NoteText));

            saveButton = new Button
            {
                Text = "Save",
                TextColor = Color.White,
                BackgroundColor = Color.Green,
                Margin = new Thickness(10)
            };
            saveButton.SetBinding(Button.CommandProperty, nameof(MainPageViewModel.SaveNoteCommand));


            deleteButton = new Button
            {
                Text = "Delete",
                TextColor = Color.White,
                BackgroundColor = Color.Red,
                Margin = new Thickness(10)
            };
            deleteButton.SetBinding(Button.CommandProperty, nameof(MainPageViewModel.EraseNotesCommand));

            collectionView = new CollectionView
            {
                ItemTemplate = new NotesTemplate(),
                Margin = new Thickness(2),
                SelectionMode = SelectionMode.Single
            };
            collectionView.SetBinding(CollectionView.ItemsSourceProperty, nameof(MainPageViewModel.NotesCollection));
            collectionView.SetBinding(CollectionView.SelectedItemProperty, nameof(MainPageViewModel.SelectedNote));
            collectionView.SetBinding(CollectionView.SelectionChangedCommandProperty, nameof(MainPageViewModel.NoteSelectedCommand));

            var grid = new Grid
            {
                Margin = new Thickness(20, 40),

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)}
                },

                RowDefinitions =
                {
                    new RowDefinition{Height = new GridLength(1.0, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength (2.5, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength (1.0, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength (2.0, GridUnitType.Star)},
                }
            };

            grid.Children.Add(Tabbycatsleeping, 0, 0);
            Grid.SetColumnSpan(Tabbycatsleeping, 2);

            grid.Children.Add(noteEditor, 0, 1);
            Grid.SetColumnSpan(noteEditor, 2);

            grid.Children.Add(saveButton, 0, 2);
            grid.Children.Add(deleteButton, 1, 2);

            grid.Children.Add(collectionView, 0, 3);
            Grid.SetColumnSpan(collectionView, 2);

            Content = grid;
        }

        class NotesTemplate : DataTemplate
        {
            public NotesTemplate() : base(LoadTemplate)
            {

            }

            static StackLayout LoadTemplate()
            {
                var textLabel = new Label();
                textLabel.SetBinding(Label.TextProperty, nameof(NoteModel.Text));

                var frame = new Frame
                {
                    VerticalOptions = LayoutOptions.Center,
                    Content = textLabel
                };

                return new StackLayout
                {
                    Children = { frame },
                    Padding = new Thickness(3)
                };
            }
        }
    }
}
