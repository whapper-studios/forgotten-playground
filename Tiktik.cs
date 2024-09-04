using Godot;
using System;
using System.Globalization;
//using System.Numerics;

using System.Reflection.Metadata.Ecma335;


public partial class Tiktik : CharacterBody2D
{
	[Export] RayCast2D Back;
	[Export] RayCast2D Front;
	[Export] AnimatedSprite2D running;
	public const float Speed = 150.0f;

	public Vector2 currentbasis = new Vector2();
	public bool test = false;
	[Export] public Timer timer;

	public int num = 0;
	public int ran = 0; 
	public bool isRotating = false;
	public bool shouldRotate = false;
    public override void _Ready()
    {
       timer.Start();
	   running.Play();
    }
    public override void _PhysicsProcess(double delta)
	{
		if(timer.IsStopped()){
			//GD.Print("happening");
			// Vector2 velocity = Velocity;
			// Vector2 basis = Transform.BasisXform(Velocity);
			// Vector2 forward = new Vector2(Mathf.Abs(Mathf.Cos(Rotation)), Mathf.Abs(Mathf.Sin(Rotation)));
			GD.Print(Velocity);
			GD.Print(this.Position);
 
			// Add the gravity.
			// if (!IsOnFloor())
			// {
			// 	velocity += GetGravity() * (float)delta;
			// }

			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			// if(start == true)
			// {
			// 	GD.Print(Velocity);
			// }
			if(!isRotating)
			{
				if((Back.IsColliding() || Front.IsColliding()))
				{
					Move();
					GD.Print("happening");
				}
				else
				{
					shouldRotate = true;
				}
			}
			
			if(shouldRotate)
			{
				isRotating = true;
				if(num <= 11)
				{
					Rotate();
				}
				else
				{
					isRotating = false;
					shouldRotate = false;
					num = 0;
				}
			}
		}
	}
	

    private void Move()
    {
		Vector2 forward = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation));
        Velocity = forward * Speed;

        MoveAndSlide();
		ran++;
	}

	private void Rotate()
	{
		GD.Print("rotating");
		this.RotationDegrees += 7.5f;
		num++;
	}


}

