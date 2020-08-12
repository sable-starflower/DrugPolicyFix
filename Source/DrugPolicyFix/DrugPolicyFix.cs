using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace DrugPolicyFix
{
	// Token: 0x02000002 RID: 2
	public static class DrugPolicyFix
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public unsafe static void CorrectPolicies()
		{
			List<ThingDef> allDefsListForReading = DefDatabase<ThingDef>.AllDefsListForReading;
			int num = 0;
			int num2 = 0;
			List<DrugPolicy> allPolicies = Current.Game.drugPolicyDatabase.AllPolicies;
			if (allPolicies.Count > 0)
			{
				foreach (DrugPolicy drugPolicy in allPolicies)
				{
					List<ThingDef> list = new List<ThingDef>();
					if (allDefsListForReading.Count > 0)
					{
						foreach (ThingDef thingDef in allDefsListForReading)
						{
							DrugCategory drugCategory;
							if (DrugPolicyFix.IsDrug(thingDef, out drugCategory))
							{
								bool flag = false;
								List<DrugPolicyEntry> list2 = NonPublicFields.DrugPolicyEntryList(drugPolicy);
								if (list2.Count > 0)
								{
									foreach (DrugPolicyEntry drugPolicyEntry in list2)
									{
										if (thingDef == drugPolicyEntry.drug)
										{
											flag = true;
											break;
										}
									}
								}
								if (!flag)
								{
									list.AddDistinct(thingDef);
								}
							}
						}
					}
					if (list.Count > 0)
					{
						num++;
						foreach (ThingDef thingDef2 in list)
						{
							num2++;
							DrugCategory drugCategory2 = thingDef2.ingestible.drugCategory;
							DrugPolicyFix.AddNewDrugToPolicy(drugPolicy, thingDef2, drugCategory2);
						}
					}
				}
			}
			string text = "DrugPolicyFix.DoneNothing".Translate();
			if (num > 0)
			{
				text = "DrugPolicyFix.Feedback".Translate(num.ToString(), num2.ToString());
			}
			Log.Message(text, false);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000226C File Offset: 0x0000046C
		public static bool IsDrug(ThingDef thingdef, out DrugCategory DC)
		{
			DC = DrugCategory.None;
			if (((thingdef != null) ? thingdef.ingestible : null) != null)
			{
				bool flag;
				if (thingdef == null)
				{
					flag = true;
				}
				else
				{
					IngestibleProperties ingestible = thingdef.ingestible;
					DrugCategory? drugCategory = (ingestible != null) ? new DrugCategory?(ingestible.drugCategory) : null;
					DrugCategory drugCategory2 = DrugCategory.None;
					flag = !(drugCategory.GetValueOrDefault() == drugCategory2 & drugCategory != null);
				}
				if (flag)
				{
					DC = thingdef.ingestible.drugCategory;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000022DC File Offset: 0x000004DC
		public unsafe static void AddNewDrugToPolicy(DrugPolicy dp, ThingDef newdrug, DrugCategory DC)
		{
			DrugPolicyEntry drugPolicyEntry = new DrugPolicyEntry();
			drugPolicyEntry.drug = newdrug;
			drugPolicyEntry.allowedForAddiction = false;
			drugPolicyEntry.allowedForJoy = false;
			drugPolicyEntry.allowScheduled = false;
			if (dp.label == "SocialDrugs".Translate())
			{
				if (DC == DrugCategory.Social)
				{
					drugPolicyEntry.allowedForJoy = true;
				}
			}
			else if (dp.label == "Unrestricted".Translate())
			{
				if (newdrug.IsPleasureDrug)
				{
					drugPolicyEntry.allowedForJoy = true;
				}
			}
			else if (dp.label == "OneDrinkPerDay".Translate() && (DrugPolicyUtility.IsAlcohol(newdrug) || DrugPolicyUtility.IsSmokey(newdrug)) && newdrug.IsPleasureDrug)
			{
				drugPolicyEntry.allowedForJoy = true;
			}
			if (DrugPolicyUtility.IsAddictive(newdrug))
			{
				drugPolicyEntry.allowedForAddiction = true;
			}
			List<DrugPolicyEntry> list = NonPublicFields.DrugPolicyEntryList(dp);
			list.AddDistinct(drugPolicyEntry);
			NonPublicFields.DrugPolicyEntryList(dp) = list;
		}
	}
}
