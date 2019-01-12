// Isy's Solar Alignment Script
// ============================
// Version: 4.1.3
// Date: 2018-11-24

// =======================================================================================
//                                                                            --- Configuration ---
// =======================================================================================

// --- Essential Configuration ---
// =======================================================================================

// Name of the group with all the solar related rotors (not needed in gyro mode)
string rotorGroupName = "Solar Rotors";

// By enabling gyro mode, the script will no longer use rotors but all gyroscopes on the grid instead.
// This mode only makes sense when used on a SHIP in SPACE. Gyro mode deactivates the following
// features: night mode, rotate to sunrise, time calculation and triggering external timer blocks.
bool useGyroMode = false;

// Name of the reference group for gyro mode. Put your main cockpit, flight seat or remote control in this group!
string referenceGroupName = "Solar Reference";


// --- Rotate to sunrise ---
// =======================================================================================

// Rotate the panels towards the sunrise during the night? (Possible values: true | false, default: true)
// The angle is figured out automatically based on the first lock of the day.
// If you want to set the angles yourself, set manualAngle to true and adjust the angles to your likings.
bool rotateToSunrise = true;
bool manualAngle = false;
int manualAngleVertical = 0;
int manualAngleHorizontal = 0;


// --- Reactor fallback ---
// =======================================================================================

// With this option, you can enable your reactors as a safety fallback, if not enough power is available
// to power all your machines or if the battery charge gets low. By default, all reactors on the same grid
// will be used. If you only want to use specific ones, put their names or group in the list.
// Example: string[] fallbackReactors = { "Small Reactor 1", "Base reactor group", "Large Reactor" };
bool useReactorFallback = false;
string[] fallbackReactors = { };

// Activation on low battery?
// The reactors will be active until 'turnOffAtPercent' of the max battery charge after it was turned on at 'turnOnAtPercent'.
bool activateOnLowBattery = true;
double turnOnAtPercent = 10;
double turnOffAtPercent = 15;

// Activate on overload?
// If the combined output of batteries and solar panels is more than 'overloadPercentage' of their max output, the reactors will be turned on.
bool activateOnOverload = true;
double overloadPercentage = 90;


// --- Base Light Management ---
// =======================================================================================

// Enable base light management? (Possible values: true | false, default: false)
// Lights will be turned on/off based on daytime.
bool baseLightManagement = false;

// Simple mode: toggle lights based on max. solar output (percentage). Time based toggle will be deactivated.
bool simpleMode = false;
int simpleThreshold = 50;

// Define the times when your lights should be turned on or off. If simple mode is active, this does nothing.
int lightOffHour = 8;
int lightOnHour = 18;

// To only toggle specific lights, declare groups for them.
// Example: string[] baseLightGroups = { "Interior Lights", "Spotlights", "Hangar Lights" };
string[] baseLightGroups = { };


// --- LCD panels ---
// =======================================================================================

// Add the following keyword to any LCD panel to show the script's informations.
// Edit the LCD's custom data to change the information that is shown there.
// When using the keyword on corner LCDs, put one of these keywords in their custom data:
// time, battery, oxygen
string lcdKeyword = "!SAS";


// --- Terminal statistics ---
// =======================================================================================

// The script can display informations in the names of the used blocks. The shown information is a percentage of
// the current efficiency (solar panels and oxygen farms) or the fill level (batteries and tanks).
// You can enable or disable single statistics or disable all using the master switch below.
bool enableTerminalStatistics = true;

bool showSolarStats = true;
bool showBatteryStats = true;
bool showOxygenFarmStats = true;
bool showOxygenTankStats = true;


// --- External timer blocks ---
// =======================================================================================

// Trigger external timer blocks at specific events? (action "Start" will be applied which takes the delay into account)
// Events can be: "sunrise", "sunset", a time like "15:00" or a number for every X seconds
// Every event needs a timer block name in the exact same order as the events.
// Calling the same timer block with multiple events requires it's name multiple times in the timers list!
// Example:
// string[] events = { "sunrise", "sunset", "15:00", "30" };
// string[] timers = { "Timer 1", "Timer 1", "Timer 2", "Timer 3" };
// This will trigger "Timer 1" at sunrise and sunset, "Timer 2" at 15:00 and "Timer 3" every 30 seconds.
bool triggerTimerBlock = false;
string[] events = { };
string[] timers = { };


// --- Settings for enthusiasts ---
// =======================================================================================

// Change percentage of the last locked output where the script should realign for a new best output (default: 2, gyro: 5)
double realginPercentageRotor = 2;
double realignPercentageGyro = 5;

// Percentage of the max detected output where the script starts night mode (default: 10)
double nightPercentage = 10;

// Percentage of the max detected output where the script detects night for time calculation (default: 50)
double nightTimePercentage = 50;

// Rotor speeds (speeds are automatically scaled between these values based on the output)
const float rotorMinSpeed = 0.1f;
const float rotorMaxSpeed = 1.0f;

// Rotor options
float rotorTorqueLarge = 33600000f;
float rotorTorqueSmall = 448000f;
bool setInertiaTensor = true;
bool setRotorLockWhenStopped = false;

// Min gyro RPM, max gyro RPM and gyro power for gyro mode
const double minGyroRPM = 0.1;
const double maxGyroRPM = 1;
const float gyroPower = 1f;

// Debugging
string debugLcd = "LCD Solar Alignment Debugging";
bool showPerformance = true;
bool showBlockCounts = true;


// =======================================================================================
//                                                                      --- End of Configuration ---
//                                                        Don't change anything beyond this point!
// =======================================================================================


// Output variables
double rotorOutput = 0;
double outputBestPanel = 0;
double maxOutputAP = 0;
double maxOutputAPLast = 0;
double maxDetectedOutputAP = 0;
double currentOutputAP = 0;

// Lists
List<IMyMotorStator> rotors = new List<IMyMotorStator>();
List<IMyMotorStator> vRotors = new List<IMyMotorStator>();
List<IMyMotorStator> hRotors = new List<IMyMotorStator>();
List<IMyGyro> gyros = new List<IMyGyro>();
List<IMyTextPanel> lcds = new List<IMyTextPanel>();
List<IMyTextPanel> cornerLcds = new List<IMyTextPanel>();
List<IMyInteriorLight> lights = new List<IMyInteriorLight>();
List<IMyReflectorLight> spotlights = new List<IMyReflectorLight>();
List<IMyReactor> reactors = new List<IMyReactor>();

// Rotor variables
List<IMySolarPanel> solarPanels = new List<IMySolarPanel>();
int solarPanelsCount = 0;
bool nightModeActive = false;
bool sunrisePosReached = false;
int nightModeTimer = 30;
int realignTimer = 90;
bool rotateAllInit = true;
List<string> defaultCustomDataRotor = new List<string> {
	"output=0",
	"outputLast=0",
	"outputLocked=0",
	"outputMax=0",
	"outputMaxAngle=0",
	"outputMaxDayBefore=0",
	"outputBestPanel=0",
	"direction=1",
	"directionChanged=0",
	"directionTimer=0",
	"allowRotation=1",
	"rotationDone=1",
	"timeSinceRotation=0",
	"firstLockOfDay=0",
	"sunriseAngle=0"
};

// Gyro variables
List<IMyShipController> gyroReference = new List<IMyShipController>();
double outputLockedPitch = 0;
double outputLockedYaw = 0;
double outputLockedRoll = 0;
double directionPitch = 1;
double directionYaw = 1;
double directionRoll = 1;
bool directionChangedPitch = false;
bool directionChangedYaw = false;
bool directionChangedRoll = false;
double directionTimerPitch = 0;
double directionTimerYaw = 0;
double directionTimerRoll = 0;
bool allowPitch = true;
bool allowYaw = true;
bool allowRoll = true;
double timeSincePitch = 0;
double timeSinceYaw = 0;
double timeSinceRoll = 0;

// Battery variables
List<IMyBatteryBlock> batteries = new List<IMyBatteryBlock>();
double batInput = 0;
double batInputMax = 0;
double batOutput = 0;
double batOutputMax = 0;
double batCharge = 0;
double batChargeMax = 0;

// Oxygen farm and tank variables
List<IMyOxygenFarm> farms = new List<IMyOxygenFarm>();
List<IMyGasTank> tanks = new List<IMyGasTank>();
double farmEfficiency = 0;
double tankCapacity = 0;
double tankFillLevel = 0;
int farmCount = 0;

// String variables for showing the information
string maxOutputAPStr = "0 kW";
string maxDetectedOutputAPStr = "0 kW";
string currentOutputAPStr = "0 kW";
string batInputStr = "0 kW";
string batInputMaxStr = "0 kW";
string batOutputStr = "0 kW";
string batOutputMaxStr = "0 kW";
string batChargeStr = "0 kW";
string batChargeMaxStr = "0 kW";
string tankCapacityStr = "0 L";
string tankFillLevelStr = "0 L";

// Information strings
string currentOperation = "Checking setup...";
string currentOperationInfo;
string[] workingIndicator = { "/", "-", "\\", "|" };
int workingCounter = 0;

// Variables for time measuring
int dayTimer = 0;
int safetyTimer = 270;
const int dayLengthDefault = 7200;
int dayLength = dayLengthDefault;
const int sunSetDefault = dayLengthDefault / 2;
int sunSet = sunSetDefault;

// LCD variables
List<string> defaultCustomDataLCD = new List<string> {
	"showCurrentOperation=true",
	"showSolarStats=true",
	"showBatteryStats=true",
	"showOxygenStats=true",
	"showLocationTime=true"
};

// Reactor fallback
bool enableReactors = false;
string lastActivation = "";

// Error handling
string error, warning;
int errorCount = 0;
int warningCount = 0;

// Command line parameters
string action = "align";
int actionTimer = 3;
bool pause = false;
string rotateMode = "both";
bool pauseAfterRotate = false;
double rotateHorizontalAngle = 0;
double rotateVerticalAngle = 0;


// Script timing variables
int ticksSinceLastRun = 0;
int ticksPerScriptStep = 15;
int execCounter = 1;
bool firstRun = true;
bool init = true;
bool refreshLCDs = true;

// Method names
string[] methodName = {
	"",
	"Get Output",
	"Time Calculation",
	"Rotation Logic",
	"Reactor Fallback"
};


public Program()
{
	// Load variables out of the programmable block's custom data field
	Load();

	// Settings for nerds recalculation
	realginPercentageRotor = (realginPercentageRotor % 100) / 100;
	realignPercentageGyro = (realignPercentageGyro % 100) / 100;
	nightPercentage = (nightPercentage % 100) / 100;
	nightTimePercentage = (nightTimePercentage % 100) / 100;

	// Set UpdateFrequency for starting the programmable block over and over again
	Runtime.UpdateFrequency = UpdateFrequency.Update1;
}


void Main(string parameter)
{
	try {
		CreatePerformanceText("", true);

		// Store the parameter
		if (parameter != "") {
			action = parameter.ToLower();
			execCounter = 3;
		}

		// Stop all rotors and create initial information
		if (firstRun) {
			GetBlocks();
			StopAll(false);
			RemoveTerminalStatistics();
			firstRun = false;
		}

		// Script timing
		if (ticksSinceLastRun < ticksPerScriptStep) {
			ticksSinceLastRun++;
			return;
		} else {
			// Get all connected inventory blocks
			if (init) {
				GetBlocks();
				init = false;
				return;
			}

			if (refreshLCDs) {
				WriteLCD();
				WriteCornerLCD();
				WriteDebugLCD();

				refreshLCDs = false;
				return;
			}

			workingCounter = workingCounter >= 3 ? 0 : workingCounter + 1;
			ticksSinceLastRun = 0;
			refreshLCDs = true;
			init = true;
		}


		// Get output
		if (execCounter == 1 && error == null) {
			// Get the output of all measured blocks
			GetOutput();
		}

		// Time calculation
		if (execCounter == 2 && !useGyroMode && error == null) {
			// Time Calculation
			TimeCalculation();

			// Switch the lights if base light management is activated
			if (baseLightManagement) LightManagement();

			// Trigger a timer block if triggerTimerBlock is true
			if (triggerTimerBlock) TriggerExternalTimerBlock();
		}

		// Rotation logic
		if (execCounter == 3 && error == null) {
			// Either execute argument or use main rotation logic
			if (!ExecuteArgument(action)) {
				if (useGyroMode) {
					RotationLogicGyro();
				} else {
					RotationLogic();
				}
			}
		}

		// Reactor fallback
		if (execCounter == 4 && error == null) {
			// Reactor fallback
			if (useReactorFallback) ReactorFallback();

			// Update variables for the next run
			foreach (var rotor in rotors) {
				double outputLast = ReadCustomData(rotor, "output");
				WriteCustomData(rotor, "outputLast", outputLast);
			}
			maxOutputAPLast = maxOutputAP;

			// Save variables into the programmable block's custom data field
			Save();
		}

		// Write the information to various channels
		Echo(CreateInformation(true));
		CreatePerformanceText(methodName[execCounter]);

		// Update the script execution counter
		if (execCounter >= 4) {
			// Reset the counter
			execCounter = 1;

			// Reset errors and warnings if none were counted
			if (errorCount == 0) error = null;
			if (warningCount == 0) warning = null;

			errorCount = 0;
			warningCount = 0;
		} else {
			execCounter++;
		}
	} catch (Exception e) {
		StopAll();
		string info = e + " \n\n";
		info += "The error occured while executing the following script step:\n" + methodName[execCounter] + " (ID: " + execCounter + ")";
		WriteLCD(info);
		throw new Exception(info);
	}
}


