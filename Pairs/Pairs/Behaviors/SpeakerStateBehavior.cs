using System;
using Pairs.ViewModels;
using Xamarin.Forms;

namespace Pairs.Behaviors
{
    public class SpeakerStateBehavior : Behavior<Frame>
    {
        private Frame frame;
        private SpeakerViewModel speaker;

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

            if (speaker is not null)
            {
                speaker.PropertyChanged -= OnSpeakerPropertyChanged;
            }
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            if (frame.BindingContext is SpeakerViewModel speakerViewModel)
            {
                speaker = speakerViewModel;
                speaker.PropertyChanged += OnSpeakerPropertyChanged;
            }
        }

        private async void OnSpeakerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SpeakerViewModel.IsSelected))
            {
                await frame.RotateXTo(90, 100, Easing.BounceIn);
                frame.Content.IsVisible = speaker.IsSelected;
                await frame.RotateXTo(0, 100, Easing.BounceIn);
            }
            else if (e.PropertyName == nameof(SpeakerViewModel.IsGuessed))
            {
                var animation = new Animation();

                animation.Add(0.0, 0.2, new Animation(v => frame.Scale = v, 1, 0.9));
                animation.Add(0.2, 0.9, new Animation(v => frame.Scale = v, 0.9, 1.2));
                animation.Add(0.9, 1.0, new Animation(v => frame.Scale = v, 1.2, 1));

                animation.Commit(
                    frame,
                    "SuccessfulMatch",
                    length: 500,
                    easing: Easing.SpringIn,
                    finished: (v, f) => frame.IsVisible = false);
            }
        }
    }
}
