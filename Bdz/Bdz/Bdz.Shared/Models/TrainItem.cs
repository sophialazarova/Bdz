﻿namespace Bdz.Models
{
   public class TrainItem
    {
        public TrainItem(string town, string time, string trainNumber)
        {
            this.Time = time;
            this.Town = town;
            this.TrainNumber = trainNumber;
        }

        public string Town { get; set; }

        public string Time { get; set; }

        public string TrainNumber { get; set; }
    }
}
