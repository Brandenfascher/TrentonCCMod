
using System.Diagnostics.CodeAnalysis;

namespace Brandenfascher.TrentonChar;

internal sealed class ApiImplementation
{
    private const string CurrentPlayerEnergyKey = "CurrentPlayerEnergy";

    private static ModEntry Instance => ModEntry.Instance;

    public bool TryGetExtensionData<T>(object o, string key, [MaybeNullWhen(false)] out T data)
        => Instance.KokoroApi.TryGetExtensionData(o, key, out data);

    #region CurrentPlayeShipEnergy

    public void SetCurrentPlayerShipEnergy(State state, int energyTotal)
    {
        Instance.KokoroApi.SetExtensionData(state, CurrentPlayerEnergyKey, energyTotal);
    }

    public int GetCurrentPlayerShipEnergy(State state)
    {
        return TryGetExtensionData(state, CurrentPlayerEnergyKey, out int value) ? value : 0;
    }

    #endregion
}
