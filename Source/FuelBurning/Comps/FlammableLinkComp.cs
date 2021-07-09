using RimWorld;
using Verse;

namespace FuelBurning
{
    internal class FlammableLinkComp : ThingComp
    {
        private const float CorrectIgnitionChance = 0.7f;
        private const float CorrectWeatherFactorCooling = -2.0f;
        private const float CorrectWeatherFactorHeating = 0.3f;
        private float amountOfHeatInt;
        private bool burningNowInt;

        private CompProperties_FlammableLink compDef;

        public bool BurningNow
        {
            get => burningNowInt;
            protected set => burningNowInt = value;
        }

        public float AmountOfHeat
        {
            get => amountOfHeatInt;
            protected set => amountOfHeatInt = value;
        }

        public float HeatRatio => AmountOfHeat / compDef.ignitionDef.maxHeatCapacity;

        public override void CompTick()
        {
            if (parent.Position.ContainsStaticFire(parent.Map))
            {
                BurningNow = true;
                for (var i = 0; i < 4; i++)
                {
                    var vec = parent.Position + GenAdj.CardinalDirectionsAround[i];
                    var things = parent.Map.thingGrid.ThingsListAt(vec);
                    // ReSharper disable once ForCanBeConvertedToForeach Collection gets modified
                    for (var index = 0; index < things.Count; index++)
                    {
                        var thing = things[index];
                        var comp = thing.TryGetComp<FlammableLinkComp>();
                        if (comp != null && IsConnected(comp))
                        {
                            comp.TrySparksFly(compDef.ignitionDef.spreadPower);
                        }
                    }
                }
            }
            else
            {
                BurningNow = false;
                AmountOfHeat -= WeatherFactor(compDef.ignitionDef.easeCooling, CorrectWeatherFactorCooling);
                if (AmountOfHeat < 0)
                {
                    AmountOfHeat = 0;
                }
            }
        }

        public SparksFlyResult TrySparksFly(float sp)
        {
            if (sp == 0)
            {
                return SparksFlyResult.ZeroValue;
            }

            AmountOfHeat += WeatherFactor(sp, CorrectWeatherFactorHeating);
            if (BurningNow)
            {
                var curFire = parent.Map.thingGrid.ThingAt<Fire>(parent.Position);
                if (curFire == null)
                {
                    return SparksFlyResult.Undefine;
                }

                curFire.fireSize = compDef.ignitionDef.firstFireSize;
                return SparksFlyResult.GrowFire;
            }

            if (!IgnitionChance())
            {
                return SparksFlyResult.Undefine;
            }

            AmountOfHeat = compDef.ignitionDef.maxHeatCapacity;
            TryBurning(compDef.ignitionDef.firstFireSize);
            return SparksFlyResult.TryBurning;
        }

        protected virtual bool IgnitionChance()
        {
            var max = compDef.ignitionDef.maxHeatCapacity;
            var now = AmountOfHeat;
            var chance = compDef.ignitionDef.baseIgnitionChance + ((now / max) - CorrectIgnitionChance);
            return Rand.Chance(chance);
        }

        protected virtual void TryBurning(float fireSize)
        {
            if (parent.Position.ContainsStaticFire(parent.Map))
            {
                return;
            }

            var fire = (Fire) ThingMaker.MakeThing(ThingDefOf.Fire);
            fire.fireSize = fireSize;
            GenSpawn.Spawn(fire, parent.Position, parent.Map, Rot4.North);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            if (!respawningAfterLoad)
            {
                AmountOfHeat = 0f;
                BurningNow = false;
            }

            compDef = props as CompProperties_FlammableLink;
            if (compDef != null)
            {
                return;
            }

            Log.Error("CompProperties_FlammableLink was not found");
            compDef = new CompProperties_FlammableLink();
        }

        public float HeatedByHitOf(DamageInfo info)
        {
            foreach (var dDef in compDef.ignitionDef.multiplierDirect)
            {
                if (info.Def == dDef.damageDef)
                {
                    return info.Amount * dDef.multiplier;
                }
            }

            return 0f;
        }

        public float HeatedBySparksOf(DamageInfo info)
        {
            foreach (var dDef in compDef.ignitionDef.multiplierSparks)
            {
                if (info.Def == dDef.damageDef)
                {
                    return info.Amount * dDef.multiplier;
                }
            }

            return 0f;
        }

        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
            if (compDef.ignitionDef.fireRemains || mode != DestroyMode.KillFinalize)
            {
                return;
            }

            var list = previousMap.thingGrid.ThingsListAt(parent.Position);
            // ReSharper disable once ForCanBeConvertedToForeach Collection is modified
            for (var index = 0; index < list.Count; index++)
            {
                var thing = list[index];
                if (thing is Fire {parent: null, Destroyed: false} fire)
                {
                    fire.Destroy(DestroyMode.KillFinalize);
                }
            }
        }

        protected float WeatherFactor(float sp, float coeff)
        {
            return sp - (sp * parent.Map.weatherManager.curWeather.rainRate * coeff);
        }

        protected bool IsConnected(FlammableLinkComp comp)
        {
            return comp.compDef.connectionID == compDef.connectionID;
        }

        public override void PostExposeData()
        {
            Scribe_Values.Look(ref burningNowInt, "burningNowInt");
            Scribe_Values.Look(ref amountOfHeatInt, "amountOfHeatInt");
            base.PostExposeData();
        }
    }
}