using System.Collections.Generic;
using Verse;

namespace FuelBurning;

internal class IgnitionDef : Def
{
    public float baseIgnitionChance = 0.5f;
    public float easeCooling = 0.001f;

    public bool fireRemains = true;
    public float firstFireSize = 1f;
    public float maxHeatCapacity = 30f;

    public List<DamageMultiplier> multiplierDirect = new List<DamageMultiplier>();
    public List<DamageMultiplier> multiplierSparks = new List<DamageMultiplier>();
    public float spreadPower = 1f;
}