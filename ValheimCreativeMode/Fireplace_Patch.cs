using HarmonyLib;

namespace ValheimCreativeMode
{
    [HarmonyPatch]
    class Fireplace_UpdateFireplace_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Fireplace), "UpdateFireplace")]
        public static void Fireplace_UpdateFireplace(Fireplace __instance, ref ZNetView ___m_nview)
        {
            if (ValheimCheats.commands.fireplaceCheatEnabled)
                ___m_nview.GetZDO().Set("fuel", __instance.m_maxFuel);
        }
    }
}
