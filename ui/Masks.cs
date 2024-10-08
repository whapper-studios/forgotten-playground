using System;
using Godot;

public partial class Masks : HBoxContainer
{

	private int totalMasks = 5;
	private int currentMasks;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentMasks = totalMasks;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Called when HK is hit by an enemy, and triggers short animation of mask breaking.
	public void RemoveMask()
	{
		GetNode<AnimatedSprite2D>($"CenterContainer{currentMasks}/AnimatedSprite2D").Play(Name="MaskBreak");
		currentMasks -= 1;
	}

	private void AddMask()
	{
		currentMasks += 1;
		GetNode<AnimatedSprite2D>($"CenterContainer{currentMasks}/AnimatedSprite2D").PlayBackwards(Name="MaskBreak");
	}

    public override void _GuiInput(InputEvent @event)
    {
        if (@event.IsActionReleased("select") && currentMasks != 0)
		{
			RemoveMask();
		}
		if (@event.IsActionReleased("secondary") && currentMasks != totalMasks)
		{
			AddMask();
		}
    }
}
