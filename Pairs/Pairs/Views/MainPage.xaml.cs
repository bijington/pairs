using Pairs.ViewModels;

namespace Pairs.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();

		BindingContext = mainPageViewModel;
	}
}
