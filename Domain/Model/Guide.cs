using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Guide : User{
        public string Name { get; set; }
        public string Surname { get ; set; }
        public double Score { get; set; } = 0.0;
        public bool IsSuper { get; set; } = false;

        public Guide() {
            Category = UserCategory.Guide;
        }

        public override string[] ToCSV() {
            string[] cssValues = {
                Id.ToString(),
                Name,
                Surname,
                Score.ToString(),
                IsSuper.ToString()
            };
            return cssValues;
        }

        public override void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Surname = values[2];
            Score = Convert.ToDouble(values[3]);
            IsSuper = Convert.ToBoolean(values[4]);
        }
    }
}
