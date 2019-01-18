using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace Pigeon.LocalDataAccess.Model
{
    public static class LocalStorageSettings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static string Email
        {
            get => AppSettings.GetValueOrDefault(nameof(Email), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Email), value);
        }

        public static string CountryCode
        {
            get => AppSettings.GetValueOrDefault(nameof(CountryCode), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(CountryCode), value);
        }

        public static string MobileNo
        {
            get => AppSettings.GetValueOrDefault(nameof(MobileNo), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(MobileNo), value);
        }

        public static string OtpCode
        {
            get => AppSettings.GetValueOrDefault(nameof(OtpCode), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(OtpCode), value);
        }

        public static string ServiceUserId
        {
            get => AppSettings.GetValueOrDefault(nameof(ServiceUserId), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(ServiceUserId), value);
        }

        public static bool IsOtpVerified
        {
            get => AppSettings.GetValueOrDefault(nameof(IsOtpVerified), false);
            set => AppSettings.AddOrUpdateValue(nameof(IsOtpVerified), value);
        }

        public static bool IsOtpSent
        {
            get => AppSettings.GetValueOrDefault(nameof(IsOtpSent), false);
            set => AppSettings.AddOrUpdateValue(nameof(IsOtpSent), value);
        }

        public static string Password
        {
            get => AppSettings.GetValueOrDefault(nameof(Password), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Password), value);
        }

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(nameof(Token), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Token), value);
        }

        public static string RefreshToken
        {
            get => AppSettings.GetValueOrDefault(nameof(RefreshToken), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(RefreshToken), value);
        }

        public static DateTime TokenExpiryDate
        {
            get => AppSettings.GetValueOrDefault(nameof(TokenExpiryDate), DateTime.UtcNow);
            set => AppSettings.AddOrUpdateValue(nameof(TokenExpiryDate), value);
        }

        public static void ClearEverything()
        {
            AppSettings.Clear();
        }

    }
}
