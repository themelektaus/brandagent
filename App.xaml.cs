using Microsoft.Maui.Controls;

namespace Brandagent;

public partial class App : Application
{
    public static bool authorized;

    public static App Instance => Current as App;
    public static App_MainPage InstanceMainPage => Instance.MainPage as App_MainPage;

    public App()
    {
        InitializeComponent();

        MainPage = new App_MainPage();
    }

    protected override void OnResume()
    {
        authorized = false;

        base.OnResume();
    }
}
