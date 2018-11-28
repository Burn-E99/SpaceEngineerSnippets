# SpaceEngineerSnippets
This repository will hold all code that I have written for the game Space Engineers, along with a self built documentation of the Space Engineers API, since I cannot find a consistent, up to date documentation.  Details of that documentation will be found in the Documentation folder.

# Project List
This is a list of all projects I am working on currently, or have finished.  If the project is finished, I will mark it with a [COMPLETE] tag, and a basic version number will be given.
- AutomaticAirLock
  - This program + blueprint is a fully automatic airlock that works based on sensors at each door.  This will automatically pressurize/depressurize the rooms on either side, along with opening and closing the doors as the player moves through it.
- FollowTheDrone
  - This program is for a generic drone that allows the player to remote control one of the drones, and any drone with the same ID and passphrase will follow the player.  This will be helpful with moving many ships/drones to the same place by traveling the path only once.
- LogisticBotDrone
  - This program's idea comes from the game ~~Cracktorio~~ Factorio and it's amazing item delivery system, logistic bots.  This program allows the user to select parts from a list and counts for each item, and the Logistic Drone will collect the desired items and deliver them to the player.
- OxygenGeneratorMonitor
  - This is a special little program that monitors the level of an oxygen tank, and turns an attached oxygen generator on and off when needed.  This allows the player to (using an air vent) depressurize the room safely and without wasting oxygen.
- RGBDerp
  - This is a stupid little program that repeatedly changed the color of a spotlight.  This was made at about 3AM in conjunction with a shutter system inside a dark "theatre" that made the spotlight only show as a very tiny pixel.  This light was also made to flash on and off using a rotor with a catwalk repeatedly blocking the light.

# Notes
So apparently at some point between now and when I last was writing these programs, Keen added support for loops into Space Engineers.  This means that any of the old code in here requires a timer block and newer ones are much less likely to need one.

Source: When last programming, the game would scream `Script too complex` at any `for` loop or `while` loop, and now the code `int i=0;while(true) {i++;Echo("" + i);}` will run `50,000` loops before stopping (game did have a major lag spike the time this ran).
