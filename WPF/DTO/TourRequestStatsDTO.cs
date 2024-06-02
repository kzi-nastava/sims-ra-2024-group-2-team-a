using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTO
{
    public class TourRequestStatsDTO : INotifyPropertyChanged{
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _titleTemplate, _descriptionTemplate, _time;
        public string TitleTemplate {
            get {
                return _titleTemplate;
            }
            set {
                if(value != _titleTemplate) {
                    _titleTemplate = value;
                    OnPropertyChanged();
                }
            }
        }
        public string DescriptionTemplate {
            get {
                return _descriptionTemplate;
            }
            set {
                if (value != _descriptionTemplate) {
                    _descriptionTemplate = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Time {
            get {
                return _time;
            }
            set {
                if (value != _time) {
                    _time = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TimeSelected { get; set; } = 0;
        public bool IsLocationSelected { get; set; } = false;
        public int LocationLanguageId { get; set; } = -1;
        public TourRequestStatsDTO() {
            TitleTemplate = "";
            DescriptionTemplate = "";
            Time = "";
        }
    }
}
