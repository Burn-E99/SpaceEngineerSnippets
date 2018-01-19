List<IMyTerminalBlock> l = new List<IMyTerminalBlock>(); 
public void Main(string a, UpdateType u) {
    // check source
    if((u & UpdateType.Trigger) != 0) {
        // update by timer
        Vector3D p = new Vector3D(0, 0, 0);
        p = Me.GetPosition();
        GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(l);
        var x = l[0] as IMyTextPanel;
        string id = x.GetPublicText();
        string m = "DF" + id + " " + p.X + " " + p.Y + " " + p.Z;
        GridTerminalSystem.GetBlocksOfType<IMyRadioAntenna>(l);
        var y = l[0] as IMyRadioAntenna;
        y.TransmitMessage(m, MyTransmitTarget.Owned);
    } else if((u & UpdateType.Antenna) != 0) {
        // update from message
        string[] s = a.Split(' ');
        GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(l);
        var x = l[0] as IMyTextPanel;
        if(s[0].Equals("DF" + x.GetPublicText())) {
            GridTerminalSystem.GetBlocksOfType<IMyRemoteControl>(l);
            var r = l[0] as IMyRemoteControl;
            Vector3D wp = new Vector3D(float.Parse(s[1]), float.Parse(s[2]), float.Parse(s[3]));  
            r.ClearWaypoints();
            r.AddWaypoint(wp, "wp");
            r.SetAutoPilotEnabled(true);
        }
    }
}