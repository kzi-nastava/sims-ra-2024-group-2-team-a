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
        public int LanguageId { get; set; }
        public double Score { get; set; } = 0.0;
        public bool IsSuper { get; set; } = false;

        public Guide() {
            Category = UserCategory.Guide;
        }
        public Guide(int id, string name, string surname, int languageId, double score, bool isSuper) {
            Id = id;
            Name = name;
            Surname = surname;
            LanguageId = languageId;
            Score = score;
            IsSuper = isSuper;
        }

        public override string[] ToCSV() {
            string[] cssValues = {
                Id.ToString(),
                Name,
                Surname,
                LanguageId.ToString(),
                Score.ToString(),
                IsSuper.ToString()
            };
            return cssValues;
        }

        public override void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Surname = values[2];
            LanguageId = Convert.ToInt32(values[3]);         
            Score = Convert.ToDouble(values[4]);
            IsSuper = Convert.ToBoolean(values[5]);
        }
    }
}
