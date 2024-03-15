using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BookingApp.Serializer
{
    public class Serializer<T> where T: ISerializable, new()
    {
        private const char Delimiter = '|';
        private readonly string _directoryName = AppDomain.CurrentDomain.BaseDirectory + "../../../Resources/Data";
        private readonly string _fileName = AppDomain.CurrentDomain.BaseDirectory + @"../../../Resources/Data/{0}";

        public Serializer() {
            if (!Directory.Exists(_directoryName)) {
                Directory.CreateDirectory(_directoryName);
            }

            _fileName = string.Format(_fileName, typeof(T).Name.ToLower() + ".csv");
        }

        public void ToCSV(List<T> objects)
        {
            StringBuilder csv = new StringBuilder();

            foreach(T obj in objects)
            {
                string line = string.Join(Delimiter.ToString(), obj.ToCSV());
                csv.AppendLine(line);
            }

            File.WriteAllText(_fileName, csv.ToString());
        }

        public List<T> FromCSV()
        {
            List<T> objects = new List<T>();

            foreach(string line in File.ReadLines(_fileName))
            {
                string[] csvValues = line.Split(Delimiter);
                T obj = new T();
                obj.FromCSV(csvValues);
                objects.Add(obj);
            }

            return objects;
        }
    }
}