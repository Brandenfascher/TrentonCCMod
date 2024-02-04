using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace Brandenfascher.TrentonChar.Cards;

internal sealed class TrentonCardAbeyance : Card, TrentonCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Abeyance", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Trenton_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Abeyance", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 0,
            exhaust = upgrade == Upgrade.B ? true : false,
            description = ModEntry.Instance.Localizations.Localize(["card", "Abeyance", "description", upgrade.ToString()])
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        string ignoreCardType = new TrentonCardAbeyance().Key();
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new ACardSelect()
                    {
                        browseAction = new AbeyancePickCard(),
                        browseSource = CardBrowse.Source.DiscardPile,
                        ignoreCardType = ignoreCardType
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new ACardSelect()
                    {
                        browseAction = new AbeyancePickCard(),
                        browseSource = CardBrowse.Source.DiscardPile,
                        ignoreCardType = ignoreCardType
                    },
                    new ACardSelect()
                    {
                        browseAction = new AbeyancePickCard(),
                        browseSource = CardBrowse.Source.DiscardPile,
                        ignoreCardType = ignoreCardType
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new ACardSelect()
                    {
                        browseAction = new AbeyancePickCard(),
                        browseSource = CardBrowse.Source.ExhaustPile,
                        ignoreCardType = ignoreCardType
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
