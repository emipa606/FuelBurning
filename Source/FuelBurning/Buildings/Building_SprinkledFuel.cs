using RimWorld;
using Verse;

namespace FuelBurning;

internal class Building_SprinkledFuel : Building
{
    private const int TicksCheckCellInterval = 20;
    private const float AttachSparksHeat = TicksCheckCellInterval * 2f;

    private FlammableLinkComp flammableLinkcomp;

    private Graphic graphicInt;

    public override Graphic Graphic
    {
        get
        {
            graphicInt ??= GraphicDatabase.Get<Graphic_LinkedCornerComplement>(def.graphicData.texPath,
                ShaderDatabase.Transparent);

            return graphicInt;
        }
    }

    protected override void Tick()
    {
        base.Tick();
        if (!this.IsHashIntervalTick(TicksCheckCellInterval))
        {
            return;
        }

        if (flammableLinkcomp == null || flammableLinkcomp.BurningNow)
        {
            return;
        }

        var things = Map.thingGrid.ThingsListAt(Position);
        foreach (var thing in things)
        {
            checkSparksFromPawn(thing as Pawn);
            checkBulletFire(thing as Mote);
        }
    }

    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);
        flammableLinkcomp = GetComp<FlammableLinkComp>();
    }

    public override void PreApplyDamage(ref DamageInfo dinfo, out bool absorbed)
    {
        absorbed = false;

        if (flammableLinkcomp == null)
        {
            return;
        }

        if (Position.ContainsStaticFire(Map))
        {
            return;
        }

        if (dinfo.Instigator != null && dinfo.Instigator.Faction != Faction.OfPlayer &&
            dinfo.Weapon is not { IsRangedWeapon: true })
        {
            return;
        }

        var heat = flammableLinkcomp.HeatedByHitOf(dinfo);
        flammableLinkcomp.TrySparksFly(heat);
        FleckUtility.DrawHeatedMote(flammableLinkcomp.HeatRatio, base.DrawPos, Position, Map);
        dinfo.SetAmount(0);
    }

    private void checkSparksFromPawn(Pawn pawn)
    {
        if (pawn == null)
        {
            return;
        }

        if (!pawn.HasAttachment(ThingDefOf.Fire))
        {
            return;
        }

        if (flammableLinkcomp.TrySparksFly(AttachSparksHeat) == SparksFlyResult.Undefine)
        {
            FleckUtility.DrawHeatedMote(flammableLinkcomp.HeatRatio, base.DrawPos, Position, Map);
        }
    }

    private void checkBulletFire(Mote mote)
    {
        if (mote == null)
        {
            return;
        }

        // Launch
        if (mote.def.defName.Contains("ShotFlash"))
        {
            var heat = mote.ExactScale.x * 4f;
            if (flammableLinkcomp.TrySparksFly(heat) == SparksFlyResult.Undefine)
            {
                FleckUtility.DrawHeatedMote(flammableLinkcomp.HeatRatio, base.DrawPos, Position, Map);
            }
        }

        // Impact
        if (!mote.def.defName.Contains("ShotHit_Dirt"))
        {
            return;
        }

        if (flammableLinkcomp.TrySparksFly(60f) == SparksFlyResult.Undefine)
        {
            FleckUtility.DrawHeatedMote(flammableLinkcomp.HeatRatio, base.DrawPos, Position, Map);
        }
    }
}