public Program()
{
	Echo("Simple Auto Rename loaded");
	Echo("");
	Echo("Enter prefix in arguments and press run");
}

public void Main(string prefix) {
	if (!prefix.Equals("")) {
		Echo("Renaming blocks");
		List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
		GridTerminalSystem.GetBlocks(blocks);

		foreach (var block in blocks) {
			Echo("Renaming " + block.CustomName);
			block.CustomName = prefix + block.CustomName;
		}
		Echo("");
		Echo("Renaming DONE");
		Echo("Please remove this programmable block now");
	}
	else {
		Echo("EROR: No prefix entered");
		Echo("Enter prefix in arguments and press run");
	}
}
