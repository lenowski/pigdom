namespace Pigdom.Actors.Interfaces;

public interface IBumpingPigState
{
    BumpingPig Context { get; set; }
    IBumpingPigState PreviousState { get; set; }

    void Enter();
    void Exit();
    void GetHurt();
}
