using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTO
{
    public class GuideProfileDTO : INotifyPropertyChanged
    {
        public GuideProfileDTO() { }
        public GuideProfileDTO(string name, string surname, double score, bool isSuper) {
            Name = name;
            Surname = surname;
            Score = score;
            IsSuper = isSuper;
        }
        public GuideProfileDTO(Guide g) {
            Id=g.Id;
            Name = g.Name;
            Surname = g.Surname;
            Score = g.Score;
            IsSuper = g.IsSuper;
        }

        private int _id;
        public int Id {
            get {
                return _id;
            }
            set {
                if (_id != value) {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name;
        public string Name {
            get {
                return _name;
            }
            set {
                if (_name != value) {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _surname;
        public string Surname {
            get {
                return _surname;
            }
            set {
                if (_surname != value) {
                    _surname = value;
                    OnPropertyChanged();
                }
            }
        }
        private double _score;
        public double Score {
            get {
                return _score;
            }
            set {
                if (_score != value) {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isSuper;
        public bool IsSuper {
            get {
                return _isSuper;
            }
            set {
                if( _isSuper != value) {
                    _isSuper = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
