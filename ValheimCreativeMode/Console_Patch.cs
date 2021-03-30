using HarmonyLib;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ValheimCreativeMode
{
    [HarmonyPatch(typeof(Console), "InputText")]
    class Console_InputText_Patch
    {
        static void Postfix(Console __instance)
        {
            string text = __instance.m_input.text;
            string[] array = text.Split(new char[]
            {' '});

            // do not enable unless we are on dedicated server
            if (__instance.IsCheatsEnabled() && !ZNet_Patch.m_isServer) 
            {
                if (array[0] == "debugmode")
                {
                    Player.m_debugMode = !Player.m_debugMode;
                    __instance.Print("Debugmode " + Player.m_debugMode.ToString());
                }

                if (text.StartsWith("god"))
                {
                    Player.m_localPlayer.SetGodMode(!Player.m_localPlayer.InGodMode());
                    __instance.Print("God mode:" + Player.m_localPlayer.InGodMode().ToString());
                    Gogan.LogEvent("Cheat", "God", Player.m_localPlayer.InGodMode().ToString(), 0L);
                }

                if (array[0] == "raiseskill")
                {
                    if (array.Length > 2)
                    {
                        string name = array[1];
                        int num4 = int.Parse(array[2]);
                        Player.m_localPlayer.GetSkills().CheatRaiseSkill(name, (float)num4);
                        return;
                    }
                    __instance.Print("Syntax: raiseskill [skill] [amount]");
                    return;
                }
                else if (array[0] == "resetskill")
                {
                    if (array.Length > 1)
                    {
                        string name2 = array[1];
                        Player.m_localPlayer.GetSkills().CheatResetSkill(name2);
                        return;
                    }
                    __instance.Print("Syntax: resetskill [skill]");
                    return;
                }

                if (text.StartsWith("exploremap"))
                {
                    Minimap.instance.ExploreAll();
                    return;
                }
                if (text.StartsWith("resetmap"))
                {
                    Minimap.instance.Reset();
                    return;
                }
                if (text.StartsWith("puke") && Player.m_localPlayer)
                {
                    Player.m_localPlayer.ClearFood();
                }
                if (text.StartsWith("tame"))
                {
                    Tameable.TameAllInArea(Player.m_localPlayer.transform.position, 20f);
                }
                if (text.StartsWith("killall"))
                {
                    foreach (Character character in Character.GetAllCharacters())
                    {
                        if (!character.IsPlayer())
                        {
                            HitData hitData = new HitData();
                            hitData.m_damage.m_damage = 1E+10f;
                            character.Damage(hitData);
                        }
                    }
                    return;
                }
                if (text.StartsWith("heal"))
                {
                    Player.m_localPlayer.Heal(Player.m_localPlayer.GetMaxHealth(), true);
                    return;
                }

                if (text.StartsWith("ghost"))
                {
                    Player.m_localPlayer.SetGhostMode(!Player.m_localPlayer.InGhostMode());
                    __instance.Print("Ghost mode:" + Player.m_localPlayer.InGhostMode().ToString());
                    Gogan.LogEvent("Cheat", "Ghost", Player.m_localPlayer.InGhostMode().ToString(), 0L);
                }

                if (text.StartsWith("removedrops"))
                {
                    __instance.Print("Removing item drops");
                    ItemDrop[] array2 = UnityEngine.Object.FindObjectsOfType<ItemDrop>();
                    for (int j = 0; j < array2.Length; j++)
                    {
                        ZNetView component = array2[j].GetComponent<ZNetView>();
                        if (component && component.IsValid() && component.IsOwner())
                        {
                            component.Destroy();
                        }
                    }
                }

                if (array[0] == "spawn")
                {
                    if (array.Length <= 1)
                    {
                        return;
                    }
                    string text4 = array[1];
                    int num8 = (array.Length >= 3) ? int.Parse(array[2]) : 1;
                    int num9 = (array.Length >= 4) ? int.Parse(array[3]) : 1;
                    GameObject prefab = ZNetScene.instance.GetPrefab(text4);
                    if (!prefab)
                    {
                        Player.m_localPlayer.Message(MessageHud.MessageType.TopLeft, "Missing object " + text4, 0, null);
                        return;
                    }
                    DateTime now = DateTime.Now;
                    if (num8 == 1)
                    {
                        Player.m_localPlayer.Message(MessageHud.MessageType.TopLeft, "Spawning object " + text4, 0, null);
                        Character component2 = UnityEngine.Object.Instantiate<GameObject>(prefab, Player.m_localPlayer.transform.position + Player.m_localPlayer.transform.forward * 2f + Vector3.up, Quaternion.identity).GetComponent<Character>();
                        if (component2 & num9 > 1)
                        {
                            component2.SetLevel(num9);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < num8; j++)
                        {
                            Vector3 vector = Random.insideUnitSphere * 0.5f;
                            Player.m_localPlayer.Message(MessageHud.MessageType.TopLeft, "Spawning object " + text4, 0, null);
                            Character component3 = UnityEngine.Object.Instantiate<GameObject>(prefab, Player.m_localPlayer.transform.position + Player.m_localPlayer.transform.forward * 2f + Vector3.up + vector, Quaternion.identity).GetComponent<Character>();
                            if (component3 & num9 > 1)
                            {
                                component3.SetLevel(num9);
                            }
                        }
                    }
                    Gogan.LogEvent("Cheat", "Spawn", text4, (long)num8);
                    return;
                }
            }

            return;
        }

    }

    [HarmonyPatch(typeof(Console), "IsCheatsEnabled")]
    class Console_IsCheatsEnabled_Patch
    {
        static bool Prefix(ref bool __result, bool ___m_cheat)
        {
            if (___m_cheat)
            {
                __result = true;
            }
            return false;
        }

    }
}
