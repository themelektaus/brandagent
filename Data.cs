using Microsoft.Maui.Storage;

using OtpNet;

using QRCoder;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Brandagent;

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
        Add(data.colors);
        Add(data.items);
    }

    public void Add(IEnumerable<Color> colors)
    {
        foreach (var color in colors)
            Add(color);
    }

    public void Add(IEnumerable<Item> items)
    {
        foreach (var item in items)
            Add(item);
    }

    public void Add(Color color)
    {
        var _color = colors.FirstOrDefault(x => x.name == color.name);
        if (_color is null)
        {
            colors.Add(color);
        }
        else
        {
            _color.value = color.value;
        }
    }

    public void Add(Item item)
    {
        var _item = items.FirstOrDefault(x => x.secret == item.secret);
        if (_item is null)
        {
            items.Add(item);
        }
        else
        {
            _item.service = item.service;
            _item.name = item.name;
        }
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

    public class Item : IJsonOnDeserialized
    {
        public string service;
        public string name;
        public string secret;
        public string totp;
        public bool hidden;

        [JsonIgnore] public string qr;

        public void OnDeserialized()
        {
            qr = "data:image/png;base64," + Convert.ToBase64String(
                PngByteQRCodeHelper.GetQRCode(totp, QRCodeGenerator.ECCLevel.M, 6)
            );
        }

        public void Compute(out string[] otps, out int timer)
        {
            timer = 0;

            try
            {
                var secretKey = Base32Encoding.ToBytes(secret);

                otps = new string[3];
                
                for (var i = -1; i <= 1; i++)
                {
                    var totp = new Totp(secretKey, timeCorrection: new(DateTime.UtcNow.AddSeconds(30 * i)));
                    otps[i + 1] = totp.ComputeTotp().Insert(3, " ");
                    if (i == 0)
                        timer = totp.RemainingSeconds();
                }
            }
            catch
            {
                otps = [];
            }
        }
    }
    public List<Item> items = new();
}
