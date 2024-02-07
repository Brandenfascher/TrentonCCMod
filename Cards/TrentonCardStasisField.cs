using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace Brandenfascher.TrentonChar.Cards;

internal sealed class TrentonCardStasisField : Card, TrentonCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("StasisField", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Trenton_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "StasisField", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        int cardCost = 3;
        switch (upgrade)
        {
            case Upgrade.A:
                cardCost = 2;
                break;
        }

        return new CardData()
        {
            cost = cardCost,
            exhaust = true
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
                    new AStatus()
                    {
                        status = Status.lockdown,
                        statusAmount = 1,
                        targetPlayer = false
                    },
                    new AStatus()
                    {
                        status = Status.perfectShield,
                        statusAmount = 1,
                        targetPlayer = false
                    },
                    new AStunShip(),
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = Status.lockdown,
                        statusAmount = 1,
                        targetPlayer = false
                    },
                    new AStatus()
                    {
                        status = Status.perfectShield,
                        statusAmount = 1,
                        targetPlayer = false
                    },
                    new AStunShip(),
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = Status.lockdown,
                        statusAmount = 2,
                        targetPlayer = false
                    },
                    new AStatus()
                    {
                        status = Status.perfectShield,
                        statusAmount = 1,
                        targetPlayer = false
                    },
                    new AStunShip(),
                    new AStatus
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
