using Microsoft.Maui.Controls;

using System;

using ZXing.Net.Maui;

namespace Brandagent;

public partial class Page_QrCodeScanner : ContentPage
{
    public Data.Item Item { get; private set; }
    public bool ShouldClear { get; private set; }

    public Page_QrCodeScanner()
    {
        InitializeComponent();
    }

    void Reader_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
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

            var secret = parameters.Get("secret");
            if (Utils.IsEmpty(secret))
                continue;

            Item = new()
            {
                service = string.Empty,
                name = System.Web.HttpUtility.UrlDecode(url[0]),
                secret = secret,
                totp = totp
            };

            Close();
        }
    }

    void CancelButton_Clicked(object sender, EventArgs e)
    {
        Close();
    }

    static void Close()
    {
        Utils.Navigation.PopModalAsync();
    }

}
