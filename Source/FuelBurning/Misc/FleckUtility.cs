using RimWorld;
using UnityEngine;
using Verse;

namespace FuelBurning;

internal class FleckUtility
{
    public static void DrawHeatedMote(float ratio, Vector3 drawPos, IntVec3 pos, Map map)
    {
        if (ratio <= 0)
        {
            return;
        }

        if (ratio < 0.33f)
        {
            FleckMaker.ThrowSmoke(drawPos, map, 0.3f + ratio);
        }
        else if (ratio < 0.66f)
        {
            FleckMaker.ThrowMetaPuff(drawPos, map);
            FleckMaker.ThrowMicroSparks(drawPos, map);
        }
        else
        {
            FleckMaker.ThrowMetaPuff(drawPos, map);
            FleckMaker.ThrowMicroSparks(drawPos, map);
            FleckMaker.ThrowFireGlow(pos.ToVector3(), map, ratio - 0.3f);
        }
    }
}