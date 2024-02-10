using Nickel;
using System.Reflection;
using System.Collections.Generic;
using System.Numerics;

namespace Brandenfascher.TrentonChar.Cards;

internal class TrentonCardChronoRepair : Card, TrentonCard
{
    private static ModEntry Instance => ModEntry.Instance;

    private static Queue<int> hullDamageQueue = new Queue<int>();

    private static int hullDamageTakenLastTurn = 0;

    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ChronoRepair", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Trenton_Deck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ChronoRepair", "name"]).Localize
        });
        helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnPlayerLoseHull), (Combat combat, int amount) =>
        {
            hullDamageQueue.Enqueue(amount);
        }, priority: 0);
        helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnTurnStart), (State state, Combat combat) =>
        {
            int totalDamage = 0;

            for (int i = 0; i < hullDamageQueue.Count; i++)
            {
                totalDamage  += hullDamageQueue.Dequeue();
            }

            hullDamageTakenLastTurn = totalDamage;

        }, priority: 0);
        helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnCombatEnd), () =>
        {
            hullDamageQueue.Clear();
            hullDamageTakenLastTurn = 0;
        }, priority: 0);
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 2,
            exhaust = true,
            retain = upgrade == Upgrade.A ? true : false
        };

        switch (upgrade)
        {
            case Upgrade.None:
                data.description = ModEntry.Instance.Localizations.Localize(["card", "ChronoRepair", "description", upgrade.ToString()]) + ((state.route is Combat) ? (": <c=heal>" + hullDamageTakenLastTurn + "</c>") : ".");
                break;
            case Upgrade.A:
                data.description = ModEntry.Instance.Localizations.Localize(["card", "ChronoRepair", "description", upgrade.ToString()]) + ((state.route is Combat) ? (": <c=heal>" + hullDamageTakenLastTurn + "</c>") : ".");
                break;
            case Upgrade.B:
                data.description = ModEntry.Instance.Localizations.Localize(["card", "ChronoRepair", "description", upgrade.ToString()]) + ((state.route is Combat) ? (": <c=heal>" + (hullDamageTakenLastTurn + 1) + "</c>") : ".");
                break;
        }

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
                    new AHeal()
                    {
                        healAmount = hullDamageTakenLastTurn,
                        targetPlayer = true
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AHeal()
                    {
                        healAmount = hullDamageTakenLastTurn,
                        targetPlayer = true
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AHeal()
                    {
                        healAmount = hullDamageTakenLastTurn + 1,
                        targetPlayer = true
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
