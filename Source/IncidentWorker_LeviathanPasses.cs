using RimWorld;
using UnityEngine;
using Verse;

namespace KenshiAnimals
{
    public class IncidentWorker_LeviathanPasses : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            Map target = (Map)parms.target;
            return !target.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout) && (!ModsConfig.BiotechActive || !target.gameConditionManager.ConditionIsActive(GameConditionDefOf.NoxiousHaze)) && target.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(ThingDefOf.KenshiFauna_Leviathan) && this.TryFindEntryCell(target, out IntVec3 _);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map target = (Map)parms.target;
            IntVec3 cell;
            if (!this.TryFindEntryCell(target, out cell))
                return false;
            PawnKindDef KenshiFaunaLeviathan = PawnKindDefOf.KenshiFauna_Leviathan;
            int num1 = Mathf.Clamp(GenMath.RoundRandom(StorytellerUtility.DefaultThreatPointsNow((IIncidentTarget)target) / KenshiFaunaLeviathan.combatPower), 2, Rand.RangeInclusive(3, 6));
            int num2 = Rand.RangeInclusive(90000, 150000);
            IntVec3 result = IntVec3.Invalid;
            if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(cell, target, 10f, out result))
                result = IntVec3.Invalid;
            Pawn newThing = (Pawn)null;
            for (int index = 0; index < num1; ++index)
            {
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(cell, target, 10);
                newThing = PawnGenerator.GeneratePawn(KenshiFaunaLeviathan);
                GenSpawn.Spawn((Thing)newThing, loc, target, Rot4.Random);
                newThing.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num2;
                if (result.IsValid)
                    newThing.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(result, target, 10);
            }
            this.SendStandardLetter("LetterLabelLeviathanPasses".Translate((NamedArgument)KenshiFaunaLeviathan.label).CapitalizeFirst(), "LetterLeviathanPasses".Translate((NamedArgument)KenshiFaunaLeviathan.label), LetterDefOf.PositiveEvent, parms, (LookTargets)(Thing)newThing);
            return true;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell) => RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
    }
}
