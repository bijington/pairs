using Pairs.ViewModels;
using Xamarin.Forms;

namespace Pairs.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void MainPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainPageViewModel.State) &&
                ((MainPageViewModel)BindingContext).State == Models.LevelState.Complete)
            {
                TrophyAnimation.PlayAnimation();
            }
        }
    }
}
