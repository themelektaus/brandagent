using Microsoft.Maui.Controls;

using System;
using System.Diagnostics.CodeAnalysis;
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
        if (Item is not null)
            return;

        foreach (var barcode in e.Results)
        {
            if (barcode.Format != BarcodeFormat.QrCode)
                continue;

            Check(barcode.Value);

            if (Item is not null)
                break;
        }

        if (Item is not null)
            Close();
    }

    void Check(string totp)
    {
        if (!totp.StartsWith("otpauth://totp/"))
            return;

        var url = totp[15..].Split('?');

        var parameters = System.Web.HttpUtility.ParseQueryString(url[1]);

        var secret = parameters.Get("secret");
        if (Utils.IsEmpty(secret))
            return;

        Item = new()
        {
            service = string.Empty,
            name = System.Web.HttpUtility.UrlDecode(url[0]),
            secret = secret,
            totp = totp
        };
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
