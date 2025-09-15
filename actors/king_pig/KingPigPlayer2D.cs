using Godot;
using Pigdom.Recipes;

namespace Pigdom.Actors.KingPig;

public partial class KingPigPlayer2D : Player2D
{
    [Signal]
    public delegate void DiedEventHandler();

    [Signal]
    public delegate void LivesIncreasedEventHandler(int amount);

    [Signal]
    public delegate void LivesDecreasedEventHandler(int amount);

    [Export]
    public int Lives { get; private set; } = 3;

    private AnimatedSprite2D _animatedSprites;
    private Node2D _sprites;
    private int _currentLives;
    private Area2D _hitBox;
    private AnimationPlayer _animationPlayer;
    private float _fallSpeed = 0.0f;
    private HurtArea2D _hurtArea2D;

    public override void _Ready()
    {
        base._Ready();

        _animatedSprites = GetNode<AnimatedSprite2D>("Sprites/AnimatedSprite2D");
        _sprites = GetNode<Node2D>("Sprites");
        _hitBox = GetNode<Area2D>("HitArea2D");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _hurtArea2D = GetNode<HurtArea2D>("HurtArea2D");

        _currentLives = Lives;

        _animatedSprites.AnimationFinished += OnAnimatedSprite2DAnimationFinished;
        _hurtArea2D.Hurt += OnHurtArea2DHurt;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        UpdateSpriteDirection();
        UpdateAnimation();
    }

    private void UpdateSpriteDirection()
    {
        if (Direction > 0)
        {
            _sprites.Scale = _sprites.Scale with { X = 1 };
            _hitBox.Scale = _hitBox.Scale with { X = 1 };
        }
        else if (Direction < 0)
        {
            _sprites.Scale = _sprites.Scale with { X = -1 };
            _hitBox.Scale = _hitBox.Scale with { X = -1 };
        }
    }

    private void UpdateAnimation()
    {
        string animation = _animatedSprites.Animation;

        if (IsOnFloor())
        {
            if (Direction != 0.0f)
            {
                animation = "run";
            }
            else
            {
                animation = "idle";
            }
        }
        else
        {
            if (Velocity.Y >= 0.0f)
            {
                animation = "fall";
                _fallSpeed = Velocity.Y;
            }
            else
            {
                animation = "jump";
            }
        }

        if (_fallSpeed > 0.0f && IsOnFloor())
        {
            animation = "ground";
        }

        _animatedSprites.Play(animation);
    }

    public override void _UnhandledInput(InputEvent evnt)
    {
        if (evnt.IsAction("attack"))
        {
            if (evnt.IsPressed())
            {
                if (IsOnFloor())
                {
                    _animationPlayer.Play("attack");
                }
            }
        }

        base._UnhandledInput(evnt);
    }

    public void FadeIn() => _animationPlayer.Play("fade_in");

    public void FadeOut() => _animationPlayer.Play("fade_out");

    private void OnAnimatedSprite2DAnimationFinished()
    {
        string currentAnimation = _animatedSprites.Animation;

        if (currentAnimation == "ground")
        {
            _fallSpeed = 0.0f;
            _animatedSprites.Play("idle");
        }
        else if (currentAnimation == "hit")
        {
            SetPhysicsProcess(true);
            SetProcessUnhandledInput(true);
            _animatedSprites.Play("idle");
        }
    }

    private async void OnHurtArea2DHurt(int damage)
    {
        SetPhysicsProcess(false);
        SetProcessUnhandledInput(false);
        Direction = 0;

        _currentLives -= damage;
        EmitSignal(SignalName.LivesDecreased, damage);

        if (_currentLives < 1)
        {
            _animatedSprites.Play("dead");
            _animationPlayer.Play("dead");
            await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
            EmitSignal(SignalName.Died);
        }
        else
        {
            _animatedSprites.Play("hit");
        }
    }
}
