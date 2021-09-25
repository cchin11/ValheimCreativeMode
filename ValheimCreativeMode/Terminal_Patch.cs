using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using HarmonyLib;

namespace ValheimCreativeMode
{
    // Only check for "devcommands" command at console to enable cheats
    //	This prevents the default behaviour of checking if we're on a dedicated server

    [HarmonyPatch(typeof(Terminal.ConsoleCommand), "IsValid")]
    class TerminalConsoleCommand_IsValid_Patch
    {
        static void Postfix(ref bool __result)
        {
            if (ValheimCheats.commands.cheatsEnabled)
                __result = true;
        }
    }

    [HarmonyPatch(typeof(Terminal), "UpdateInput")]
    class Terminal_UpdateInput_Patch
    {
        static bool Prefix(InputField ___m_input)
        {
            string command = ___m_input.text.Trim();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (command == "devcommands")
                {
                    ValheimCheats.commands.ToggleCheats();
                }
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(Terminal), "TryRunCommand")]
    class Terminal_TryRunCommand_Patch
    {
        static bool Prefix(Terminal __instance, string text)
        {
            string command = text.Trim();
            if (ValheimCheats.commands.cheatsEnabled && command == "stamina")
            {
                ValheimCheats.commands.ToggleStaminaCheat();
                __instance.AddString("infinite stamina: " + ValheimCheats.commands.staminaCheatEnabled);
                return false;
            }

            if (ValheimCheats.commands.cheatsEnabled && command == "damage")
            {
                ValheimCheats.commands.ToggleDamageCheat();
                __instance.AddString("damage cheat: " + ValheimCheats.commands.damageCheatEnabled);
                return false;
            }

            if (ValheimCheats.commands.cheatsEnabled && command == "fireplace")
            {
                ValheimCheats.commands.ToggleFireplaceCheat();
                __instance.AddString("infinite fuel for fireplaces: " + ValheimCheats.commands.fireplaceCheatEnabled);
                return false;
            }

            if (ValheimCheats.commands.cheatsEnabled && command == "nobreak")
            {
                ValheimCheats.commands.ToggleWearNTearCheat();
                __instance.AddString("no damage to buildings: " + ValheimCheats.commands.wearNTearCheatEnabled);
                return false;
            }

            if (ValheimCheats.commands.cheatsEnabled && command == "nodrop")
            {
                ValheimCheats.commands.ToggleDropItemsCheat();
                __instance.AddString("no dropping items on death: " + ValheimCheats.commands.dropItemsCheatEnabled);
                return false;
            }

            return true;
        }
    }
}