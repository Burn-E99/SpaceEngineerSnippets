public void Main(string a) {
    IMySensorBlock si = GridTerminalSystem.GetBlockWithName("B Inside Sensor 1") as IMySensorBlock;
    IMySensorBlock sc = GridTerminalSystem.GetBlockWithName("B Center Sensor 1") as IMySensorBlock;
    IMySensorBlock so = GridTerminalSystem.GetBlockWithName("B Outside Sensor 1") as IMySensorBlock;
    IMyDoor _di = GridTerminalSystem.GetBlockWithName("B Inside Door 1") as IMyDoor;
    IMyDoor _do = GridTerminalSystem.GetBlockWithName("B Outside Door 1") as IMyDoor;
    IMyAirVent av = GridTerminalSystem.GetBlockWithName("B Airlock Airvent 1") as IMyAirVent;
    IMyTimerBlock tb = GridTerminalSystem.GetBlockWithName("B Airlock Timer 1") as IMyTimerBlock;
    if(a.Equals("out")) {
        if(Storage == "") {
            av.Depressurize = true;
            _do.Enabled = true;
            _do.OpenDoor();
            Storage = "o";
        } else if(Storage == "ict") {
            _do.CloseDoor();
            Storage = "icto";
            tb.StartCountdown();
        }
    } else if(a.Equals("center")) {
        if(Storage == "i") {
            _di.CloseDoor();
            Storage = "ic";
            tb.StartCountdown();
        } else if(Storage == "o") {
            _do.CloseDoor();
            Storage = "oc";
            tb.StartCountdown();
        }
    } else if(a.Equals("in")) {
        if(Storage == "") {
            av.Depressurize = false;
            _di.Enabled = true;
            _di.OpenDoor();
            Storage = "i";
        } else if(Storage == "oct") {
            _di.CloseDoor();
            Storage = "octi";
            tb.StartCountdown();
        }
    } else if(a.Equals("timer")) {
        if(Storage == "ic") {
            _di.Enabled = false;
            av.Depressurize = true;
            _do.Enabled = true;
            _do.OpenDoor();
            Storage = "ict";
        } else if(Storage == "icto") {
            _do.Enabled = false;
            Storage = "";
        } else if(Storage == "oc") {
            _do.Enabled = false;
            av.Depressurize = false;
            _di.Enabled = true;
            _di.OpenDoor();
            Storage = "oct";
        } else if(Storage == "octi") {
            _di.Enabled = false;
            Storage = "";
        }
    }
}