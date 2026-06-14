using System;
using System.Collections.Generic;

namespace Sinaf.Me.Data.Warhammer;

public partial class CharacterDetail
{
    public uint Id { get; set; }

    public string? Name { get; set; }

    public int ThumbnailX { get; set; }

    public int ThumbnailY { get; set; }

    public byte ThumbnailS { get; set; }

    public string? Description { get; set; }

    public string? Commentary { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint? ClanId { get; set; }

    public string? ClanName { get; set; }

    public long TotalBattles { get; set; }

    public decimal TotalKillsParticipating { get; set; }

    public decimal TotalUnitKills { get; set; }

    public decimal TotalObjectives { get; set; }

    public decimal TotalFailedCharges { get; set; }

    public decimal TotalImpossibleSaves { get; set; }

    public decimal TotalDamageDone { get; set; }

    public decimal TotalDamageTaken { get; set; }

    public decimal TotalDamageBlocked { get; set; }

    public decimal? TotalDeaths { get; set; }
}
