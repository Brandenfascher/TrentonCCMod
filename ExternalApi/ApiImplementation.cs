
namespace Brandenfascher.TrentonChar;

internal sealed class ApiImplementation
{
    private const string CurrentPlayerEnergyKey = "CurrentPlayerEnergy";

    private static ModEntry Instance => ModEntry.Instance;

    public void SetCurrentPlayerShipEnergy(State state, int energyTotal)
    {
        Instance.KokoroApi.SetExtensionData(state, CurrentPlayerEnergyKey, energyTotal);
    }

    public int GetCurrentPlayerShipEnergy(State state)
    {
        return Instance.KokoroApi.GetExtensionData<int>(state, CurrentPlayerEnergyKey);
    }
    
}