void GetBlocks()
{
	// Get base grid of the PB
	if (baseGrid == null) {
		GetBaseGrid(Me.CubeGrid);
	}

	// LCDs
	GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(lcds, l => l.CustomName.Contains(lcdKeyword) && !l.BlockDefinition.SubtypeId.Contains("Corner"));

	// Corner LCDs
	GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(cornerLcds, l => l.CustomName.Contains(lcdKeyword) && l.BlockDefinition.SubtypeId.Contains("Corner"));


	// Rotor Mode
	if (!useGyroMode) {
		// Get rotors
		var rotorGroup = GridTerminalSystem.GetBlockGroupWithName(rotorGroupName);

		// If present, copy rotors into rotors list, else throw message
		if (rotorGroup != null) {
			rotorGroup.GetBlocksOfType<IMyMotorStator>(rotors);

			// Create error if no rotor was in the group
			if (rotors.Count == 0) {
				CreateError("There are no rotors in the rotor group:\n'" + rotorGroupName + "'");
				return;
			}
		} else {
			CreateError("Rotor group not found:\n'" + rotorGroupName + "'");
			return;
		}

		HashSet<IMyCubeGrid> grids = new HashSet<IMyCubeGrid>();

		// Get unique grids and prepare the rotors
		foreach (var rotor in rotors) {
			if (!rotor.IsFunctional) CreateWarning("'" + rotor.CustomName + "' is broken!\nRepair it to use it for aligning!");
			if (!rotor.Enabled) CreateWarning("'" + rotor.CustomName + "' is turned off!\nTurn it on to use it for aligning!");
			if (!rotor.IsAttached) CreateWarning("'" + rotor.CustomName + "' has no rotor head!\nAdd one to use it for aligning!");

			grids.Add(rotor.CubeGrid);

			// Set basic stats for every rotor
			if (rotor.CubeGrid.GridSize == 0.5) {
				rotor.Torque = rotorTorqueSmall;
			} else {
				rotor.Torque = rotorTorqueLarge;
			}

			// Give warning, if the owner is different
			if (rotor.GetOwnerFactionTag() != Me.GetOwnerFactionTag()) {
				CreateWarning("'" + rotor.CustomName + "' has a different owner / faction!\nAll blocks should have the same owner / faction!");
			}
		}

		// Find vertical and horizontal rotors and add them to their respective list
		vRotors.Clear();
		hRotors.Clear();
		foreach (var rotor in rotors) {
			if (grids.Contains(rotor.TopGrid)) {
				vRotors.Add(rotor);
			} else {
				hRotors.Add(rotor);

				// Set inertia tensor for horizontal rotors that are not on the main grid and if active in the config
				if (rotor.CubeGrid != baseGrid && setInertiaTensor) {
					try {
						rotor.SetValueBool("ShareInertiaTensor", true);
					} catch (Exception) {
						// Ignore if it fails on DS
					}
				}
			}
		}

		// Check, if a U-Shape is used and rebuild the list with only one of the connected rotors
		List<IMyMotorStator> hRotorsTemp = new List<IMyMotorStator>();
		hRotorsTemp.AddRange(hRotors);
		hRotors.Clear();
		bool addRotor;

		foreach (var rotorTemp in hRotorsTemp) {
			addRotor = true;

			foreach (var rotor in hRotors) {
				if (rotor.TopGrid == rotorTemp.TopGrid) {
					rotorTemp.RotorLock = false;
					rotorTemp.TargetVelocityRPM = 0f;
					rotorTemp.Torque = 0f;
					rotorTemp.BrakingTorque = 0f;
					addRotor = false;
					break;
				}
			}

			if (addRotor) hRotors.Add(rotorTemp);
		}

		// Get solar panels and oxygen farms
		solarPanels.Clear();
		farms.Clear();

		// Cycle through all hRotors and check if they have solar panels or oxygen farms and sum up their output
		foreach (var hRotor in hRotors) {
			rotorOutput = 0;
			outputBestPanel = 0;

			// Find all grids that are on top of this rotor
			GetConnectedGrids(hRotor.TopGrid, true);

			// Get all solar panels on these grids
			var rotorSolarPanels = new List<IMySolarPanel>();
			GridTerminalSystem.GetBlocksOfType<IMySolarPanel>(rotorSolarPanels, s => connectedGrids.Contains(s.CubeGrid) && s.IsWorking);

			// Get all oxygen farms on these grids
			var rotorOxygenFarms = new List<IMyOxygenFarm>();
			GridTerminalSystem.GetBlocksOfType<IMyOxygenFarm>(rotorOxygenFarms, o => connectedGrids.Contains(o.CubeGrid) && o.IsWorking);

			// Sum up the solar panels' output
			foreach (var solarPanel in rotorSolarPanels) {
				solarPanels.Add(solarPanel);
				rotorOutput += solarPanel.MaxOutput;
				if (solarPanel.MaxOutput > outputBestPanel) outputBestPanel = solarPanel.MaxOutput;
			}

			// Sum up the oxygen farms' output
			foreach (var oxygenFarm in rotorOxygenFarms) {
				farms.Add(oxygenFarm);
				rotorOutput += oxygenFarm.GetOutput();
				if (oxygenFarm.GetOutput() > outputBestPanel) outputBestPanel = oxygenFarm.GetOutput();
			}

			// Print a warning if a rotor has neither a solar panel nor an oxygen farm
			if (rotorSolarPanels.Count == 0 && rotorOxygenFarms.Count == 0) {
				CreateWarning("'" + hRotor.CustomName + "' can't see the sun!\nAdd a solar panel or oxygen farm to it!");
			}

			// Write the output in the custom data field
			WriteCustomData(hRotor, "output", rotorOutput);
			WriteCustomData(hRotor, "outputBestPanel", outputBestPanel);

			// If it's higher than the max detected output, write it, too and also remember the rotor's current angle
			if (rotorOutput > ReadCustomData(hRotor, "outputMax")) {
				WriteCustomData(hRotor, "outputMax", rotorOutput);
				WriteCustomData(hRotor, "outputMaxAngle", GetAngle(hRotor));
			}
		}

		// Read and store the combined output of all hRotors that are on top of vRotors
		foreach (var vRotor in vRotors) {
			double output = 0;
			outputBestPanel = double.MaxValue;

			foreach (var hRotor in hRotors) {
				if (hRotor.CubeGrid == vRotor.TopGrid) {
					output += ReadCustomData(hRotor, "output");
					if (ReadCustomData(hRotor, "outputBestPanel") < outputBestPanel) outputBestPanel = ReadCustomData(hRotor, "outputBestPanel");
				}
			}

			// Write the output in the custom data field
			WriteCustomData(vRotor, "output", output);
			WriteCustomData(vRotor, "outputBestPanel", outputBestPanel);

			// If it's higher than the max detected output, write it, too and also remember the rotor's current angle
			if (output > ReadCustomData(vRotor, "outputMax")) {
				WriteCustomData(vRotor, "outputMax", output);
				WriteCustomData(vRotor, "outputMaxAngle", GetAngle(vRotor));
			}
		}
	}

	// Get grids that are connected via rotors or pistons to the base grid
	GetConnectedGrids(baseGrid, true);

	// Gyro Mode
	if (useGyroMode) {
		// Create error if the grid is stationary
		if (Me.CubeGrid.IsStatic) {
			CreateError("The grid is stationary!\nConvert it to a ship in the Info tab!");
			return;
		}

		// Get reference group
		var referenceGroup = GridTerminalSystem.GetBlockGroupWithName(referenceGroupName);
		if (referenceGroup != null) {
			referenceGroup.GetBlocksOfType<IMyShipController>(gyroReference);

			if (gyroReference.Count == 0) {
				CreateError("There are no cockpits, flight seats or remote controls in the reference group:\n'" + referenceGroupName + "'");
				return;
			}
		} else {
			CreateError("Reference group not found!\nPut your main cockpit, flight seat or remote control in a group called '" + referenceGroupName + "'!");
			return;
		}

		// Get gyroscopes
		GridTerminalSystem.GetBlocksOfType<IMyGyro>(gyros, g => g.CubeGrid == baseGrid && g.IsWorking);

		// Create error if no gyroscopes were found
		if (gyros.Count == 0) {
			CreateError("No gyroscopes found!\nAre they enabled and completely built?");
			return;
		}

		// Get solar panels and oxygen farms
		GridTerminalSystem.GetBlocksOfType<IMySolarPanel>(solarPanels, s => connectedGrids.Contains(s.CubeGrid) && s.IsWorking);
		GridTerminalSystem.GetBlocksOfType<IMyOxygenFarm>(farms, o => connectedGrids.Contains(o.CubeGrid) && o.IsWorking);
	}

	// If solar panels or oxygen farm count changed, reset maxOutput of all rotors
	if (solarPanelsCount != solarPanels.Count || farmCount != farms.Count) {
		foreach (var rotor in rotors) {
			WriteCustomData(rotor, "outputMax", 0);
		}
		maxDetectedOutputAP = 0;

		// Update solar panels and oxygen farms count
		solarPanelsCount = solarPanels.Count;
		farmCount = farms.Count;

		CreateError("Amount of solar panels or oxygen farms changed!\nRestarting..");
		return;
	}

	// Show error if no solar panels or oxygen farms were found
	if (solarPanels.Count == 0 && farms.Count == 0) {
		CreateError("No solar panels or oxygen farms found!\nHow should I see the sun now?");
		return;
	}

	// Batteries
	batteries.Clear();
	GridTerminalSystem.GetBlocksOfType<IMyBatteryBlock>(batteries, b => connectedGrids.Contains(b.CubeGrid) && b.IsWorking);

	// Show warning if no battery was found
	if (batteries.Count == 0) {
		CreateWarning("No batteries found!\nDon't you want to store your Power?");
	}

	// Oxygen tanks
	tanks.Clear();
	GridTerminalSystem.GetBlocksOfType<IMyGasTank>(tanks, t => !t.BlockDefinition.SubtypeId.Contains("Hydrogen") && connectedGrids.Contains(t.CubeGrid) && t.IsWorking);

	// Reactors
	if (useReactorFallback) {
		reactors.Clear();

		// Cycle through all the items in regularLcds to find groups or LCDs
		foreach (var item in fallbackReactors) {
			// If the item is a group, get the reactors and join the list with reactors list
			var reactorGroup = GridTerminalSystem.GetBlockGroupWithName(item);
			if (reactorGroup != null) {
				var tempReactors = new List<IMyReactor>();
				reactorGroup.GetBlocksOfType<IMyReactor>(tempReactors);
				reactors.AddRange(tempReactors);
				// Else try adding a single reactor
			} else {
				IMyReactor reactor = GridTerminalSystem.GetBlockWithName(item) as IMyReactor;
				if (reactors != null) {
					reactors.Add(reactor);
				} else {
					CreateWarning("Reactor not found:\n'" + reactor + "'\nUsing all reactors on the grid!");
				}
			}
		}

		// If the list is still empty, add all reactors on the grid
		if (reactors.Count == 0) {
			GridTerminalSystem.GetBlocksOfType<IMyReactor>(reactors, r => connectedGrids.Contains(r.CubeGrid) && r.IsFunctional);
		}
	}

	// Lights
	if (baseLightManagement) {
		lights.Clear();
		spotlights.Clear();

		// If set, fill the list only with the group's lights
		if (baseLightGroups.Length > 0) {
			var tempLights = new List<IMyInteriorLight>();
			var tempSpotlights = new List<IMyReflectorLight>();
			foreach (var group in baseLightGroups) {
				var lightGroup = GridTerminalSystem.GetBlockGroupWithName(group);
				if (lightGroup != null) {
					lightGroup.GetBlocksOfType<IMyInteriorLight>(tempLights);
					lights.AddRange(tempLights);
					lightGroup.GetBlocksOfType<IMyReflectorLight>(tempSpotlights);
					spotlights.AddRange(tempSpotlights);
				} else {
					CreateWarning("Light group not found:\n'" + group + "'");
				}
			}

			// Else search for all interior lights and spotlights and fill the groups
		} else {
			GridTerminalSystem.GetBlocksOfType<IMyInteriorLight>(lights, l => connectedGrids.Contains(l.CubeGrid));
			GridTerminalSystem.GetBlocksOfType<IMyReflectorLight>(spotlights, l => connectedGrids.Contains(l.CubeGrid));
		}
	}
}


