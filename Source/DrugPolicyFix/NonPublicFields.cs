using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace DrugPolicyFix
{
	// Token: 0x02000007 RID: 7
	[StaticConstructorOnStartup]
	public static class NonPublicFields
	{
		// Token: 0x04000002 RID: 2
		public static AccessTools.FieldRef<DrugPolicy, List<DrugPolicyEntry>> DrugPolicyEntryList = AccessTools.FieldRefAccess<DrugPolicy, List<DrugPolicyEntry>>("entriesInt");
	}
}
