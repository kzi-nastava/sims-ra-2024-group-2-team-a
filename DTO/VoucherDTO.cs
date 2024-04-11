using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO {
    public class VoucherDTO : INotifyPropertyChanged {
        private string _image;
        public string Image {
            get {
                return _image;
            }
            set {
                if (_image != value) {
                    _image = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _expireDate;
        public DateTime ExpireDate {
            get { return _expireDate; }
            set {
                if (_expireDate != value) {
                    _expireDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public VoucherDTO(Voucher voucher) {
            Image = voucher.Image;
            ExpireDate = voucher.ExpireDate;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
