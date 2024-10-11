using Godot;
using System;

public partial class TheKnight : CharacterBody2D
{
  [Export]
  public CanvasGroup SpriteWrapper;
  [Export]
  public AnimatedSprite2D KnightSprite;
  [Export]
  public AnimatedSprite2D FrontSlashEffectSprite;

  public const float WalkSpeed = 300f;
  public const float RunSpeed = 500f;
  public const float SprintSpeed = 500f;
  public const float JumpVelocity = -800f;

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
    if(IsOnFloor() && currentVelocity.X == 0)
      KnightSprite.Play("idle");

    if(IsOnFloor() && currentVelocity.X != 0) {
      if(KnightSprite.Animation != "walk" && KnightSprite.Animation != "sprint")
        KnightSprite.Play("sprint");
      else if(!KnightSprite.IsPlaying()) 
        KnightSprite.Play("walk");
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
			SpriteWrapper.FlipH = false;
		}
		else if (currentVelocity.X > 0)
		{
			SpriteWrapper.FlipH = true;
		}
  }
}
