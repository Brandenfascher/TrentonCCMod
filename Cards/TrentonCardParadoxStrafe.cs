using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Brandenfascher.TrentonChar.Cards;

internal class TrentonCardParadoxStrafe : Card, TrentonCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ParadoxStrafe", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Trenton_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ParadoxStrafe", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = upgrade == Upgrade.A ? 1 : 2
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.B:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = 1
                    },
                    new AMove()
                    {
                        dir = -1,
                        targetPlayer = true,
                        isRandom = true,
                        isTeleport = true
                    },
                    new AAttack()
                    {
                        damage = 2
                    },
                    new AMove()
                    {
                        dir = -2,
                        targetPlayer = true,
                        isRandom = true,
                        isTeleport = true
                    },
                    new AAttack()
                    {
                        damage = 3
                    }
                };
                actions = cardActionList1;
                break;
            default:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = 1
                    },
                    new AMove()
                    {
                        dir = -1,
                        targetPlayer = true,
                        isRandom = true,
                        isTeleport = true
                    },
                    new AAttack()
                    {
                        damage = 1
                    },
                    new AMove()
                    {
                        dir = -2,
                        targetPlayer = true,
                        isRandom = true,
                        isTeleport = true
                    },
                    new AAttack()
                    {
                        damage = 1
                    }
                };
                actions = cardActionList2;
                break;
        }
        return actions;
    }
}
