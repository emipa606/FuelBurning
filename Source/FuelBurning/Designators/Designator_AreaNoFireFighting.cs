using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace FuelBurning {
    public abstract class Designator_AreaNoFireFighting : Designator_Area {

        private DesignateMode mode;

        public override int DraggableDimensions => 2;
        public override bool DragDrawMeasurements => true;

        public static Area NoFireFightingArea(AreaManager areaManager) {
            Area result = areaManager.Get<Area_NoFireFighting>();
            if (result == null) {
                result = new Area_NoFireFighting(areaManager);
                areaManager.AllAreas.Add(result);
            }
            return result;
        }

        public Designator_AreaNoFireFighting(DesignateMode mode) {
            this.mode = mode;
            this.soundDragSustain = SoundDefOf.Designate_DragStandard;
            this.soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            this.useMouseIcon = true;
        }


        public override AcceptanceReport CanDesignateCell(IntVec3 loc) {
            if (!loc.InBounds(base.Map)) {
                return false;
            }
            bool flag = Designator_AreaNoFireFighting.NoFireFightingArea(base.Map.areaManager)[loc];
            if (this.mode == DesignateMode.Add) {
                return !flag;
            }
            return flag;
        }

        public override void DesignateSingleCell(IntVec3 c) {
            Designator_AreaNoFireFighting.NoFireFightingArea(base.Map.areaManager)[c] = this.mode == DesignateMode.Add;
        }

        public override void SelectedUpdate() {
            GenUI.RenderMouseoverBracket();
            Designator_AreaNoFireFighting.NoFireFightingArea(base.Map.areaManager).MarkForDraw();
        }
    }
}
