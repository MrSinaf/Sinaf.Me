namespace Sinaf.Me.Data.Warhammer;

public partial class Character
{
	public string GetUrlThumbnail()
		=> $"warhammer/characters/{Id}_thumbnail.png" + (UpdatedAt.HasValue ? 
				$"?v={UpdatedAt?.Ticks}" : string.Empty);
	
	public string GetUrlImage()
		=> $"warhammer/characters/{Id}.png" + (UpdatedAt.HasValue ? 
				$"?v={UpdatedAt?.Ticks}" : string.Empty);
}