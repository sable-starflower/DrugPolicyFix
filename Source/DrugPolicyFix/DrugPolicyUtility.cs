using RimWorld;
using Verse;

namespace DrugPolicyFix
{
    // Token: 0x02000005 RID: 5
    public class DrugPolicyUtility
    {
        // Token: 0x0600000A RID: 10 RVA: 0x0000265C File Offset: 0x0000085C
        public static bool IsAlcohol(ThingDef Drug)
        {
            if (Drug?.comps == null)
            {
                return false;
            }

            var comps = Drug.comps;
            if (comps.Count <= 0)
            {
                return false;
            }

            using var enumerator = comps.GetEnumerator();
            while (enumerator.MoveNext())
            {
                CompProperties_Drug compProperties_Drug;
                if ((compProperties_Drug = enumerator.Current as CompProperties_Drug) != null &&
                    compProperties_Drug.chemical == ChemicalDefOf.Alcohol)
                {
                    return true;
                }
            }

            return false;
        }

        // Token: 0x0600000B RID: 11 RVA: 0x000026E0 File Offset: 0x000008E0
        public static bool IsSmokey(ThingDef Drug)
        {
            if (Drug?.comps == null)
            {
                return false;
            }

            var comps = Drug.comps;
            if (comps.Count <= 0)
            {
                return false;
            }

            using var enumerator = comps.GetEnumerator();
            while (enumerator.MoveNext())
            {
                CompProperties_Drug compProperties_Drug;
                if ((compProperties_Drug = enumerator.Current as CompProperties_Drug) != null &&
                    compProperties_Drug.chemical == DefDatabase<ChemicalDef>.GetNamed("Smokeleaf"))
                {
                    return true;
                }
            }

            return false;
        }

        // Token: 0x0600000C RID: 12 RVA: 0x00002768 File Offset: 0x00000968
        public static bool IsAddictive(ThingDef Drug)
        {
            return Drug.IsAddictiveDrug;
        }
    }
}