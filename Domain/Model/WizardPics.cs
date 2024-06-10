using System;
using System.Collections.Generic;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model {
    public class WizardPics : ISerializable, IIdentifiable {
        public int Id { get; set; }
        public List<string> Pictures { get; set; } = new List<string>();

        public WizardPics() {

        }

        public WizardPics(int id, List<string> pictures) {
            Id= id;
            Pictures= pictures;
        }
        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString()
                };

            if (Pictures != null) {
                foreach (string pictures in Pictures) {
                    Array.Resize(ref csvValues, csvValues.Length + 1);
                    csvValues[csvValues.Length - 1] = pictures;
                }
            }

            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = Convert.ToInt32(values[0]);
            Pictures.AddRange(values[1..]);
        }
    }
}
