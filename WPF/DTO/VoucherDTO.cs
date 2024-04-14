using BookingApp.Domain.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.DTO {
    public class VoucherDTO : INotifyPropertyChanged
    {
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

        private string _image;
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _expireDate;
        public DateTime ExpireDate
        {
            get { return _expireDate; }
            set
            {
                if (_expireDate != value)
                {
                    _expireDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _used;
        public bool Used
        {
            get
            {
                return _used;
            }
            set
            {
                if (_used != value)
                {
                    _used = value;
                    OnPropertyChanged();
                }
            }
        }

        public VoucherDTO(Voucher voucher)
        {
            Id = voucher.Id;
            Image = voucher.Image;
            ExpireDate = voucher.ExpireDate;
            Used = voucher.Used;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
