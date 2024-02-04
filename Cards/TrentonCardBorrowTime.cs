using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace Brandenfascher.TrentonChar.Cards;

internal sealed class TrentonCardBorrowTime : Card, TrentonCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("BorrowTime", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Trenton_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "BorrowTime", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        return new CardData()
        {
            cost = 0
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
                    new AEnergy() {
                        changeAmount = 1
                    },
                    new AStatus()
                    {
                        status = Status.energyLessNextTurn,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new ADrawCard()
                    {
                        count = 1
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AEnergy() {
                        changeAmount = 2
                    },
                    new AStatus()
                    {
                        status = Status.energyLessNextTurn,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new ADrawCard()
                    {
                        count = 1
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AEnergy() {
                        changeAmount = 1
                    },
                    new AStatus()
                    {
                        status = Status.energyLessNextTurn,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new ADrawCard()
                    {
                        count = 2
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
