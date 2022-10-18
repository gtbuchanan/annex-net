namespace Annex.Test.Delegates;

using Annex.Delegates;

public sealed class ActionAnnex_EmptyTest
{
    [Test]
    public void DoesNotThrow() => Should.NotThrow(ActionAnnex.Empty);
}
