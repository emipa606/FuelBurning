using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace FuelBurning {
    public class Designator_AreaNoFireFightingClear : Designator_AreaNoFireFighting {
        public Designator_AreaNoFireFightingClear() : base(DesignateMode.Remove)
		{
            this.defaultLabel = "DesignatorNoFireFightingClear".Translate();
            this.defaultDesc = "DesignatorNoFireFightingClearDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/Designators/NoFireFightingOff", true);
            this.soundDragSustain = SoundDefOf.Designate_DragAreaDelete;
            this.soundDragChanged = null;
            this.soundSucceeded = SoundDefOf.Designate_ZoneDelete;
        }
    }
}
