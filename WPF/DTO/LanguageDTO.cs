using BookingApp.Domain.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.DTO {
    public class LanguageDTO : INotifyPropertyChanged
    {


        public LanguageDTO() { }
        public LanguageDTO(string name)
        {
            Name = name;
        }

        public LanguageDTO(Language language)
        {
            Id = language.Id;
            Name = language.Name;
        }

        private int _id = -1;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name = "";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LanguageOptionTemplate => $"{Name}";

        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
