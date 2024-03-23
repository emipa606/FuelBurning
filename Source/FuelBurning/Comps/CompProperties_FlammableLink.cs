using Verse;

namespace FuelBurning;

internal class CompProperties_FlammableLink : CompProperties
{
    public readonly int connectionID = 1;
    public readonly IgnitionDef ignitionDef = new IgnitionDef();
}