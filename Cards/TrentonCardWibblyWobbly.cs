using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace Brandenfascher.TrentonChar.Cards;

internal sealed class TrentonCardWibblyWobbly : Card, TrentonCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("WibblyWobbly", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Trenton_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "WibblyWobbly", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 2,
            flippable = true
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AMove()
                    {
                        dir = -2,
                        targetPlayer = true
                    },
                    new AMove()
                    {
                        dir = +1,
                        targetPlayer = true
                    },
                    new AMove()
                    {
                        dir = -2,
                        targetPlayer = true
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AMove()
                    {
                        dir = -3,
                        targetPlayer = true
                    },
                    new AMove()
                    {
                        dir = +1,
                        targetPlayer = true
                    },
                    new AMove()
                    {
                        dir = -3,
                        targetPlayer = true
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AMove()
                    {
                        dir = -2,
                        targetPlayer = true
                    },
                    new AMove()
                    {
                        dir = +1,
                        targetPlayer = true
                    },
                    new AMove()
                    {
                        dir = -2,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = Status.energyNextTurn,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
