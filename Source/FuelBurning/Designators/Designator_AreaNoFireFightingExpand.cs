using RimWorld;
using UnityEngine;
using Verse;

namespace FuelBurning;

public class Designator_AreaNoFireFightingExpand : Designator_AreaNoFireFighting
{
    public Designator_AreaNoFireFightingExpand() : base(DesignateMode.Add)
    {
        defaultLabel = "DesignatorNoFireFightingExpand".Translate();
        defaultDesc = "DesignatorNoFireFightingExpandDesc".Translate();
        icon = ContentFinder<Texture2D>.Get("UI/Designators/NoFireFightingOn");
        soundDragSustain = SoundDefOf.Designate_DragAreaAdd;
        soundDragChanged = null;
        soundSucceeded = SoundDefOf.Designate_ZoneDelete;
    }
}