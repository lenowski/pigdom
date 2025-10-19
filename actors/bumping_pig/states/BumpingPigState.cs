using Godot;
using Pigdom.Actors.Interfaces;

namespace Pigdom.Actors.States;

public abstract partial class BumpingPigState : Node, IBumpingPigState
{
    public BumpingPig Context { get; set; }
    public IBumpingPigState PreviousState { get; set; }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void GetHurt() { }
}
