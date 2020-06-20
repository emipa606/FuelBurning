using RimWorld;
using UnityEngine;
using Verse;

namespace FuelBurning
{
    public class Designator_CleanFuel : Designator_Deconstruct
    {
        public Designator_CleanFuel()
        {
            this.defaultLabel = "DesignatorCleanFuel".Translate();
            this.defaultDesc = "DesignatorCleanFuelDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/Designators/CleanFuel", true);
            this.soundDragSustain = SoundDefOf.Designate_DragStandard;
            this.soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            this.useMouseIcon = true;
            this.soundSucceeded = SoundDefOf.Designate_Deconstruct;
            this.hotKey = null;
        }

        public override AcceptanceReport CanDesignateThing(Thing t)
        {
            if (base.CanDesignateThing(t).Accepted)
            {
                if(t is Building_SprinkledFuel)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
