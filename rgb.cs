public void Main() {
	IMyLightingBlock l = GridTerminalSystem.GetBlockWithName("Spotlight") as IMyLightingBlock;
	IMyTimerBlock t = GridTerminalSystem.GetBlockWithName("tb12") as IMyTimerBlock;
	Color c = l.Color;
	int r = c.R;
	int g = c.G;
	int b = c.B;
	b++;
	if(b==256) {
		b = 0;
		g++;
	}
	if(g==256) {
		g = 0;
		r++;
	}
	if(r==256) {
		r = 0;
	}
	l.Color = new Color(r,g,b);
	t.StartCountdown();
}