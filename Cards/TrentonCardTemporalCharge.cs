using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace Brandenfascher.TrentonChar.Cards;

internal sealed class TrentonCardTemporalCharge : Card, TrentonCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("TemporalCharge", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Trenton_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "TemporalCharge", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        return new CardData()
        {
            cost = 0,
            description = ModEntry.Instance.Localizations.Localize(["card", "TemporalCharge", "description", upgrade.ToString()])
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
                    new AAddCard()
                    {
                        card = new TrentonCardDelayedPotential()
                        {
                            upgrade = Upgrade.None
                        },
                        insertRandomly = false,
                        destination = CardDestination.Deck
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AAddCard()
                    {
                        card = new TrentonCardDelayedPotential()
                        {
                            upgrade = Upgrade.A
                        },
                        insertRandomly = false,
                        destination = CardDestination.Deck
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AAddCard()
                    {
                        card = new TrentonCardDelayedPotential()
                        {
                            upgrade = Upgrade.B
                        },
                        insertRandomly = false,
                        destination = CardDestination.Deck
                    }
                };
                actions = cardActionList3;
                break;
        }

        return actions;
    }
}
