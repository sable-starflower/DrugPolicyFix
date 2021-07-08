using Verse;

namespace DrugPolicyFix
{
    // Token: 0x02000003 RID: 3
    public class DrugPolicyFixComponent : GameComponent
    {
        // Token: 0x04000001 RID: 1
        public bool fixedthisplay;

        // Token: 0x06000005 RID: 5 RVA: 0x000023ED File Offset: 0x000005ED
        public DrugPolicyFixComponent(Game game)
        {
        }

        // Token: 0x06000006 RID: 6 RVA: 0x000023F5 File Offset: 0x000005F5
        public DrugPolicyFixComponent()
        {
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000023CC File Offset: 0x000005CC
        public override void LoadedGame()
        {
            base.LoadedGame();
            if (!fixedthisplay)
            {
                DrugPolicyFix.CorrectPolicies();
                DrugPolicySort.SortPolicies();
            }

            fixedthisplay = true;
        }
    }
}