using Microsoft.Maui.Controls;

using System;

using ZXing.Net.Maui;

namespace Brandagent;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();

        cameraBarcodeReaderView.Options = new() { Formats = BarcodeFormats.TwoDimensional };
    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        foreach (var barcode in e.Results)
        {
            Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");
        }
    }
}
