using RimWorld;
using UnityEngine;
using Verse;

namespace FuelBurning;

public class Designator_CleanFuel : Designator_Deconstruct
{
    public Designator_CleanFuel()
    {
        defaultLabel = "DesignatorCleanFuel".Translate();
        defaultDesc = "DesignatorCleanFuelDesc".Translate();
        icon = ContentFinder<Texture2D>.Get("UI/Designators/CleanFuel");
        soundDragSustain = SoundDefOf.Designate_DragStandard;
        soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
        useMouseIcon = true;
        soundSucceeded = SoundDefOf.Designate_Deconstruct;
        hotKey = null;
    }

    public override AcceptanceReport CanDesignateThing(Thing t)
    {
        if (!base.CanDesignateThing(t).Accepted)
        {
            return false;
        }

        return t is Building_SprinkledFuel;
    }
}