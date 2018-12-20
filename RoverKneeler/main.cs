public void Main() {
    IMyDoor r = GridTerminalSystem.GetBlockWithName("Boarding Ramp") as IMyDoor;
    IMyTimerBlock t1 = GridTerminalSystem.GetBlockWithName("RoverLower Timer Block") as IMyTimerBlock;
    IMyTimerBlock t2 = GridTerminalSystem.GetBlockWithName("RoverRaise Timer Block") as IMyTimerBlock;
    IMyLightingBlock l1 = GridTerminalSystem.GetBlockWithName("Ramp Status Light 1") as IMyLightingBlock;
    IMyLightingBlock l2 = GridTerminalSystem.GetBlockWithName("Ramp Status Light 2") as IMyLightingBlock;

    Color yellow = new Color(233,195,0);
    l1.Color = yellow;
    l2.Color = yellow;
    l1.BlinkLength = 50f;
    l2.BlinkLength = 50f;

    if(Storage == "up") {
        t1.StartCountdown();

        Storage = "down";
    } else if(Storage == "down") {
        r.CloseDoor();

        t2.StartCountdown();

        Storage = "up";
    }
}