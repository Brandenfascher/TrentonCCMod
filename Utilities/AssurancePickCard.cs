
using System;

namespace Brandenfascher.TrentonChar;

internal class AssurancePickCard : CardAction
{
    public bool sendToHand = true;
    public override void Begin(G g, State s, Combat c)
    {
        Card? card = selectedCard;
        if (card != null)
        {
            s.RemoveCardFromWhereverItIs(card.uuid);
            if (sendToHand)
            {
                c.SendCardToHand(s, card);
            } else
            {
                s.SendCardToDeck(card, true, false);
            }
        }
    }

    public override string GetCardSelectText(State s)
    {
        return sendToHand ? ModEntry.Instance.Localizations.Localize(["card", "Assurance", "GetCardSelectText", "ToHand"]) :
            ModEntry.Instance.Localizations.Localize(["card", "Assurance", "GetCardSelectText", "ToTopDrawPile"]);
    }
}
