using CommunityToolkit.Mvvm.ComponentModel;
using Pairs.Models;

namespace Pairs.ViewModels;

public class TileViewModel : ObservableObject
{
    private bool isGuessed;
    private bool isSelected;
    private readonly Shape shape;

    public string Path => shape.Path;

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

    public TileViewModel(Shape shape)
    {
        this.shape = shape;
    }
}
