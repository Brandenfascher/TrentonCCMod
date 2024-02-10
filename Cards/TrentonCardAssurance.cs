using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace Brandenfascher.TrentonChar.Cards;

internal class TrentonCardAssurance : Card, TrentonCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Assurance", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Trenton_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Assurance", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        return new CardData()
        {
            cost = 0,
            buoyant = upgrade == Upgrade.None ? false : true,
            exhaust = upgrade == Upgrade.B ? true : false,
            description = ModEntry.Instance.Localizations.Localize(["card", "Assurance", "description", upgrade.ToString()])
        };
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();

        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new ACardSelect()
                    {
                        browseAction = new AssurancePickCard() { sendToHand = true },
                        browseSource = CardBrowse.Source.DrawPile
                    },
                    new ACardSelect()
                    {
                        browseAction = new AssurancePickCard() { sendToHand = false },
                        browseSource = CardBrowse.Source.DrawPile
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new ACardSelect()
                    {
                        browseAction = new AssurancePickCard() { sendToHand = true },
                        browseSource = CardBrowse.Source.DrawPile
                    },
                    new ACardSelect()
                    {
                        browseAction = new AssurancePickCard() { sendToHand = false },
                        browseSource = CardBrowse.Source.DrawPile
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new ACardSelect()
                    {
                        browseAction = new AssurancePickCard() { sendToHand = true },
                        browseSource = CardBrowse.Source.DrawPile
                    },
                    new ACardSelect()
                    {
                        browseAction = new AssurancePickCard() { sendToHand = true },
                        browseSource = CardBrowse.Source.DrawPile
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
