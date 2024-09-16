using Godot;

public partial class AtlasAnimator : Node
{
	/// <summary>
	/// Uses the current value of the region property and the height/width of the Texture2D found in the atlas property
	/// to change the region to the next frame in the sprite sheet. Assumes the region size doesn't change 
	/// from frame to frame and that there is no margin between the frames in the sprite sheet. 
	/// </summary>
	/// <param name="spriteSheetResource">AtlasTexture containing a sprite sheet in the atlas property.</param>
	public static void NextFrame(AtlasTexture spriteSheetResource)
	{
		Texture2D atlas = spriteSheetResource.Atlas;
		Rect2 oldRegion = spriteSheetResource.Region;
		if (oldRegion.Position.X + (2 * oldRegion.Size.X) <= atlas.GetSize().X)
		{
			Rect2 newRegion = new Rect2(oldRegion.Position.X + oldRegion.Size.X, oldRegion.Position.Y, oldRegion.Size);
			spriteSheetResource.Region = newRegion;
			return;
		}
		else if (oldRegion.Position.Y + (2 * oldRegion.Size.Y) <= atlas.GetSize().Y)
		{
			Rect2 newRegion = new Rect2(0, oldRegion.Position.Y + oldRegion.Size.Y, oldRegion.Size);
			spriteSheetResource.Region = newRegion;
			return;
		}
		else
		{
			Rect2 newRegion = new Rect2(0, 0, oldRegion.Size); 
			spriteSheetResource.Region = newRegion;
		}
	}
}
