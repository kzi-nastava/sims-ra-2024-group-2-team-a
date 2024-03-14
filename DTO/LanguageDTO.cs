using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO {
    public class LanguageDTO : INotifyPropertyChanged {
        

        public LanguageDTO() { }
        public LanguageDTO(string name) {
            Name = name;
        }

        public LanguageDTO(Language language) {
            Id = language.Id;
            Name = language.Name;
        }

        private int _id = -1;
        public int Id {
            get {
                return _id;
            }
            set {
                if (value != _id) {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name = "";
        public string Name {
            get {
                return _name;
            }
            set {
                if (value != _name) {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LanguageOptionTemplate => $"{Name}";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