bool ExecuteArgument(string arg)
{
	bool validArgument = true;

	// Pause the alignment when set via argument
	if (arg == "pause") {
		StopAll();

		if (pause) {
			action = "align";
			pause = false;
			return false;
		} else {
			action = "paused";
			pause = true;
		}

		currentOperation = "Automatic alignment paused.\n";
		currentOperation += "Run 'pause' again to continue..";

		// After stopping all rotors, only show the pause message in further runs (so that users can rotate manually)
	} else if (arg == "paused") {
		currentOperation = "Automatic alignment paused.\n";
		currentOperation += "Run 'pause' again to continue..";

		// Force a realign to the current best output
	} else if (arg == "realign" && !useGyroMode) {
		Realign();

		currentOperation = "Forced realign by user.\n";
		currentOperation += "Searching highest output for " + realignTimer + " more seconds.";

		if (realignTimer == 0) {
			action = "";
			realignTimer = 90;
		} else {
			realignTimer -= 1;
		}

		// Reset the time calculation when set via argument
	} else if (arg == "reset" && !useGyroMode) {
		dayTimer = 0;
		safetyTimer = 270;
		sunSet = sunSetDefault;
		dayLength = dayLengthDefault;

		currentOperation = "Calculated time resetted.\n";
		currentOperation += "Continuing in " + actionTimer + " seconds.";

		if (actionTimer == 0) {
			action = "";
			actionTimer = 3;
		} else {
			actionTimer -= 1;
		}

		// Rotate to a specific angle when set via argument
	} else if (arg.Contains("rotate") && !useGyroMode) {
		String[] parameters = arg.Split(' ');
		bool couldParse = false;
		rotateMode = "both";
		pauseAfterRotate = false;
		if (parameters[0].Contains("pause")) pauseAfterRotate = true;

		// If 2 parameters were specified, check if it's vertical or horizontal mode
		if (parameters.Length == 2) {
			// Should only the horizontals be rotated?
			if (parameters[1].Contains("h")) {
				couldParse = Double.TryParse(parameters[1].Replace("h", ""), out rotateHorizontalAngle);
				rotateMode = "horizontalOnly";

				// Should only the verticals be rotated?
			} else if (parameters[1].Contains("v")) {
				couldParse = Double.TryParse(parameters[1].Replace("v", ""), out rotateVerticalAngle);
				rotateMode = "verticalOnly";
			}

			if (couldParse) {
				currentOperation = "Checking rotation parameters...";
				action = "rotNormal";
			} else {
				StopAll();
				CreateWarning("Wrong format!\n\nShould be (e.g. 90 degrees):\nrotate h90 OR\nrotate v90");
			}

			// If 3 parameters were specified, check whether horizontal or vertical should be moved first
		} else if (parameters.Length == 3) {
			string plannedAction = "rotNormal";

			// Should the verticals be rotated first?
			if (parameters[1].Contains("v")) {
				couldParse = Double.TryParse(parameters[1].Replace("v", ""), out rotateVerticalAngle);
				if (couldParse) couldParse = Double.TryParse(parameters[2].Replace("h", ""), out rotateHorizontalAngle);
				plannedAction = "rotVH1";

				// Else try parsing normally
			} else {
				couldParse = Double.TryParse(parameters[1].Replace("h", ""), out rotateHorizontalAngle);
				if (couldParse) couldParse = Double.TryParse(parameters[2].Replace("v", ""), out rotateVerticalAngle);
			}

			if (couldParse) {
				currentOperation = "Checking rotation parameters...";
				action = plannedAction;
			} else {
				StopAll();
				CreateWarning("Wrong format!\n\nShould be (e.g. 90 degrees):\nrotate h90 v90 OR\nrotate v90 h90");
			}
		} else {
			StopAll();
			CreateWarning("Not enough parameters!\n\nShould be 2 or 3:\nrotate h90 OR\nrotate h90 v90");
		}

		// Normal rotation
	} else if (arg == "rotNormal") {
		currentOperation = "Rotating to user defined values...";
		bool rotationDone = RotateAll(rotateMode, rotateHorizontalAngle, rotateVerticalAngle);
		if (rotationDone && pauseAfterRotate) {
			action = "paused";
		} else if (rotationDone && !pauseAfterRotate) {
			action = "resume";
		}

		// Vertical first rotation stage 1
	} else if (arg == "rotVH1") {
		currentOperation = "Rotating to user defined values...";
		bool rotationDone = RotateAll("verticalOnly", rotateHorizontalAngle, rotateVerticalAngle);
		if (rotationDone) action = "rotVH2";

		// Vertical first rotation stage 2
	} else if (arg == "rotVH2") {
		currentOperation = "Rotating to user defined values...";
		bool rotationDone = RotateAll("horizontalOnly", rotateHorizontalAngle, rotateVerticalAngle);
		if (rotationDone && pauseAfterRotate) {
			action = "paused";
		} else if (rotationDone && !pauseAfterRotate) {
			action = "resume";
		}
	} else {
		validArgument = false;
	}

	return validArgument;
}


double ReadCustomData(IMyTerminalBlock block, string field)
{
	CheckCustomData(block);
	var customData = block.CustomData.Split('\n').ToList();

	// Find entry
	int index = customData.FindIndex(i => i.Contains(field + "="));

	// Return value of entry if index was found
	if (index > -1) {
		return Convert.ToDouble(customData[index].Replace(field + "=", ""));
	}

	return 0;
}


void WriteCustomData(IMyTerminalBlock block, string field, double value)
{
	CheckCustomData(block);
	var customData = block.CustomData.Split('\n').ToList();

	// Find entry
	int index = customData.FindIndex(i => i.Contains(field + "="));

	// Write new entry if index was found
	if (index > -1) {
		customData[index] = field + "=" + value;
		block.CustomData = String.Join("\n", customData);
	}
}


void CheckCustomData(IMyTerminalBlock block)
{
	var customData = block.CustomData.Split('\n').ToList();

	// Create new default customData if a too short one is found
	if (customData.Count != defaultCustomDataRotor.Count) {
		block.CustomData = String.Join("\n", defaultCustomDataRotor);
	}
}


bool ReadCustomDataLCD(IMyTextPanel lcd, string field)
{
	CheckCustomDataLCD(lcd);
	var customData = lcd.CustomData.Split('\n').ToList();

	// Find entry
	int index = customData.FindIndex(i => i.Contains(field + "="));

	// Return value of entry if index was found
	if (index > -1) {
		try {
			return Convert.ToBoolean(customData[index].Replace(field + "=", ""));
		} catch {
			return true;
		}
	}

	return true;
}


void CheckCustomDataLCD(IMyTextPanel lcd)
{
	var customData = lcd.CustomData.Split('\n').ToList();

	// Create new default customData if a too short one is found
	if (customData.Count != defaultCustomDataLCD.Count) {
		lcd.CustomData = String.Join("\n", defaultCustomDataLCD);
		lcd.FontSize = 0.5f;
	}
}


void GetOutput()
{
	// Solar panels
	maxOutputAP = 0;
	currentOutputAP = 0;

	foreach (var solarPanel in solarPanels) {
		maxOutputAP += solarPanel.MaxOutput;
		currentOutputAP += solarPanel.CurrentOutput;

		// Terminal solar stats
		if (showSolarStats && enableTerminalStatistics) {
			double maxPanelOutput = 0;
			double.TryParse(solarPanel.CustomData, out maxPanelOutput);

			if (maxPanelOutput < solarPanel.MaxOutput) {
				maxPanelOutput = solarPanel.MaxOutput;
				solarPanel.CustomData = maxPanelOutput.ToString();
			}

			AddStatusToName(solarPanel, true, "", solarPanel.MaxOutput, maxPanelOutput);
		}
	}

	// Oxygen farms
	foreach (var oxygenFarm in farms) {
		maxOutputAP += oxygenFarm.GetOutput() / 1000000;
	}

	// Set the max. detected output if a higher output was measured
	if (maxOutputAP > maxDetectedOutputAP) {
		maxDetectedOutputAP = maxOutputAP;
	}

	// Format the output strings
	maxOutputAPStr = maxOutputAP.ToPowerString();
	currentOutputAPStr = currentOutputAP.ToPowerString();
	maxDetectedOutputAPStr = maxDetectedOutputAP.ToPowerString();

	// Find batteries
	batInput = 0;
	batInputMax = 0;
	batOutput = 0;
	batOutputMax = 0;
	batCharge = 0;
	batChargeMax = 0;

	// Add their current values
	foreach (var battery in batteries) {
		batInput += battery.CurrentInput;
		batInputMax += battery.MaxInput;
		batOutput += battery.CurrentOutput;
		batOutputMax += battery.MaxOutput;
		batCharge += battery.CurrentStoredPower;
		batChargeMax += battery.MaxStoredPower;

		if (showBatteryStats && enableTerminalStatistics) {
			string status = "";

			if (battery.CurrentStoredPower < battery.MaxStoredPower * 0.99) {
				status = "Draining";
				if (battery.CurrentInput > battery.CurrentOutput) status = "Recharging";
			}

			AddStatusToName(battery, true, status, battery.CurrentStoredPower, battery.MaxStoredPower);
		}
	}

	// Round the values to be nicely readable
	batInputStr = batInput.ToPowerString();
	batInputMaxStr = batInputMax.ToPowerString();
	batOutputStr = batOutput.ToPowerString();
	batOutputMaxStr = batOutputMax.ToPowerString();
	batChargeStr = batCharge.ToPowerString(true);
	batChargeMaxStr = batChargeMax.ToPowerString(true);

	// Find oxygen farms and tanks
	farmEfficiency = 0;
	tankCapacity = 0;
	tankFillLevel = 0;

	foreach (var oxygenFarm in farms) {
		farmEfficiency += oxygenFarm.GetOutput();

		if (showOxygenFarmStats && enableTerminalStatistics) {
			AddStatusToName(oxygenFarm, true, "", oxygenFarm.GetOutput(), 1);
		}
	}

	farmEfficiency = Math.Round(farmEfficiency / farms.Count * 100, 2);

	foreach (var oxygenTank in tanks) {
		tankCapacity += oxygenTank.Capacity;
		tankFillLevel += oxygenTank.Capacity * oxygenTank.FilledRatio;

		if (showOxygenTankStats && enableTerminalStatistics) {
			AddStatusToName(oxygenTank, true, "", oxygenTank.FilledRatio, 1);
		}
	}

	tankCapacityStr = tankCapacity.ToTankVolumeString();
	tankFillLevelStr = tankFillLevel.ToTankVolumeString();
}


