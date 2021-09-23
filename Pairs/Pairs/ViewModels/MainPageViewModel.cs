using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Pairs.Data;
using Pairs.Models;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Pairs.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private SpeakerViewModel currentSpeaker;
        private CancellationTokenSource cancelAnimationTokenSource;
        private int guessedCount;
        private LevelState state;

        public int GuessedCount
        {
            get => guessedCount;
            set => SetProperty(ref guessedCount, value);
        }

        public LevelState State
        {
            get => state;
            set => SetProperty(ref state, value);
        }

        public ICommand PlayCommand { get; }

        public MvvmHelpers.ObservableRangeCollection<SpeakerViewModel> GuessedSpeakers { get; } = new MvvmHelpers.ObservableRangeCollection<SpeakerViewModel>();

        public MvvmHelpers.ObservableRangeCollection<SpeakerViewModel> Speakers { get; } = new MvvmHelpers.ObservableRangeCollection<SpeakerViewModel>();

        public ICommand SelectTileCommand { get; }

        public MainPageViewModel()
        {
            PlayCommand = new AsyncCommand(LoadAsync, allowsMultipleExecutions: false);
            SelectTileCommand = new AsyncCommand<SpeakerViewModel>(OnSelectTile, allowsMultipleExecutions: true);
        }

        private async Task OnSelectTile(SpeakerViewModel speaker)
        {
            cancelAnimationTokenSource?.Cancel();

            speaker.IsSelected = !speaker.IsSelected;

            if (this.currentSpeaker is not null)
            {
                if (this.currentSpeaker.Name == speaker.Name &&
                    ReferenceEquals(this.currentSpeaker, speaker) == false)
                {
                    this.currentSpeaker.IsGuessed = true;
                    speaker.IsGuessed = true;
                }

                cancelAnimationTokenSource = new CancellationTokenSource();

                try
                {
                    await Task.Delay(500, cancelAnimationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {

                }
                finally
                {
                    cancelAnimationTokenSource = null;
                }

                if (this.currentSpeaker.Name == speaker.Name &&
                    ReferenceEquals(this.currentSpeaker, speaker) == false)
                {
                    if (GuessedSpeakers.Count < 5)
                    {
                        GuessedSpeakers.Add(speaker);
                    }

                    this.currentSpeaker.IsSelected = false;
                    this.currentSpeaker = null;

                    GuessedCount++;
                }
                else
                {
                    this.currentSpeaker.IsSelected = false;
                }

                speaker.IsSelected = false;
            }

            this.currentSpeaker = speaker.IsSelected ? speaker : null;

            if (Speakers.All(s => s.IsGuessed))
            {
                State = LevelState.Complete;
            }
        }

        private async Task LoadAsync()
        {
            var speakerRepository = new SpeakerRepository();

            var allSpeakers = await speakerRepository.ListAsync();

            var random = new Random();

            const int gridSize = 20;
            var requiredSpeakerCount = gridSize / 2;

            var actualSpeakers = new List<SpeakerViewModel>(gridSize);

            for (int i = 0; i < requiredSpeakerCount; i++)
            {
                var speakerIndex = random.Next(allSpeakers.Count);
                var speaker = allSpeakers[speakerIndex];
                allSpeakers.RemoveAt(speakerIndex);

                actualSpeakers.Add(new SpeakerViewModel(speaker));
                actualSpeakers.Add(new SpeakerViewModel(speaker));
            }

            int n = actualSpeakers.Count;

            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                var value = actualSpeakers[k];
                actualSpeakers[k] = actualSpeakers[n];
                actualSpeakers[n] = value;
            }

            State = LevelState.Playing;
            Speakers.ReplaceRange(actualSpeakers);
        }
    }
}
