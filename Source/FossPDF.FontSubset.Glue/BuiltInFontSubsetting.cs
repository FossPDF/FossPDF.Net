using FossPDF.Drawing;

namespace FossPDF.FontSubset.Glue;

public static class BuiltInFontSubsetting
{
    public static Action<DocumentSpecificFontManager, IEnumerable<FontToBeSubset>> Callback =>
        (fontManager, subsets) =>
        {
            var newFonts = new List<byte[]>(subsets.Count());
            var tasks = subsets.Select(fontToBeSubset => Task.Run(() =>
            {
                var subset = fontToBeSubset.Glyphs;
                var builder = new FontSubsetBuilder();
                builder.AddGlyphs(subset);
                builder.SetFont(fontToBeSubset.ShaperFont);

                var bytes = builder.Build();
                newFonts.Add(bytes);
                return Task.CompletedTask;
            }));
            Task.WaitAll(tasks.ToArray());

            fontManager.ClearCacheReadyForSubsets();

            foreach (var newFont in newFonts)
            {
                using var ms = new MemoryStream(newFont);
                fontManager.RegisterFont(ms);
            }

            newFonts.Clear();
        };
}
