using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace FuelBurning {
    public class Area_NoFireFighting : Area {

        public Area_NoFireFighting() { }
        public Area_NoFireFighting(AreaManager areaManager) : base(areaManager) { }

        public override string Label => "NoFireFighting".Translate();

        public override Color Color => new Color(0.9f, 0.2f, 0.1f);

        public override int ListPriority => 10500;

        public override string GetUniqueLoadID() {
            return "Area_" + this.ID + "_NoFireFighting";
        }
    }
}