void RotationLogicGyro()
{
	if (gyros.Count == 0) return;

	if (gyroReference[0].IsUnderControl) {
		StopAll();
		currentOperation = "Automatic alignment paused.\n";
		currentOperation += "Ship is currently controlled by a player.";
		return;
	}

	// Rotation timeout
	int rotationTimeout = 10;

	// Variables for the current operation
	bool pitching = false;
	bool yawing = false;
	bool rolling = false;
	string direction = "";

	// Get the global output as shorter, local variables
	double output = maxOutputAP;
	double outputMax = maxDetectedOutputAP;
	double outputLast = maxOutputAPLast;

	double speed = maxGyroRPM - (maxGyroRPM - minGyroRPM) * (output / outputMax);
	speed = speed / (Math.PI * 3);

	// Pitch
	// Only move the ship, if the output is 1% below or above the last locked output and it's allowed to rotate
	if (!output.IsWithin(outputLockedPitch - outputLockedPitch * realignPercentageGyro, outputLockedPitch + outputLockedPitch * realignPercentageGyro) && allowPitch && timeSincePitch >= rotationTimeout) {
		// Disallow other axes
		allowYaw = false;
		allowRoll = false;
		outputLockedPitch = 0;

		// Check if the output goes down to reverse the rotation
		if (output < outputLast && directionTimerPitch == 3 && !directionChangedPitch) {
			directionPitch = -directionPitch;
			directionTimerPitch = 0;
			directionChangedPitch = true;
		}

		RotateGyros((float)(directionPitch * speed), 0, 0);

		// Information for current operation
		if (directionPitch == -1) {
			direction = "down";
		} else {
			direction = "up";
		}

		// If the output reached maximum, stop the ship
		if (output < outputLast && directionTimerPitch >= 4) {
			// Stop the gyros and allow other axes
			StopGyros();
			allowYaw = true;
			allowRoll = true;

			outputLockedPitch = output;
			directionChangedPitch = false;
			directionTimerPitch = 0;
			timeSincePitch = 0;
		} else {
			pitching = true;
			directionTimerPitch++;
		}

	} else if (allowPitch) {
		// Stop the gyros and allow other axes
		StopGyros();
		allowYaw = true;
		allowRoll = true;

		// Update directionChanged and directionTimer
		directionChangedPitch = false;
		directionTimerPitch = 0;
		timeSincePitch++;
	} else {
		timeSincePitch++;
	}


	// Yaw
	// Only move the ship, if the output is 1% below or above the last locked output and it's allowed to rotate
	if (!output.IsWithin(outputLockedYaw - outputLockedYaw * realignPercentageGyro, outputLockedYaw + outputLockedYaw * realignPercentageGyro) && allowYaw && timeSinceYaw >= rotationTimeout) {
		// Disallow other axes
		allowPitch = false;
		allowRoll = false;
		outputLockedYaw = 0;

		// Check if the output goes down to reverse the rotation
		if (output < outputLast && directionTimerYaw == 3 && !directionChangedYaw) {
			directionYaw = -directionYaw;
			directionTimerYaw = 0;
			directionChangedYaw = true;
		}

		RotateGyros(0, (float)(directionYaw * speed), 0);

		// Information for current operation
		if (directionYaw == -1) {
			direction = "left";
		} else {
			direction = "right";
		}

		// If the output reached maximum, stop the ship
		if (output < outputLast && directionTimerYaw >= 4) {
			// Stop the gyros and allow other axes
			StopGyros();
			allowPitch = true;
			allowRoll = true;

			outputLockedYaw = output;
			directionChangedYaw = false;
			directionTimerYaw = 0;
			timeSinceYaw = 0;
		} else {
			yawing = true;
			directionTimerYaw++;
		}

	} else if (allowYaw) {
		// Stop the gyros and allow other axes
		StopGyros();
		allowPitch = true;
		allowRoll = true;

		// Update directionChanged and directionTimer
		directionChangedYaw = false;
		directionTimerYaw = 0;
		timeSinceYaw++;
	} else {
		timeSinceYaw++;
	}


	// Roll
	// Only move the ship, if the output is 1% below or above the last locked output and it's allowed to rotate
	if (!output.IsWithin(outputLockedRoll - outputLockedRoll * realignPercentageGyro, outputLockedRoll + outputLockedRoll * realignPercentageGyro) && allowRoll && timeSinceRoll >= rotationTimeout) {
		// Disallow other axes
		allowPitch = false;
		allowYaw = false;
		outputLockedRoll = 0;

		// Check if the output goes down to reverse the rotation
		if (output < outputLast && directionTimerRoll == 3 && !directionChangedRoll) {
			directionRoll = -directionRoll;
			directionTimerRoll = 0;
			directionChangedRoll = true;
		}

		RotateGyros(0, 0, (float)(directionRoll * speed));

		// Information for current operation
		if (directionRoll == -1) {
			direction = "left";
		} else {
			direction = "right";
		}

		// If the output reached maximum, stop the ship
		if (output < outputLast && directionTimerRoll >= 4) {
			// Stop the gyros and allow other axes
			StopGyros();
			allowPitch = true;
			allowYaw = true;

			outputLockedRoll = output;
			directionChangedRoll = false;
			directionTimerRoll = 0;
			timeSinceRoll = 0;
		} else {
			rolling = true;
			directionTimerRoll++;
		}

	} else if (allowRoll) {
		// Stop the gyros and allow other axes
		StopGyros();
		allowPitch = true;
		allowYaw = true;

		// Update directionChanged and directionTimer
		directionChangedRoll = false;
		directionTimerRoll = 0;
		timeSinceRoll++;
	} else {
		timeSinceRoll++;
	}

	// Create information about the movement
	if (!pitching && !yawing && !rolling) {
		currentOperation = "Aligned.";
	} else if (pitching) {
		currentOperation = "Aligning by pitching the ship " + direction + "..";
	} else if (yawing) {
		currentOperation = "Aligning by yawing the ship " + direction + "..";
	} else if (rolling) {
		currentOperation = "Aligning by rolling the ship " + direction + "..";
	}
}


void RotationLogic()
{
	// If output is less than nightPercentage of max detected output, it's night time
	if (maxOutputAP < maxDetectedOutputAP * nightPercentage && nightModeTimer >= 30) {
		currentOperation = "Night Mode.";
		nightModeActive = true;

		// Rotate the panels to the base angle or stop them
		if (rotateToSunrise && !sunrisePosReached) {
			if (manualAngle) {
				sunrisePosReached = RotateAll("both", manualAngleHorizontal, manualAngleVertical);
			} else {
				sunrisePosReached = RotateAll("sunrise");
			}

			if (sunrisePosReached) {
				foreach (var rotor in rotors) {
					WriteCustomData(rotor, "firstLockOf", 1);
					WriteCustomData(rotor, "rotationDone", 0);
				}
			}
		} else {
			StopAll();
		}

		// If output is measured, start rotating
	} else {
		// Check the night mode setting and reset the timer, if it was night mode before
		if (nightModeActive) {
			nightModeActive = false;
			nightModeTimer = 0;

			// Reset max output of every rotor for a nicer start in the day
			foreach (var rotor in rotors) {
				WriteCustomData(rotor, "outputMaxDayBefore", ReadCustomData(rotor, "outputMax"));
				WriteCustomData(rotor, "outputMax", 0);
			}
		} else if (nightModeTimer > 172800) {
			// Fail safe if no night was measured in 48 hours
			nightModeTimer = 0;
		} else {
			nightModeTimer++;
		}
		sunrisePosReached = false;
		rotateAllInit = true;

		// Rotation timeout
		int rotationTimeout = 10;

		if (maxOutputAP < maxDetectedOutputAP * 0.5) {
			rotationTimeout = 30;
		}

		// Counter variables for the currently moving rotors
		int vRotorMoving = 0;
		int hRotorMoving = 0;

		// Vertical rotors
		foreach (var vRotor in vRotors) {
			double output = ReadCustomData(vRotor, "output");
			double outputLast = ReadCustomData(vRotor, "outputLast");
			double outputLocked = ReadCustomData(vRotor, "outputLocked");
			double outputMax = ReadCustomData(vRotor, "outputMax");
			double direction = ReadCustomData(vRotor, "direction");
			double directionChanged = ReadCustomData(vRotor, "directionChanged");
			double directionTimer = ReadCustomData(vRotor, "directionTimer");
			double allowRotation = ReadCustomData(vRotor, "allowRotation");
			double timeSinceRotation = ReadCustomData(vRotor, "timeSinceRotation");
			bool forceStop = false;

			// Skip this rotor, if it's not allowed to rotate
			if (allowRotation == 0 || timeSinceRotation < rotationTimeout) {
				Stop(vRotor);
				WriteCustomData(vRotor, "allowRotation", 1);
				WriteCustomData(vRotor, "timeSinceRotation", timeSinceRotation + 1);
				continue;
			}

			// Only move the rotor, if the output is outside the limits
			if (!output.IsWithin(outputLocked - outputLocked * realginPercentageRotor, outputLocked + outputLocked * realginPercentageRotor)) {
				// Disallow rotation for the hRotors on the of the vRotor
				SetAllowRotationH(vRotor, false);
				outputLocked = 0;

				// Check if the output goes down to reverse the rotation
				if (output < outputLast && directionTimer == 3 && directionChanged == 0) {
					direction = -direction;
					directionTimer = 0;
					directionChanged = 1;
				}

				// If the rotor has limits and reached it's maximum or minimum angle, reverse the rotation
				if ((vRotor.LowerLimitDeg != float.MinValue || vRotor.UpperLimitDeg != float.MaxValue) && directionTimer >= 5) {
					double vRotorAngle = GetAngle(vRotor);
					float vRotorLL = (float)Math.Round(vRotor.LowerLimitDeg);
					float vRotorUL = (float)Math.Round(vRotor.UpperLimitDeg);

					if (vRotorAngle == vRotorLL || vRotorAngle == 360 + vRotorLL || vRotorAngle == vRotorUL || vRotorAngle == 360 + vRotorUL) {
						if (output < outputLast && directionChanged == 0) {
							direction = -direction;
							directionTimer = 0;
							directionChanged = 1;
						} else {
							forceStop = true;
						}
					}
				}

				// Rotate the rotor with a speed between rotorMinSpeed and rotorMaxSpeed based on current output
				Rotate(vRotor, direction, (float)(rotorMaxSpeed - rotorMaxSpeed * (output / outputMax) + rotorMinSpeed));

				// If the output reached maximum or is zero, stop the rotor
				if (output < outputLast && directionTimer >= 4 || output == 0 || forceStop) {
					// Stop the rotor and allow the hRotor to rotate
					Stop(vRotor);

					// If this is the first lock of the day and the output is above 90% of the max output of the day before, store the sunrise angle
					if (ReadCustomData(vRotor, "firstLockOfDay") == 1) {
						if (output > ReadCustomData(vRotor, "outputMaxDayBefore") * 0.9) {
							WriteCustomData(vRotor, "firstLockOfDay", 0);
							WriteCustomData(vRotor, "sunriseAngle", GetAngle(vRotor));
						}
					}

					outputLocked = output;
					directionChanged = 0;
					directionTimer = 0;
					timeSinceRotation = 0;
				} else {
					vRotorMoving++;
					directionTimer++;
				}

				// Update custom data
				WriteCustomData(vRotor, "outputLocked", outputLocked);
				WriteCustomData(vRotor, "direction", direction);
				WriteCustomData(vRotor, "directionChanged", directionChanged);
				WriteCustomData(vRotor, "directionTimer", directionTimer);
				WriteCustomData(vRotor, "timeSinceRotation", timeSinceRotation);
			} else {
				// Stop the rotor and allow the hRotor to rotate
				Stop(vRotor);

				// Update custom data
				WriteCustomData(vRotor, "directionChanged", directionChanged);
				WriteCustomData(vRotor, "directionTimer", directionTimer);
			}
		}

		// Horizontal rotors
		foreach (var hRotor in hRotors) {
			double output = ReadCustomData(hRotor, "output");
			double outputLast = ReadCustomData(hRotor, "outputLast");
			double outputLocked = ReadCustomData(hRotor, "outputLocked");
			double outputMax = ReadCustomData(hRotor, "outputMax");
			double direction = ReadCustomData(hRotor, "direction");
			double directionChanged = ReadCustomData(hRotor, "directionChanged");
			double directionTimer = ReadCustomData(hRotor, "directionTimer");
			double allowRotation = ReadCustomData(hRotor, "allowRotation");
			double timeSinceRotation = ReadCustomData(hRotor, "timeSinceRotation");
			bool forceStop = false;

			// Skip this rotor, if it's not allowed to rotate
			if (allowRotation == 0 || timeSinceRotation < rotationTimeout) {
				Stop(hRotor);
				WriteCustomData(hRotor, "allowRotation", 1);
				WriteCustomData(hRotor, "timeSinceRotation", timeSinceRotation + 1);
				continue;
			}

			// Only move the rotor, if the output is outside the limits
			if (!output.IsWithin(outputLocked - outputLocked * realginPercentageRotor, outputLocked + outputLocked * realginPercentageRotor)) {
				// Disallow rotation for the vRotor below the hRotor
				SetAllowRotationV(hRotor, false);
				outputLocked = 0;

				// Check if the output goes down to reverse the rotation
				if (output < outputLast && directionTimer == 3 && directionChanged == 0) {
					direction = -direction;
					directionTimer = 0;
					directionChanged = 1;
				}

				// If the rotor has limits and reached it's maximum or minimum angle, reverse the rotation
				if ((hRotor.LowerLimitDeg != float.MinValue || hRotor.UpperLimitDeg != float.MaxValue) && directionTimer >= 5) {
					double hRotorAngle = GetAngle(hRotor);
					float hRotorLL = (float)Math.Round(hRotor.LowerLimitDeg);
					float hRotorUL = (float)Math.Round(hRotor.UpperLimitDeg);

					if (hRotorAngle == hRotorLL || hRotorAngle == 360 + hRotorLL || hRotorAngle == hRotorUL || hRotorAngle == 360 + hRotorUL) {
						if (output < outputLast && directionChanged == 0) {
							direction = -direction;
							directionTimer = 0;
							directionChanged = 1;
						} else {
							forceStop = true;
						}
					}
				}

				// Rotate the rotor with a speed between rotorMinSpeed and rotorMaxSpeed based on current output
				Rotate(hRotor, direction, (float)(rotorMaxSpeed - rotorMaxSpeed * (output / outputMax) + rotorMinSpeed));

				// If the output reached maximum or is zero, force lock
				if (output < outputLast && directionTimer >= 4 || output == 0 || forceStop) {
					// Stop the rotor
					Stop(hRotor);

					// If this is the first lock of the day and the output is above 90% of the max output of the day before, store the sunrise angle
					if (ReadCustomData(hRotor, "firstLockOfDay") == 1) {
						if (output > ReadCustomData(hRotor, "outputMaxDayBefore") * 0.9) {
							WriteCustomData(hRotor, "firstLockOfDay", 0);
							WriteCustomData(hRotor, "sunriseAngle", GetAngle(hRotor));
						}
					}

					outputLocked = output;
					directionChanged = 0;
					directionTimer = 0;
					timeSinceRotation = 0;
				} else {
					hRotorMoving++;
					directionTimer++;
				}

				// Update custom data
				WriteCustomData(hRotor, "outputLocked", outputLocked);
				WriteCustomData(hRotor, "direction", direction);
				WriteCustomData(hRotor, "directionChanged", directionChanged);
				WriteCustomData(hRotor, "directionTimer", directionTimer);
				WriteCustomData(hRotor, "timeSinceRotation", timeSinceRotation);
			} else {
				// Stop the rotor
				Stop(hRotor);

				// Update custom data
				WriteCustomData(hRotor, "directionChanged", 0);
				WriteCustomData(hRotor, "directionTimer", 0);
			}
		}

		// Create information about the moving rotors
		if (vRotorMoving == 0 && hRotorMoving == 0) {
			currentOperation = "Aligned.";
		} else if (vRotorMoving == 0) {
			currentOperation = "Aligning " + hRotorMoving + " horizontal rotors..";
		} else if (hRotorMoving == 0) {
			currentOperation = "Aligning " + vRotorMoving + " vertical rotors..";
		} else {
			currentOperation = "Aligning " + hRotorMoving + " horizontal and " + vRotorMoving + " vertical rotors..";
		}
	}
}


