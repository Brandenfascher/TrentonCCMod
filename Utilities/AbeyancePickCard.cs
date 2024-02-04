
namespace Brandenfascher.TrentonChar;

internal class AbeyancePickCard : CardAction
{
    public override void Begin(G g, State s, Combat c)
    {
        Card? card = selectedCard;
        if (card != null)
        {
            s.RemoveCardFromWhereverItIs(card.uuid);
            s.SendCardToDeck(card, true, false);
        }
    }

    public override string GetCardSelectText(State s)
    {
        return ModEntry.Instance.Localizations.Localize(["card", "Abeyance", "GetCardSelectText"]);
    }
}
