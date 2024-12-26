/* This program assumes the crane is set up as such:
 * -----
 * |A|B|
 * -----
 * |P|S|
 * -----
 * Where A and B are battery bays, P is the safe idle parking location for the crane, and S is the location where ships can pick batteries up.
 * 
 * Additionally, P should be near (L:0, M:0) and B should be near (L:10, M:10), i.e. P should be pistons at min extension and B should be pistons at max extension.
 * 
 * Sci-Fi Four-Button Panel must be manually configured as follows (exclude the "" in the arguments):
 *  Action 1: Run this programmable block with argument "r.park"
 *  Action 2: Run this programmable block with argument "r.ship"
 *  Action 3: Run this programmable block with argument "r.baya"
 *  Action 4: Run this programmable block with argument "r.bayb"
 *
 * Sci-Fi One-Button Panel must be manually configured as follows (exclude the "" in the arguments):
 *  Action 1: Run this programmable block with argument "stop"
 */

// Piston group for moving forward/back (movements: P<->A, S<->B)
const string _lowerPistonGroup = "BC Lower Pistons";
// Piston group for moving left/right (movements: P<->S, A<->B)
const string _middlePistonGroup = "BC Middle Pistons";
// Piston group for moving up/down
const string _upperPistonGroup = "BC Upper Pistons";
// Magnetic Plate group used for holding battery to crane
const string _magPlateGroup = "BC Mag Plates";
// Merge Block group for Battery Bay A
const string _bayAMergeBlockGroup = "BC Bay A Merge Blocks";
// Merge Block group for Battery Bay B
const string _bayBMergeBlockGroup = "BC Bay B Merge Blocks";
// LCD Panel (designed for a 1x1 panel) for showing current status
const string _statusPanelName = "BC Status LCD";
// Light Block group for status lights
const string _statusLightGroup = "BC Status Lights";
// Hinge for floor door
const string _doorHingeName = "BC Door Hinge";
// Sci-Fi Four-Button Panel for controlling the crane
const string _controlPanelName = "BC Button Panel";
// Group of Sci-Fi One-Button Panel for E-STOP buttons
const string _eStopButtons = "BC E-STOP Buttons";

// Max velocities for each piston group, must be positive values
const float _maxLowerPistonVelocity = 0.5f;
const float _maxMiddlePistonVelocity = 0.5f;
const float _maxUpperPistonVelocity = 0.5f;

// Piston positions for each location, expressed as an array {Lower, Middle, Upper}
readonly float[] _parkLocation = {0.0f, 0.0f, 0.0f};
readonly float[] _shipLocation = {0.0f, 10.0f, 2.1f};
readonly float[] _bayALocation = {9.35f, 0.3f, 0.5f};
readonly float[] _bayBLocation = {9.35f, 7.8f, 0.5f};

// Max RPM for Door Hinge, must be a positive value
const float _maxDoorHingeRPM = 30.0f;

// Door Hinge positions
const float _doorHingeOpenAngle = 90.0f;
const float _doorHingeCloseAngle = 0.0f;

/*
 * Once all above instructions have been followed and variables have been set, run this Programmable Block with argument "init" to enable the buttons.
 *
 * This is the end of readable/editable stuff, go away and enjoy your crane!
 */
// Locations
const string parkStr = "park";
const string shipStr = "ship";
const string bayAStr = "baya";
const string bayBStr = "bayb";
// Piston groups
const int lowerPistonIdx = 0;
const int middlePistonIdx = 1;
const int upperPistonIdx = 2;
// Splitters
const char modeSplit = '.';
const char valSplit = '-';
const char statusSplit = '#';
// Arguments
const string initStr = "init";
const string requestStr = "r";
const string stopStr = "stop";
// Command Modes
const string manualStr = "m";
const string rawAutoStr = "a";
const string rawCompleteStr = "c";
// Completion Values
const string waitStr = "w";
const string userStr = "u";
// Auto Statuses
const string startStr = "s";
const string openingDoorStr = "od";
const string upperDownStr = "ud";
const string lowerMoveStr = "lm";
const string middleMoveStr = "mm";
const string bothMoveStr = "bm";
const string upperUpStr = "uu";
const string closingDoorStr = "cd";
const string finishStr = "f";