void SetAllowRotationV(IMyMotorStator rotor, bool value)
{
	foreach (var vRotor in vRotors) {
		if (rotor.CubeGrid == vRotor.TopGrid) {
			if (value) {
				WriteCustomData(vRotor, "allowRotation", 1);
			} else {
				Stop(vRotor);
				WriteCustomData(vRotor, "allowRotation", 0);
			}
		}
	}
}


void SetAllowRotationH(IMyMotorStator rotor, bool value)
{
	foreach (var hRotor in hRotors) {
		if (rotor.TopGrid == hRotor.CubeGrid) {
			if (value) {
				WriteCustomData(hRotor, "allowRotation", 1);
			} else {
				Stop(hRotor);
				WriteCustomData(hRotor, "allowRotation", 0);
			}
		}
	}
}


void Rotate(IMyMotorStator rotor, double direction, float speed = rotorMinSpeed)
{
	rotor.RotorLock = false;
	rotor.TargetVelocityRPM = speed * (float)direction;
}


void RotateGyros(double pitch, double yaw, double roll)
{
	Vector3D localRotation = new Vector3D(-pitch, yaw, roll);
	Vector3D relativeRotation = Vector3D.TransformNormal(localRotation, gyroReference[0].WorldMatrix);

	foreach (var gyro in gyros) {
		Vector3D gyroRotation = Vector3D.TransformNormal(relativeRotation, Matrix.Transpose(gyro.WorldMatrix));

		gyro.GyroOverride = true;
		gyro.GyroPower = gyroPower;

		gyro.Pitch = (float)gyroRotation.X;
		gyro.Yaw = (float)gyroRotation.Y;
		gyro.Roll = (float)gyroRotation.Z;
	}
}


void Stop(IMyMotorStator rotor, bool rotationDone = true)
{
	rotor.TargetVelocityRPM = 0f;

	if (rotationDone) {
		WriteCustomData(rotor, "rotationDone", 1);
	} else {
		WriteCustomData(rotor, "rotationDone", 0);
	}

	if (setRotorLockWhenStopped) {
		rotor.RotorLock = true;
	}
}


void StopGyros(bool disableOverride = false)
{
	foreach (var gyro in gyros) {
		gyro.Pitch = 0;
		gyro.Yaw = 0;
		gyro.Roll = 0;

		if (disableOverride) gyro.GyroOverride = false;
	}
}


void StopAll(bool rotationDone = true)
{
	foreach (var rotor in rotors) {
		Stop(rotor, rotationDone);
		WriteCustomData(rotor, "timeSinceRotation", 0);
	}

	StopGyros(true);

	timeSincePitch = 0;
	timeSinceYaw = 0;
	timeSinceRoll = 0;
}


bool RotateToAngle(IMyMotorStator rotor, double targetAngle, bool relativeAngle = true)
{
	double rotorAngle = GetAngle(rotor);
	bool invert = false;

	if (relativeAngle) {
		// Rotor angle correction
		if (rotor.CustomName.IndexOf("[90]") >= 0) {
			targetAngle += 90;
		} else if (rotor.CustomName.IndexOf("[180]") >= 0) {
			targetAngle += 180;
		} else if (rotor.CustomName.IndexOf("[270]") >= 0) {
			targetAngle += 270;
		}
		if (targetAngle >= 360) targetAngle -= 360;

		// Invert rotorangle if rotor is facing forward, up or right
		if (rotor.Orientation.Up.ToString() == "Down") {
			invert = true;
		} else if (rotor.Orientation.Up.ToString() == "Backward") {
			invert = true;
		} else if (rotor.Orientation.Up.ToString() == "Left") {
			invert = true;
		}
	}

	// If rotor has limits, limit the targetAngle too
	if (rotor.LowerLimitDeg != float.MinValue || rotor.UpperLimitDeg != float.MaxValue) {
		if (invert) targetAngle = -targetAngle;
		if (targetAngle > rotor.UpperLimitDeg) {
			targetAngle = Math.Floor(rotor.UpperLimitDeg);
		}
		if (targetAngle < rotor.LowerLimitDeg) {
			targetAngle = Math.Ceiling(rotor.LowerLimitDeg);
		}
	} else {
		if (invert) targetAngle = 360 - targetAngle;
	}

	// If angle is correct, stop the rotor
	if (rotorAngle.IsWithin(targetAngle - 1, targetAngle + 1) || rotorAngle.IsWithin(360 + targetAngle - 1, 360 + targetAngle + 1)) {
		Stop(rotor);

		return true;

		// Else move the rotor
	} else {
		// Figure out the shortest rotation direction
		int direction = rotorAngle < targetAngle ? 1 : -1;

		if (rotorAngle <= 90 && targetAngle >= 270) {
			direction = -1;
		}
		if (rotorAngle >= 270 && targetAngle <= 90) {
			direction = 1;
		}

		// Move rotor
		Single speed = Math.Abs(rotorAngle - targetAngle) > 15 ? rotorMaxSpeed : rotorMinSpeed;
		if (Math.Abs(rotorAngle - targetAngle) < 3) speed = 0.1f;
		Rotate(rotor, direction, speed);

		return false;
	}
}


bool RotateAll(string mode, double horizontalAngle = 0, double verticalAngle = 0)
{
	// Return variable
	bool rotationDone = true;

	// Counter variables for the currently moving rotors
	int vRotorMoving = 0;
	int hRotorMoving = 0;

	// Stop all rotors when initiating the rotation
	if (rotateAllInit) {
		rotateAllInit = false;

		StopAll(false);
	}

	// Horizontal rotors
	if (mode != "verticalOnly") {
		foreach (var hRotor in hRotors) {
			// Skip aligned rotors
			if (ReadCustomData(hRotor, "rotationDone") == 1) continue;

			bool relativeAngle = true;
			double targetAngle = horizontalAngle;
			if (mode == "sunrise") {
				targetAngle = ReadCustomData(hRotor, "sunriseAngle");
				relativeAngle = false;
			}

			// Rotate to the target angle and while rotating, create info string
			if (!RotateToAngle(hRotor, targetAngle, relativeAngle)) {
				rotationDone = false;
				hRotorMoving++;

				// Create information
				currentOperationInfo = hRotorMoving + " horizontal rotors are set to " + horizontalAngle + "°";
				if (mode == "sunrise") currentOperationInfo = hRotorMoving + " horizontal rotors are set to sunrise position";
			}
		}
	}

	if (!rotationDone) return false;

	// Vertical rotors
	if (mode != "horizontalOnly") {
		foreach (var vRotor in vRotors) {
			// Skip aligned rotors
			if (ReadCustomData(vRotor, "rotationDone") == 1) continue;

			bool relativeAngle = true;
			double targetAngle = verticalAngle;
			if (mode == "sunrise") {
				targetAngle = ReadCustomData(vRotor, "sunriseAngle");
				relativeAngle = false;
			}

			// Rotate to the target angle and while rotating, create info string
			if (!RotateToAngle(vRotor, targetAngle, relativeAngle)) {
				rotationDone = false;
				vRotorMoving++;

				// Create information
				currentOperationInfo = vRotorMoving + " vertical rotors are set to " + verticalAngle + "°";
				if (mode == "sunrise") currentOperationInfo = vRotorMoving + " vertical rotors are set to sunrise position";
			}
		}
	}

	if (rotationDone) rotateAllInit = true;
	return rotationDone;
}


