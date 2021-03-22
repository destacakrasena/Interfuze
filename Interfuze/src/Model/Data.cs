using System;

namespace Interfuze
{
	public class Data
	{
		public string DeviceID { get; set; }
		public DateTime Time { get; set; }
		public int Rainfall { get; set; }

		public Data(string DeviceID, DateTime Time, int Rainfall)
		{
			this.DeviceID = DeviceID;
			this.Time = Time;
			this.Rainfall = Rainfall;
		}
    }
}