public Program() {
  Runtime.UpdateFrequency = UpdateFrequency.Update10;
}

// Utility functions for piston groups
void applyPistonDistanceLimit(List<IMyPistonBase> pistons, float minDist, float maxDist) {
  foreach (IMyPistonBase piston in pistons) {
    piston.MinLimit = minDist;
    piston.MaxLimit = maxDist;
  }
}
void applyPistonVelocity(List<IMyPistonBase> pistons, float velocity) {
  foreach (IMyPistonBase piston in pistons) {
    piston.Velocity = velocity;
  }
}
void applyPistonEnabled(List<IMyPistonBase> pistons, bool enabled) {
  foreach (IMyPistonBase piston in pistons) {
    piston.Enabled = enabled;
  }
}
bool getPistonsStopped(List<IMyPistonBase> pistons) {
  bool allStopped = true;
  foreach (IMyPistonBase piston in pistons) {
    if (piston.Status == PistonStatus.Stopped) {
      allStopped = false;
    }
  }
  return allStopped;
}
float getPistonDistance(List<IMyPistonBase> pistons) {
  float sum = 0.0f;
  foreach (IMyPistonBase piston in pistons) {
    sum += piston.CurrentPosition;
  }
  return sum / (float) pistons.Count;
}

// Utility functions for Hinges
void applyHingeEnabled(IMyMotorAdvancedStator hinge, bool enabled) {
  if (!enabled) {
    hinge.TargetVelocityRPM = 0;
  }
  hinge.RotorLock = !enabled;
  hinge.Enabled = enabled;
}

// Utility functions for reading block states
bool getMagPlatesLocked(List<IMyLandingGear> plates) {
  bool locked = false;
  foreach (IMyLandingGear plate in plates) {
    if(plate.IsLocked) {
      locked = true;
    }
  }
  return locked;
}
bool getMergeBlocksLocked(List<IMyShipMergeBlock> mergeBlocks) {
  bool locked = false;
  foreach (IMyShipMergeBlock mergeBlock in mergeBlocks) {
    if (mergeBlock.IsConnected) {
      locked = true;
    }
  }
  return locked;
}

// Generic utility functions
bool roughlyEquals(float valueA, float valueB, float tolerance = 0.001f) {
  return (valueA < (valueB + tolerance)) && (valueA > (valueB - tolerance));
}
float convertRadToDeg(float radians) {
  return radians * 180.0f / (float) Math.PI;
}
string findStart(List<IMyPistonBase> lowerPistons, List<IMyPistonBase> middlePistons) {
  float lowerMidPoint = (_bayALocation[0] - _parkLocation[0]) / 2.0f;
  float middleMidPoint = (_bayBLocation[1] - _bayALocation[1]) / 2.0f;

  float lowerLocation = getPistonDistance(lowerPistons);
  float middleLocation = getPistonDistance(middlePistons);

  if (lowerLocation < lowerMidPoint && middleLocation < middleMidPoint) {
    return parkStr;
  } else if (lowerLocation < lowerMidPoint && middleLocation > middleMidPoint) {
    return shipStr;
  } else if (lowerLocation > lowerMidPoint && middleLocation < middleMidPoint) {
    return bayAStr;
  } else {
    return bayBStr;
  }
}
float determineDirectionToTarget(float currentLocation, float targetLocation) {
  return currentLocation < targetLocation ? 1.0f : -1.0f;
}
void emergencyStop(List<IMyPistonBase> allPistons, IMyMotorAdvancedStator doorHinge, string statusMessage) {
  Echo("Emergency Stop: " + statusMessage);
  applyPistonEnabled(allPistons, false);
  applyPistonDistanceLimit(allPistons, 0.0f, 10.0f);
  applyHingeEnabled(doorHinge, false);
  Storage = rawCompleteStr + modeSplit + userStr + statusSplit + statusMessage;
  return;
}
float getTargetLocation(int pistonGroup, string targetLocation) {
  switch (targetLocation) {
    case parkStr:
      return _parkLocation[pistonGroup];
    case shipStr:
      return _shipLocation[pistonGroup];
    case bayAStr:
      return _bayALocation[pistonGroup];
    case bayBStr:
      return _bayBLocation[pistonGroup];
    default:
      return 0.0f;
  }
}
string generateChecksum(List<IMyPistonBase> allPistons, IMyMotorAdvancedStator doorHinge, bool magPlatesLocked, bool bayALocked, bool bayBLocked) {
  string checksum = doorHinge.Enabled ? "1" : "0";
  checksum += magPlatesLocked ? "1" : "0";
  checksum += bayALocked ? "1" : "0";
  checksum += bayBLocked ? "1" : "0";
  foreach (IMyPistonBase piston in allPistons) {
    checksum += piston.Enabled;
  }
  return checksum;
}

