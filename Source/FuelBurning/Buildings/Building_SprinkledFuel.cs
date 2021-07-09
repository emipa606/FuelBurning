using RimWorld;
using Verse;

namespace FuelBurning
{
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
                if (graphicInt == null)
                {
                    graphicInt =
                        GraphicDatabase.Get<Graphic_LinkedCornerComplement>(def.graphicData.texPath,
                            ShaderDatabase.Transparent);
                }

                return graphicInt;
            }
        }

        public override void Tick()
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
                CheckSparksFromPawn(thing as Pawn);
                CheckBulletFire(thing as Mote);
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
                !(dinfo.Weapon != null && dinfo.Weapon.IsRangedWeapon))
            {
                return;
            }

            var heat = flammableLinkcomp.HeatedByHitOf(dinfo);
            flammableLinkcomp.TrySparksFly(heat);
            FleckUtility.DrawHeatedMote(flammableLinkcomp.HeatRatio, base.DrawPos, Position, Map);
            dinfo.SetAmount(0);
        }

        private void CheckSparksFromPawn(Pawn pawn)
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

        private void CheckBulletFire(Mote mote)
        {
            if (mote == null)
            {
                return;
            }

            // Launch
            if (mote.def.defName.Contains("ShotFlash"))
            {
                var heat = mote.exactScale.x * 4f;
                if (flammableLinkcomp.TrySparksFly(heat) == SparksFlyResult.Undefine)
                {
                    FleckUtility.DrawHeatedMote(flammableLinkcomp.HeatRatio, base.DrawPos, Position, Map);
                }
#if DEBUG
                Log.Message("Mote_ShotFlash base:" + base.DrawPos + " heat:" + heat + " cap:" + this.flammableLinkcomp.AmountOfHeat);
#endif
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
#if DEBUG
                Log.Message("Mote_ShotHit_Dirt base:" + base.DrawPos + " cap:" + this.flammableLinkcomp.AmountOfHeat);
#endif
        }
    }
}