using System;

namespace Tasks.App.Models
{
    public class OrderTaskInputModel
    {
        public Guid TaskId { get; set; }
        public Guid[] OriginalOrder { get; set; }
        public Guid[] NewOrder { get; set; }

        public Guid OriginalPrev { get; set; }
        public Guid OriginalNext { get; set; }

        public Guid NewPrev { get; set; }
        public Guid NewNext { get; set; }
    }
}