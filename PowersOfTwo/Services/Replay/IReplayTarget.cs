using PowersOfTwo.ViewModels;

namespace PowersOfTwo.Services.Replay
{
    public interface IReplayTarget
    {
        PlayerViewModel Player1 { get; set; }
        PlayerViewModel Player2 { get; set; }
    }
}