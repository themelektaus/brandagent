using Microsoft.Maui.Controls;

using QRCoder;

using System;

using ZXing.Net.Maui;

namespace Brandagent;

public partial class NewPage1 : ContentPage
{
    public Data.Item Item { get; private set; }

    public NewPage1()
    {
        InitializeComponent();

        cameraBarcodeReaderView.Options = new() { Formats = BarcodeFormats.TwoDimensional };
    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        foreach (var barcode in e.Results)
        {
            if (barcode.Format != BarcodeFormat.QrCode)
                continue;

            var totp = barcode.Value;

            if (!totp.StartsWith("otpauth://totp/"))
                continue;

            var url = totp[15..].Split('?');

            var parameters = System.Web.HttpUtility.ParseQueryString(url[1]);

            var secret = parameters.Get("secret") ?? string.Empty;
            if (secret == string.Empty)
                continue;

            Item = new()
            {
                service = string.Empty,
                name = System.Web.HttpUtility.UrlDecode(url[0]),
                secret = secret,
                totp = totp,
                qr = "data:image/png;base64," + Convert.ToBase64String(
                    PngByteQRCodeHelper.GetQRCode(totp, QRCodeGenerator.ECCLevel.M, 6)
                )
            };

            App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
