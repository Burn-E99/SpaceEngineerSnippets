public void Main() {
    IMyDoor r = GridTerminalSystem.GetBlockWithName("Boarding Ramp") as IMyDoor;
    IMyTimerBlock t = GridTerminalSystem.GetBlockWithName("RoverLower Timer Block") as IMyTimerBlock;
    IMyMotorSuspension w1 = GridTerminalSystem.GetBlockWithName("Wheel Suspension 5x5 Left Front") as IMyMotorSuspension;
    IMyMotorSuspension w2 = GridTerminalSystem.GetBlockWithName("Wheel Suspension 5x5 Right Front") as IMyMotorSuspension;
    IMyLightingBlock l1 = GridTerminalSystem.GetBlockWithName("Ramp Status Light 1") as IMyLightingBlock;
    IMyLightingBlock l2 = GridTerminalSystem.GetBlockWithName("Ramp Status Light 2") as IMyLightingBlock;

    w1.Height += .25f;
    w2.Height += .25f;

    if(w1.Height < .5f) {
        t.StartCountdown();
    } else if(w1.Height == .5f) {
        Color green = new Color(0,255,0);
        l1.Color = green;
        l2.Color = green;
        l1.BlinkLength = 100f;
        l2.BlinkLength = 100f;

        r.OpenDoor();
    }
}