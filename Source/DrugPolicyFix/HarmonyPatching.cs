using System;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace DrugPolicyFix
{
	// Token: 0x02000006 RID: 6
	[StaticConstructorOnStartup]
	public static class HarmonyPatching
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000277D File Offset: 0x0000097D
		static HarmonyPatching()
		{
			new Harmony("com.Pelador.Rimworld.DrugPolicyFix").PatchAll(Assembly.GetExecutingAssembly());
		}
	}
}
