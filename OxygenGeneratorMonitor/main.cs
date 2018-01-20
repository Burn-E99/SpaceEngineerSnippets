public void Main() {
    IMyGasTank t0 = GridTerminalSystem.GetBlockWithName("B OT#1") as IMyGasTank;
    IMyGasGenerator g0 = GridTerminalSystem.GetBlockWithName("B OG#1") as IMyGasGenerator;
    if(t0.FilledRatio >= .75) {
        g0.Enabled = false;
    } else if(t0.FilledRatio < .50) {
        g0.Enabled = true;
    }
}