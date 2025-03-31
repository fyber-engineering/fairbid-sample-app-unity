//
// FairBid Unity SDK
//
// Copyright (c) 2023 Digital Turbine. All rights reserved.
//
#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

#if UNITY_IOS

namespace DigitalTurbine.OfferWall
{
    internal class IOSUser : IUser
    {
        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_age(int age);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_birthdate(long timestamp);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_gender(string gender);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_sexual_orientation(string sexual_orientation);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_ethnicity(string ethnicity);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_location(double lat, double lon);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_marital_status(string marital_status);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_number_of_children(int children);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_annual_household_income(int income);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_education(string ethnicity);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_zip_code(string zip_code);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_interests(string interests);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_iap(bool iap);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_iap_amout(double iap_amout);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_number_of_session(double number_of_session);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_ps_time(double ps_time);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_last_session(double last_session);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_connection_type(string connection_type);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_device(string? device);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_app_version(string app_version);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_set_custom_params(string custom_params);

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_clear_age();

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_clear_number_of_children();

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_clear_annual_income();

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_clear_birthdate();

        [DllImport("__Internal")]
        private static extern void fyb_ofw_user_clear_location();

        public int? Age
        {
            set
            {
                if (value is int age)
                {
                    fyb_ofw_user_set_age(age);
                }
                else
                {
                    fyb_ofw_user_clear_age();
                }
            }
        }

        public DateTime? Birthdate
        {
            set
            {
                long? timestamp = ((DateTimeOffset?)value)?.ToUnixTimeSeconds();
                if (timestamp is long ts)
                {
                    fyb_ofw_user_set_birthdate(ts);
                }
                else
                {
                    fyb_ofw_user_clear_birthdate();
                }
            }
        }

        public Gender? Gender
        {
            set
            {
                fyb_ofw_user_set_gender(value.ToString());
            }
        }

        public SexualOrientation? SexualOrientation
        {
            set
            {
                fyb_ofw_user_set_sexual_orientation(value.ToString());
            }
        }

        public Ethnicity? Ethnicity
        {
            set
            {
                fyb_ofw_user_set_ethnicity(value.ToString());
            }
        }

        public Location? Location
        {
            set
            {
                if (value is Location l)
                {
                    fyb_ofw_user_set_location(l.Lat, l.Long);
                }
                else
                {
                    fyb_ofw_user_clear_location();
                }
            }
        }

        public MaritalStatus? MaritalStatus
        {
            set
            {
                fyb_ofw_user_set_marital_status(value.ToString());
            }
        }

        public int? NumberOfChildren
        {
            set
            {
                if (value is int numberOfChilder)
                {
                    fyb_ofw_user_set_number_of_children(numberOfChilder);
                }
                else
                {
                    fyb_ofw_user_clear_number_of_children();
                }
            }
        }

        public int? AnnualHouseholdIncome
        {
            set
            {
                if (value is int income)
                {
                    fyb_ofw_user_set_annual_household_income(income);
                }
                else
                {
                    fyb_ofw_user_clear_annual_income();
                }
            }
        }

        public Education? Education
        {
            set
            {
                fyb_ofw_user_set_education(value.ToString());
            }
        }

        public string Zipcode
        {
            set
            {
                fyb_ofw_user_set_zip_code(value.ToString());
            }
        }

        public string[] Interests
        {
            set
            {
                var enumerator = value.GetEnumerator();
                var interests = "";
                while (enumerator.MoveNext())
                {
                    interests += enumerator.Current + ",";
                }
                fyb_ofw_user_set_interests(interests);
            }
        }

        public bool? Iap
        {
            set
            {
                if (value is bool iap)
                {
                    fyb_ofw_user_set_iap(iap);
                }
            }
        }

        public float? IapAmount
        {
            set
            {
                if (value is float iapAmount)
                {
                    fyb_ofw_user_set_iap_amout(iapAmount);
                }
            }
        }

        public int? NumberOfSessions
        {
            set
            {
                if (value is int income)
                {
                    fyb_ofw_user_set_annual_household_income(income);
                }
                else
                {
                    fyb_ofw_user_clear_annual_income();
                }
            }
        }

        public long? PsTime
        {
            set
            {
                if (value is long psTime)
                {
                    fyb_ofw_user_set_ps_time(psTime);
                }
            }
        }

        public long? LastSession
        {
            set
            {
                if (value is long lastSession)
                {
                    fyb_ofw_user_set_last_session(lastSession);
                }
            }
        }

        public ConnectionType? ConnectionType
        {
            set
            {
                fyb_ofw_user_set_connection_type(value.ToString());
            }
        }

        public string? Device
        {
            set
            {
                fyb_ofw_user_set_device(value);
            }
        }

        public string AppVersion
        {
            set { fyb_ofw_user_set_app_version(value.ToString()); }
        }

        public Dictionary<string, string?> CustomParameters
        {
            set { fyb_ofw_user_set_custom_params(JsonUtility.ToJson(value, false)); }
        }
    }
}

#endif
