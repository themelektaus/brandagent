using Microsoft.Maui.Storage;

using OtpNet;

using QRCoder;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Brandagent
{
    public class Data
    {
        readonly static JsonSerializerOptions jsonOptions = new()
        {
            WriteIndented = true,
            IncludeFields = true
        };

        static string GetPath()
        {
            return Path.Combine(
                FileSystem.AppDataDirectory,
                "data.json"
            );
        }

        public static async Task<Data> LoadAsync()
        {
            return await LoadAsync(GetPath());
        }

        public static async Task<Data> LoadAsync(string path)
        {

            if (!File.Exists(path))
            {
                return new();
            }

            using var stream = File.OpenRead(path);

            try
            {
                return await JsonSerializer.DeserializeAsync<Data>(stream, jsonOptions);
            }
            catch
            {
                var json = await File.ReadAllTextAsync(path);

                return new();
            }
        }

        public void Add(Data data)
        {
            foreach (var color in data.colors)
                if (!colors.Any(x => x.name == color.name))
                    colors.Add(color);

            foreach (var item in data.items)
                if (!items.Any(x => x.secret == item.secret))
                    items.Add(item);
        }

        public async Task SaveAsync()
        {
            await SaveAsync(GetPath());
        }

        public async Task SaveAsync(string path)
        {
            using var stream = File.Create(path);
            await JsonSerializer.SerializeAsync(stream, this, jsonOptions);
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, jsonOptions);
        }

        public class Color
        {
            public string name;
            public string value;
        }

        public List<Color> colors = new();

        public class Item
        {
            public string service;
            public string name;
            public string secret;
            public string totp;
            public bool hidden;

            public string GetStyle(List<Color> colors)
            {
                var color = colors.FirstOrDefault(x => x.name == service);
                if (color is null)
                    return string.Empty;

                return $"background-color: {color.value}; ";
            }

            public void Compute(out string otp, out int timer)
            {
                try
                {
                    var secretKey = Base32Encoding.ToBytes(secret);
                    var totp = new Totp(secretKey);
                    otp = totp.ComputeTotp().Insert(3, " ");
                    timer = totp.RemainingSeconds();
                }
                catch
                {
                    otp = null;
                    timer = 0;
                }
            }

            public string GenerateQr()
            {
                return "data:image/png;base64," + Convert.ToBase64String(
                    PngByteQRCodeHelper.GetQRCode(totp, QRCodeGenerator.ECCLevel.M, 6)
                );
            }

            public string GenerateQrStyle()
            {
                return $"background-image: url({GenerateQr()}); ";
            }

        }
        public List<Item> items = new();
    }
}
