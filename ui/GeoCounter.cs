using Godot;
using System;

public partial class GeoCounter : Control
{
	[Export]
	public AnimatedSprite2D GeoIcon;
	[Export]
	public Label PlusSignLabel;
	[Export]
	public Label CurrentGeoLabel;
	[Export]
	public Label GeoToAddLabel;

	private bool IsGeoTransferring = false;
	private int GeoToAdd = 0;
	private int CurrentGeo = 0;
	private int GeoToAddChunkSize = 0;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (IsGeoTransferring)
		{
			int geoToTransfer = (int)Math.Round(GeoToAddChunkSize*(delta/1.5));
			if (geoToTransfer > 0) // There is enough geo stocked up to transfer a nonzero amount per frame
			{
				IsGeoTransferring = TransferGeo(geoToTransfer);
			}
			else 
			{
				IsGeoTransferring = TransferGeo(1);
			}
		}
	}

	// Updates the counter on GeoToAddLabel and starts/restarts its child timer node.
	public void UpdateGeoAdded(int newGeo)
	{
		if (GeoToAdd == 0)
		{
			PlusSignLabel.Text = "+";
		}
		GeoToAddLabel.Text = (GeoToAdd+newGeo).ToString();
		GeoToAdd += newGeo;
		GeoToAddLabel.GetChild<Timer>(0).Stop();
		GeoToAddLabel.GetChild<Timer>(0).Start();
	}

	// Both decreases GeoToAdd and increases CurrentGeo by geoToTransfer and checks
	// if there is no geo left to transfer, in order to make GeoToAdd and PlusSign invisible
	// and stop GeoIcon animation. 
	public bool TransferGeo(int geoToTransfer)
	{
		if ((GeoToAdd-geoToTransfer)<0)
		{
			GeoToAddLabel.Text = "";
			CurrentGeoLabel.Text = (CurrentGeo+GeoToAdd).ToString();
			GeoToAdd = 0;
			CurrentGeo += GeoToAdd; // Account for possible rounding issue. 
		}
		else 
		{
			GeoToAddLabel.Text = (GeoToAdd-geoToTransfer).ToString();
			CurrentGeoLabel.Text = (CurrentGeo+geoToTransfer).ToString();
			GeoToAdd -= geoToTransfer;
			CurrentGeo += geoToTransfer;
		}
		
		if (GeoToAdd > 0)
		{
			return true;
		}
		else
		{
			PlusSignLabel.Text = "";
			GeoToAddLabel.Text = "";
			GeoIcon.Stop();
			return false;
		} 
	}

	// When the GeoToAdd Label's child timer times out, start transferring all 
	// geo in GeoToAdd and play GeoIcon animation.
	public void OnTimerTimeout()
	{
		IsGeoTransferring = true;
		GeoToAddChunkSize = GeoToAdd;
		GeoIcon.Play();
	}

	
    // Placeholder to test geo "pickups" with large numbers as well as small. 
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
