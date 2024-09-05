using Godot;
using System;
using System.Globalization;
//using System.Numerics;

using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;


public partial class Tiktik : CharacterBody2D
{
	[Export] RayCast2D Back;
	[Export] RayCast2D Front;
	[Export] AnimatedSprite2D running;
	public const float Speed = 150.0f;
	public const float Gravity = 500f;

	public int rotatedNum = 0; 
	public bool isRotating = false;
	public bool firsttouch = false;
	public bool shouldRotate = false;
    public override void _Ready()
    {
	   running.Play();
    }
    public override void _PhysicsProcess(double delta)
	{
		if(!IsOnFloor() && firsttouch == false)
		{
			ApplyGravity((float)delta);
		}
		else
		{
			firsttouch = true;
			if(!isRotating)
			{
				if(Back.IsColliding() || Front.IsColliding())
				{
					Move();
				}
				else
				{
					shouldRotate = true;
				}
			}
			
			if(shouldRotate)
			{
				isRotating = true;
				if(rotatedNum <= 11)
				{
					Rotate();
				}
				else
				{

					isRotating = false;
					shouldRotate = false;
					rotatedNum = 0;
				}
				
			}
		}
	}
	
	private void ApplyGravity(float delta)
	{
		Vector2 upDirection = new Vector2(0,-1).Rotated(Rotation);
		this.SetUpDirection(upDirection);
		Velocity -= delta * upDirection * Gravity;
		MoveAndCollide(Velocity);
		Velocity = new Vector2(0,0);
		MoveAndSlide();
	}

    private void Move()
    {
		Vector2 forward = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation));
		Velocity = forward * Speed;
        
		MoveAndSlide();
	}
	
	private void Rotate()
	{
		this.RotationDegrees += 7.5f;
		rotatedNum++;
	}
}

