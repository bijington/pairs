using Pairs.Models;

namespace Pairs.Data;

public class ShapeRepository
{
    private readonly IList<Shape> speakers;

    public ShapeRepository()
    {
        speakers = new List<Shape>();

        speakers.Add(new Shape("M25,0 L50,0 L50,25 C50,38.8071187 38.8071187,50 25,50 L0,50 L0,25 C0,11.1928813 11.1928813,0 25,0 Z"));
        speakers.Add(new Shape("M25,0 L50,0 L50,50 L0,50 L0,25 C0,11.1928813 11.1928813,0 25,0 Z"));
        speakers.Add(new Shape("M25,0 L0,0 L0,50 L50,50 L50,25 C50,11.1928813 38.8071187,0 25,0 Z"));
        speakers.Add(new Shape("M50,0 L50,50 L25,50 C11.1928813,50 0,38.8071187 0,25 C0,11.3309524 10.9701429,0.224119049 24.5865793,0.00334928573 L25,0 L50,0 Z"));
        speakers.Add(new Shape("M0,0 L50,0 L50,50 L0,50 L0,0 Z"));
        speakers.Add(new Shape("M0,50 L25,0 L50,50 L0,50 Z"));
        speakers.Add(new Shape("M25,0 L46.6506351,12.5 L46.6506351,37.5 L25,50 L3.34936491,37.5 L3.34936491, 12.5 Z"));
        speakers.Add(new Shape("M8,25 25,0 42,25 25,50 Z"));
        speakers.Add(new Shape("M24,0 47.7764129,17.2745751 38.6946313,45.2254249 9.30536869,45.2254249 0.223587093,17.2745751 Z"));
        speakers.Add(new Shape("M0,14 12,0 38,0 50,14 25,50 Z"));
        speakers.Add(new Shape("M0,36 0,13.5 13.5,0 36,0 50,13.5 50,36 36,50 13.5,50 Z"));
    }

    public Task<IList<Shape>> ListAsync() => Task.FromResult(speakers);
}
