﻿using BookingApp.Serializer;
using BookingApp.WPF.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BookingApp.Domain.Model {
    public class TourReview : ISerializable, IIdentifiable
    {
        public int Id { get; set; }
        public int KnowledgeGrade { get; set; }
        public int LanguageGrade { get; set; }
        public int InterestGrade { get; set; }
        public double AvrageGrade { get; set; }
        public DateTime Posted { get; set; }
        public bool IsValid { get; set; }
        public int TouristId { get; set; }
        public int TourId { get; set; }
        public string Comment { get; set; }
        public List<string> Pictures { get; set; }

        public TourReview()
        {
            Pictures = new List<string>();
        }
        public TourReview(int id, int knowledgeGrade, int languageGrade, int interestGrade, double avrageGrade, DateTime posted, bool isValid, int touristId, int tourId, string comment, List<string> pictures)
        {
            Id = id;
            KnowledgeGrade = knowledgeGrade;
            LanguageGrade = languageGrade;
            InterestGrade = interestGrade;
            AvrageGrade = avrageGrade;
            Posted = posted;
            IsValid = isValid;
            TouristId = touristId;
            TourId = tourId;
            Comment = comment;
            Pictures = pictures;
        }

        public TourReview(TourReviewDTO tourReview)
        {
            KnowledgeGrade = tourReview.KnowledgeGrade;
            LanguageGrade = tourReview.LanguageGrade;
            InterestGrade = tourReview.InterestGrade;
            AvrageGrade = (tourReview.KnowledgeGrade + tourReview.InterestGrade + tourReview.LanguageGrade) / 3.0;
            TouristId = tourReview.TouristId;
            TourId = tourReview.TourId;
            Comment = tourReview.Comment;
            Pictures = tourReview.Pictures;
            Posted = DateTime.Now;
            IsValid = true;
        }
        public string[] ToCSV()
        {
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


            if (Pictures != null)
            {
                foreach (string pic in Pictures)
                {
                    Array.Resize(ref csvValues, csvValues.Length + 1);
                    csvValues[csvValues.Length - 1] = pic;
                }
            }

            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            KnowledgeGrade = int.Parse(values[1]);
            LanguageGrade = int.Parse(values[2]);
            InterestGrade = int.Parse(values[3]);
            AvrageGrade = Convert.ToDouble(values[4]);
            Posted = DateTime.ParseExact(values[5], "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            IsValid = bool.Parse(values[6]);
            TouristId = int.Parse(values[7]);
            TourId = int.Parse(values[8]);
            Comment = values[9];
            Pictures.AddRange(values[10..]);
        }
    }
}
