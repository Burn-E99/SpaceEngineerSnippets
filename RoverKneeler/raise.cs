public void Main() {
    IMyTimerBlock t = GridTerminalSystem.GetBlockWithName("RoverRaise Timer Block") as IMyTimerBlock;
    IMyMotorSuspension w1 = GridTerminalSystem.GetBlockWithName("Wheel Suspension 5x5 Left Front") as IMyMotorSuspension;
    IMyMotorSuspension w2 = GridTerminalSystem.GetBlockWithName("Wheel Suspension 5x5 Right Front") as IMyMotorSuspension;
    IMyLightingBlock l1 = GridTerminalSystem.GetBlockWithName("Ramp Status Light 1") as IMyLightingBlock;
    IMyLightingBlock l2 = GridTerminalSystem.GetBlockWithName("Ramp Status Light 2") as IMyLightingBlock;

    w1.Height -= .25f;
    w2.Height -= .25f;

    if(w1.Height > -1.5f) {
        t.StartCountdown();
    } else if(w1.Height == -1.5f) {
        Color red = new Color(255,0,0);
        l1.Color = red;
        l2.Color = red;
        l1.BlinkLength = 100f;
        l2.BlinkLength = 100f;
    }
}