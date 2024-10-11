using Godot;
using System;

public partial class TheKnight : CharacterBody2D
{
  [Export]
  public CanvasGroup SpriteWrapper;
  [Export]
  public AnimatedSprite2D KnightSprite;
  [Export]
  public AnimatedSprite2D LeftSlashEffectSprite;

  public const float WalkSpeed = 600f;
  public const float RunSpeed = 800f;
  public const float SprintSpeed = 1000f;
  public const float JumpVelocity = -1800f;

  public override void _PhysicsProcess(double delta)
  {
    Vector2 velocity = Velocity;

    velocity = UpdateVerticalMovement((float)delta, velocity);
    velocity = UpdateHorizontalMovement(velocity);
    HandleSpriteOrientation(velocity);
    HandleSpriteAnimation(velocity);

    Velocity = velocity;
    MoveAndSlide();
  }

  /// <summary>
  /// Takes in a Vector of the current velocity and returns an updated Vector
  /// with updates to the vertical velocity
  /// </summary>
  /// <param name="currentVelocity"></param>
  /// <returns></returns>
  private Vector2 UpdateVerticalMovement(float delta, Vector2 currentVelocity)
  {
    if(!IsOnFloor())
    {
      currentVelocity += GetGravity() * delta;

      if(!Input.IsActionPressed("jump") && currentVelocity.Y < 0)
        currentVelocity.Y = currentVelocity.Y/2;
    }

    if (Input.IsActionJustPressed("jump") && IsOnFloor())
      currentVelocity.Y = JumpVelocity;
    
    return currentVelocity;
  }

  /// <summary>
  /// Takes in a Vector of the current velocity and returns an updated Vector
  /// with updates to the horizontal movement
  /// </summary>
  /// <param name="currentVelocity"></param>
  /// <returns></returns>
  private Vector2 UpdateHorizontalMovement(Vector2 currentVelocity)
  {
    currentVelocity.X = 0;

    if (Input.IsActionPressed("left"))
      currentVelocity.X -= RunSpeed;
    
    if (Input.IsActionPressed("right"))
      currentVelocity.X += RunSpeed;

    return currentVelocity;
  }

  /// <summary>
  /// Handles setting the sprite animation based on current actions
  /// </summary>
  /// <param name="currentVelocity"></param>
  private void HandleSpriteAnimation(Vector2 currentVelocity)
  {
    if(IsOnFloor())
    {
      if(currentVelocity.X == 0)
        KnightSprite.Play("idle");
      else if(KnightSprite.Animation != "run" && KnightSprite.Animation != "idle_to_run")
        KnightSprite.Play("idle_to_run");
      else if(!KnightSprite.IsPlaying())
        KnightSprite.Play("run");
    }
    else {
      if(KnightSprite.Animation != "fall" && KnightSprite.Animation != "airborne")
        KnightSprite.Play("airborne");
      else if(!KnightSprite.IsPlaying())
        KnightSprite.Play("fall");
    }
  }

  /// <summary>
  /// Takes in the current velocity and handles flipping the sprite orientation when necessary
  /// </summary>
  /// <param name="currentVelocity"></param>
  private void HandleSpriteOrientation(Vector2 currentVelocity)
  {
    if (currentVelocity.X < 0)
		{
			KnightSprite.FlipH = false;
		}
		else if (currentVelocity.X > 0)
		{
			KnightSprite.FlipH = true;
		}
  }
}
