using HarmonyLib;
using RimWorld;

namespace DrugPolicyFix
{
    // Token: 0x02000008 RID: 8
    [HarmonyPatch(typeof(DrugPolicyDatabase), "MakeNewDrugPolicy")]
    public class MakeNewDrugPolicy_Patch
    {
        // Token: 0x06000010 RID: 16 RVA: 0x000027A4 File Offset: 0x000009A4
        [HarmonyPostfix]
        [HarmonyPriority(0)]
        public static void Postfix(ref DrugPolicy __result)
        {
            DrugPolicySort.SortPolicy(__result);
        }
    }
}