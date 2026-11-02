using System.Linq;
using RimWorld;
using Verse;

namespace FuelBurning;

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
        foreach (var thingDef in things.Select(thingItem => thingItem.def))
        {
            if (thingDef == ThingDefOf.Wall || thingDef.passability == Traversability.PassThroughOnly &&
                thingDef.category == ThingCategory.Item)
            {
                return false;
            }

            var prop2 = thingDef.GetCompProperties<CompProperties_FlammableLink>();
            if (prop2 != null && prop.connectionID == prop2.connectionID)
            {
                return false;
            }

            var curBuildDef = GenConstruct.BuiltDefOf(thingDef);
            if (curBuildDef is not ThingDef curDef)
            {
                continue;
            }

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