using RimWorld;
using Verse;

namespace KenshiAnimals
{
    [DefOf]
    public static class ThingDefOf
    {
        public static ThingDef KenshiFauna_Leviathan;

        static ThingDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
    }
}
