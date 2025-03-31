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
    public static class User
    {
        private static readonly IUser UserImpl =
            #if UNITY_ANDROID
                new AndroidUser()
            #elif UNITY_IOS
                new IOSUser()
            #else
                null
            #endif
            ;

        public static int? Age
        {
            set => UserImpl.Age = value;
        }

        public static DateTime? Birthdate
        {
            set => UserImpl.Birthdate = value;
        }

        public static Gender? Gender
        {
            set => UserImpl.Gender = value;
        }

        public static SexualOrientation? SexualOrientation
        {
            set => UserImpl.SexualOrientation = value;
        }

        public static Ethnicity? Ethnicity
        {
            set => UserImpl.Ethnicity = value;
        }

        public static Location? Location
        {
            set => UserImpl.Location = value;
        }

        public static MaritalStatus? MaritalStatus
        {
            set => UserImpl.MaritalStatus = value;
        }

        public static int? NumberOfChildren
        {
            set => UserImpl.NumberOfChildren = value;
        }

        public static int? AnnualHouseholdIncome
        {
            set => UserImpl.AnnualHouseholdIncome = value;
        }

        public static Education? Education
        {
            set => UserImpl.Education = value;
        }

        public static string Zipcode
        {
            set => UserImpl.Zipcode = value;
        }

        public static string[] Interests
        {
            set => UserImpl.Interests = value;
        }

        public static bool? Iap
        {
            set => UserImpl.Iap = value;
        }

        public static float? IapAmount
        {
            set => UserImpl.IapAmount = value;
        }

        public static int? NumberOfSessions
        {
            set => UserImpl.NumberOfSessions = value;
        }

        public static long? PsTime
        {
            set => UserImpl.PsTime = value;
        }

        public static long? LastSession
        {
            set => UserImpl.LastSession = value;
        }

        public static ConnectionType? ConnectionType
        {
            set => UserImpl.ConnectionType = value;
        }

        public static string? Device
        {
            set => UserImpl.Device = value;
        }

        public static string AppVersion
        {
            set => UserImpl.AppVersion = value;
        }

        public static Dictionary<string, string?> CustomParameters
        {
            set => UserImpl.CustomParameters = value;
        }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public enum SexualOrientation
    {
        Heterosexual,
        Homosexual,
        Bisexual,
        Other
    }

    public enum Ethnicity
    {
        Asian,
        Black,
        Hispanic,
        White,
        Other,
    }

    public class Location
    {
        public double Long
        {
            get;
            set;
        }

        public double Lat
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"{Lat}:{Long}";
        }
    }

    public enum MaritalStatus
    {
        Single,
        Married,
        Divorced,
        Engaged,
        Relationship
    }

    public enum Education
    {
        Highschool,
        Bachelor,
        Master,
        Phd,
        Other,
    }

    public enum ConnectionType
    {
        Wifi,
        Cellular,
    }
}