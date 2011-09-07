using System;

namespace Tasks.App.Models
{
    public class OrderTaskInputModel
    {
        public Guid TaskId { get; set; }
        public Guid[] OriginalOrder { get; set; }
        public Guid[] NewOrder { get; set; }
    }
}