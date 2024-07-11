using Microsoft.Maui.Storage;

using OtpNet;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Brandagent
{
    public class Data
    {
        readonly static JsonSerializerOptions jsonOptions = new() { IncludeFields = true };

        public static async Task<Data> LoadAsync()
        {
            using var stream = await FileSystem.Current.OpenAppPackageFileAsync("data.json");
            return await JsonSerializer.DeserializeAsync<Data>(stream, jsonOptions);
        }

        public class Color
        {
            public string name;
            public string value;
        }

        public List<Color> colors;

        public class Item
        {
            public string service;
            public string name;
            public string secret;
            public string totp;
            public string qr;
            public bool hidden;

            public string GetStyle(List<Color> colors)
            {
                var color = colors.FirstOrDefault(x => x.name == service);
                if (color is null)
                    return string.Empty;

                return $"background-color: {color.value}; ";
            }

            public string GetQrStyle()
            {
                return $"background-image: url({qr}); ";
            }

            public (string otp, int timer) Compute()
            {
                var secretKey = Base32Encoding.ToBytes(secret);
                var totp = new Totp(secretKey);
                return (totp.ComputeTotp().Insert(3, " "), totp.RemainingSeconds());
            }
        }
        public List<Item> items = new();
    }
}
