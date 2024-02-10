using HarmonyLib;

namespace Brandenfascher.TrentonChar;

internal sealed class CombatStateDataManager
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void ApplyPatches(Harmony harmony)
    {
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Combat), nameof(Combat.Update)),
            postfix: new HarmonyMethod(typeof(CombatStateDataManager), nameof(Combat_Update_Postfix))
        );
    }

    private static void Combat_Update_Postfix(Combat __instance, G g)
    {
        Instance.Api.SetCurrentPlayerShipEnergy(g.state, __instance.energy);
    }
}
