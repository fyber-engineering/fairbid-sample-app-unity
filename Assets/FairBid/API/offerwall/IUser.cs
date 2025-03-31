//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//
#nullable enable

using System;
using System.Collections.Generic;

namespace DigitalTurbine.OfferWall
{
    internal interface IUser
    {
        public int? Age { set; }
        public DateTime? Birthdate { set; }
        public Gender? Gender { set; }
        public SexualOrientation? SexualOrientation { set; }
        public Ethnicity? Ethnicity { set; }
        public Location? Location { set; }
        public MaritalStatus? MaritalStatus { set; }
        public int? NumberOfChildren { set; }
        public int? AnnualHouseholdIncome { set; }
        public Education? Education { set; }
        public string Zipcode { set; }
        public string[] Interests { set; }
        public bool? Iap { set; }
        public float? IapAmount { set; }
        public int? NumberOfSessions { set; }
        public long? PsTime { set; }
        public long? LastSession { set; }
        public ConnectionType? ConnectionType { set; }
        public string? Device { set; }
        public string AppVersion { set; }
        public Dictionary<string, string?> CustomParameters { set; }
    }
}