using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinGOT.Models
{
    public class House
    {
        public string url { get; set; }
        public string name { get; set; }
        public string region { get; set; }
        public string coatOfArms { get; set; }
        public string words { get; set; }
        public string[] titles { get; set; }
        public string[] seats { get; set; }
        public string currentLord { get; set; }
        public string heir { get; set; }
        public string overlord { get; set; }
        public string founded { get; set; }
        public string founder { get; set; }
        public string diedOut { get; set; }
        public string[] ancestralWeapons { get; set; }
        public string[] cadetBranches { get; set; }
        public string[] swornMembers { get; set; }
    }

}

