using Godot;
using Pigdom.Game;
using Pigdom.Recipes;

namespace Pigdom.Actors;

public partial class BumpingPig : Node2D
{
    [Export]
    public int InitialDirection { get; set; }

    [Export]
    public int MaxHealth { get; set; } = 3;

    private ScorePoint _scorePoint;
    private BumpingEnemy2D _body;
    private AnimationPlayer _animationPlayer;
    private Node2D _sprites;
    private AnimatedSprite2D _animatedSprites;
    private int _health;
    private HurtArea2D _hurtArea2D;

    private enum State
    {
        Run,
        Idle,
        Dead,
    }

    private State _currentState = State.Run;

    public override void _Ready()
    {
        _scorePoint = GetNode<ScorePoint>("ScorePoint");
        _body = GetNode<BumpingEnemy2D>("BumpingEnemy2D");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _sprites = GetNode<Node2D>("BumpingEnemy2D/Sprites");
        _animatedSprites = GetNode<AnimatedSprite2D>("BumpingEnemy2D/Sprites/AnimatedSprite2D");
        _hurtArea2D = GetNode<HurtArea2D>("BumpingEnemy2D/HurtArea2D");

        _health = MaxHealth;
        _body.Direction = InitialDirection;

        _hurtArea2D.Hurt += OnHurtArea2DHurt;
        _animatedSprites.AnimationFinished += OnAnimatedSprite2DAnimationFinished;
    }

    public override void _PhysicsProcess(double delta)
    {
        switch (_currentState)
        {
            case State.Idle:
                _animatedSprites.Play("idle");
                break;
            case State.Run:
                _animatedSprites.Play("run");
                break;
            case State.Dead:
                _body.Direction = 0;
                break;
        }

        _sprites.Scale = _body.Direction switch
        {
            > 0 => _sprites.Scale with { X = -1 },
            < 0 => _sprites.Scale with { X = 1 },
            _ => _sprites.Scale,
        };
    }

    protected virtual void Die()
    {
        _animationPlayer.Play("die");
        _currentState = State.Dead;
        _scorePoint.IncreaseScore();
    }

    private void Hit(int damage)
    {
        _health -= damage;

        if (_health < 1)
        {
            CallDeferred(MethodName.Die);
        }
        else
        {
            CallDeferred(MethodName.PlayHitAnimation);
        }
    }

    private void PlayHitAnimation() => _animationPlayer.Play("hit");

    private void OnHurtArea2DHurt(int damage) => Hit(damage);

    private void OnAnimatedSprite2DAnimationFinished()
    {
        if (_animatedSprites.Animation == "hit")
        {
            _currentState = State.Run;
        }
    }
}
