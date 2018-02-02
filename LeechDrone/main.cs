public void Main(string a){
    List<List<IMyTerminalBlock>> l = new List<List<IMyTerminalBlock>> {
        new List<IMyTerminalBlock>(), new List<IMyTerminalBlock>(), new List<IMyTerminalBlock>(), new List<IMyTerminalBlock>(), new List<IMyTerminalBlock>(), new List<IMyTerminalBlock>()
    };
    GridTerminalSystem.GetBlockGroupWithName("iu").GetBlocks(l[0]);
    GridTerminalSystem.GetBlockGroupWithName("id").GetBlocks(l[1]);
    GridTerminalSystem.GetBlockGroupWithName("il").GetBlocks(l[2]);
    GridTerminalSystem.GetBlockGroupWithName("ir").GetBlocks(l[3]);
    GridTerminalSystem.GetBlockGroupWithName("if").GetBlocks(l[4]);
    GridTerminalSystem.GetBlockGroupWithName("ib").GetBlocks(l[5]);
    IMyShipController sc = GridTerminalSystem.GetBlockWithName("sc") as IMyShipController;
    IMyGyro sg = GridTerminalSystem.GetBlockWithName("sg") as IMyGyro;
    IMyTimerBlock tb = GridTerminalSystem.GetBlockWithName("BTB") as IMyTimerBlock;
    const float gm = 10;
    if(a.Equals("d")) {
        //Dampers on/off
        sc.DampenersOverride = !sc.DampenersOverride;
    } else if(a.StartsWith("g")) {
        //Gyro
        sg.GyroOverride=true;
        int m;
        if(a.Split(' ')[2].Equals("-")) {
            m=-1;
        } else {
            m=1;
        }
        if(a.Split(' ')[1].Equals("y")) {
            sg.Yaw=m*gm;
        } else if(a.Split(' ')[1].Equals("p")) {
            sg.Pitch=m*gm;
        } else if(a.Split(' ')[1].Equals("r")) {
            sg.Roll=m*gm;
        }
        tb.StartCountdown();
    } else if(a.StartsWith("t")) {
        //Thrusters
        int dir = int.Parse(a.Split(' ')[1]);
        foreach(IMyThrust t in l[dir]) {
            t.ThrustOverridePercentage = 1;
        }
        tb.StartCountdown();
    } else if(a.Equals("r")) {
        sg.Yaw=0;
        sg.Pitch=0;
        sg.Roll=0;
        sg.GyroOverride=false;
        for(int i=0;i<6;i++) {
            foreach(IMyThrust t in l[i]) {
                t.ThrustOverridePercentage = 0;
            }
        }
    }
}