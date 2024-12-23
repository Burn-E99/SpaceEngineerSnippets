public void Main(string a) {
    string myID = "";
    string myPF = " ";
    if(Me.CustomName.Split(' ')[0].Equals("al")) {
        myID = Me.CustomName.Split(' ')[2];
    } else {
        myID = Me.CustomName.Split(' ')[3];
        myPF = Me.CustomName.Split(' ')[0] + " ";
    }
    IMySensorBlock si = GridTerminalSystem.GetBlockWithName(myPF + "al si " + myID) as IMySensorBlock;
    IMySensorBlock sc = GridTerminalSystem.GetBlockWithName(myPF + "al sc " + myID) as IMySensorBlock;
    IMySensorBlock so = GridTerminalSystem.GetBlockWithName(myPF + "al so " + myID) as IMySensorBlock;
    IMyDoor _di = GridTerminalSystem.GetBlockWithName(myPF + "al di " + myID) as IMyDoor;
    IMyDoor _do = GridTerminalSystem.GetBlockWithName(myPF + "al do " + myID) as IMyDoor;
    IMyAirVent av = GridTerminalSystem.GetBlockWithName(myPF + "al av " + myID) as IMyAirVent;
    IMyTimerBlock tb = GridTerminalSystem.GetBlockWithName(myPF + "al tb " + myID) as IMyTimerBlock;
    if(myID.Equals("0") && a.StartsWith("id:")) {
        string newID = a.Split(':')[1];
        Me.CustomName = Me.CustomName.Substring(0, Me.CustomName.Length - 1) + newID;
        si.CustomName = si.CustomName.Substring(0, si.CustomName.Length - 1) + newID;
        sc.CustomName = sc.CustomName.Substring(0, sc.CustomName.Length - 1) + newID;
        so.CustomName = so.CustomName.Substring(0, so.CustomName.Length - 1) + newID;
        _di.CustomName = _di.CustomName.Substring(0, _di.CustomName.Length - 1) + newID;
        _do.CustomName = _do.CustomName.Substring(0, _do.CustomName.Length - 1) + newID;
        av.CustomName = av.CustomName.Substring(0, av.CustomName.Length - 1) + newID;
        tb.CustomName = tb.CustomName.Substring(0, tb.CustomName.Length - 1) + newID;
    } else if(!myID.Equals("0")) {
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
                av.Depressurize = true;
                tb.StartCountdown();
            } else if(Storage == "o") {
                _do.CloseDoor();
                Storage = "oc";
                av.Depressurize = false;
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
}