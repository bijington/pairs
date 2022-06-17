using CommunityToolkit.Mvvm.ComponentModel;
using Pairs.Models;

namespace Pairs.ViewModels;

public partial class TileViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isGuessed;

    [ObservableProperty]
    private bool isSelected;

    private readonly Shape shape;

    public string Path => shape.Path;

    public TileViewModel(Shape shape)
    {
        this.shape = shape;
    }
}
