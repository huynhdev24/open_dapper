using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GasStationFeedback
    {
        public long GasStationFeedbackId { get; set; }
        public long GasStationId { get; set; }
        public string Feedback { get; set; }
        public DateTime FeedbackAt { get; set; }
        public GasStation GasStation { get; set; }
    }
}
