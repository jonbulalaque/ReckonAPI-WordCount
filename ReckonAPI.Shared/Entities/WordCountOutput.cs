using System;
using System.Collections.Generic;
using System.Text;

namespace ReckonAPI.Shared.Entities
{
    public class WordCountOutput
    {
        public string Candidate { get; set; }
        public string Text { get; set; }
        public IEnumerable<SubTextIndexResult> Results { get; set; }
    }
}
