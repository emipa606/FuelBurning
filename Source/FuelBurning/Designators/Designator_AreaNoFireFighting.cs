using RimWorld;
using Verse;

namespace FuelBurning
{
    public abstract class Designator_AreaNoFireFighting : Designator_Area
    {
        private readonly DesignateMode mode;

        public Designator_AreaNoFireFighting(DesignateMode mode)
        {
            this.mode = mode;
            soundDragSustain = SoundDefOf.Designate_DragStandard;
            soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            useMouseIcon = true;
        }

        public override int DraggableDimensions => 2;
        public override bool DragDrawMeasurements => true;

        public static Area NoFireFightingArea(AreaManager areaManager)
        {
            Area result = areaManager.Get<Area_NoFireFighting>();
            if (result != null)
            {
                return result;
            }

            result = new Area_NoFireFighting(areaManager);
            areaManager.AllAreas.Add(result);

            return result;
        }


        public override AcceptanceReport CanDesignateCell(IntVec3 loc)
        {
            if (!loc.InBounds(Map))
            {
                return false;
            }

            if (mode == DesignateMode.Add)
            {
                return !NoFireFightingArea(Map.areaManager)[loc];
            }

            return NoFireFightingArea(Map.areaManager)[loc];
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            NoFireFightingArea(Map.areaManager)[c] = mode == DesignateMode.Add;
        }

        public override void SelectedUpdate()
        {
            GenUI.RenderMouseoverBracket();
            NoFireFightingArea(Map.areaManager).MarkForDraw();
        }
    }
}