using Pairs.ViewModels;

namespace Pairs.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();

		BindingContext = mainPageViewModel;
	}

    void SKLottieView_AnimationFailed(System.Object sender, System.EventArgs e)
    {
    }

    void SKLottieView_AnimationLoaded(System.Object sender, System.EventArgs e)
    {
    }
}
