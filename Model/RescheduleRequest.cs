﻿using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model {

    public enum RescheduleRequestStatus {
        Pending,
        Approved,
        Rejected
    }

    public class RescheduleRequest : ISerializable, IIdentifiable {
        
        public int Id { get; set; } = 0;
        public RescheduleRequestStatus Status { get; set; } = RescheduleRequestStatus.Pending;
        public int ReservationId { get; set; } = 0;
        public int GuestId { get; set; } = 0;
        public int OwnerId { get; set; } = 0;
        public DateOnly OldStartDate { get; set; } = new DateOnly();
        public DateOnly NewStartDate { get; set; } = new DateOnly();
        public string OwnerComment { get; set; } = "";

        public RescheduleRequest() {}

        public RescheduleRequest(int resId, int guestId, int ownerId, DateOnly oldStartDate, DateOnly newStartDate) {
            ReservationId = resId;
            GuestId = guestId;
            OwnerId = ownerId;
            OldStartDate = oldStartDate;
            NewStartDate = newStartDate;
        }

        public RescheduleRequest(RescheduleRequestStatus status, int resId, int guestId, int ownerId, DateOnly oldStartDate, DateOnly newStartDate, string ownerComment) {
            Status = status;
            ReservationId = resId;
            GuestId = guestId;
            OwnerId = ownerId;
            OldStartDate = oldStartDate;
            NewStartDate = newStartDate;
            OwnerComment = ownerComment;
        }

        public void FromCSV(string[] values) {
            Id = int.Parse(values[0]);
            Status = (RescheduleRequestStatus) Enum.Parse(typeof(RescheduleRequestStatus), values[1]);
            ReservationId = int.Parse(values[2]);
            GuestId = int.Parse(values[3]);
            OwnerId = int.Parse(values[4]);
            OldStartDate = DateOnly.ParseExact(values[5], "dd-MM-yyyy");
            NewStartDate = DateOnly.ParseExact(values[6], "dd-MM-yyyy");
            OwnerComment = values[7];
        }

        public string[] ToCSV() {
            string[] csvValues = {
                Id.ToString(),
                Status.ToString(),
                ReservationId.ToString(),
                GuestId.ToString(),
                OwnerId.ToString(),
                OldStartDate.ToString("dd-MM-yyyy"),
                NewStartDate.ToString("dd-MM-yyyy"),
                OwnerComment
            };

            return csvValues;
        }
    }
}