// Display update functions
void updateTextSurfaceSettings(IMyTextSurface surface, float fontSize, float padding, Color textColor, Color bgColor, TextAlignment textAlign = TextAlignment.CENTER) {
  surface.BackgroundColor = bgColor;
  surface.FontColor = textColor;
  surface.FontSize = fontSize;
  surface.Alignment = textAlign;
  surface.TextPadding = padding;
}
void updateTextSurfaceSettings(IMyTextSurface surface, float fontSize, float padding) {
  updateTextSurfaceSettings(surface, fontSize, padding, new Color(255, 255, 255), new Color(0, 0, 0), TextAlignment.CENTER);
}
void updateTextSurfaceSettings(IMyTextSurface surface, Color bgColor, float scale = 1.0f) {
  updateTextSurfaceSettings(surface, 3.0f * scale, 10.0f * scale, new Color(255, 255, 255), bgColor, TextAlignment.CENTER);
}
void updateButtons(IMyButtonPanel controlPanel, List<IMyButtonPanel> eStopButtons, bool parkSafe, bool bayASafe, bool bayBSafe) {
  Color unsafeColor = new Color(100, 0, 0);
  Color autoColor = new Color(200, 100, 0);
  Color readyColor = new Color(0, 0, 100);

  IMyTextSurfaceProvider controlPanelSurfaces = controlPanel as IMyTextSurfaceProvider;
  IMyTextSurface parkPanel = controlPanelSurfaces.GetSurface(0);
  IMyTextSurface shipPanel = controlPanelSurfaces.GetSurface(1);
  IMyTextSurface bayAPanel = controlPanelSurfaces.GetSurface(2);
  IMyTextSurface bayBPanel = controlPanelSurfaces.GetSurface(3);

  parkPanel.ContentType = ContentType.TEXT_AND_IMAGE;
  shipPanel.ContentType = ContentType.TEXT_AND_IMAGE;
  bayAPanel.ContentType = ContentType.TEXT_AND_IMAGE;
  bayBPanel.ContentType = ContentType.TEXT_AND_IMAGE;

  foreach (IMyButtonPanel eStopButton in eStopButtons) {
    IMyTextSurfaceProvider eStopPanel = eStopButton as IMyTextSurfaceProvider;
    IMyTextSurface eStopSurface = eStopPanel.GetSurface(0);
    eStopSurface.ContentType = ContentType.TEXT_AND_IMAGE;
    updateTextSurfaceSettings(eStopSurface, 3.9f, 5.0f, new Color(255, 255, 255), new Color(255, 0, 0), TextAlignment.CENTER);
    eStopSurface.WriteText("Crane\nEmergency\nStop", false);
    eStopButton.SetCustomButtonName(0, "E-STOP");
  }

  if (Storage == "") {
    controlPanel.SetCustomButtonName(0, "");
    controlPanel.SetCustomButtonName(1, "");
    controlPanel.SetCustomButtonName(2, "");
    controlPanel.SetCustomButtonName(3, "");

    updateTextSurfaceSettings(parkPanel, unsafeColor, 2.0f);
    updateTextSurfaceSettings(shipPanel, unsafeColor, 2.0f);
    updateTextSurfaceSettings(bayAPanel, unsafeColor, 2.0f);
    updateTextSurfaceSettings(bayBPanel, unsafeColor, 2.0f);

    parkPanel.WriteText("Please", false);
    shipPanel.WriteText("Finish", false);
    bayAPanel.WriteText("Script", false);
    bayBPanel.WriteText("Setup", false);
  } else if (Storage.StartsWith(rawAutoStr)) {
    controlPanel.SetCustomButtonName(0, "");
    controlPanel.SetCustomButtonName(1, "");
    controlPanel.SetCustomButtonName(2, "");
    controlPanel.SetCustomButtonName(3, "");

    updateTextSurfaceSettings(parkPanel, autoColor, 2.0f);
    updateTextSurfaceSettings(shipPanel, autoColor, 2.0f);
    updateTextSurfaceSettings(bayAPanel, autoColor, 2.0f);
    updateTextSurfaceSettings(bayBPanel, autoColor, 2.0f);

    parkPanel.WriteText("Please", false);
    shipPanel.WriteText("Wait:", false);
    bayAPanel.WriteText("Auto", false);
    bayBPanel.WriteText("Mode", false);
  } else {
    controlPanel.SetCustomButtonName(0, parkSafe ? "Move Crane to Park" : "");
    controlPanel.SetCustomButtonName(1, "Move Crane to Ship Hole");
    controlPanel.SetCustomButtonName(2, bayASafe ? "Move Crane to Bay A" : "");
    controlPanel.SetCustomButtonName(3, bayBSafe ? "Move Crane to Bay B": "");

    updateTextSurfaceSettings(parkPanel, parkSafe ? readyColor : unsafeColor);
    updateTextSurfaceSettings(shipPanel, readyColor);
    updateTextSurfaceSettings(bayAPanel, bayASafe ? readyColor : unsafeColor);
    updateTextSurfaceSettings(bayBPanel, bayBSafe ? readyColor : unsafeColor);

    parkPanel.WriteText(parkSafe ? "Move Crane\nto\nPark" : "Cannot\nPark With\nBattery", false);
    shipPanel.WriteText("Move Crane\nto\nShip Hole", false);
    bayAPanel.WriteText(bayASafe ? "Move Crane\nto\nBay A" : "Disabled\nUnsafe\nMovement", false);
    bayBPanel.WriteText(bayBSafe ? "Move Crane\nto\nBay B" : "Disabled\nUnsafe\nMovement", false);
  }
}

