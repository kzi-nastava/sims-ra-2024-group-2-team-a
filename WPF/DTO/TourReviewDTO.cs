using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.DTO {
    public class TourReviewDTO : INotifyPropertyChanged
    {
        public TourReviewDTO()
        {
            Pictures = new List<string>();
        }
        public TourReviewDTO(TourReview t)
        {
            Id = t.Id;
            KnowledgeGrade = t.KnowledgeGrade;
            LanguageGrade = t.LanguageGrade;
            InterestGrade = t.InterestGrade;
            AvrageGrade = t.AvrageGrade;
            Posted = t.Posted;
            IsValid = t.IsValid;
            TouristId = t.TouristId;
            TourId = t.TourId;
            Comment = t.Comment;
            Pictures = t.Pictures;
        }
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value; OnPropertyChanged();
                }
            }
        }
        private int _knowledgeGrade;
        public int KnowledgeGrade
        {
            get { return _knowledgeGrade; }
            set
            {
                if (_knowledgeGrade != value)
                {
                    _knowledgeGrade = value; OnPropertyChanged();
                }
            }
        }
        private int _languageGrade;
        public int LanguageGrade
        {
            get { return _languageGrade; }
            set
            {
                if (_languageGrade != value)
                {
                    _languageGrade = value; OnPropertyChanged();
                }
            }
        }
        private int _interestGrade;
        public int InterestGrade
        {
            get { return _interestGrade; }
            set
            {
                if (_interestGrade != value)
                {
                    _interestGrade = value; OnPropertyChanged();
                }
            }
        }
        private double _avrageGrade;
        public double AvrageGrade
        {
            get { return _avrageGrade; }
            set
            {
                if (_avrageGrade != value)
                {
                    _avrageGrade = value; OnPropertyChanged();
                }
            }
        }

        private DateTime _posted;
        public DateTime Posted
        {
            get { return _posted; }
            set
            {
                if (_posted != value)
                {
                    _posted = value; OnPropertyChanged();
                }
            }
        }

        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                if (_isValid != value)
                {
                    _isValid = value; OnPropertyChanged();
                }
            }
        }
        private int _touristId;
        public int TouristId
        {
            get { return _touristId; }
            set
            {
                if (_touristId != value)
                {
                    _touristId = value; OnPropertyChanged();
                }
            }
        }
        private int _tourId;
        public int TourId
        {
            get { return _tourId; }
            set
            {
                if (_tourId != value)
                {
                    _tourId = value; OnPropertyChanged();
                }
            }
        }
        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value; OnPropertyChanged();
                }
            }
        }
        private string _touristName;
        public string TouristName
        {
            get { return _touristName; }
            set
            {
                if (_touristName != value)
                {
                    _touristName = value; OnPropertyChanged();
                }
            }
        }
        private string _touristSurname;
        public string TouristSurname
        {
            get { return _touristSurname; }
            set
            {
                if (_touristSurname != value)
                {
                    _touristSurname = value; OnPropertyChanged();
                }
            }
        }
        private string _pointOfInterestName;
        public string PointOfInterestName
        {
            get { return _pointOfInterestName; }
            set
            {
                if (_pointOfInterestName != value)
                {
                    _pointOfInterestName = value; OnPropertyChanged();
                }
            }
        }

        public TourReview ToModel()
        {
            return new TourReview(Id, KnowledgeGrade, LanguageGrade, InterestGrade, AvrageGrade, Posted, IsValid, TouristId, TourId, Comment, Pictures);
        }
        public List<string> Pictures { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
