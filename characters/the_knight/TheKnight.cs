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
  public const float RunSpeed = 400f;
  public const float SprintSpeed = 500f;
  public const float JumpVelocity = -400f;

  public override void _PhysicsProcess(double delta)
  {
    Vector2 velocity = Velocity;

    // Add the gravity.
    if (!IsOnFloor())
    {
      velocity += GetGravity() * (float)delta;
    }

    // Handle Jump.
    if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
    {
      velocity.Y = JumpVelocity;
    }

    // Get the input direction and handle the movement/deceleration.
    // As good practice, you should replace UI actions with custom gameplay actions.
    Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
    if (direction != Vector2.Zero)
    {
      velocity.X = direction.X * WalkSpeed;
    }
    else
    {
      velocity.X = Mathf.MoveToward(Velocity.X, 0, WalkSpeed);
    }

    Velocity = velocity;
    MoveAndSlide();
  }

  private void HandleHorizontalMovement()

  private void HandleAnimationDirection(Vector2 velocity)
  {
    

    if (velocity.X < 0)
		{
			KnightSprite.FlipH = true;
		}
		else if (velocity.X > 0)
		{
			KnightSprite.FlipH = false;
		}
  }
}
