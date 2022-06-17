﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Pairs.Data;
using Pairs.Models;

namespace Pairs.ViewModels;

public class MainPageViewModel : ObservableObject
{
    private TileViewModel currentTile;
    private CancellationTokenSource cancelAnimationTokenSource;
    private int guessedCount = 10;
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

    public ObservableCollection<TileViewModel> GuessedTiles { get; } = new ObservableCollection<TileViewModel>();

    public ObservableCollection<TileViewModel> Tiles { get; } = new ObservableCollection<TileViewModel>();

    public ICommand SelectTileCommand { get; }

    public MainPageViewModel()
    {
        //PlayCommand = new AsyncCommand(LoadAsync, allowsMultipleExecutions: false);
        //SelectTileCommand = new AsyncCommand<TileViewModel>(OnSelectTile, allowsMultipleExecutions: true);

        GuessedTiles.Add(new TileViewModel(new Shape("")));
        GuessedTiles.Add(new TileViewModel(new Shape("")));
        GuessedTiles.Add(new TileViewModel(new Shape("")));
    }

    private async Task OnSelectTile(TileViewModel tile)
    {
        cancelAnimationTokenSource?.Cancel();

        tile.IsSelected = !tile.IsSelected;

        if (this.currentTile is not null)
        {
            if (this.currentTile.Path == tile.Path &&
                ReferenceEquals(this.currentTile, tile) == false)
            {
                this.currentTile.IsGuessed = true;
                tile.IsGuessed = true;
            }

            await ShowSelectionAsync();

            if (this.currentTile.Path == tile.Path &&
                ReferenceEquals(this.currentTile, tile) == false)
            {
                if (GuessedTiles.Count < 5)
                {
                    GuessedTiles.Add(tile);
                }

                this.currentTile.IsSelected = false;
                this.currentTile = null;

                GuessedCount++;
            }
            else
            {
                this.currentTile.IsSelected = false;
            }

            tile.IsSelected = false;
        }

        this.currentTile = tile.IsSelected ? tile : null;

        if (Tiles.All(s => s.IsGuessed))
        {
            State = LevelState.Complete;
        }
    }

    private async Task ShowSelectionAsync()
    {
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
    }

    private async Task LoadAsync()
    {
        var shapeRepository = new ShapeRepository();

        var allShapes = await shapeRepository.ListAsync();

        var random = new Random();

        const int gridSize = 8;
        var requiredSpeakerCount = gridSize / 2;

        var actualTiles = new List<TileViewModel>(gridSize);

        for (int i = 0; i < requiredSpeakerCount; i++)
        {
            var shapeIndex = random.Next(allShapes.Count);
            var shape = allShapes[shapeIndex];
            allShapes.RemoveAt(shapeIndex);

            actualTiles.Add(new TileViewModel(shape));
            actualTiles.Add(new TileViewModel(shape));
        }

        int n = actualTiles.Count;

        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            var value = actualTiles[k];
            actualTiles[k] = actualTiles[n];
            actualTiles[n] = value;
        }

        State = LevelState.Playing;
        Tiles.Clear();
        foreach (var tile in actualTiles)
        {
            Tiles.Add(tile);
        }
    }
}
