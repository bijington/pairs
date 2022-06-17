using System;
using System.Linq;
using Pairs.Effects;
using Pairs.ViewModels;
using Xamarin.Forms;

namespace Pairs.Behaviors
{
    public class TileStateBehavior : Behavior<Frame>
    {
        private Frame frame;
        private TileViewModel tileViewModel;

        protected override void OnAttachedTo(Frame bindable)
        {
            base.OnAttachedTo(bindable);

            frame = bindable;
            frame.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(Frame bindable)
        {
            base.OnDetachingFrom(bindable);

            frame.BindingContextChanged -= OnBindingContextChanged;

            if (tileViewModel is not null)
            {
                tileViewModel.PropertyChanged -= OnTileViewModelPropertyChanged;
            }
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            if (frame.BindingContext is TileViewModel tile)
            {
                tileViewModel = tile;
                tileViewModel.PropertyChanged += OnTileViewModelPropertyChanged;
            }
        }

        private async void OnTileViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TileViewModel.IsSelected))
            {
                await frame.RotateXTo(90, 100, Easing.BounceIn);
                frame.Content.IsVisible = tileViewModel.IsSelected;
                await frame.RotateXTo(0, 100, Easing.BounceIn);
            }
            else if (e.PropertyName == nameof(TileViewModel.IsGuessed))
            {
                var animation = new Animation();

                animation.Add(0.0, 0.2, new Animation(v => frame.Scale = v, 1, 0.9));
                animation.Add(0.2, 0.75, new Animation(v => frame.Scale = v, 0.9, 1.2));
                animation.Add(0.75, 1.0, new Animation(v => frame.Scale = v, 1.2, 0));

                animation.Commit(
                    frame,
                    "SuccessfulMatch",
                    length: 500,
                    easing: Easing.SpringIn,
                    finished: (v, f) =>
                    {
                        frame.Parent.Effects.OfType<ParticleEffect>().First().RaiseEmit();

                        frame.IsVisible = false;
                    });
            }
        }
    }
}
