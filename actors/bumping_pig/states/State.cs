using Godot;

namespace Pigdom.Actors.BumpingPig.States;

public abstract partial class State : Node
{
    public BumpingPig Context { get; set; }
    public State PreviousState { get; set; }

    public abstract void Enter();

    public virtual void Exit() { }

    public virtual void Move(int direction) { }

    public virtual void Stop() { }

    public virtual void Turn(int direction) { }

    public virtual void Jump() { }

    public virtual void CancelJump() { }

    public virtual void Attack() { }

    public virtual void Pick(Node2D item) { }

    public virtual void Throw(Vector2 throwForce) { }

    public virtual void GetHurt(int damage) { }
}
