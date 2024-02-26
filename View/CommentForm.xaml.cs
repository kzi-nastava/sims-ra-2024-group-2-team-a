using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for CommentForm.xaml
    /// </summary>
    public partial class CommentForm : Window
    {

        public User LoggedInUser { get; set; }

        public Comment SelectedComment { get; set; }

        private readonly CommentRepository _repository;

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                if (value != _text)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        

        public CommentForm(User user)
        {
            InitializeComponent();
            Title = "Create new comment";
            DataContext = this;
            LoggedInUser = user;
            _repository = new CommentRepository();
        }

        public CommentForm(Comment selectedComment)
        {
            InitializeComponent();
            DataContext = this;
            Title = "View comment";
            txtCommentText.IsEnabled = false;
            btnSave.Visibility = Visibility.Collapsed;
            SelectedComment = selectedComment;
            Text = selectedComment.Text;
            _repository = new CommentRepository();
        }

        public CommentForm(Comment selectedComment, User user)
        {
            InitializeComponent();
            DataContext = this;
            Title = "Update comment";
            LoggedInUser = user;
            SelectedComment = selectedComment;
            Text = selectedComment.Text;
            _repository = new CommentRepository();
        }

        private void SaveComment(object sender, RoutedEventArgs e)
        {

            if(SelectedComment != null)
            {
                SelectedComment.Text = Text;
                SelectedComment.CreationTime = DateTime.Now;
                Comment updatedComment = _repository.Update(SelectedComment);
                if (updatedComment != null)
                {
                    // Update observable collection
                    int index = CommentsOverview.Comments.IndexOf(SelectedComment);
                    CommentsOverview.Comments.Remove(SelectedComment);
                    CommentsOverview.Comments.Insert(index, updatedComment);
                }
            } 
            else
            {
                Comment newComment = new Comment(DateTime.Now, Text, LoggedInUser);
                Comment savedComment = _repository.Save(newComment);
                CommentsOverview.Comments.Add(savedComment);
            }
            
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e) 
        { 
            Close();
        } 
    }
}