void Realign()
{
	// Counter variable for the currently moving rotors
	int hRotorMoving = 0;
	int vRotorMoving = 0;

	// Erase the max detected output in the first run
	if (realignTimer == 90) {
		foreach (var rotor in rotors) {
			Stop(rotor, false);

			// Set initial direction
			double initDirection = 1;
			if (rotor.Orientation.Up.ToString() == "Up") {
				initDirection = -1;
			} else if (rotor.Orientation.Up.ToString() == "Forward") {
				initDirection = -1;
			} else if (rotor.Orientation.Up.ToString() == "Right") {
				initDirection = -1;
			}

			WriteCustomData(rotor, "outputMax", ReadCustomData(rotor, "output"));
			WriteCustomData(rotor, "direction", initDirection);
			WriteCustomData(rotor, "directionChanged", 0);
			WriteCustomData(rotor, "directionTimer", 0);
			maxDetectedOutputAP = 0;
		}
	}

	// Rotate the hRotors
	foreach (var hRotor in hRotors) {
		// Skip aligned rotors
		if (ReadCustomData(hRotor, "rotationDone") == 1) continue;

		// Get rotor stats
		double output = ReadCustomData(hRotor, "output");
		double outputLast = ReadCustomData(hRotor, "outputLast");
		double outputMax = ReadCustomData(hRotor, "outputMax");
		double outputMaxAngle = ReadCustomData(hRotor, "outputMaxAngle");
		double direction = ReadCustomData(hRotor, "direction");
		double directionChanged = ReadCustomData(hRotor, "directionChanged");
		double directionTimer = ReadCustomData(hRotor, "directionTimer");

		// If outputMax == 0, set it to 1 in order to prevent division by zero
		if (outputMax == 0) outputMax = 1;

		// Rotate in both directions to find the highest output
		if (directionChanged != 2) {
			hRotorMoving++;

			// Check if the output goes down to reverse the rotation
			if (output < outputLast && directionTimer >= 7 && directionChanged == 0) {
				WriteCustomData(hRotor, "direction", -direction);
				WriteCustomData(hRotor, "directionChanged", 1);
				directionTimer = 0;
			}

			// If the rotor has limits and reached it's maximum or minimum angle, reverse the rotation
			if ((hRotor.LowerLimitDeg != float.MinValue || hRotor.UpperLimitDeg != float.MaxValue) && directionTimer >= 3 && directionChanged == 0) {
				double hRotorAngle = GetAngle(hRotor);
				float hRotorLL = (float)Math.Round(hRotor.LowerLimitDeg);
				float hRotorUL = (float)Math.Round(hRotor.UpperLimitDeg);

				if (hRotorAngle == hRotorLL || hRotorAngle == 360 + hRotorLL || hRotorAngle == hRotorUL || hRotorAngle == 360 + hRotorUL) {
					WriteCustomData(hRotor, "direction", -direction);
					WriteCustomData(hRotor, "directionChanged", 1);
					directionTimer = 0;
				}
			}

			// Rotate the rotor
			Rotate(hRotor, direction, (float)(2.75 - (output / outputMax) * 2));

			// If the output reached maximum or is zero, force lock
			if (output < outputLast && directionTimer >= 7 && directionChanged == 1) {
				// Stop the rotor
				Stop(hRotor, false);
				WriteCustomData(hRotor, "directionChanged", 2);
			} else {
				WriteCustomData(hRotor, "directionTimer", directionTimer + 1);
			}
		} else {
			// After that, rotate to the new found highest output
			if (!RotateToAngle(hRotor, outputMaxAngle, false)) hRotorMoving++;
		}
	}

	if (hRotorMoving != 0) return;

	// Rotate the vRotors
	foreach (var vRotor in vRotors) {
		// Skip aligned rotors
		if (ReadCustomData(vRotor, "rotationDone") == 1) continue;

		// Get rotor stats
		double output = ReadCustomData(vRotor, "output");
		double outputLast = ReadCustomData(vRotor, "outputLast");
		double outputMax = ReadCustomData(vRotor, "outputMax");
		double outputMaxAngle = ReadCustomData(vRotor, "outputMaxAngle");
		double direction = ReadCustomData(vRotor, "direction");
		double directionChanged = ReadCustomData(vRotor, "directionChanged");
		double directionTimer = ReadCustomData(vRotor, "directionTimer");

		// If outputMax == 0, set it to 1 in order to prevent division by zero
		if (outputMax == 0) outputMax = 1;

		// Rotate in both directions to find the highest output
		if (directionChanged != 2) {
			vRotorMoving++;

			// Check if the output goes down to reverse the rotation
			if (output < outputLast && directionTimer >= 7 && directionChanged == 0) {
				WriteCustomData(vRotor, "direction", -direction);
				WriteCustomData(vRotor, "directionChanged", 1);
				directionTimer = 0;
			}

			// If the rotor has limits and reached it's maximum or minimum angle, reverse the rotation
			if ((vRotor.LowerLimitDeg != float.MinValue || vRotor.UpperLimitDeg != float.MaxValue) && directionTimer >= 3 && directionChanged == 0) {
				double vRotorAngle = GetAngle(vRotor);
				float vRotorLL = (float)Math.Round(vRotor.LowerLimitDeg);
				float vRotorUL = (float)Math.Round(vRotor.UpperLimitDeg);

				if (vRotorAngle == vRotorLL || vRotorAngle == 360 + vRotorLL || vRotorAngle == vRotorUL || vRotorAngle == 360 + vRotorUL) {
					WriteCustomData(vRotor, "direction", -direction);
					WriteCustomData(vRotor, "directionChanged", 1);
					directionTimer = 0;
				}
			}

			// Rotate the rotor
			Rotate(vRotor, direction, (float)(2.75 - (output / outputMax) * 2));

			// If the output reached maximum or is zero, force lock
			if (output < outputLast && directionTimer >= 7 && directionChanged == 1) {
				// Stop the rotor
				Stop(vRotor, false);
				WriteCustomData(vRotor, "directionChanged", 2);
			} else {
				WriteCustomData(vRotor, "directionTimer", directionTimer + 1);
			}
		} else {
			// After that, rotate to the new found highest output
			if (!RotateToAngle(vRotor, outputMaxAngle, false)) vRotorMoving++;
		}
	}

	// End realigning when all rotors are stopped
	if (hRotorMoving == 0 && vRotorMoving == 0) {
		realignTimer = 0;
	}
}


void AddStatusToName(IMyTerminalBlock block, bool addStatus = true, string status = "", double currentValue = 0, double maxValue = 0)
{
	string newName = block.CustomName;
	string oldStatus = System.Text.RegularExpressions.Regex.Match(block.CustomName, @" *\(\d+\.*\d*%.*\)").Value;
	if (oldStatus != String.Empty) {
		newName = block.CustomName.Replace(oldStatus, "");
	}

	if (addStatus) {
		// Add percentages
		newName += " (" + currentValue.ToPercentString(maxValue);

		// Add status
		if (status != "") {
			newName += ", " + status;
		}

		// Add closing bracket
		newName += ")";
	}

	// Rename the block if the name has changed
	if (newName != block.CustomName) {
		block.CustomName = newName;
	}
}


double GetAngle(IMyMotorStator rotor)
{
	return Math.Round(rotor.Angle * 180 / Math.PI);
}


string CreateInformation(bool shortUnderline = false, float fontSize = 0.65f, int charsPerLine = 26, bool addCurrentOperation = true, bool addSolarStats = true, bool addBatteryStats = true, bool addOxygenStats = true, bool addLocationTime = true)
{
	string info = "";
	bool infoShown = false;

	// Terminal / LCD information string
	info = "Isy's Solar Alignment Script " + workingIndicator[workingCounter] + "\n";
	info += "=======================";
	if (!shortUnderline) info += "=======";
	info += "\n\n";

	// If any error occurs, show it
	if (error != null) {
		info += "Error!\n";
		info += error + "\n\n";
		info += "Script stopped!\n\n";

		return info;
	}

	// Add warning message for minor errors
	if (warning != null) {
		info += "Warning!\n";
		info += warning + "\n\n";
		infoShown = true;
	}

	// Current Operation
	if (addCurrentOperation) {
		if (currentOperationInfo != null) currentOperation += "\n" + currentOperationInfo;
		info += currentOperation;
		info += '\n'.Repeat(3 - currentOperation.Count(n => n == '\n'));
		currentOperationInfo = null;
		infoShown = true;
	}

	// Solar Panels
	if (addSolarStats) {
		info += "Statistics for " + solarPanels.Count + " Solar Panels:\n";
		info += CreateBar(fontSize, charsPerLine, "Efficiency", maxOutputAP, maxDetectedOutputAP, maxOutputAPStr, maxDetectedOutputAPStr);
		info += CreateBar(fontSize, charsPerLine, "Output", currentOutputAP, maxOutputAP, currentOutputAPStr, maxOutputAPStr) + "\n\n";
		infoShown = true;
	}

	// Batteries
	if (batteries.Count > 0 && addBatteryStats) {
		info += "Statistics for " + batteries.Count + " Batteries:\n";
		info += CreateBar(fontSize, charsPerLine, "Input", batInput, batInputMax, batInputStr, batInputMaxStr);
		info += CreateBar(fontSize, charsPerLine, "Output", batOutput, batOutputMax, batOutputStr, batOutputMaxStr);
		info += CreateBar(fontSize, charsPerLine, "Charge", batCharge, batChargeMax, batChargeStr, batChargeMaxStr) + "\n\n";
		infoShown = true;
	}

	// Oxygen Farms / Tanks
	if (addOxygenStats && (farms.Count > 0 || tanks.Count > 0)) {
		info += "Statistics for Oxygen:\n";
		if (farms.Count > 0) {
			info += CreateBar(fontSize, charsPerLine, farms.Count + " Farms", farmEfficiency, 100);
		}

		if (tanks.Count > 0) {
			info += CreateBar(fontSize, charsPerLine, tanks.Count + " Tanks", tankFillLevel, tankCapacity, tankFillLevelStr, tankCapacityStr);
		}
		info += "\n\n";
		infoShown = true;
	}

	// Location time
	if (addLocationTime && !useGyroMode) {
		string inaccurate = "";
		string inaccurateLegend = "";
		string duskDawnTimer = "";

		if (dayLength < dayTimer) {
			inaccurateLegend = " inaccurate";
			inaccurate = "*";
		} else if (dayLength == dayLengthDefault || sunSet == sunSetDefault) {
			inaccurateLegend = " inaccurate, still calculating";
			inaccurate = "*";
		}

		if (dayTimer < sunSet && inaccurate == "") {
			duskDawnTimer = " / Dusk in: " + ConvertSecondsToTime(sunSet - dayTimer);
		} else if (dayTimer > sunSet && inaccurate == "") {
			duskDawnTimer = " / Dawn in: " + ConvertSecondsToTime(dayLength - dayTimer);
		}

		info += "Time of your location:\n";
		info += "Time: " + GetTimeString(dayTimer) + duskDawnTimer + inaccurate + "\n";
		info += "Dawn: " + GetTimeString(dayLength) + " / Daylength: " + ConvertSecondsToTime(sunSet) + inaccurate + "\n";
		info += "Dusk: " + GetTimeString(sunSet) + " / Nightlength: " + ConvertSecondsToTime(dayLength - sunSet) + inaccurate + "\n";

		if (inaccurate != "") {
			info += inaccurate + inaccurateLegend;
		}
		infoShown = true;
	}

	if (!infoShown) {
		info += "-- No informations to show --";
	}

	return info;
}


void WriteLCD(string text = null)
{
	if (lcds.Count == 0) return;

	foreach (var lcd in lcds) {
		// Get the wanted statistics to show
		bool addCurrentOperation = ReadCustomDataLCD(lcd, "showCurrentOperation");
		bool addSolarStats = ReadCustomDataLCD(lcd, "showSolarStats");
		bool addBatteryStats = ReadCustomDataLCD(lcd, "showBatteryStats");
		bool addOxygenStats = ReadCustomDataLCD(lcd, "showOxygenStats");
		bool addLocationTime = ReadCustomDataLCD(lcd, "showLocationTime");

		// Get the font size
		float fontSize = lcd.FontSize;
		int charsPerline = 26;
		if (lcd.BlockDefinition.SubtypeName.Contains("Wide")) charsPerline = 52;

		string info = "";

		// Create the text
		if (text != null) {
			info = text;
		} else {
			info = CreateInformation(false, fontSize, charsPerline, addCurrentOperation, addSolarStats, addBatteryStats, addOxygenStats, addLocationTime);
		}
		string lcdText = CreateScrollingText(fontSize, info, lcd);

		// Print contents to its public text
		lcd.WritePublicTitle("Isy's Solar Alignment Script");
		lcd.WritePublicText(lcdText, false);
		lcd.Font = "Monospace";
		lcd.ShowPublicTextOnScreen();
	}
}


void WriteCornerLCD()
{
	if (cornerLcds.Count == 0) return;

	foreach (var lcd in cornerLcds) {
		string customData = lcd.CustomData.ToLower().Replace(" ", "").TrimEnd('\n');
		if (customData == "time" || customData == "realtime") {
			if (lcd.Font == "Monospace") lcd.Font = "Debug";
			lcd.FontSize = 2.5f;
		} else {
			lcd.Font = "Monospace";
			lcd.FontSize = 0.5f;
		}

		float fontSize = lcd.FontSize;
		int charsPerLine = 26;
		int lcdWidth = (int)(charsPerLine / fontSize);

		// Prepare the text based on the custom data of the panel
		string text = "";

		if (customData == "time") {
			text = GetTimeString(dayTimer);
		} else if (customData == "realtime") {
			text = DateTime.Now.ToString(@"HH:mm:ss");
		} else if (customData == "solar") {
			text = "Statistics for " + solarPanels.Count + " Solar Panels:\n";
			text += CreateBar(fontSize, charsPerLine, "Efficiency", maxOutputAP, maxDetectedOutputAP, maxOutputAPStr, maxDetectedOutputAPStr, singleLine: true);
			text += CreateBar(fontSize, charsPerLine, "Output    ", currentOutputAP, maxOutputAP, currentOutputAPStr, maxOutputAPStr, singleLine: true);
		} else if (customData == "battery") {
			text = "Statistics for " + batteries.Count + " Batteries:\n";
			text += CreateBar(fontSize, charsPerLine, "Input ", batInput, batInputMax, batInputStr, batInputMaxStr, singleLine: true);
			text += CreateBar(fontSize, charsPerLine, "Output", batOutput, batOutputMax, batOutputStr, batOutputMaxStr, singleLine: true);
			text += CreateBar(fontSize, charsPerLine, "Charge", batCharge, batChargeMax, batChargeStr, batChargeMaxStr, singleLine: true);
		} else if (customData == "oxygen") {
			if (farms.Count > 0) {
				text += "Statistics for " + farms.Count + " Oxygen Farms:\n";
				text += CreateBar(fontSize, charsPerLine, "Efficiency", farmEfficiency, 100, singleLine: true) + "\n";
			}

			if (tanks.Count > 0) {
				text += "Statistics for " + tanks.Count + " Oxygen Tanks:\n";
				text += CreateBar(fontSize, charsPerLine, "Fill Level", tankFillLevel, tankCapacity, tankFillLevelStr, tankCapacityStr, singleLine: true);
			}
		} else {
			text = "Keyword required!\n\nPlease edit the custom data of this LCD\nto set, what should be shown here!";
			customData = "In order to show informations on this LCD, delete this text and leave\none of the following keywords behind:\n\n";
			customData += "solar\nbattery\noxygen\ntime\nrealtime\n\n";
			customData += "If you didn't specify a valid or no keyword, this message will reappear.";
			lcd.CustomData = customData;
		}

		// Print contents to its public text
		lcd.WritePublicText(text, false);
		lcd.WritePublicTitle("Please edit Custom Data!");
		lcd.SetValue<Int64>("alignment", 2);
		lcd.ShowPublicTextOnScreen();
	}
}


