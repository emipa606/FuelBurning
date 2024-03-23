using System.Collections.Generic;
using Verse;

namespace FuelBurning;

internal class IgnitionDef : Def
{
    public readonly float baseIgnitionChance = 0.5f;
    public readonly float easeCooling = 0.001f;

    public readonly bool fireRemains = true;
    public readonly float firstFireSize = 1f;
    public readonly float maxHeatCapacity = 30f;

    public readonly List<DamageMultiplier> multiplierDirect = [];
    public readonly List<DamageMultiplier> multiplierSparks = [];
    public readonly float spreadPower = 1f;
}