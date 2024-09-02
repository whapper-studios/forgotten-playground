using System;
using Godot;

public partial class Masks : HBoxContainer
{

	private string pathToFullMask = "[img]assets/Mask.png[/img]";
	private string pathToEmptyMask = "[img]assets/MaskEmpty.png[/img]";
	private int totalMasks = 5;
	private int currentMasks;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentMasks = totalMasks;
		UpdateMasks();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Called when HK is hit by an enemy, and triggers short animation of mask breaking.
	public void RemoveMask()
	{
		currentMasks -= 1;
		UpdateMasks();
	}

	public void UpdateMasks()
	{
		for (int i = 0; i<totalMasks; i++)
		{
			
			if (i<currentMasks)
			{
				GetNode<RichTextLabel>($"Mask{i}").Text = pathToFullMask;
			}
			else 
			{
				GetNode<RichTextLabel>($"Mask{i}").Text = pathToEmptyMask;
			}
		}

	}
}
