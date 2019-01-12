public void Main(string s) {
    IMyMotorSuspension w1 = GridTerminalSystem.GetBlockWithName("Wheel Suspension 5x5 Left Front") as IMyMotorSuspension;
    IMyMotorSuspension w3 = GridTerminalSystem.GetBlockWithName("Wheel Suspension 5x5 Left Rear 1") as IMyMotorSuspension;
    IMyMotorSuspension w5 = GridTerminalSystem.GetBlockWithName("Wheel Suspension 5x5 Left Rear 2") as IMyMotorSuspension;
    IMyMotorSuspension w2 = GridTerminalSystem.GetBlockWithName("Wheel Suspension 5x5 Right Front") as IMyMotorSuspension;
    IMyMotorSuspension w4 = GridTerminalSystem.GetBlockWithName("Wheel Suspension 5x5 Right Rear 1") as IMyMotorSuspension;
    IMyMotorSuspension w6 = GridTerminalSystem.GetBlockWithName("Wheel Suspension 5x5 Right Rear 2") as IMyMotorSuspension;

    if(s.Equals("scrape")) {
        w1.SetValue("Speed Limit", 3f);
        w2.SetValue("Speed Limit", 3f);
        w3.SetValue("Speed Limit", 3f);
        w4.SetValue("Speed Limit", 3f);
        w5.SetValue("Speed Limit", 3f);
        w6.SetValue("Speed Limit", 3f);
    } else if(s.Equals("normal")) {
        w1.SetValue("Speed Limit", 108f);
        w2.SetValue("Speed Limit", 108f);
        w3.SetValue("Speed Limit", 108f);
        w4.SetValue("Speed Limit", 108f);
        w5.SetValue("Speed Limit", 108f);
        w6.SetValue("Speed Limit", 108f);
    }
}