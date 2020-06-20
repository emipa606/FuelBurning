using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace FuelBurning {
    public class Designator_AreaNoFireFightingExpand : Designator_AreaNoFireFighting {
        public Designator_AreaNoFireFightingExpand() : base(DesignateMode.Add)
		{
            this.defaultLabel = "DesignatorNoFireFightingExpand".Translate();
            this.defaultDesc = "DesignatorNoFireFightingExpandDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/Designators/NoFireFightingOn", true);
            this.soundDragSustain = SoundDefOf.Designate_DragAreaAdd;
            this.soundDragChanged = null;
            this.soundSucceeded = SoundDefOf.Designate_ZoneDelete;
        }
    }
}
