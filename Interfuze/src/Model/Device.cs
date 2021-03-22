using System;

namespace Interfuze
{
	public class Device
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }

		public Device(string ID, string Name, string Location)
		{
			this.ID = ID;
			this.Name = Name;
			this.Location = Location;
		}

		public override string ToString()
		{
			return "Device: " + ID + ", " + Name + ", " + Location;
		}
	}
}