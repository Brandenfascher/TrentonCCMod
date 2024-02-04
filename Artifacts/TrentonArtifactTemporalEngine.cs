using Nickel;
using System.Reflection;

namespace Brandenfascher.TrentonChar.Artifacts;

internal sealed class TrentonArtifactTemporalEngine : Artifact, TrentonArtifact
{
    public int counter = 0;

    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("Temporal Engine", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.Trenton_Deck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/trenton/trenton_artifact_temporal_engine.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "TemporalEngine", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "TemporalEngine", "description"]).Localize
        });
    }

    public override int? GetDisplayNumber(State s)
    {
        return counter;
    }

    public override void OnTurnStart(State s, Combat c)
    {
        if (!c.isPlayerTurn)
            return;

        this.Pulse();
        if (counter < 2)
        {
            c.energy++;
            counter++;
        } else
        {
            c.energy--;
            counter = 0;
        }
    }
}
