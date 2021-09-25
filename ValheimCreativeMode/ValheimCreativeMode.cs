using BepInEx;
using HarmonyLib;

namespace ValheimCreativeMode
{
    [BepInPlugin("cchin.ValheimCreativeMode", "Valheim Creative Mode", "1.2.0")]
    [BepInProcess("valheim.exe")]
    public class ValheimCheats : BaseUnityPlugin
    {
        public static Harmony harmony = new Harmony("cchin.ValheimCreativeMode");
        public static Commands commands = new Commands();

        void Awake()
        {
            harmony.PatchAll();
        }
    }
}