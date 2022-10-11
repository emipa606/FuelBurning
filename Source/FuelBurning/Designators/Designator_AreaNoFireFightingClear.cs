using RimWorld;
using UnityEngine;
using Verse;

namespace FuelBurning;

public class Designator_AreaNoFireFightingClear : Designator_AreaNoFireFighting
{
    public Designator_AreaNoFireFightingClear() : base(DesignateMode.Remove)
    {
        defaultLabel = "DesignatorNoFireFightingClear".Translate();
        defaultDesc = "DesignatorNoFireFightingClearDesc".Translate();
        icon = ContentFinder<Texture2D>.Get("UI/Designators/NoFireFightingOff");
        soundDragSustain = SoundDefOf.Designate_DragAreaDelete;
        soundDragChanged = null;
        soundSucceeded = SoundDefOf.Designate_ZoneDelete;
    }
}