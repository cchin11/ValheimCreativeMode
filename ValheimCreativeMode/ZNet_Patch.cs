using HarmonyLib;
using UnityEngine;

namespace ValheimCreativeMode
{
    // store ___m_isServer so we can tell if we are local or online
    [HarmonyPatch(typeof(ZNet), "Awake")]
    class ZNet_Awake_Patch
    {
        static void Postfix(bool ___m_isServer)
        {
            ValheimCheats.commands.isServer = ___m_isServer;
        }
    }
}