void updateLight(IMyLightingBlock light, Color c, bool blink) {
  light.Color = c;
  if (blink) {
    light.BlinkIntervalSeconds = 1.0f;
    light.BlinkLength = 50.0f;
  } else {
    light.BlinkIntervalSeconds = 0.0f;
    light.BlinkLength = 0.0f;
  }
}
void updateLights(List<IMyLightingBlock> lights) {
  foreach (IMyLightingBlock light in lights) {
    if (Storage.StartsWith(rawAutoStr)) {
      updateLight(light, new Color(255, 128, 0), true);
    } else if (Storage.StartsWith(manualStr)) {
      updateLight(light, new Color(0, 0, 255), false);
    } else if (Storage.StartsWith(rawCompleteStr + modeSplit + waitStr)) {
      updateLight(light, new Color(0, 255, 0), false);
    } else {
      updateLight(light, new Color(255, 0, 0), true);
    }
  }
}

void updateScreen(IMyTextPanel panel, bool magPlatesLocked, bool bayALocked, bool bayBLocked) {
  string detailedStatusLine1 = "";
  string detailedStatusLine2 = "";
  string detailedStatusLine3 = "";

  if (Storage.StartsWith(rawAutoStr)) {
    string curStart = Storage.Split(modeSplit)[1].Split(valSplit)[0];
    string curTarget = Storage.Split(modeSplit)[1].Split(valSplit)[1].Split(statusSplit)[0];
    string curStatus = Storage.Split(modeSplit)[1].Split(valSplit)[1].Split(statusSplit)[1];

    detailedStatusLine1 = "Moving from " + curStart + " to " + curTarget;
    switch (curStatus) {
      case startStr:
        detailedStatusLine2 = "Startup . . .";
        break;
      case openingDoorStr:
        detailedStatusLine2 = "Ensuring Battery Door is open . . .";
        break;
      case upperDownStr:
        detailedStatusLine2 = "Moving Upper Pistons down . . .";
        break;
      case lowerMoveStr:
        detailedStatusLine2 = "Moving Lower Pistons to target . . .";
        break;
      case middleMoveStr:
        detailedStatusLine2 = "Moving Middle Pistons to target . . .";
        break;
      case bothMoveStr:
        detailedStatusLine2 = "Moving Lower&Middle Pistons to target . . .";
        break;
      case upperUpStr:
        detailedStatusLine2 = "Moving Upper Pistons to target . . .";
        break;
      case closingDoorStr:
        detailedStatusLine2 = "Closing Battery Door if needed . . .";
        break;
      case finishStr:
        detailedStatusLine2 = "Shutdown . . .";
        break;
      default:
        detailedStatusLine2 = "Unknown Status";
        break;
    }
  } else if (Storage.StartsWith(manualStr)) {
    detailedStatusLine1 = "Ready for next command.";
  } else if (Storage.StartsWith(rawCompleteStr + modeSplit + waitStr)) {
    detailedStatusLine1 = "Movement Complete.";
  } else if (Storage.StartsWith(rawCompleteStr + modeSplit + userStr)) {
    detailedStatusLine1 = "Movement complete, awaiting user input:";
    if (Storage.IndexOf(statusSplit) > 0) {
      string completeStatus = Storage.Split(modeSplit)[1].Split(statusSplit)[0];
      string completeMsg = Storage.Split(modeSplit)[1].Split(statusSplit)[1];
      switch (completeMsg) {
        case shipStr:
          detailedStatusLine2 = magPlatesLocked ? "Please attach battery to ship," : "Please attach battery to crane,";
          detailedStatusLine3 = magPlatesLocked ? "and detach from crane." : "and detach from ship.";
          break;
        case bayAStr:
          detailedStatusLine2 = magPlatesLocked ? "Please attach battery to Bay A," : "Please attach battery to crane";
          detailedStatusLine3 = magPlatesLocked ? "and detach from crane." : "and detach from bay A.";
          break;
        case bayBStr:
          detailedStatusLine2 = magPlatesLocked ? "Please attach battery to Bay B" : "Please attach battery to crane";
          detailedStatusLine3 = magPlatesLocked ? "and detach from crane." : "and detach from bay B.";
          break;
        default:
          detailedStatusLine2 = completeMsg;
          break;
      }
    } else {
      detailedStatusLine2 = "Condition not passed.";
    }
  }

  panel.ContentType = ContentType.TEXT_AND_IMAGE;
  panel.TextPadding = 0.0f;
  panel.WriteText("Battery Crane Status: " + (Storage.StartsWith(rawAutoStr) ? "Auto" : "Manual") + " Mode\n", false);
  panel.WriteText("------------------------------------------------\n", true);
  panel.WriteText("Battery Storage:\n", true);
  panel.WriteText("    Crane: " + (magPlatesLocked ? "Occupied" : "Empty") + "\n", true);
  panel.WriteText("    Bay A: " + (bayALocked ? "Occupied" : "Empty") + "\n", true);
  panel.WriteText("    Bay B: " + (bayBLocked ? "Occupied" : "Empty") + "\n", true);
  panel.WriteText("\n", true);
  panel.WriteText("Detailed Status:\n", true);
  panel.WriteText("    " + detailedStatusLine1 + "\n", true);
  panel.WriteText("    " + detailedStatusLine2 + "\n", true);
  panel.WriteText("    " + detailedStatusLine3 + "\n", true);
  panel.WriteText("\n", true);
  panel.WriteText("Debug Code:\n", true);
  panel.WriteText(Storage + "\n", true);
}

