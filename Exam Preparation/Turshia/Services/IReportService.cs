using System.Collections.Generic;
using Turshia.Models;

namespace Turshia.Services
{
    public interface IReportService
    {
        ReportDetailsModel Details(int id);
        AllTaskReportsViewModel List();
    }
}