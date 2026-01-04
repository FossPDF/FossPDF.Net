using FossPDF.Fluent;
using FossPDF.ReportSample;
using FossPDF.ReportSample.Layouts;

var model = DataSource.GetReport();
var report = new StandardReport(model);
report.GeneratePdf("out.pdf");
