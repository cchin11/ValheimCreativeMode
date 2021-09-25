using HarmonyLib;

namespace ValheimCreativeMode
{
    // FOR "vcm damage" COMMAND (max damage cheat)
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetDamage), new [] {typeof(int)})]
    class ItemDrop_GetDamage_Patch
    {
        static bool Prefix(ref int quality)
        {
            if (ValheimCheats.commands.damageCheatEnabled)
            {
                quality = 99999;
            }
            return true;
        }
    }
}
