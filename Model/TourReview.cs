using BookingApp.DTO;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public DateTime Posted { get; set; }
        public bool IsValid { get; set; }
        public int TouristId { get; set; }
        public int TourId {  get; set; }
        public string Comment {  get; set; }
        public List<string> Pictures {  get; set; }
        
        public TourReview() {
            Pictures = new List<string>();
        }
        public TourReview(int id, int knowledgeGrade, int languageGrade, int interestGrade, string comment, int touristId, int tourId, bool isValid, List<string> pictures) {
            Id = id;
            KnowledgeGrade = knowledgeGrade;
            LanguageGrade = languageGrade;
            InterestGrade = interestGrade;
            AvrageGrade = (knowledgeGrade + languageGrade + interestGrade)/3;
            Posted = DateTime.Now;
            IsValid = isValid;
            TouristId = touristId;
            TourId = tourId;
            Comment = comment;
            Pictures = pictures;
        }
        public TourReview(int knowledgeGrade, int languageGrade, int interestGrade, string comment, int touristId, int tourId, bool isValid, List<string> pictures) {
            KnowledgeGrade = knowledgeGrade;
            LanguageGrade = languageGrade;
            InterestGrade = interestGrade;
            AvrageGrade = (knowledgeGrade + languageGrade + interestGrade) / 3;
            Posted = DateTime.Now;
            IsValid = isValid;
            TouristId = touristId;
            TourId = tourId;
            Comment = comment;
            Pictures = pictures;
        }

        public TourReview(TourReviewDTO tourReview) {
            this.KnowledgeGrade = tourReview.KnowledgeGrade;
            this.LanguageGrade = tourReview.LanguageGrade;
            this.InterestGrade = tourReview.InterestGrade;
            this.AvrageGrade = (tourReview.KnowledgeGrade + tourReview.InterestGrade + tourReview.LanguageGrade) / 3;
            this.TouristId = tourReview.TouristId;
            this.TourId = tourReview.TourId;
            this.Comment = tourReview.Comment;
            this.Pictures = tourReview.Pictures;
            this.Posted = DateTime.Now;
            this.IsValid = true;
        }
        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                KnowledgeGrade.ToString(),
                LanguageGrade.ToString(),
                InterestGrade.ToString(),
                AvrageGrade.ToString(),
                Posted.ToString("dd-MM-yyyy HH:mm"),
                IsValid.ToString(),
                TouristId.ToString(),
                TourId.ToString(),
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
            AvrageGrade = double.Parse(values[4]);
            Posted = DateTime.ParseExact(values[5], "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            IsValid = bool.Parse(values[6]);
            TouristId = int.Parse(values[7]);
            TourId = int.Parse(values[8]);
            Comment = values[9];
            Pictures.AddRange(values[10..]);
        }
    }
}
