using Godot;
using System;
using System.Data;

public partial class GeoCounter : Control
{
	
	private bool isGeoTransferring = false;
	private int geoAdded = 0;
	private int geoTotal = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Updates the counter on GeoAdded node, and, after a short time, decreases the GeoAdded count 
	// while increasing the CurrentGeo count. While the geo is "transferring", plays animation on GeoIcon
	public void UpdateGeoAdded(int newGeo)
	{
		if (geoAdded == 0)
		{
			GetNode<RichTextLabel>("HBoxContainer/VBoxContainer/Control/PlusSign").Visible = true;
		}
		RichTextLabel addedGeoNode = GetNode<RichTextLabel>("HBoxContainer/VBoxContainer2/CenterContainer2/AddedGeo");
		addedGeoNode.Text = (geoAdded+newGeo).ToString();
		geoAdded += newGeo;
		addedGeoNode.GetChild<Timer>(0).Stop();
		addedGeoNode.GetChild<Timer>(0).Start();
		
	}

	public void OnTimerTimeout()
	{
		GetNode<AnimatedSprite2D>("HBoxContainer/VBoxContainer/CenterContainer/Control/GeoIcon").Play();
	}

	
    //
    public override void _GuiInput(InputEvent @event)
    {
        if (@event.IsActionPressed("select"))
		{
			UpdateGeoAdded(1);
		}
    }
}
