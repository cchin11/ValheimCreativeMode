using HarmonyLib;

namespace ValheimCreativeMode
{
    [HarmonyPatch(typeof(WearNTear), "Damage")]
    class WearNTear_Damage_Patch
    {
        // Disable WearNTear Damage from running if we have cheat enabled
        static bool Prefix()
        {
            if (ValheimCheats.commands.wearNTearCheatEnabled)
            {
                return false;
            }
            return true;
        }
    }
}
