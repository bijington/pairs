using System.Linq;
using AView = Android.Views.View;
//using Com.Plattysoft.Leonids;
//using Xamarin.Forms;
//using Microsoft.Maui.Platform.Android;
using AndroidX.Core.Content;
using System;
using System.Threading.Tasks;

namespace Pairs.Behaviors;

public partial class ParticleEmitterBehavior
{
    private AView platformView;

    public ParticleEmitterBehavior()
    {
        EmitCommand = new Command(OnEmit);
    }

    protected override void OnAttachedTo(VisualElement bindable, AView platformView)
    {
        base.OnAttachedTo(bindable, platformView);

        this.platformView = platformView;
    }

    protected override void OnDetachedFrom(VisualElement bindable, AView platformView)
    {
        base.OnDetachedFrom(bindable, platformView);
    }

    private void OnEmit()
    {
        //var lifeTime = LifeTime;
        //var numberOfItems = NumberOfParticles;
        //var scale = Scale;
        //var speed = Speed;
        //var image = Image;

        //var location = new int[2];
        //platformView.GetLocationOnScreen(location);

        //var drawableImage = ContextCompat.GetDrawable(Xamarin.Essentials.Platform.CurrentActivity, Xamarin.Essentials.Platform.CurrentActivity.Resources.GetIdentifier(image, "drawable", Xamarin.Essentials.Platform.CurrentActivity.PackageName));
        //var particleSystem = new ParticleSystem(Xamarin.Essentials.Platform.CurrentActivity, numberOfItems, drawableImage, lifeTime);
        //particleSystem
        //  .SetSpeedRange(0f, speed)
        //  .SetScaleRange(0, scale)
        //  .Emit(location[0] + platformView.Width / 2, location[1] + platformView.Height / 2, numberOfItems);

        //Task.Run(async () =>
        //{
        //    await Task.Delay(200);
        //    particleSystem?.StopEmitting();
        //});
    }
}