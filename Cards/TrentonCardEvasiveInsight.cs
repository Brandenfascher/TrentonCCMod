using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace Brandenfascher.TrentonChar.Cards;

internal sealed class TrentonCardEvasiveInsight : Card, TrentonCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("EvasiveInsight", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Trenton_Deck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EvasiveInsight", "name"]).Localize
        });

    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 3,
            exhaust = upgrade == Upgrade.A ? false : true,
            description = upgrade == Upgrade.B ? ModEntry.Instance.Localizations.Localize(["card", "EvasiveInsight", "description", upgrade.ToString()]) + ((state.route is Combat) ? (": <c=FFFFFF>" + GetEnemyCannonDamageTotal(state, false) + "</c") : "") :
                                                 ModEntry.Instance.Localizations.Localize(["card", "EvasiveInsight", "description", upgrade.ToString()]) + ((state.route is Combat) ? (": <c=FFFFFF>" + GetEnemyCannonDamageTotal(state, true) + "</c") : "")
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
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = GetEnemyCannonDamageTotal(s, true),
                        targetPlayer = true
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = GetEnemyCannonDamageTotal(s, true),
                        targetPlayer = true
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = GetEnemyCannonDamageTotal(s, false),
                        targetPlayer = true
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }

    private int GetEnemyCannonDamageTotal(State s, bool halfDamage)
    {
        int num = 0;
        if (s.route is Combat combat)
        {
            foreach (Part part in combat.otherShip.parts)
            {
                if (part.intent is IntentAttack intentAttack)
                {
                    num += GetDmg(s, intentAttack.damage, targetPlayer: true) * intentAttack.multiHit;
                }
            }
            if (halfDamage) num = IntegerDivideByTwoRoundedUp(num);
        }

        return num;
    }

    private int IntegerDivideByTwoRoundedUp(int dividend)
    {
        int divisor = 2;
        int dividedByTwoQuotient = dividend / divisor;
        bool noRemainder = (dividend % divisor) == 0;
        if (noRemainder) return dividedByTwoQuotient;
        else return dividedByTwoQuotient + 1;
    }
}
