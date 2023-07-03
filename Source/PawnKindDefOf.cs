using RimWorld;
using Verse;

namespace KenshiAnimals
{
    [DefOf]
    public static class PawnKindDefOf
    {
        public static PawnKindDef KenshiFauna_Leviathan;

        static PawnKindDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
    }
}
