using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseBuilderWPF {
    public class Book {
        public string url { get; set; }
        public string name { get; set; }
        public string isbn { get; set; }
        public string[] authors { get; set; }
        public int numberOfPages { get; set; }
        public string publisher { get; set; }
        public string country { get; set; }
        public string mediaType { get; set; }
        public DateTime released { get; set; }
        public string[] characters { get; set; }
        public string[] povCharacters { get; set; }
    }

}
