using RimWorld;
using UnityEngine;
using Verse;

namespace FuelBurning;

internal static class FleckUtility
{
    public static void DrawHeatedMote(float ratio, Vector3 drawPos, IntVec3 pos, Map map)
    {
        switch (ratio)
        {
            case <= 0:
                return;
            case < 0.33f:
                FleckMaker.ThrowSmoke(drawPos, map, 0.3f + ratio);
                break;
            case < 0.66f:
                FleckMaker.ThrowMetaPuff(drawPos, map);
                FleckMaker.ThrowMicroSparks(drawPos, map);
                break;
            default:
                FleckMaker.ThrowMetaPuff(drawPos, map);
                FleckMaker.ThrowMicroSparks(drawPos, map);
                FleckMaker.ThrowFireGlow(pos.ToVector3(), map, ratio - 0.3f);
                break;
        }
    }
}