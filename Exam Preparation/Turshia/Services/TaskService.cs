using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace Turshia.Services
{
    using Data;
    using Data.Models;
    using Models;

    public class TaskService : ITaskService
    {
        private readonly TurshiaDbContext context;

        public TaskService()
        {
            this.context = new TurshiaDbContext();

        }

        public IndexViewModel All()
        {
            var tasks = this.context.Tasks
            .Where(p => p.IsReported == false)
            .Select(p => new AllTasksModel
            {
                Name = p.Title,
                Level = p.Level,
                Id = p.Id
            })
            .ToArray();

            var model = new IndexViewModel
            {
                Tasks = tasks
            };

            return model;

        }

        public void Report(int id, string username)
        {
            var task = this.context.Tasks.SingleOrDefault(p => p.Id == id);
            task.IsReported = true;
            context.SaveChanges();

            var rnd = new Random();
            int num = rnd.Next(1, 4);
            var status = num <= 3 ? Status.Completed : Status.Archived;
          

            var report = new Report
            {
                Task = task,
                Reporter = this.context.Users.SingleOrDefault(p => p.Username == username),
                ReportedOn = DateTime.Now,
                Status = status
            };           
           
            context.Reports.Add(report);

            context.SaveChanges();

        }

        public void Create(CreateTaskModel model)
        {
            var affectedSectors = new List<TaskSector>();


            var task = new Task
            {
                Title = model.Title,
                DueDate = DateTime.ParseExact(model.DueDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Description = model.Description,

                IsReported = false
            };

            foreach (Sector value in Enum.GetValues(typeof(Sector)))
            {

                var property = model
                    .GetType()
                    .GetProperties()
                    .FirstOrDefault(p => p.Name.ToLower()
                    .Equals(value.ToString().ToLower()));

                if (property.GetValue(model) != null)
                {
                    var taskSector = new TaskSector
                    {
                        Task = task,
                        Sector = value
                    };
                    affectedSectors.Add(taskSector);
                }               
            }

            var participants = new List<TaskParticipant>();
            foreach (var name in model.Participants.Split(new[] { ", ", "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                var participant = new TaskParticipant
                {
                    Name = name.Trim(),
                    Task = task
                };

                participants.Add(participant);
            }

            task.Participants = participants;
            task.AffectedSectors = affectedSectors;
            task.Level = affectedSectors.Count();
            this.context.Tasks.Add(task);
            this.context.SaveChanges();
            //this.context.TaskParticipants.AddRange(participants);
            //this.context.SaveChanges();
            //this.context.TaskSectors.AddRange(affectedSectors);
        }


        public TaskDetailsModel Details(int taskId)
        {
            return this.context.Tasks.Where(p => p.Id == taskId)
            .Select(k => new TaskDetailsModel
            {
                Name = k.Title,
                Level = k.Level == 0 ? k.AffectedSectors.Count() : k.Level,
                Participants = String.Join(", ", k.Participants.Select(p => p.Name)),
                AffectedSectors = String.Join(", ", k.AffectedSectors.Select(p => p.Sector.ToString()).ToArray()),
                Description = k.Description,
                DueDate = k.DueDate.ToString("dd/MM/yyyy")
            })
            .SingleOrDefault();

        }

    }
}

