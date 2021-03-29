using BepInEx;
using HarmonyLib;

namespace ValheimCreativeMode
{
    [BepInPlugin("cchin.ValheimCreativeMode", "Valheim Creative Mode", "1.0.0")]
    [BepInProcess("valheim.exe")]
    public class ValheimCheats : BaseUnityPlugin
    {
        public static Harmony harmony = new Harmony("cchin.ValheimCreativeMode");

        void Awake()
        {
            harmony.PatchAll();
        }
    }
}