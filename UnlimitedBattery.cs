using HarmonyLib;
using HMLLibrary;

public class UnlimitedBattery : Mod
{
    private HarmonyLib.Harmony harmony;

    public void Start()
    {
        harmony = new HarmonyLib.Harmony("com.you.unlimitedbattery");
        harmony.PatchAll();
    }

    public void OnModUnload()
    {
        harmony.UnpatchAll("com.you.unlimitedbattery");
    }
}

[HarmonyPatch(typeof(Battery), "Update", new System.Type[] { typeof(int) })]
public class Patch_Battery_Update
{
    [HarmonyPrefix]
    static bool Prefix(Battery __instance, ref int batteryUses)
    {
        if (__instance.BatterySlotIsEmpty)
            return true; // nothing to protect

        if (batteryUses < __instance.BatteryUses)
            return false; // block any decrease, charge stays put

        return true; // allow increases through normally
    }
}