using Godot;
using System;
using System.Data;

public partial class GeoCounter : Control
{
	

	[Export]
	public AnimatedSprite2D geoIcon;
	[Export]
	public Label plusSign;
	[Export]
	public Label currentGeo;
	[Export]
	public Label addedGeo;

	private bool isGeoTransferring = false;
	private int geoAdded = 0;
	private int geoTotal = 0;
	private int geoAddChunkSize = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		if (isGeoTransferring)
		{
			int geoToTransfer = (int)Math.Round(geoAddChunkSize*(delta/1.5));
			if (geoToTransfer > 0)
			{
				isGeoTransferring = TransferGeo(geoToTransfer);
			}
			else 
			{
				isGeoTransferring = TransferGeo(1);
			}
			
		}
	}

	// Updates the counter on GeoAdded node, and, after a short time, decreases the GeoAdded count 
	// while increasing the CurrentGeo count. While the geo is "transferring", plays animation on GeoIcon
	public void UpdateGeoAdded(int newGeo)
	{
		if (geoAdded == 0)
		{
			plusSign.Visible = true;
			addedGeo.Visible = true;
		}
		addedGeo.Text = (geoAdded+newGeo).ToString();
		geoAdded += newGeo;
		addedGeo.GetChild<Timer>(0).Stop();
		addedGeo.GetChild<Timer>(0).Start();
		
	}

	public bool TransferGeo(int geo)
	{
		if ((geoAdded-geo)<0)
		{
			addedGeo.Text = "0";
			currentGeo.Text = (geoTotal+geoAdded).ToString();
			geoAdded = 0;
			geoTotal += geoAdded; // Account for possible rounding issue. 
		}
		else 
		{
			addedGeo.Text = (geoAdded-geo).ToString();
			currentGeo.Text = (geoTotal+geo).ToString();
			geoAdded -= geo;
			geoTotal += geo;
		}
		
		if (geoAdded > 0)
		{
			return true;
		}
		else
		{
			plusSign.Visible = false;
			addedGeo.Visible = false;
			geoIcon.Stop();
			return false;
		} 
	}

	public void OnTimerTimeout()
	{
		isGeoTransferring = true;
		geoAddChunkSize = geoAdded;
		geoIcon.Play();
	}

	
    //
    public void OnGuiInput(InputEvent @event)
    {
        if (@event.IsActionPressed("select"))
		{
			UpdateGeoAdded(1);
		}
		if (@event.IsActionPressed("secondary"))
		{
			UpdateGeoAdded(100);
		}
    }
}
