using RimWorld;
using Verse;

namespace FuelBurning
{
    internal class PlaceWorker_SprinkledFuel : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map,
            Thing thingToIgnore = null, Thing thing = null)
        {
            var checkThingDef = checkingDef as ThingDef;
            var prop = checkThingDef?.GetCompProperties<CompProperties_FlammableLink>();
            if (prop == null)
            {
                return true;
            }

            var things = loc.GetThingList(map);
            foreach (var thing1 in things)
            {
                if (thing1.def == ThingDefOf.Wall || thing1.def.passability == Traversability.PassThroughOnly &&
                    thing1.def.category == ThingCategory.Item)
                {
                    return false;
                }

                var prop2 = thing1.def.GetCompProperties<CompProperties_FlammableLink>();
                if (prop2 != null && prop.connectionID == prop2.connectionID)
                {
                    return false;
                }

                var curBuildDef = GenConstruct.BuiltDefOf(thing1.def);
                if (curBuildDef is not ThingDef)
                {
                    continue;
                }

                var curDef = (ThingDef) curBuildDef;
                if (curDef == ThingDefOf.Wall)
                {
                    return false;
                }

                prop2 = curDef.GetCompProperties<CompProperties_FlammableLink>();
                if (prop2 != null && prop.connectionID == prop2.connectionID)
                {
                    return false;
                }
            }

            return true;
        }
    }
}