using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class TourReview : ISerializable, IIdentifiable {
        public int Id { get; set; }
        public int KnowledgeGrade {  get; set; }
        public int LanguageGrade { get; set; }
        public int InterestGrade { get; set; }
        public double AvrageGrade {  get; set; }
        public String Comment {  get; set; }
        public List<string> Pictures {  get; set; }
        
        public TourReview() {
            Pictures = new List<string>();
        }
        public TourReview(int id, int knowledgeGrade, int languageGrade, int interestGrade, double avrageGrade, string comment, List<string> pictures) {
            Id = id;
            KnowledgeGrade = knowledgeGrade;
            LanguageGrade = languageGrade;
            InterestGrade = interestGrade;
            AvrageGrade = avrageGrade;
            Comment = comment;
            Pictures = pictures;
        }
        public TourReview(int knowledgeGrade, int languageGrade, int interestGrade, double avrageGrade, string comment, List<string> pictures) {
            KnowledgeGrade = knowledgeGrade;
            LanguageGrade = languageGrade;
            InterestGrade = interestGrade;
            AvrageGrade = avrageGrade;
            Comment = comment;
            Pictures = pictures;
        }

        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                KnowledgeGrade.ToString(),
                LanguageGrade.ToString(),
                InterestGrade.ToString(),
                AvrageGrade.ToString(),
                Comment,
                };


             if (Pictures != null) {
                foreach (string pic in Pictures) {
                    Array.Resize(ref csvValues, csvValues.Length + 1);
                    csvValues[csvValues.Length - 1] = pic;
                }
            }

            return csvValues;
        
        }

        public void FromCSV(string[] values) {
            Id = int.Parse(values[0]);
            KnowledgeGrade = int.Parse(values[1]);
            LanguageGrade = int.Parse(values[2]);
            InterestGrade = int.Parse(values[3]);
            AvrageGrade = int.Parse(values[4]);
            Comment = values[5];
            Pictures.AddRange(values[6..]);
        }
    }
}
