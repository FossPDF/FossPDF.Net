using FossPDF.Drawing;
using FossPDF.Fluent;
using FossPDF.FontSubset.Glue;
using FossPDF.ReportSample;
using FossPDF.ReportSample.Layouts;

var model = DataSource.GetReport();
var report = new StandardReport(model);
FontManager.RegisterSubsetCallback(BuiltInFontSubsetting.Callback);
report.GeneratePdf("out.pdf");
