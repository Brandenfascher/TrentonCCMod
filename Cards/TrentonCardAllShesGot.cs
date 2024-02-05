using Nickel;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace Brandenfascher.TrentonChar.Cards;

internal sealed class TrentonCardAllShesGot : Card, TrentonCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("AllShesGot", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Trenton_Deck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "AllShesGot", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        int? totalEnergyNullable = Instance.Api?.GetCurrentPlayerShipEnergy(state);
        int totalEnergy = totalEnergyNullable == null ? 0 : totalEnergyNullable.Value;

        CardData data = new CardData()
        {
            cost = totalEnergy,
            exhaust = true
        };

        switch (upgrade)
        {
            case Upgrade.None:
                data.description = ModEntry.Instance.Localizations.Localize(["card", "AllShesGot", "description", upgrade.ToString()]) + ((state.route is Combat) ? (": <c=hurt>" + totalEnergy * 2 + "</c>") : "");
                break;
            case Upgrade.A:
                data.description = ModEntry.Instance.Localizations.Localize(["card", "AllShesGot", "description", upgrade.ToString()]) + ((state.route is Combat) ? (": <c=hurt>" + (int)Math.Ceiling(totalEnergy * 2.5) + "</c>") : "");
                break;
            case Upgrade.B:
                data.description = ModEntry.Instance.Localizations.Localize(["card", "AllShesGot", "description", upgrade.ToString()]) + ((state.route is Combat) ? (": <c=hurt>" + totalEnergy * 2 + "</c>") : "");
                data.description += ModEntry.Instance.Localizations.Localize(["card", "AllShesGot", "description", upgrade.ToString() + "-Extended"]);
                break;
        }

        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        int currentCost = this.GetDataWithOverrides(s).cost;

        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = currentCost * 2
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = (int) Math.Ceiling(currentCost * 2.5)
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = currentCost * 2
                    },
                    new AEnergy()
                    {
                        changeAmount = 1
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
