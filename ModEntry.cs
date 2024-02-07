using Nickel;
using Nanoray.PluginManager;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using HarmonyLib;
using System.Linq;
using Brandenfascher.TrentonChar.Cards;
using Brandenfascher.TrentonChar.Artifacts;
using Shockah.Kokoro;
using Brandenfascher.TrentonChar.Utilities;

namespace Brandenfascher.TrentonChar;

public sealed class ModEntry : SimpleMod
{
    internal static ModEntry Instance { get; private set; } = null!;
    internal ApiImplementation Api { get; private set; } = null!;

    internal Harmony Harmony { get; }
    internal IKokoroApi KokoroApi { get; }

    internal CombatStateDataManager CombatStateDataManager { get; private set; } = null!;

    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }
    internal static Color TrentonColor => new("088482");
    internal static Color TrentonCardTitleColor => new("000000");

    internal ISpriteEntry Trenton_Character_CardBackground { get; }
    internal ISpriteEntry Trenton_Character_CardFrame { get; }
    internal ISpriteEntry Trenton_Character_Panel { get; }
    internal ISpriteEntry Trenton_Character_Neutral_0 {  get; }
    internal ISpriteEntry Trenton_Character_Neutral_1 {  get; }
    internal ISpriteEntry Trenton_Character_Neutral_2 {  get; }
    internal ISpriteEntry Trenton_Character_Neutral_3 {  get; }
    internal ISpriteEntry Trenton_Character_Neutral_4 {  get; }
    internal ISpriteEntry Trenton_Character_Mini_0 {  get; }
    internal ISpriteEntry Trenton_Character_Panic_0 { get; }
    internal ISpriteEntry Trenton_Character_Panic_1 { get; }
    internal ISpriteEntry Trenton_Character_Panic_2 { get; }
    internal ISpriteEntry Trenton_Character_Panic_3 { get; }
    internal ISpriteEntry Trenton_Character_Panic_4 { get; }
    internal ISpriteEntry Trenton_Character_Squint_0 { get; }
    internal ISpriteEntry Trenton_Character_Squint_1 { get; }
    internal ISpriteEntry Trenton_Character_Squint_2 { get; }
    internal ISpriteEntry Trenton_Character_Squint_3 { get; }
    internal ISpriteEntry Trenton_Character_Squint_4 { get; }
    internal IDeckEntry Trenton_Deck { get; }
    internal static IReadOnlyList<Type> Trenton_StarterCard_Types { get; } = [
        typeof(TrentonCardWibblyWobbly),
        typeof(TrentonCardTemporalCharge)
    ];

    internal static IReadOnlyList<Type> Trenton_CommonCard_Types { get; } = [
        typeof(TrentonCardRaincheck),
        typeof(TrentonCardBorrowTime),
        typeof(TrentonCardAbeyance)
    ];

    internal static IReadOnlyList<Type> Trenton_UncommonCard_Types { get; } = [
        typeof(TrentonCardStasisField),
        typeof(TrentonCardParadoxStrafe)
    ];

    internal static IReadOnlyList<Type> Trenton_RareCard_Types { get; } = [
        typeof(TrentonCardEvasiveInsight),
        typeof(TrentonCardAllShesGot)
    ];

    internal static IReadOnlyList<Type> Trenton_NoRarity_Types { get; } = [
        typeof(TrentonCardDelayedPotential)
    ];

    internal static IEnumerable<Type> Trenton_AllCard_Types
        => Trenton_StarterCard_Types
        .Concat(Trenton_CommonCard_Types)
        .Concat(Trenton_UncommonCard_Types)
        .Concat(Trenton_RareCard_Types)
        .Concat(Trenton_NoRarity_Types);

    internal static IReadOnlyList<Type> Trenton_CommonArtifact_Types { get; } = [
        typeof(TrentonArtifactTemporalEngine)
    ];

    internal static IEnumerable<Type> Trenton_AllArtifact_Types
        => Trenton_CommonArtifact_Types;

    public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {
        Instance = this;

        KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!;
        KokoroApi.RegisterTypeForExtensionData(typeof(State));

        Api = new();
        CombatStateDataManager = new();

        Harmony = new(package.Manifest.UniqueName);

        CustomTTGlossary.ApplyPatches(Harmony);
        CombatStateDataManager.ApplyPatches(Harmony);

        this.AnyLocalizations = new JsonLocalizationProvider(
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
        );
        this.Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(this.AnyLocalizations)
        );

        Trenton_Character_CardBackground = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_cardbackground_0.png"));
        Trenton_Character_CardFrame = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_cardframe.png"));
        
        Trenton_Character_Panel = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_panel.png"));

        Trenton_Character_Neutral_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_neutral_0.png"));
        Trenton_Character_Neutral_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_neutral_1.png"));
        Trenton_Character_Neutral_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_neutral_2.png"));
        Trenton_Character_Neutral_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_neutral_3.png"));
        Trenton_Character_Neutral_4 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_neutral_4.png"));

        Trenton_Character_Mini_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_mini_0.png"));

        Trenton_Character_Panic_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_panic_0.png"));
        Trenton_Character_Panic_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_panic_1.png"));
        Trenton_Character_Panic_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_panic_2.png"));
        Trenton_Character_Panic_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_panic_3.png"));
        Trenton_Character_Panic_4 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_panic_4.png"));

        Trenton_Character_Squint_0 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_squint_0.png"));
        Trenton_Character_Squint_1 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_squint_1.png"));
        Trenton_Character_Squint_2 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_squint_2.png"));
        Trenton_Character_Squint_3 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_squint_3.png"));
        Trenton_Character_Squint_4 = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/trenton/trenton_character_squint_4.png"));

        Trenton_Deck = Helper.Content.Decks.RegisterDeck("TrentonDeck", new DeckConfiguration()
        {
            Definition = new DeckDef()
            {
                color = TrentonColor,
                titleColor = TrentonCardTitleColor
            },
            DefaultCardArt = Trenton_Character_CardBackground.Sprite,
            BorderSprite = Trenton_Character_CardFrame.Sprite,

            Name = this.AnyLocalizations.Bind(["character", "Trenton", "name"]).Localize,
        });

        Helper.Content.Characters.RegisterCharacter("Trenton", new CharacterConfiguration()
        {
            Deck = Trenton_Deck.Deck,
            StartLocked = false,
            StarterCardTypes = Trenton_StarterCard_Types,
            Description = this.AnyLocalizations.Bind(["character", "Trenton", "description"]).Localize,
            BorderSprite = Trenton_Character_Panel.Sprite,
            NeutralAnimation = new()
            {
                Deck = Trenton_Deck.Deck,
                LoopTag = "neutral",
                Frames = [
                    Trenton_Character_Neutral_0.Sprite,
                Trenton_Character_Neutral_1.Sprite,
                Trenton_Character_Neutral_2.Sprite,
                Trenton_Character_Neutral_3.Sprite,
                Trenton_Character_Neutral_4.Sprite
                    ]
            },
            MiniAnimation = new()
            {
                Deck = Trenton_Deck.Deck,
                LoopTag = "mini",
                Frames = [
                Trenton_Character_Mini_0.Sprite
                    ]
            }
        });;

        Helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration()
        {
            Deck = Trenton_Deck.Deck,
            LoopTag = "panic",
            Frames = new[]
            {
                Trenton_Character_Panic_0.Sprite,
                Trenton_Character_Panic_1.Sprite,
                Trenton_Character_Panic_2.Sprite,
                Trenton_Character_Panic_3.Sprite,
                Trenton_Character_Panic_4.Sprite
            }
        });
        Helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration()
        {
            Deck = Trenton_Deck.Deck,
            LoopTag = "squint",
            Frames = new[]
            {
                Trenton_Character_Squint_0.Sprite,
                Trenton_Character_Squint_1.Sprite,
                Trenton_Character_Squint_2.Sprite,
                Trenton_Character_Squint_3.Sprite,
                Trenton_Character_Squint_4.Sprite
            }
        });


        foreach (var cardType in Trenton_AllCard_Types)
            AccessTools.DeclaredMethod(cardType, nameof(TrentonCard.Register))?.Invoke(null, [helper]);

        foreach (var artifactType in Trenton_AllArtifact_Types)
            AccessTools.DeclaredMethod(artifactType, nameof(TrentonArtifact.Register))?.Invoke(null, [helper]);

    }
}
