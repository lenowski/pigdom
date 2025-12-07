using Godot;

namespace Pigdom.Recipes;

public partial class WallJumpingCharacter2D : PassThroughCharacter2D
{
  [Export]
  public Vector2 WallJumpStrength { get; set; } = new(800, -800);

  [Export]
  public float SlideGravityFactor { get; set; } = 0.2f;

  [Export]
  public float HorizontalAcceleration { get; set; } = 8000.0f;

  [Export]
  public float MaxSlideSpeed { get; set; } = 200f;

  private WallDetectorRayCast2D _wallDetector;
  private WallState _wallState = WallState.NotOnWall;

  private enum WallState
  {
    NotOnWall,
    WallSliding,
    WallJumping,
  }

  public override void _Ready()
  {
    _wallDetector = GetNode<WallDetectorRayCast2D>("WallDetectorRayCast2D");
  }

  public override void _PhysicsProcess(double delta)
  {
    _wallDetector.Enabled = Direction != 0;

    if (Direction < 0)
    {
      _wallDetector.Scale = _wallDetector.Scale with { X = -1 };
    }
    else
    {
      _wallDetector.Scale = _wallDetector.Scale with { X = 1 };
    }

    switch (_wallState)
    {
      case WallState.NotOnWall:
        if (_wallDetector.WallColliding && IsOnWallOnly())
        {
          if (Direction == -GetWallNormal().X)
          {
            _wallState = WallState.WallSliding;
          }
        }
        base._PhysicsProcess(delta);
        return;

      case WallState.WallSliding:
        Velocity += new Vector2(0, Gravity * (float)delta);

        if (Direction != 0 && _wallDetector.WallColliding && IsOnWallOnly())
        {
          if (Direction == -GetWallNormal().X)
          {
            Velocity += new Vector2(0, Gravity * (float)delta * SlideGravityFactor);
          }
        }

        if (Direction != -GetWallNormal().X)
        {
          _wallState = WallState.NotOnWall;
        }

        if (IsOnFloor() || !_wallDetector.WallColliding)
        {
          _wallState = WallState.NotOnWall;
        }

        Velocity = Velocity with { Y = Mathf.Min(Velocity.Y, MaxSlideSpeed) };
        break;

      case WallState.WallJumping:
        Velocity += new Vector2(
            (HorizontalAcceleration * (float)delta) * Direction,
            Gravity * (float)delta
        );

        if (_wallDetector.WallColliding && Velocity.Y > 0)
        {
          _wallState = WallState.WallSliding;
        }

        if (IsOnFloor() || !_wallDetector.WallColliding)
        {
          _wallState = WallState.NotOnWall;
        }
        break;
    }

    MoveAndSlide();
  }

  public override void Jump()
  {
    if (_wallState == WallState.WallSliding)
    {
      WallJump();
    }
    else if (_wallState == WallState.NotOnWall)
    {
      base.Jump();
    }
  }

  private void WallJump()
  {
    Velocity = Velocity with { Y = WallJumpStrength.Y, X = WallJumpStrength.X * -Direction };
    _wallState = WallState.WallJumping;
  }
}
