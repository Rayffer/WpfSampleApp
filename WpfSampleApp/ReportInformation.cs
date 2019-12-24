using System;

namespace WpfSampleApp
{
    public class ReportInformation
    {
        public string Name { get; set; }
        public ReportType? Type { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}