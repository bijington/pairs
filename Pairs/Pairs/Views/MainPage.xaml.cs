using Pairs.ViewModels;

namespace Pairs.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();

		BindingContext = mainPageViewModel;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            var s = await FileSystem.OpenAppPackageFileAsync("background.json");

            var animation = SkiaSharp.Skottie.Animation.Create(s);
        }
        catch (Exception ex)
        {

        }
    }

    void SKLottieView_AnimationFailed(System.Object sender, System.EventArgs e)
    {
    }

    void SKLottieView_AnimationLoaded(System.Object sender, System.EventArgs e)
    {
    }
}
