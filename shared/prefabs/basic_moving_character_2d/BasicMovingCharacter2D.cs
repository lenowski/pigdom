using Godot;

namespace Pigdom.Recipes;

public partial class BasicMovingCharacter2D : CharacterBody2D
{
    [Signal]
    public delegate void DirectionChangedEventHandler(int newDirection);

    private int _direction;

    [Export]
    public int Direction
    {
        get => _direction;
        set
        {
            if (_direction == value)
            {
                return;
            }

            _direction = value;
            EmitSignal(SignalName.DirectionChanged, _direction);
        }
    }

    [Export]
    public float Gravity { get; set; } = 2000.0f;

    [Export]
    public float JumpStrength { get; set; } = 800.0f;

    [Export]
    public float Speed { get; set; } = 500.0f;

    public override void _PhysicsProcess(double delta)
    {
        Velocity = Velocity with { Y = Velocity.Y + Gravity * (float)delta, X = Direction * Speed };
        MoveAndSlide();
    }

    public virtual void Jump()
    {
        if (IsOnFloor())
        {
            Velocity = Velocity with { Y = -JumpStrength };
        }
    }

    public void CancelJump()
    {
        if (!IsOnFloor() && Velocity.Y < 0.0f)
        {
            Velocity = Velocity with { Y = 0.0f };
        }
    }
}