// Auto Mode Handlers
bool handleMoveHinge(IMyMotorAdvancedStator hinge, float targetSpeed, float targetAngle) {
  if (roughlyEquals(convertRadToDeg(hinge.Angle), targetAngle, 0.1f)) {
    applyHingeEnabled(hinge, false);
    return true;
  } else {
    hinge.TargetVelocityRPM = targetSpeed * determineDirectionToTarget(convertRadToDeg(hinge.Angle), targetAngle);
    applyHingeEnabled(hinge, true);
    return false;
  }
}
bool handleMovePistons(List<IMyPistonBase> pistons, float targetSpeed, float targetPosition) {
  if (getPistonsStopped(pistons)) {
    float curPos = getPistonDistance(pistons);
    if (roughlyEquals(curPos, targetPosition)) {
      applyPistonEnabled(pistons, false);
      return true;
    } else {
      if (targetPosition > curPos) {
        applyPistonDistanceLimit(pistons, 0.0f, targetPosition);
      } else {
        applyPistonDistanceLimit(pistons, targetPosition, 10.0f);
      }
      applyPistonVelocity(pistons, targetSpeed * determineDirectionToTarget(curPos, targetPosition));
      applyPistonEnabled(pistons, true);
      return false;
    }
  }
  return false;
}

public void Main(string arg, UpdateType updateType) {
  if (Storage == null) {
    Storage = "";
  }

  Random rnd = new Random();
  Echo("Running: rand:" + rnd.Next(100));
  Echo("Storage:" + Storage);

  string autoStr = rawAutoStr + modeSplit;
  string completeStr = rawCompleteStr + modeSplit;

  List<IMyPistonBase> lowerPistons = new List<IMyPistonBase>();
  IMyBlockGroup lowerPistonsGroup = GridTerminalSystem.GetBlockGroupWithName(_lowerPistonGroup);
  lowerPistonsGroup.GetBlocksOfType<IMyPistonBase>(lowerPistons);

  List<IMyPistonBase> middlePistons = new List<IMyPistonBase>();
  IMyBlockGroup middlePistonsGroup = GridTerminalSystem.GetBlockGroupWithName(_middlePistonGroup);
  middlePistonsGroup.GetBlocksOfType<IMyPistonBase>(middlePistons);

  List<IMyPistonBase> upperPistons = new List<IMyPistonBase>();
  IMyBlockGroup upperPistonsGroup = GridTerminalSystem.GetBlockGroupWithName(_upperPistonGroup);
  upperPistonsGroup.GetBlocksOfType<IMyPistonBase>(upperPistons);

  List<IMyLandingGear> magPlates = new List<IMyLandingGear>();
  IMyBlockGroup magPlatesGroup = GridTerminalSystem.GetBlockGroupWithName(_magPlateGroup);
  magPlatesGroup.GetBlocksOfType<IMyLandingGear>(magPlates);

  List<IMyShipMergeBlock> bayAMergeBlocks = new List<IMyShipMergeBlock>();
  IMyBlockGroup bayAMergeBlocksGroup = GridTerminalSystem.GetBlockGroupWithName(_bayAMergeBlockGroup);
  bayAMergeBlocksGroup.GetBlocksOfType<IMyShipMergeBlock>(bayAMergeBlocks);

  List<IMyShipMergeBlock> bayBMergeBlocks = new List<IMyShipMergeBlock>();
  IMyBlockGroup bayBMergeBlocksGroup = GridTerminalSystem.GetBlockGroupWithName(_bayBMergeBlockGroup);
  bayBMergeBlocksGroup.GetBlocksOfType<IMyShipMergeBlock>(bayBMergeBlocks);

  List<IMyLightingBlock> statusLights = new List<IMyLightingBlock>();
  IMyBlockGroup statusLightsGroup = GridTerminalSystem.GetBlockGroupWithName(_statusLightGroup);
  statusLightsGroup.GetBlocksOfType<IMyLightingBlock>(statusLights);

  List<IMyButtonPanel> eStopButtons = new List<IMyButtonPanel>();
  IMyBlockGroup eStopButtonsGroup = GridTerminalSystem.GetBlockGroupWithName(_eStopButtons);
  eStopButtonsGroup.GetBlocksOfType<IMyButtonPanel>(eStopButtons);

  IMyTextPanel statusPanel = GridTerminalSystem.GetBlockWithName(_statusPanelName) as IMyTextPanel;
  IMyButtonPanel controlPanel = GridTerminalSystem.GetBlockWithName(_controlPanelName) as IMyButtonPanel;
  IMyMotorAdvancedStator doorHinge = GridTerminalSystem.GetBlockWithName(_doorHingeName) as IMyMotorAdvancedStator;

  List<IMyPistonBase> allPistons = lowerPistons.Concat(middlePistons).Concat(upperPistons).ToList();

  if ((updateType & (UpdateType.Trigger | UpdateType.Terminal)) != 0) {
    if (arg == stopStr) {
      emergencyStop(allPistons, doorHinge, "User pressed E-STOP");
      return;
    } else if (arg == initStr) {
      // Handle init
      Storage = completeStr + userStr;
    } else if (!Storage.StartsWith(autoStr) && arg.StartsWith(requestStr)) {
      // Handle starting an auto movement
      string target = arg.Split(modeSplit)[1];
      string start = findStart(lowerPistons, middlePistons);
      Storage = autoStr + start + valSplit + target + statusSplit + startStr;
    }
  }
  
  if ((updateType & UpdateType.Update10) != 0) {
    bool magPlatesLocked = getMagPlatesLocked(magPlates);
    bool bayALocked = getMergeBlocksLocked(bayAMergeBlocks);
    bool bayBLocked = getMergeBlocksLocked(bayBMergeBlocks);
    string curMode = Storage.Split(modeSplit)[0];
    if (curMode == rawAutoStr) {
      string curStart = Storage.Split(modeSplit)[1].Split(valSplit)[0];
      string curTarget = Storage.Split(modeSplit)[1].Split(valSplit)[1].Split(statusSplit)[0];
      string curStatus = Storage.Split(modeSplit)[1].Split(valSplit)[1].Split(statusSplit)[1];

      if (curTarget == bayBStr && bayBLocked && magPlatesLocked || curTarget == bayAStr && bayALocked && magPlatesLocked) {
        emergencyStop(allPistons, doorHinge, "Cannot move to Occupied Bay");
        return;
      }
      if (curTarget == parkStr && magPlatesLocked) {
        emergencyStop(allPistons, doorHinge, "Cannot park with Battery");
        return;
      } 

      switch (curStatus) {
        case startStr:
          applyPistonEnabled(allPistons, false);
          applyHingeEnabled(doorHinge, false);
          Storage = autoStr + curStart + valSplit + curTarget + statusSplit + openingDoorStr;
          break;
        case openingDoorStr:
          if (handleMoveHinge(doorHinge, _maxDoorHingeRPM, _doorHingeOpenAngle)) {
            Storage = autoStr + curStart + valSplit + curTarget + statusSplit + upperDownStr;
          }
          break;
        case upperDownStr:
          if (handleMovePistons(upperPistons, _maxUpperPistonVelocity, 0.0f)) {
            switch (curStart) {
              case parkStr:
                switch (curTarget) {
                  case parkStr:
                  case shipStr:
                  case bayAStr:
                    Storage = autoStr + curStart + valSplit + curTarget + statusSplit + bothMoveStr;
                    break;
                  case bayBStr:
                    Storage = autoStr + curStart + valSplit + curTarget + statusSplit + middleMoveStr;
                    break;
                  default:
                    emergencyStop(allPistons, doorHinge, "Invalid Position");
                    break;
                }
                break;
              case shipStr:
                switch (curTarget) {
                  case shipStr:
                  case parkStr:
                  case bayBStr:
                    Storage = autoStr + curStart + valSplit + curTarget + statusSplit + bothMoveStr;
                    break;
                  case bayAStr:
                    Storage = autoStr + curStart + valSplit + curTarget + statusSplit + middleMoveStr;
                    break;
                  default:
                    emergencyStop(allPistons, doorHinge, "Invalid Position");
                    break;
                }
                break;
              case bayAStr:
                switch (curTarget) {
                  case bayAStr:
                  case parkStr:
                  case bayBStr:
                    Storage = autoStr + curStart + valSplit + curTarget + statusSplit + bothMoveStr;
                    break;
                  case shipStr:
                    Storage = autoStr + curStart + valSplit + curTarget + statusSplit + lowerMoveStr;
                    break;
                  default:
                    emergencyStop(allPistons, doorHinge, "Invalid Position");
                    break;
                }
                break;
              case bayBStr:
                switch (curTarget) {
                  case bayBStr:
                  case shipStr:
                  case bayAStr:
                    Storage = autoStr + curStart + valSplit + curTarget + statusSplit + bothMoveStr;
                    break;
                  case parkStr:
                    Storage = autoStr + curStart + valSplit + curTarget + statusSplit + lowerMoveStr;
                    break;
                  default:
                    emergencyStop(allPistons, doorHinge, "Invalid Position");
                    break;
                }
                break;
              default:
                emergencyStop(allPistons, doorHinge, "Invalid Position");
                break;
            }
          }
          break;
        case lowerMoveStr:
          if (handleMovePistons(lowerPistons, _maxLowerPistonVelocity, getTargetLocation(lowerPistonIdx, curTarget))) {
            switch (curStart) {
              case parkStr:
              case shipStr:
                Storage = autoStr + curStart + valSplit + curTarget + statusSplit + upperUpStr;
                break;
              case bayAStr:
              case bayBStr:
                Storage = autoStr + curStart + valSplit + curTarget + statusSplit + middleMoveStr;
                break;
              default:
                emergencyStop(allPistons, doorHinge, "Invalid Location");
                break;
            }
          }
          break;
        case middleMoveStr:
          if (handleMovePistons(middlePistons, _maxMiddlePistonVelocity, getTargetLocation(middlePistonIdx, curTarget))) {
            switch (curStart) {
              case parkStr:
              case shipStr:
                Storage = autoStr + curStart + valSplit + curTarget + statusSplit + lowerMoveStr;
                break;
              case bayAStr:
              case bayBStr:
                Storage = autoStr + curStart + valSplit + curTarget + statusSplit + upperUpStr;
                break;
              default:
                emergencyStop(allPistons, doorHinge, "Invalid Location");
                break;
            }
          }
          break;
        case bothMoveStr:
          bool lowerDone = handleMovePistons(lowerPistons, _maxLowerPistonVelocity, getTargetLocation(lowerPistonIdx, curTarget));
          bool middleDone = handleMovePistons(middlePistons, _maxMiddlePistonVelocity, getTargetLocation(middlePistonIdx, curTarget));
          if (lowerDone && middleDone) {
            Storage = autoStr + curStart + valSplit + curTarget + statusSplit + upperUpStr;
          }
          break;
        case upperUpStr:
          if (handleMovePistons(upperPistons, _maxUpperPistonVelocity, getTargetLocation(upperPistonIdx, curTarget))) {
            if (curTarget == parkStr) {
              Storage = autoStr + curStart + valSplit + curTarget + statusSplit + closingDoorStr;
            } else {
              Storage = autoStr + curStart + valSplit + curTarget + statusSplit + finishStr;
            }
          }
          break;
        case closingDoorStr:
          if (handleMoveHinge(doorHinge, _maxDoorHingeRPM, _doorHingeCloseAngle)) {
            Storage = autoStr + curStart + valSplit + curTarget + statusSplit + finishStr;
          }
          break;
        case finishStr:
          Me.CustomData = generateChecksum(allPistons, doorHinge, magPlatesLocked, bayALocked, bayBLocked);
          applyPistonEnabled(allPistons, false);
          applyPistonDistanceLimit(allPistons, 0.0f, 10.0f);
          applyHingeEnabled(doorHinge, false);
          if (curTarget == parkStr) {
            Storage = completeStr + waitStr;
          } else {
            Storage = completeStr + userStr + statusSplit + curTarget;
          }
          break;
        default:
          emergencyStop(allPistons, doorHinge, "Invalid Status");
          break;
      }
    } else if (curMode == rawCompleteStr) {
      if (Me.CustomData != generateChecksum(allPistons, doorHinge, magPlatesLocked, bayALocked, bayBLocked)) {
        Storage = manualStr;
      }
    }

    // Done doing stuff, update user on status
    updateButtons(controlPanel, eStopButtons, !magPlatesLocked, !(magPlatesLocked && bayALocked), !(magPlatesLocked && bayBLocked));
    updateLights(statusLights);
    updateScreen(statusPanel, magPlatesLocked, bayALocked, bayBLocked);
  }
}