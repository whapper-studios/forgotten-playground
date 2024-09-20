using Godot;
using System;
using System.Data;

public partial class GeoCounter : Control
{
	

	[Export]
	public AnimatedSprite2D geoIcon;
	[Export]
	public RichTextLabel plusSign;
	[Export]
	public RichTextLabel currentGeo;
	[Export]
	public RichTextLabel addedGeo;

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
			GD.Print(plusSign);
			plusSign.Visible = true;
		}
		addedGeo.Text = (geoAdded+newGeo).ToString();
		geoAdded += newGeo;
		addedGeo.GetChild<Timer>(0).Stop();
		addedGeo.GetChild<Timer>(0).Start();
		
	}

	public void OnTimerTimeout()
	{
		geoIcon.Play();
	}

	
    //
    public void OnGuiInput(InputEvent @event)
    {
        if (@event.IsActionPressed("select"))
		{
			UpdateGeoAdded(1);
		}
    }
}
