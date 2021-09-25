using HarmonyLib;
using UnityEngine;

namespace ValheimCreativeMode
{
    // FOR "vcm stamina" COMMAND (infinite stamina cheat)
    [HarmonyPatch(typeof(Player), "UseStamina")]
    class Player_UseStamina_Patch
    {
        static bool Prefix(ref float v)
        {
            if (ValheimCheats.commands.staminaCheatEnabled)
            {
                v = 0f;
            }
            return true;
        } 
    }

    // for "vcm nodrop" COMMAND (prevent items from dropping cheat)
    [HarmonyPatch(typeof(Player), "CreateTombStone")]
    class Player_CreateTombStone_Patch
    {
        static bool Prefix()
        {
            if (ValheimCheats.commands.dropItemsCheatEnabled)
            {
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(Player), "Update")]
    class Player_Update_Patch
    {
        static bool Prefix(Player __instance, ref bool ___m_debugMode)
        {
            if (!ValheimCheats.commands.isServer && ___m_debugMode)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    __instance.ToggleDebugFly();
                }
                if (Input.GetKeyDown(KeyCode.B))
                {
                    __instance.ToggleNoPlacementCost();
                }
                if (Input.GetKeyDown(KeyCode.K))
                {
                    global::Console.instance.TryRunCommand("killall", false, false);
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    global::Console.instance.TryRunCommand("removedrops", false, false);
                }
            }
            return true;
        }
    }
}
