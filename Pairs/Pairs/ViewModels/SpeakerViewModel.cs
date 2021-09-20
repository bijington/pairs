using MvvmHelpers;
using Pairs.Models;

namespace Pairs.ViewModels
{
    public class SpeakerViewModel : BaseViewModel
    {
        private bool isGuessed;
        private bool isSelected;
        private readonly Speaker speaker;

        public string Name => speaker.Name;
        public string ProfileImageUrl => speaker.ProfileImageUrl;

        public bool IsGuessed
        {
            get => isGuessed;
            set => SetProperty(ref isGuessed, value);
        }

        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        public SpeakerViewModel(Speaker speaker)
        {
            this.speaker = speaker;
        }
    }
}
