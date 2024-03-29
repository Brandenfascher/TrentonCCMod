﻿using Nickel;
using System.Reflection;
using System.Collections.Generic;

namespace Brandenfascher.TrentonChar.Cards;

internal class TestingCard : Card, TrentonCard
{
    private static ISpriteEntry FlippableDiscountIcon = null!;
    public static void Register(IModHelper helper)
    {
        FlippableDiscountIcon = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/icons/unused/flippable_discount.png"));

        helper.Content.Cards.RegisterCard("Testing", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Trenton_Deck.Deck,
                dontOffer = true,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Testing", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        return new CardData()
        {
            cost = flipped == true ? 0 : 1,
            floppable = true,
            art = (flipped ? Spr.cards_Adaptability_Bottom : Spr.cards_Adaptability_Top),
            artTint = "ffffff",
            exhaust = true,
            temporary = true
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
                    new AAttack()
                    {
                        damage = 2,
                        disabled = flipped
                    },
                    new ADummyAction(),
                    new ADrawCard()
                    {
                        count = 2,
                        disabled = !flipped
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = 3,
                        disabled = flipped
                    },
                    new ADummyAction(),
                    new ADrawCard()
                    {
                        count = 2,
                        disabled = !flipped
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = 2,
                        piercing = true,
                        disabled = flipped
                    },
                    new ADummyAction(),
                    new ADrawCard()
                    {
                        count = 2,
                        disabled = !flipped
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }

    private static void Card_GetAllTooltips_Postfix(Card __instance, State s, bool showCardTraits, ref IEnumerable<Tooltip> __result)
    {
        if (!showCardTraits)
            return;
        if (__instance is not TestingCard testingCard)
            return;
        //Work In Progress
    }
}
