using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Area51.Classes
{
    public class Elevator
    {
        public readonly FloorPanel _floorPanel = new FloorPanel();
        public int CurrentFloor { get; set; }
        public int TargetFloor { get; set; }

        public void MoveToFloor(IPerson person)
        {
            TargetFloor = person.TargetFloor;
            // TODO: after some time passes
            ArrivedAtFloor();
            TargetFloor = -1;
            // TODO: person has reached goal - remove person completely
        }

        public void ArrivedAtFloor()
        {
            CurrentFloor = TargetFloor;
        }
    }
}
