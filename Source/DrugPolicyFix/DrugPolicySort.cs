using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace DrugPolicyFix
{
	// Token: 0x02000004 RID: 4
	public static class DrugPolicySort
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002400 File Offset: 0x00000600
		public unsafe static void SortPolicy(DrugPolicy DP)
		{
			
			List<DrugPolicyEntry> list = NonPublicFields.DrugPolicyEntryList(DP);
			List<DrugPolicyEntry> list2 = new List<DrugPolicyEntry>();
			if (list.Count > 0)
			{
				foreach (DrugPolicyEntry drugPolicyEntry in list)
				{
					if (((drugPolicyEntry != null) ? drugPolicyEntry.drug : null) != null)
					{
						DrugPolicyEntry drugPolicyEntry2;
						DrugPolicySort.CheckValues(DP, drugPolicyEntry, drugPolicyEntry.drug, out drugPolicyEntry2);
						if (drugPolicyEntry2 != null)
						{
							list2.AddDistinct(drugPolicyEntry2);
						}
					}
				}
				if (list2.Count > 0)
				{
					List<DrugPolicyEntry> list3 = (from dpe in list2
					orderby dpe.drug.label
					select dpe).ToList<DrugPolicyEntry>();
					NonPublicFields.DrugPolicyEntryList(DP) = list3;
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000024D4 File Offset: 0x000006D4
		public unsafe static void SortPolicies()
		{
			List<DrugPolicy> allPolicies = Current.Game.drugPolicyDatabase.AllPolicies;
			int num = 0;
			if (allPolicies.Count > 0)
			{
				foreach (DrugPolicy drugPolicy in allPolicies)
				{
					List<DrugPolicyEntry> list = NonPublicFields.DrugPolicyEntryList(drugPolicy);
					List<DrugPolicyEntry> list2 = new List<DrugPolicyEntry>();
					if (list.Count > 0)
					{
						foreach (DrugPolicyEntry drugPolicyEntry in list)
						{
							if (((drugPolicyEntry != null) ? drugPolicyEntry.drug : null) != null)
							{
								DrugPolicyEntry drugPolicyEntry2;
								DrugPolicySort.CheckValues(drugPolicy, drugPolicyEntry, drugPolicyEntry.drug, out drugPolicyEntry2);
								if (drugPolicyEntry2 != null)
								{
									list2.AddDistinct(drugPolicyEntry2);
								}
							}
						}
						if (list2.Count > 0)
						{
							List<DrugPolicyEntry> list3 = (from dpe in list2
							orderby dpe.drug.label
							select dpe).ToList<DrugPolicyEntry>();
							NonPublicFields.DrugPolicyEntryList(drugPolicy) = list3;
							num++;
						}
					}
				}
			}
			Log.Message("DrugPolicyFix.Sorted".Translate(num.ToString()), false);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002634 File Offset: 0x00000834
		public static void CheckValues(DrugPolicy DP, DrugPolicyEntry DPE, ThingDef drug, out DrugPolicyEntry DPEChecked)
		{
			DPEChecked = DPE;
			if (!DrugPolicyUtility.IsAddictive(drug))
			{
				DPEChecked.allowedForAddiction = false;
			}
			if (!drug.IsPleasureDrug)
			{
				DPEChecked.allowedForJoy = false;
			}
		}
	}
}
