using BookingApp.Domain.Model;
using BookingApp.Services;
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

        private readonly LanguageService _languageService = ServicesPool.GetService<LanguageService>();
        public GuideProfileDTO() { }
        public GuideProfileDTO(Guide g) {
            Id=g.Id;
            Name = g.Name;
            Surname = g.Surname;
            LanguageId = g.LanguageId;
            SetLanguageTemplate();
            Score = g.Score;
            IsSuper = g.IsSuper;
            SuperUntil = g.SuperUntil;
            IsFirstTime = false;
            IsHelpActive = false;
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
        public void SetLanguageTemplate() {
            if (_languageId == -1) {
                LanguageTemplate = "Not enough tours";
                return;
            }
            LanguageTemplate = _languageService.GetById(_languageId).Name;
        }
        public String LanguageTemplate { get; set; }

        private int _languageId;
        public int LanguageId {
            get {
                return _languageId;
            }
            set {
                if (_languageId != value) {
                    _languageId = value;
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
        private DateTime _superUntil;
        public DateTime SuperUntil {
            get {
                return _superUntil;
            }
            set {
                if ( _superUntil != value) {
                    _superUntil = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isFirstTime;
        public bool IsFirstTime {
            get {
                return _isFirstTime;
            }
            set {
                if (_isFirstTime != value) {
                    _isFirstTime = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isHelpActive;
        public bool IsHelpActive {
            get {
                return _isHelpActive;
            }
            set {
                if (_isHelpActive != value) {
                    _isHelpActive = value;
                    OnPropertyChanged();
                }
            }
        }
        public Guide ToModel() {
            return new Guide(Id, Name, Surname, LanguageId, Score, IsSuper, SuperUntil, IsFirstTime, IsHelpActive);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