void WriteDebugLCD()
{
	// Find the debugLcd
	IMyTextPanel lcd = GridTerminalSystem.GetBlockWithName(debugLcd) as IMyTextPanel;
	if (lcd == null) return;

	// Average counter increase/reset
	if (avgCounter == 99) {
		avgCounter = 0;
	} else {
		avgCounter++;
	}

	// Get the font size
	if (lcd.GetPublicText() == "") lcd.FontSize = 0.5f;
	float fontSize = lcd.FontSize;

	// Create the debug text
	string text = "Solar Alignment Debug\n=====================\n\n";
	text += "Task: " + methodName[execCounter] + "\n";
	text += "Script step: " + execCounter + " / " + (methodName.Length - 1) + "\n\n";

	if (showPerformance) text += performanceText;

	// Blocks
	if (showBlockCounts) {
		text += "Main Grid: " + baseGrid.CustomName + "\n";
		text += "Connected Grids: " + connectedGrids.Count + "\n";
		text += "Rotors: " + rotors.Count + "\n";
		text += "Gyros: " + gyros.Count + "\n";
		text += "Solar Panels: " + solarPanels.Count + "\n";
		text += "Oxygen Farms: " + farms.Count + "\n";
		text += "Oxygen Tanks: " + tanks.Count + "\n";
		text += "Batteries: " + batteries.Count + "\n";
		text += "Reactors: " + reactors.Count + "\n";
		text += "LCDs: " + lcds.Count + "\n";
		text += "Corner LCDs: " + cornerLcds.Count + "\n";
		text += "Lights: " + lights.Count + "\n";
		text += "Spotlights: " + spotlights.Count + "\n";
		text += "Timer Blocks: " + timers.Length + "\n";
	}

	// Print contents to its public text
	lcd.WritePublicTitle("Solar Alignment Debug");
	lcd.WritePublicText(CreateScrollingText(fontSize, text, lcd), false);
	lcd.Font = "Monospace";
	lcd.ShowPublicTextOnScreen();
}


void TimeCalculation()
{
	// Continous day timer in seconds
	dayTimer += 1;
	safetyTimer += 1;

	// Failsafe for day timer if no day / night cycle could be measured after 48 hours
	if (dayTimer > 172800) {
		dayTimer = 0;
		safetyTimer = 0;
	}

	double nightOutput = maxDetectedOutputAP * nightTimePercentage;

	// Detect sunset
	if (maxOutputAP < nightOutput && maxOutputAPLast >= nightOutput && safetyTimer > 300) {
		sunSet = dayTimer;
		safetyTimer = 0;
	}

	// Reset day timer (sunrise)
	if (maxOutputAP > nightOutput && maxOutputAPLast <= nightOutput && safetyTimer > 300) {
		if (sunSet != sunSetDefault) {
			dayLength = dayTimer;
		}
		dayTimer = 0;
		safetyTimer = 0;
	}

	// Correction of daylength in case sunset is higher from an old run
	if (sunSet > dayLength) {
		dayLength = sunSet * 2;
	}
}


string GetTimeString(double timeToEvaluate, bool returnHour = false)
{
	string timeString = "";

	// Mod the timeToEvaluate by dayLength in order to avoid unrealistic times
	timeToEvaluate = timeToEvaluate % dayLength;

	// Calculate Midnight
	double midNight = sunSet + (dayLength - sunSet) / 2D;

	// Calculate Time
	double hourLength = dayLength / 24D;
	double time;
	if (timeToEvaluate < midNight) {
		time = (timeToEvaluate + (dayLength - midNight)) / hourLength;
	} else {
		time = (timeToEvaluate - midNight) / hourLength;
	}

	double timeHour = Math.Floor(time);
	double timeMinute = Math.Floor((time % 1 * 100) * 0.6);
	string timeHourStr = timeHour.ToString("00");
	string timeMinuteStr = timeMinute.ToString("00");

	timeString = timeHourStr + ":" + timeMinuteStr;

	if (returnHour) {
		return timeHour.ToString();
	} else {
		return timeString;
	}
}


string ConvertSecondsToTime(int seconds)
{
	string result = "";

	TimeSpan ts = TimeSpan.FromSeconds(seconds);
	result = ts.ToString(@"hh\:mm\:ss");

	return result;
}


void ReactorFallback()
{
	if (reactors.Count == 0) return;

	double turnOn = turnOnAtPercent % 100 / 100;
	double turnOff = turnOffAtPercent % 100 / 100;
	double overload = overloadPercentage % 100 / 100;

	// Activate on low battery charge
	if (lastActivation == "lowBat" || lastActivation == "") {
		if (activateOnLowBattery && batCharge < batChargeMax * turnOn) {
			enableReactors = true;
			lastActivation = "lowBat";
			currentOperationInfo = "Reactors active: Low battery charge!";
		} else if (activateOnLowBattery && batCharge > batChargeMax * turnOff) {
			enableReactors = false;
			lastActivation = "";
		}
	}

	// Activate on overload
	if (lastActivation == "overload" || lastActivation == "") {
		if (activateOnOverload && batOutput + currentOutputAP > (batOutputMax + maxOutputAP) * overload) {
			enableReactors = true;
			lastActivation = "overload";
			currentOperationInfo = "Reactors active: Overload!";
		} else {
			enableReactors = false;
			lastActivation = "";
		}
	}

	// Set the reactor state
	foreach (var reactor in reactors) {
		if (enableReactors) {
			reactor.Enabled = true;
		} else {
			reactor.Enabled = false;
		}
	}
}


void LightManagement()
{
	if (lights.Count == 0 && spotlights.Count == 0) return;

	int hour = 0;
	int.TryParse(GetTimeString(dayTimer, true), out hour);
	bool lightState = true;

	// Figure out if the lights should be on or off
	if (!simpleMode) {
		if (dayTimer != dayLength && hour >= lightOffHour && hour < lightOnHour) {
			lightState = false;
		} else if (dayTimer == dayLength && maxOutputAP > maxDetectedOutputAP * nightTimePercentage) {
			lightState = false;
		}
	} else {
		if (maxOutputAP > maxDetectedOutputAP * (simpleThreshold % 100) / 100) lightState = false;
	}

	// Toggle all interior lights
	foreach (var light in lights) {
		light.Enabled = lightState;
	}

	// Toggle all spotlights
	foreach (var spotLight in spotlights) {
		spotLight.Enabled = lightState;
	}
}


void TriggerExternalTimerBlock()
{
	// Error management
	if (events.Length == 0) {
		CreateWarning("No events for triggering specified!");
	} else if (timers.Length == 0) {
		CreateWarning("No timers for triggering specified!");
	} else if (events.Length != timers.Length) {
		CreateWarning("Every event needs a timer block name!\nFound " + events.Length + " events and " + timers.Length + " timers.");
	} else {
		int timerToTrigger = -1;
		string triggerEvent = "";
		int seconds;

		// Cycle through each entry in events and check if the current conditions match the entry
		for (int i = 0; i <= events.Length - 1; i++) {
			if (events[i] == "sunrise" && dayTimer == 0) {
				timerToTrigger = i;
				triggerEvent = "sunrise";
			} else if (events[i] == "sunset" && dayTimer == sunSet) {
				timerToTrigger = i;
				triggerEvent = "sunset";
			} else if (int.TryParse(events[i], out seconds) == true && dayTimer % seconds == 0) {
				timerToTrigger = i;
				triggerEvent = seconds + " seconds";
			} else if (GetTimeString(dayTimer) == events[i]) {
				timerToTrigger = i;
				triggerEvent = events[i];
			}
		}

		// Cycle through all the timers and see if everything is set up correctly
		foreach (var item in timers) {
			var timer = GridTerminalSystem.GetBlockWithName(item) as IMyTimerBlock;
			if (timer == null) {
				CreateWarning("External timer block not found:\n'" + timer.CustomName + "'");
			} else {
				if (timer.GetOwnerFactionTag() != Me.GetOwnerFactionTag()) {
					CreateWarning("'" + timer.CustomName + "' has a different owner / faction!\nAll blocks should have the same owner / faction!");
				}
				if (timer.Enabled == false) {
					CreateWarning("'" + timer.CustomName + "' is turned off!\nTurn it on in order to be used by the script!");
				}
			}
		}

		// Trigger the timer block if a event matches the current conditions
		if (timerToTrigger >= 0) {
			// Find the timer block
			var timer = GridTerminalSystem.GetBlockWithName(timers[timerToTrigger]) as IMyTimerBlock;

			if (timer != null) {
				timer.ApplyAction("Start");
				currentOperation = "External timer triggered! Reason: " + triggerEvent;
			}
		}
	}
}


void CreateError(string text)
{
	StopAll();
	if (error == null) {
		error = text;
		errorCount++;
	}
}


void CreateWarning(string text)
{
	if (warning == null) {
		warning = text;
		warningCount++;
	}
}


void RemoveTerminalStatistics()
{
	// Solar Panels
	foreach (var solarPanel in solarPanels) {
		solarPanel.CustomData = "";
		AddStatusToName(solarPanel, false);
	}

	// Batteries
	foreach (var battery in batteries) {
		AddStatusToName(battery, false);
	}

	// Oxygen Farms
	foreach (var oxygenFarm in farms) {
		AddStatusToName(oxygenFarm, false);
	}

	// Oxygen Tanks
	foreach (var oxygenTank in tanks) {
		AddStatusToName(oxygenTank, false);
	}
}


void Load()
{
	// Load variables out of the programmable block's custom data field
	if (Me.CustomData.Length > 0) {
		var data = Me.CustomData.Split('\n');

		foreach (var line in data) {
			var entry = line.Split('=');
			if (entry.Length != 2) continue;

			if (entry[0] == "dayTimer") {
				int.TryParse(entry[1], out dayTimer);
			} else if (entry[0] == "dayLength") {
				int.TryParse(entry[1], out dayLength);
			} else if (entry[0] == "sunSet") {
				int.TryParse(entry[1], out sunSet);
			} else if (entry[0] == "outputLast") {
				double.TryParse(entry[1], out maxOutputAPLast);
			} else if (entry[0] == "maxDetectedOutput") {
				double.TryParse(entry[1], out maxDetectedOutputAP);
			} else if (entry[0] == "solarPanelsCount") {
				int.TryParse(entry[1], out solarPanelsCount);
			} else if (entry[0] == "oxygenFarmsCount") {
				int.TryParse(entry[1], out farmCount);
			} else if (entry[0] == "action") {
				action = entry[1];
			}
		}

		if (action == "paused") pause = true;
	}
}


public void Save()
{
	// Save variables into the programmable block's custom data field
	string customData = "";

	customData += "dayTimer=" + dayTimer + "\n";
	customData += "dayLength=" + dayLength + "\n";
	customData += "sunSet=" + sunSet + "\n";
	customData += "outputLast=" + maxOutputAPLast + "\n";
	customData += "maxDetectedOutput=" + maxDetectedOutputAP + "\n";
	customData += "solarPanelsCount=" + solarPanels.Count + "\n";
	customData += "oxygenFarmsCount=" + farms.Count + "\n";
	customData += "action=" + action;

	Me.CustomData = customData;
}

