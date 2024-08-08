using Microsoft.Maui.Controls;

using Plugin.Maui.Biometric;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Brandagent;

public partial class App_MainPage : ContentPage
{
    public event Action backButton;

    public App_MainPage()
    {
        Appearing += async (sender, e) => await App_MainPage_Appearing(sender, e);

        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        if (backButton is not null)
        {
            backButton.Invoke();
            return true;
        }

        return base.OnBackButtonPressed();
    }

    async Task App_MainPage_Appearing(object sender, EventArgs e)
    {
        if (App.authorized)
        {
            blazorWebView.IsVisible = true;
            return;
        }

        App.authorized = true;

        var service = BiometricAuthenticationService.Default;

        if (!service.IsPlatformSupported)
        {
            blazorWebView.IsVisible = true;
            return;
        }

        blazorWebView.IsVisible = false;

        var request = new AuthenticationRequest
        {
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
            blazorWebView.IsVisible = true;
            return;
        }

        if (response.Status != BiometricResponseStatus.Success)
        {
            await DisplayAlert($"Status: {response.Status}", response.ErrorMsg, "Close");
            Utils.App.Quit();
            return;
        }

        blazorWebView.IsVisible = true;
    }
}
