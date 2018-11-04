using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Turshia.Services
{
    using Models;
    using Data;
    using Data.Models;

    public class ReportService : IReportService
    {
        private readonly TurshiaDbContext context;

        public ReportService()
        {
            this.context = new TurshiaDbContext();

        }


        public AllTaskReportsViewModel List()
        {
            var tasks = this.context.Reports.Select(p => new TaskReportModel
            {
                Name = p.Task.Title,
                Level = p.Task.Level,
                Status = p.Status.ToString(),
                Id = p.Id
            })
            .ToArray();

            var model = new AllTaskReportsViewModel
            {
                TaskReports = tasks
            };

            return model;

        }

        public ReportDetailsModel Details(int id)
        {
            return this.context.Reports.Where(p => p.Id == id)
            .Select(p => new ReportDetailsModel
            {
                Id = p.Id.ToString(),
                TaskName = p.Task.Title,
                Level = p.Task.Level == 0 ? p.Task.AffectedSectors.Count() : p.Task.Level,
                Status = p.Status.ToString(),
                Participants = String.Join(", ", p.Task.Participants.Select(l => l.Name)),
                AffectedSectors = String.Join(", ", p.Task.AffectedSectors.Select(k => k.Sector.ToString())),
                Description = p.Task.Description,
                DueDate = p.Task.DueDate.ToString("dd/MM/yyyy"),
                ReportedOn = p.ReportedOn.ToString("dd/MM/yyyy"),
                Reporter = p.Reporter.Username

            })
            .SingleOrDefault();

        }
    }
}
