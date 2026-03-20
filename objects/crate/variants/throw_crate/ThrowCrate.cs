using Godot;

namespace Pigdom.Objects;

public partial class ThrowCrate : Crate
{
    public override void _Ready()
    {
        base._Ready();
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node body)
    {
        Shatter();
    }
}
