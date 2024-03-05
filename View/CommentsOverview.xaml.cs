using BookingApp.Model;
using BookingApp.Repository;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for CommentsOverview.xaml
    /// </summary>
    public partial class CommentsOverview : Window
    {

        public static ObservableCollection<Comment> Comments { get; set; }

        public Comment SelectedComment { get; set; }

        public User LoggedInUser { get; set; }

        private readonly CommentRepository _repository;

        /*private readonly AccomodationRepository _accomodationRep;

        private readonly LocationRepository _locationRep;*/

        public CommentsOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new CommentRepository();
            Comments = new ObservableCollection<Comment>(_repository.GetByUser(user));

            //Testiranje Repositorija

            /*_accomodationRep = new AccomodationRepository();
            _locationRep = new LocationRepository();
            string[] profPics = {};

            Location loc = new Location("Ns","Srb");
            loc = _locationRep.Save(loc);
            _locationRep.Delete(loc);

            Accomodation acc = new Accomodation("Ime", loc, AccomodationType.apartment,
            5, 1, 5, profPics);
            acc.Id = 0;

            _accomodationRep.Save(acc);
            acc.Name = "Novo ime";
            _accomodationRep.Update(acc);
            _accomodationRep.Delete(acc);*/
        }

        private void ShowCreateCommentForm(object sender, RoutedEventArgs e)
        {
            CommentForm createCommentForm = new CommentForm(LoggedInUser);
            createCommentForm.Show();
        }

        private void ShowViewCommentForm(object sender, RoutedEventArgs e)
        {
            if (SelectedComment != null)
            {
                CommentForm viewCommentForm = new CommentForm(SelectedComment);
                viewCommentForm.Show();
            }
        }

        private void ShowUpdateCommentForm(object sender, RoutedEventArgs e)
        {
            if (SelectedComment != null)
            {
                CommentForm updateCommentForm = new CommentForm(SelectedComment, LoggedInUser);
                updateCommentForm.Show();
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (SelectedComment != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete comment",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _repository.Delete(SelectedComment);
                    Comments.Remove(SelectedComment);
                }
            }
        }
    }
}
