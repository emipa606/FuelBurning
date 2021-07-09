using RimWorld;
using Verse;
using Verse.AI;

namespace FuelBurning
{
    public class WorkGiver_ExtendedFightFires : WorkGiver_Scanner
    {
        private const int NearbyPawnRadius = 15;
        private const int MaxReservationCheckDistance = 15;
        private const float HandledDistance = 5f;

        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(ThingDefOf.Fire);
        public override PathEndMode PathEndMode => PathEndMode.Touch;

        public override Danger MaxPathDanger(Pawn pawn)
        {
            return Danger.Deadly;
        }

        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!(t is Fire fire))
            {
                return false;
            }

            if (fire.parent is Pawn pawn2)
            {
                if (pawn2 == pawn)
                {
                    return false;
                }

                if ((pawn2.Faction == pawn.Faction || pawn2.HostFaction == pawn.Faction ||
                     pawn2.HostFaction == pawn.HostFaction) && !pawn.Map.areaManager.Home[fire.Position] &&
                    IntVec3Utility.ManhattanDistanceFlat(pawn.Position, pawn2.Position) > MaxReservationCheckDistance)
                {
                    return false;
                }

                if (!pawn.CanReach(pawn2, PathEndMode.Touch, Danger.Deadly))
                {
                    return false;
                }
            }
            else
            {
                if (pawn.story.DisabledWorkTagsBackstoryAndTraits.OverlapsWithOnAnyWorkType(WorkTags.Firefighting))
                {
                    return false;
                }

                if (!pawn.Map.areaManager.Home[fire.Position])
                {
                    JobFailReason.Is(WorkGiver_FixBrokenDownBuilding.NotInHomeAreaTrans);
                    return false;
                }
            }

            if (Designator_AreaNoFireFighting.NoFireFightingArea(pawn.Map.areaManager)[fire.Position])
            {
                return false;
            }

            if ((pawn.Position - fire.Position).LengthHorizontalSquared <=
                MaxReservationCheckDistance * NearbyPawnRadius)
            {
                return !FireIsBeingHandled(fire, pawn);
            }

            LocalTargetInfo target = fire;
            if (!pawn.CanReserve(target, 1, -1, null, forced))
            {
                return false;
            }

            return !FireIsBeingHandled(fire, pawn);
        }

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            return new Job(JobDefOf.BeatFire, t);
        }

        public static bool FireIsBeingHandled(Fire f, Pawn potentialHandler)
        {
            if (!f.Spawned)
            {
                return false;
            }

            var pawn = f.Map.reservationManager.FirstRespectedReserver(f, potentialHandler);
            return pawn != null && pawn.Position.InHorDistOf(f.Position, HandledDistance);
        }
    }
}