using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Verse;

namespace FuelBurning
{
    class Building_SprinkledFuel : Building
    {
        private const int TicksCheckCellInterval = 20;
        private const float AttachSparksHeat = TicksCheckCellInterval * 2f;

        private FlammableLinkComp flammableLinkcomp;
        
        private Graphic graphicInt;
        public override Graphic Graphic
        {
            get
            {
                if (this.graphicInt == null)
                {
                    this.graphicInt = GraphicDatabase.Get<Graphic_LinkedCornerComplement>(base.def.graphicData.texPath, ShaderDatabase.Transparent);
                }
                return this.graphicInt;
            }
        }

        public override void Tick()
        {
            base.Tick();
            if(this.IsHashIntervalTick(TicksCheckCellInterval))
            {
                if (this.flammableLinkcomp == null || this.flammableLinkcomp.BurningNow)
                {
                    return;
                }
                List<Thing> things = base.Map.thingGrid.ThingsListAt(base.Position);
                for (int i = 0; i < things.Count; i++)
                {
                    this.CheckSparksFromPawn(things[i] as Pawn);
                    this.CheckBulletFire(things[i] as Mote);
                }
            }
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            this.flammableLinkcomp = base.GetComp<FlammableLinkComp>();
        }

        public override void PreApplyDamage(ref DamageInfo dinfo, out bool absorbed) {
            absorbed = false;
            
            if (this.flammableLinkcomp != null) {
                if (!(FireUtility.ContainsStaticFire(base.Position, base.Map))) {
                    
                    if ((dinfo.Instigator != null && dinfo.Instigator.Faction != Faction.OfPlayer) && !(dinfo.Weapon != null && dinfo.Weapon.IsRangedWeapon)) {
                        return;
                    }
                    
                    float heat = flammableLinkcomp.HeatedByHitOf(dinfo);
                    this.flammableLinkcomp.TrySparksFly(heat);
                    MoteUtility.DrawHeatedMote(this.flammableLinkcomp.HeatRatio, base.DrawPos, base.Position, base.Map);
                    dinfo.SetAmount(0);
                }
            }
        }
        private void CheckSparksFromPawn(Pawn pawn)
        {
            if (pawn == null)
            {
                return;
            }
            if (pawn.HasAttachment(ThingDefOf.Fire))
            {
                if (this.flammableLinkcomp.TrySparksFly(AttachSparksHeat) == SparksFlyResult.Undefine)
                {
                    MoteUtility.DrawHeatedMote(this.flammableLinkcomp.HeatRatio, base.DrawPos, base.Position, base.Map);
                }
            }
        }

        private void CheckBulletFire(Mote mote)
        {
            if(mote == null)
            {
                return;
            }
            // Launch
            if(mote.def == ThingDefOf.Mote_ShotFlash)
            {
                float heat = mote.exactScale.x * 4f;
                if (this.flammableLinkcomp.TrySparksFly(heat) == SparksFlyResult.Undefine)
                {
                    MoteUtility.DrawHeatedMote(this.flammableLinkcomp.HeatRatio, base.DrawPos, base.Position, base.Map);
                }
#if DEBUG
                Log.Message("Mote_ShotFlash base:" + base.DrawPos + " heat:" + heat + " cap:" + this.flammableLinkcomp.AmountOfHeat);
#endif
            }

            // Impact
            if (mote.def == ThingDefOf.Mote_ShotHit_Dirt)
            {
                if (this.flammableLinkcomp.TrySparksFly(60f) == SparksFlyResult.Undefine)
                {
                    MoteUtility.DrawHeatedMote(this.flammableLinkcomp.HeatRatio, base.DrawPos, base.Position, base.Map);
                }
#if DEBUG
                Log.Message("Mote_ShotHit_Dirt base:" + base.DrawPos + " cap:" + this.flammableLinkcomp.AmountOfHeat);
#endif
            }
        }
    }
}
