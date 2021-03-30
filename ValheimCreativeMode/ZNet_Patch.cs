using HarmonyLib;

namespace ValheimCreativeMode
{
    [HarmonyPatch(typeof(ZNet), "Awake")]
    public static class ZNet_Patch
    {
        // store result of m_isServer so we can determine if we are playing on dedicated server
        public static bool m_isServer;
        
        static void Postfix(bool ___m_isServer)
        {
            m_isServer = ___m_isServer;
        }
    }
}
