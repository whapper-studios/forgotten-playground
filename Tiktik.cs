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
		if(timer.IsStopped())
		{
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
		this.RotationDegrees += 7.5f;
		num++;
	}


}

