using Godot;
using Pigdom.Actors.BumpingPig.Components;

public abstract partial class InteractionStrategy : Node
{
    public Brain Context { get; set; }
    public Area2D InteractedArea { get; set; }

    public abstract void Execute();
}
