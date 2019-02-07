using System;

namespace Reminders.Common
{
    public class Reminder
    {
        public Reminder(int id, string description, DateTime date, bool isFinished)
            {
            this.Id = id;
            this.Description = description;
            this.Date = date;
            this.IsFinished = isFinished; 
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsFinished { get; set; }
    }
}