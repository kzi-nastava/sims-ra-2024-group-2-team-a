using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.Utils.Validation {
    public class ValidationErrors : INotifyPropertyChanged
    {
        private readonly Dictionary<string, string> validationErrors = new Dictionary<string, string>();

        public bool IsValid
        {
            get
            {
                return this.validationErrors.Count < 1;
            }
        }

        public string this[string fieldName]
        {
            get
            {
                return this.validationErrors.ContainsKey(fieldName) ?
                    this.validationErrors[fieldName] : string.Empty;
            }



            set
            {
                if (this.validationErrors.ContainsKey(fieldName))
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        this.validationErrors.Remove(fieldName);
                    }
                    else
                    {
                        this.validationErrors[fieldName] = value;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        this.validationErrors.Add(fieldName, value);
                    }
                }
                this.OnPropertyChanged("IsValid");
            }
        }

        public void Clear()
        {
            validationErrors.Clear();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
