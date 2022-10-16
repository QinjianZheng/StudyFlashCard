using System;
using System.Collections.Generic;

namespace StudyFlashCard
{
    public partial class Card
    {
        public long Id { get; set; }
        public long Type { get; set; }
        public string Front { get; set; } = null!;
        public string Back { get; set; } = null!;
        public bool? Known { get; set; }
    }
}