string CreateBar(double fontSize, int charsPerLine, string heading, double value, double valueMax, string valueStr = null, string valueMaxStr = null, bool noBar = false, bool singleLine = false)
{
	string current = value.ToString();
	string max = valueMax.ToString();

	if (valueStr != null) {
		current = valueStr;
	}

	if (valueMaxStr != null) {
		max = valueMaxStr;
	}

	string percent = value.ToPercentString(valueMax);
	percent = ' '.Repeat(6 - percent.Length) + percent;
	string values = current + " / " + max;
	double level = value / valueMax >= 1 ? 1 : value / valueMax;
	int lcdWidth = (int)(charsPerLine / fontSize);

	StringBuilder firstLine = new StringBuilder(heading + " ");
	StringBuilder secondLine = new StringBuilder();

	if (singleLine) {
		if (fontSize <= 0.5 || (fontSize <= 1 && charsPerLine == 52)) {
			// Create the bar for wide LCDs
			firstLine.Append(' '.Repeat(9 - current.Length) + current);
			firstLine.Append(" / " + max + ' '.Repeat(9 - max.Length));

			int dotStart = firstLine.Length + 1;
			int dotsAmount = lcdWidth - firstLine.Length - percent.Length - 2;
			int fillLevel = (int)Math.Ceiling(dotsAmount * level);

			firstLine.Append("[" + 'I'.Repeat(fillLevel) + '.'.Repeat(dotsAmount - fillLevel) + "]");
			firstLine.Append(percent + "\n");
		} else {
			// Create the bar for regular and corner LCDs
			int dotsAmount = lcdWidth - firstLine.Length - percent.Length - 2;
			int fillLevel = (int)Math.Ceiling(dotsAmount * level);

			firstLine.Append("[" + 'I'.Repeat(fillLevel) + '.'.Repeat(dotsAmount - fillLevel) + "]");
			firstLine.Append(percent + "\n");
		}

		return firstLine.ToString();
	} else {
		if (fontSize <= 0.6 || (fontSize <= 1 && charsPerLine == 52)) {
			firstLine.Append(' '.Repeat(lcdWidth / 2 - (firstLine.Length + current.Length)));
			firstLine.Append(current + " / " + max);
			firstLine.Append(' '.Repeat(lcdWidth - (firstLine.Length + percent.Length)));
			firstLine.Append(percent + "\n");

			if (!noBar) {
				int dotsAmount = lcdWidth - 2;
				int fillLevel = (int)Math.Ceiling(dotsAmount * level);
				secondLine = new StringBuilder("[" + 'I'.Repeat(fillLevel) + '.'.Repeat(dotsAmount - fillLevel) + "]\n");
			}
		} else {
			firstLine.Append(' '.Repeat(lcdWidth - (firstLine.Length + values.Length)));
			firstLine.Append(values + "\n");

			if (!noBar) {
				int dotsAmount = lcdWidth - percent.Length - 2;
				int fillLevel = (int)Math.Ceiling(dotsAmount * level);
				secondLine = new StringBuilder("[" + 'I'.Repeat(fillLevel) + '.'.Repeat(dotsAmount - fillLevel) + "]");
				secondLine.Append(percent + "\n");
			}
		}

		return firstLine.Append(secondLine).ToString();
	}
}

string performanceText = "Performance information is generated..";
Dictionary<string, int> instructionsPerMethodDict = new Dictionary<string, int>();
List<int> instructions = new List<int>(new int[100]);
List<double> runtime = new List<double>(new double[100]);
double maxInstructions, maxRuntime;
int avgCounter = 0;
DateTime runStart;

void CreatePerformanceText(string currentMethod, bool start = false)
{
	if (start) {
		runStart = DateTime.Now;
		return;
	}

	performanceText = "";

	int curInstructions = Runtime.CurrentInstructionCount;
	if (curInstructions > maxInstructions) maxInstructions = curInstructions;
	instructions[avgCounter] = curInstructions;
	double avgInstructions = instructions.Sum() / instructions.Count;

	performanceText += "Instructions: " + curInstructions + " / " + Runtime.MaxInstructionCount + "\n";
	performanceText += "Max. Instructions: " + maxInstructions + " / " + Runtime.MaxInstructionCount + "\n";
	performanceText += "Avg. Instructions: " + Math.Floor(avgInstructions) + " / " + Runtime.MaxInstructionCount + "\n\n";

	double curRuntime = (DateTime.Now - runStart).TotalMilliseconds;
	if (curRuntime > maxRuntime) maxRuntime = curRuntime;
	runtime[avgCounter] = curRuntime;
	double avgRuntime = runtime.Sum() / runtime.Count;

	performanceText += "Last runtime " + Math.Round(curRuntime, 4) + " ms\n";
	performanceText += "Max. runtime " + Math.Round(maxRuntime, 4) + " ms\n";
	performanceText += "Avg. runtime " + Math.Round(avgRuntime, 4) + " ms\n\n";

	performanceText += "Instructions per Method:\n";
	instructionsPerMethodDict[currentMethod] = curInstructions;

	foreach (var item in instructionsPerMethodDict.OrderByDescending(i => i.Value)) {
		performanceText += "- " + item.Key + ": " + item.Value + "\n";
	}

	performanceText += "\n";
}

DateTime scrollTime = DateTime.Now;
Dictionary<long, List<int>> scroll = new Dictionary<long, List<int>>();

string CreateScrollingText(float fontSize, string text, IMyTextPanel lcd, int headingHeight = 3, bool scrollText = true, int lcdAmount = 1)
{
	// Get the LCD EntityId
	long id = lcd.EntityId;

	// Create default entry for the LCD in the dictionary
	if (!scroll.ContainsKey(id)) {
		scroll[id] = new List<int> { 1, 3, headingHeight, 0 };
	}

	int scrollDirection = scroll[id][0];
	int scrollWait = scroll[id][1];
	int lineStart = scroll[id][2];
	int scrollSecond = scroll[id][3];

	// Figure out the amount of lines for scrolling content
	var linesTemp = text.TrimEnd('\n').Split('\n');
	List<string> lines = new List<string>();
	int lcdHeight = (int)Math.Ceiling(17 / fontSize * lcdAmount);
	int lcdWidth = (int)(26 / fontSize);
	string lcdText = "";

	// Adjust height for corner LCDs
	if (lcd.BlockDefinition.SubtypeName.Contains("Corner")) {
		if (lcd.CubeGrid.GridSize == 0.5) {
			lcdHeight = (int)Math.Floor(5 / fontSize);
		} else {
			lcdHeight = (int)Math.Floor(3 / fontSize);
		}
	}

	// Adjust width for wide LCDs
	if (lcd.BlockDefinition.SubtypeName.Contains("Wide")) {
		lcdWidth = (int)(52 / fontSize);
	}

	// Build the lines list out of lineTemp and add line breaks if text is too long for one line
	foreach (var line in linesTemp) {
		if (line.Length <= lcdWidth) {
			lines.Add(line);
		} else {
			try {
				string currentLine = "";
				var words = line.Split(' ');
				string number = System.Text.RegularExpressions.Regex.Match(line, @".+(\.|\:)\ ").Value;
				string tab = ' '.Repeat(number.Length);

				foreach (var word in words) {
					if ((currentLine + " " + word).Length > lcdWidth) {
						lines.Add(currentLine);
						currentLine = tab + word + " ";
					} else {
						currentLine += word + " ";
					}
				}

				lines.Add(currentLine);
			} catch {
				lines.Add(line);
			}
		}
	}

	if (scrollText) {
		if (lines.Count > lcdHeight) {
			if (DateTime.Now.Second != scrollSecond) {
				scrollSecond = DateTime.Now.Second;
				if (scrollWait > 0) scrollWait--;
				if (scrollWait <= 0) lineStart += scrollDirection;

				if (lineStart + lcdHeight - headingHeight >= lines.Count && scrollWait <= 0) {
					scrollDirection = -1;
					scrollWait = 3;
				}
				if (lineStart <= headingHeight && scrollWait <= 0) {
					scrollDirection = 1;
					scrollWait = 3;
				}
			}
		} else {
			lineStart = headingHeight;
			scrollDirection = 1;
			scrollWait = 3;
		}

		// Save the current scrolling in the dictionary
		scroll[id][0] = scrollDirection;
		scroll[id][1] = scrollWait;
		scroll[id][2] = lineStart;
		scroll[id][3] = scrollSecond;
	} else {
		lineStart = headingHeight;
	}

	// Always create header
	for (var line = 0; line < headingHeight; line++) {
		lcdText += lines[line] + "\n";
	}

	// Create content based on the starting line
	for (var line = lineStart; line < lines.Count; line++) {
		lcdText += lines[line] + "\n";
	}

	return lcdText;
}

IMyCubeGrid baseGrid = null;
HashSet<IMyCubeGrid> checkedGrids = new HashSet<IMyCubeGrid>();

void GetBaseGrid(IMyCubeGrid currentGrid)
{
	checkedGrids.Add(currentGrid);

	List<IMyMotorStator> scanRotors = new List<IMyMotorStator>();
	List<IMyPistonBase> scanPistons = new List<IMyPistonBase>();
	GridTerminalSystem.GetBlocksOfType<IMyMotorStator>(scanRotors, r => r.IsAttached && r.TopGrid == currentGrid && !checkedGrids.Contains(r.CubeGrid));
	GridTerminalSystem.GetBlocksOfType<IMyPistonBase>(scanPistons, p => p.IsAttached && p.TopGrid == currentGrid && !checkedGrids.Contains(p.CubeGrid));

	if (scanRotors.Count == 0 && scanPistons.Count == 0) {
		baseGrid = currentGrid;
		return;
	} else {
		foreach (var rotor in scanRotors) {
			GetBaseGrid(rotor.CubeGrid);
		}

		foreach (var piston in scanPistons) {
			GetBaseGrid(piston.CubeGrid);
		}
	}
}

HashSet<IMyCubeGrid> connectedGrids = new HashSet<IMyCubeGrid>();

void GetConnectedGrids(IMyCubeGrid currentGrid, bool clearGridList = false)
{
	if (clearGridList) connectedGrids.Clear();

	connectedGrids.Add(currentGrid);

	List<IMyMotorStator> scanRotors = new List<IMyMotorStator>();
	List<IMyPistonBase> scanPistons = new List<IMyPistonBase>();
	GridTerminalSystem.GetBlocksOfType<IMyMotorStator>(scanRotors, r => r.CubeGrid == currentGrid && r.IsAttached && !connectedGrids.Contains(r.TopGrid));
	GridTerminalSystem.GetBlocksOfType<IMyPistonBase>(scanPistons, p => p.CubeGrid == currentGrid && p.IsAttached && !connectedGrids.Contains(p.TopGrid));

	foreach (var rotor in scanRotors) {
		GetConnectedGrids(rotor.TopGrid);
	}

	foreach (var piston in scanPistons) {
		GetConnectedGrids(piston.TopGrid);
	}
}

}
public static partial class Extensions
{
	public static bool IsWithin(this double value, double min, double max, bool excludeMax = false, bool excludeMin = false)
	{
		bool moreThanMin = value >= min;
		bool lessThanMax = value <= max;

		if (excludeMin) moreThanMin = value > min;
		if (excludeMax) lessThanMax = value < max;

		return moreThanMin && lessThanMax;
	}
}

public static partial class Extensions
{
	public static string Repeat(this char charToRepeat, int numberOfRepetitions)
	{
		if (numberOfRepetitions <= 0) {
			return "";
		}
		return new string(charToRepeat, numberOfRepetitions);
	}
}

public static partial class Extensions
{
	public static string ToPercentString(this double numerator, double denominator)
	{
		double percentage = Math.Round(numerator / denominator * 100, 1);
		if (denominator == 0) {
			return "0%";
		} else {
			return percentage + "%";
		}
	}
}

public static partial class Extensions
{
	public static string ToPowerString(this double value, bool wattHours = false)
	{
		string unit = "MW";

		if (value < 1) {
			value *= 1000;
			unit = "kW";
		} else if (value >= 1000 && value < 1000000) {
			value /= 1000;
			unit = "GW";
		} else if (value >= 1000000 && value < 1000000000) {
			value /= 1000000;
			unit = "TW";
		} else if (value >= 1000000000) {
			value /= 1000000000;
			unit = "PW";
		}

		if (wattHours) unit += "h";

		return Math.Round(value, 1) + " " + unit;
	}
}

public static partial class Extensions
{
	public static string ToTankVolumeString(this double value)
	{
		string unit = "L";

		if (value >= 1000 && value < 1000000) {
			value /= 1000;
			unit = "KL";
		} else if (value >= 1000000 && value < 1000000000) {
			value /= 1000000;
			unit = "ML";
		} else if (value >= 1000000000 && value < 1000000000000) {
			value /= 1000000000;
			unit = "BL";
		} else if (value >= 1000000000000) {
			value /= 1000000000000;
			unit = "TL";
		}

		return Math.Round(value, 1) + " " + unit;
	}