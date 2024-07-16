using Microsoft.Maui.Controls;

using Plugin.Maui.Biometric;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Brandagent;

public partial class App_MainPage : ContentPage
{
    public App_MainPage()
    {
        Appearing += async (sender, e) => await App_MainPage_Appearing(sender, e);

        InitializeComponent();
    }

    async Task App_MainPage_Appearing(object sender, EventArgs e)
    {
        var service = BiometricAuthenticationService.Default;

        if (!service.IsPlatformSupported)
        {
            blazorWebView.IsVisible = true;
            return;
        }

        blazorWebView.IsVisible = false;

        var request = new AuthenticationRequest {
            Title = "Authentication",
            AllowPasswordAuth = true
        };

        AuthenticationResponse response;

        try
        {
            response = await service.AuthenticateAsync(request, CancellationToken.None);
        }
        catch (NotImplementedException ex)
        {
            await DisplayAlert("Not Implemented Exception", ex.Message, "Close");
            return;
        }

        if (response.Status != BiometricResponseStatus.Success)
        {
            await DisplayAlert($"Status: {response.Status}", response.ErrorMsg, "Close");
            return;
        }

        blazorWebView.IsVisible = true;
    }
}
