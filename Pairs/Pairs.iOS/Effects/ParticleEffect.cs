using System;
using System.Linq;
using System.Threading.Tasks;
using CoreAnimation;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName(nameof(Pairs) + "." + nameof(Pairs.Effects))]
[assembly: ExportEffect(typeof(Pairs.iOS.Effects.ParticleEffect), nameof(Pairs.iOS.Effects.ParticleEffect))]
namespace Pairs.iOS.Effects
{
    public class ParticleEffect : PlatformEffect
    {
        private Pairs.Effects.ParticleEffect particleEffect;

        protected override void OnAttached()
        {
            var effect = Element.Effects.OfType<Pairs.Effects.ParticleEffect>().FirstOrDefault();

            if (effect != null)
            {
                particleEffect = effect;
                effect.Emit += OnEffectEmit;
            }
        }

        protected override void OnDetached()
        {
            if (particleEffect != null)
            {
                particleEffect.Emit -= OnEffectEmit;
            }
        }

        private void OnEffectEmit(object sender, EventArgs e)
        {
            var control = Control ?? Container;

            var effect = (Pairs.Effects.ParticleEffect)Element.Effects.FirstOrDefault(p => p is Pairs.Effects.ParticleEffect);

            if (effect is null)
            {
                return;
            }

            //var mode = effect.Mode;
            var lifeTime = effect.LifeTime;
            var numberOfItems = effect.NumberOfParticles;
            var scale = effect.Scale;
            var speed = effect.Speed * 1000;
            var image = effect.Image;

            var emitterLayer = new CAEmitterLayer
            {
                Position = new CoreGraphics.CGPoint(
                    control.Bounds.Location.X + control.Bounds.Width / 2,
                    control.Bounds.Location.Y + control.Bounds.Height / 2),
                Shape = CAEmitterLayer.ShapeCircle
            };

            var cell = new CAEmitterCell
            {
                Name = "pEmitter",
                BirthRate = numberOfItems,
                Scale = 0f,
                ScaleRange = scale,
                Velocity = speed,
                LifeTime = (float)lifeTime,
                EmissionRange = (nfloat)Math.PI * 2.0f,
                Contents = UIImage.FromBundle(image).CGImage
            };

            emitterLayer.Cells = new CAEmitterCell[] { cell };

            control.Layer.AddSublayer(emitterLayer);

            //if (mode == EmitMode.OneShot)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(100).ConfigureAwait(false);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        emitterLayer.SetValueForKeyPath(NSNumber.FromInt32(0), new NSString("emitterCells.pEmitter.birthRate"));
                    });
                });
            }
        }
    }
}
