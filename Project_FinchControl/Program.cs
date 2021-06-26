using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control - Master
    // Description: Finch Robot tasks - Mission 3
    // Application Type: Console
    // Author: Ludwig, Ben
    // Dated Created: 6/4/21
    // Last Modified: 6/19/21
    //
    // **************************************************

    /// <summary>
    /// User Commands
    /// </summary>

    public enum Command
    {
        NONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        WAIT,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        SOUNDON,
        SOUNDOFF,
        GETTEMPERATURE,
        DONE
    }

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayMenuScreen(finchRobot);
                        break;

                    case "c":
                        DataRecorderMenuScreen(finchRobot);
                        break;

                    case "d":
                        LightAlarmDisplayMenu(finchRobot);
                        break;

                    case "e":
                        UserProgrammingDisplayMenu(finchRobot);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }


        #region USER PROGRAMMING

        /// <summary>
        /// ****************************************************************
        ///                         User Programming Menu
        /// ****************************************************************
        /// </summary>

        private static void UserProgrammingDisplayMenu(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitProgrammingMenu = false;
            string menuChoice;
            (int motorSpeed, int ledBrightness, double waitSeconds, double soundPitch) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;
            commandParameters.soundPitch = 0;
            List<Command> commands = new List<Command>();

            do
            {
                DisplayScreenHeader("User Programming Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Command Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "a":
                        commandParameters = UserProgrammingGetParameters();
                        break;

                    case "b":
                        UserProgrammingGetCommands(commands);
                        break;

                    case "c":
                        UserProgrammingDisplayCommands(commands);
                        break;

                    case "d":
                        UserProgrammingExecuteCommands(finchRobot,commands,commandParameters);
                        break;

                    case "q":
                        quitProgrammingMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitProgrammingMenu);
        }

        //Execute user commands

        private static void UserProgrammingExecuteCommands(Finch finchRobot, List<Command> commands, (int motorSpeed, int ledBrightness, double waitSeconds, double soundPitch) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMilliseconds = (int)(commandParameters.waitSeconds * 1000);
            int soundPitch = (int)(commandParameters.soundPitch);
            string commandFeedback = "";
            const int TURNING_MOTOR_SPEED = 100;

            DisplayScreenHeader("Execute Commands");
            Console.WriteLine("\tThe Finch robot is ready to execute commands.");
            DisplayContinuePrompt();

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;

                    case Command.MOVEFORWARD:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        commandFeedback = Command.MOVEFORWARD.ToString();
                        break;

                    case Command.MOVEBACKWARD:
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        commandFeedback = Command.MOVEBACKWARD.ToString();
                        break;

                    case Command.STOPMOTORS:
                        finchRobot.setMotors(0, 0);
                        commandFeedback = Command.STOPMOTORS.ToString();
                        break;

                    case Command.WAIT:
                        finchRobot.wait(waitMilliseconds);
                        commandFeedback = Command.WAIT.ToString();
                        break;

                    case Command.TURNRIGHT:
                        finchRobot.setMotors(TURNING_MOTOR_SPEED, -TURNING_MOTOR_SPEED);
                        commandFeedback = Command.TURNRIGHT.ToString();
                        break;

                    case Command.TURNLEFT:
                        finchRobot.setMotors(-TURNING_MOTOR_SPEED, TURNING_MOTOR_SPEED);
                        commandFeedback = Command.TURNLEFT.ToString();
                        break;

                    case Command.LEDON:
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        commandFeedback = Command.LEDON.ToString();
                        break;

                    case Command.LEDOFF:
                        finchRobot.setLED(0, 0, 0);
                        commandFeedback = Command.LEDOFF.ToString();
                        break;

                    case Command.SOUNDON:
                        finchRobot.noteOn(soundPitch);
                        commandFeedback = Command.SOUNDON.ToString();
                        break;

                    case Command.SOUNDOFF:
                        finchRobot.noteOff();
                        commandFeedback = Command.SOUNDOFF.ToString();
                        break;

                    case Command.GETTEMPERATURE:
                        commandFeedback = $"Temperature: {finchRobot.getTemperature().ToString("n2")}\n";
                        break;

                    case Command.DONE:
                        finchRobot.setLED(0, 0, 0);
                        finchRobot.setMotors(0, 0);
                        finchRobot.noteOff();
                        commandFeedback = Command.DONE.ToString();
                        break;

                    default:

                        break;

                }

                Console.WriteLine($"\t{commandFeedback}");
            }

        }

        //Display list of commands

        private static void UserProgrammingDisplayCommands(List<Command> commands)
        {
            DisplayScreenHeader("Current Robot Commands");

            foreach (Command command in commands)
            {
                Console.WriteLine($"\t{command}");
            }

            DisplayContinuePrompt();

        }

        //Get command list from user

        private static void UserProgrammingGetCommands(List<Command> commands)
        {
            Command command = Command.NONE;

            DisplayScreenHeader("Add Commands");

            int commandCount = 1;

            Console.WriteLine();
            Console.WriteLine("\tList of Available Commands");
            Console.WriteLine();

            foreach  (string commandName in Enum.GetNames(typeof(Command)))
            {
                Console.Write($"- {commandName.ToLower()} -");
                if (commandCount % 5 == 0) Console.Write("-\n\t-");
                commandCount++;
            }

            Console.WriteLine();
            Console.WriteLine();

            while(command != Command.DONE)
            {
                Console.Write("\tEnter command: ");

                if(Enum.TryParse(Console.ReadLine().ToUpper(), out command))
                {
                    commands.Add(command);
                }
                else
                {
                    Console.WriteLine("\tPlease enter a command from the list above.");
                }
            }

            DisplayContinuePrompt();
            
        }

        //Get command parameters from user

        private static (int motorSpeed, int ledBrightness, double waitSeconds, double soundPitch) UserProgrammingGetParameters()
        {
            (int motorSpeed, int ledBrightness, double waitSeconds, double soundPitch) commandParameters;
            string userResponse;
            bool validResponse = false;

            DisplayScreenHeader("Set Command Parameters");

            do
            {
                validResponse = false;
                Console.Write("Please enter the desired motor speed [0-255]: ");
                userResponse = Console.ReadLine();
                
                if(int.TryParse(userResponse, out commandParameters.motorSpeed))
                {
                    if (commandParameters.motorSpeed < 0 || commandParameters.motorSpeed > 255)
                    {
                        Console.WriteLine("Please enter a number between 0 and 255.");
                    }
                    else
                    {
                        Console.WriteLine("You have selected a motorspeed of {0}.", commandParameters.motorSpeed);
                        validResponse = true;
                    }
                }

                else 
                {
                    Console.WriteLine("Please enter a number between 0 and 255.");
                }

            } while (!validResponse);

            do
            {
                validResponse = false;
                Console.Write("Please enter the desired LED brightness [0-255]: ");
                userResponse = Console.ReadLine();

                if (int.TryParse(userResponse, out commandParameters.ledBrightness))
                {
                    if (commandParameters.ledBrightness < 0 || commandParameters.ledBrightness > 255)
                    {
                        Console.WriteLine("Please enter a number between 0 and 255.");
                    }
                    else
                    {
                        Console.WriteLine("You have selected a brightness of {0}.", commandParameters.ledBrightness);
                        validResponse = true;
                    }
                }

                else
                {
                    Console.WriteLine("Please enter a number between 0 and 255.");
                }

            } while (!validResponse);

            do
            {
                validResponse = false;
                Console.Write("Please enter the desired wait duration in seconds: ");
                userResponse = Console.ReadLine();

                if (double.TryParse(userResponse, out commandParameters.waitSeconds))
                {
                    if (commandParameters.waitSeconds < 0)
                    {
                        Console.WriteLine("Please enter a positive number.");
                    }
                    else
                    {
                        Console.WriteLine("You have selected a wait time of {0} seconds.", commandParameters.waitSeconds);
                        validResponse = true;
                    }
                }

                else
                {
                    Console.WriteLine("Please enter a positive number.");
                }
            } while (!validResponse);

            do
            {
                validResponse = false;
                Console.Write("Please enter the desired sound pitch in Hertz: ");
                userResponse = Console.ReadLine();

                if (double.TryParse(userResponse, out commandParameters.soundPitch))
                {
                    if (commandParameters.soundPitch < 16.35)
                    {
                        Console.WriteLine("Please enter a frequency of 16.35 or higher.");
                    }
                    else
                    {
                        Console.WriteLine("You have selected a pitch frequency of {0} Hertz.", commandParameters.soundPitch);
                        validResponse = true;
                    }
                }

                else
                {
                    Console.WriteLine("Please enter a number.");
                }

            } while (!validResponse);

            DisplayContinuePrompt();

            return commandParameters;
        }


        #endregion

        #region ALARM SYSTEM

        /// <summary>
        /// ****************************************************************
        ///                         Alarm System Menu
        /// ****************************************************************
        /// </summary>
        /// <param name="finchRobot"></param>
        private static void LightAlarmDisplayMenu(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitAlarmMenu = false;
            string menuChoice;
            string sensorsToMonitor = "";
            string rangeType = "";
            string lightOrTemp = "";
            int minMaxValue = 0;
            int timeToMonitor = 0;

            do
            {
                DisplayScreenHeader("Light Alarm Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Alarm Type");
                Console.WriteLine("\tb) Set Sensors to Monitor (Light Only)");
                Console.WriteLine("\tc) Set Range Type");
                Console.WriteLine("\td) Set Threshold Values");
                Console.WriteLine("\te) Set Time to Monitor");
                Console.WriteLine("\tf) Activate Alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "a":
                        lightOrTemp = SetAlarmType();
                        break;

                    case "b":
                        sensorsToMonitor = LightAlarmSelectSensors();
                        break;

                    case "c":
                        rangeType = LightAlarmRangeType();
                        break;

                    case "d":
                        minMaxValue = LightAlarmSetThreshold(rangeType, lightOrTemp, finchRobot);
                        break;

                    case "e":
                        timeToMonitor = AlarmSetDuration();
                        break;

                    case "f":
                        AlarmActivateAlarm(lightOrTemp, rangeType, minMaxValue, sensorsToMonitor, timeToMonitor, finchRobot);
                        break;

                    case "q":
                        quitAlarmMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitAlarmMenu);
        }

        //user activates alarm

        private static void AlarmActivateAlarm(
            string lightOrTemp, 
            string rangeType, 
            int minMaxValue, 
            string sensorsToMonitor, 
            int timeToMonitor, 
            Finch finchRobot)
        {
            int secondsElapsed = 0;
            bool thresholdExceeded = false;
            int currentLightSensorValue = 0;
            double currentTempSensorValue = 0;

            DisplayScreenHeader("Activate Alarm");

            Console.WriteLine($"Alarm Type: {lightOrTemp}");
            Console.WriteLine($"Sensors to Monitor (Light Only): {sensorsToMonitor}");
            Console.WriteLine($"Range Type: {rangeType}");
            Console.WriteLine($"Threshold Value: {minMaxValue}");
            Console.WriteLine($"Time to monitor: {timeToMonitor} seconds");
            Console.WriteLine();

            Console.WriteLine("Press any key to activate the alarm.");
            Console.ReadKey();
            Console.WriteLine();

            while (secondsElapsed < timeToMonitor && !thresholdExceeded)
            {
                if (lightOrTemp == "light")
                {
                    switch (sensorsToMonitor)
                    {
                        case "left":
                            currentLightSensorValue = finchRobot.getLeftLightSensor();
                            break;

                        case "right":
                            currentLightSensorValue = finchRobot.getRightLightSensor();
                            break;

                        case "both":
                            currentLightSensorValue = (finchRobot.getRightLightSensor() + finchRobot.getLeftLightSensor()) / 2;
                            break;

                        default:

                            break;
                    }

                    switch (rangeType)
                    {
                        case "minimum":
                            if (currentLightSensorValue < minMaxValue)
                            {
                                thresholdExceeded = true;
                            }
                            break;

                        case "maximum":
                            if (currentLightSensorValue > minMaxValue)
                            {
                                thresholdExceeded = true;
                            }
                            break;
                    }
                    Console.WriteLine($"{secondsElapsed} seconds. Light level is {currentLightSensorValue}.");
                }

                else
                {
                    currentTempSensorValue = finchRobot.getTemperature();

                    switch (rangeType)
                    {
                        case "minimum":
                            if (currentTempSensorValue < minMaxValue)
                            {
                                thresholdExceeded = true;
                            }
                            break;

                        case "maximum":
                            if (currentTempSensorValue > minMaxValue)
                            {
                                thresholdExceeded = true;
                            }
                            break;
                    }
                    Console.WriteLine($"{secondsElapsed} seconds. Temperature is {currentTempSensorValue} degrees Celsius.");
                }
                finchRobot.wait(1000);
                secondsElapsed++;
            }

            if (thresholdExceeded)
            {
                Console.WriteLine($"The {rangeType} threshold of {minMaxValue} has been exceeded!");
            }

            else
            {
                Console.WriteLine($"The {rangeType} threshold of {minMaxValue} was not exceeded.");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }


        //user selects time to monitor

        private static int AlarmSetDuration()
        {
            bool validResponse = false;
            string userResponse;
            int timeToMonitor;

            do
            {
                DisplayScreenHeader("Enter Time to Monitor");


                Console.WriteLine();
                Console.Write("\tPlease enter the time to monitor in seconds: ");
                userResponse = Console.ReadLine();
                timeToMonitor = int.Parse(userResponse);
                Console.WriteLine();
                if (timeToMonitor <= 0)
                {
                    Console.WriteLine("\tPlease enter a positive number.");
                    Console.ReadKey();
                }
                else
                {
                    validResponse = true;
                }
            } while (!validResponse);

            Console.WriteLine($"\tYou have entered a monitoring time of {timeToMonitor} seconds.");

            return timeToMonitor;
        }

        // user selects alarm type (light or temperature)

        private static string SetAlarmType()
        {
            string lightOrTemp;
            bool validResponse = false;

            do
            {
                DisplayScreenHeader("Select Alarm Type");

                Console.Write("\tPlease select whether the alarm will measure [light] or [temperature]: ");
                lightOrTemp = Console.ReadLine();
                Console.WriteLine();

                switch (lightOrTemp)
                {
                    case "light":
                        Console.WriteLine("\tThe Finch robot will monitor light levels.");
                        validResponse = true;
                        DisplayContinuePrompt();
                        break;

                    case "temperature":
                        Console.WriteLine("\tThe Finch robot will monitor temperature levels.");
                        validResponse = true;
                        DisplayContinuePrompt();
                        break;

                    default:
                        Console.WriteLine("\tThat was not a valid response. Please enter [light] or [temperature].");
                        validResponse = false;
                        DisplayContinuePrompt();
                        break;
                }

            } while (!validResponse);

            return lightOrTemp;
        }

        // user selects alarm threshold

        private static int LightAlarmSetThreshold(string rangeType, string lightOrTemp, Finch finchRobot)
        {
            bool validResponse = false;
            string userResponse;
            int minMaxValue;

            do
            {
                DisplayScreenHeader("Enter Threshold Value");

                Console.WriteLine($"\tCurrent ambient left light sensor value: {finchRobot.getLeftLightSensor()}");
                Console.WriteLine($"\tCurrent ambient right light sensor value: {finchRobot.getRightLightSensor()}");
                Console.WriteLine();
                Console.WriteLine($"\tCurrent ambient temperature: {finchRobot.getTemperature()}");
                Console.WriteLine();
                Console.Write("\tEnter the {0} threshold value for the {1} sensor(s): ",rangeType, lightOrTemp);
                userResponse = Console.ReadLine();
                minMaxValue = int.Parse(userResponse);
                Console.WriteLine();
                if (minMaxValue <= 0)
                {
                    Console.WriteLine("\tPlease enter a positive number.");
                    Console.ReadKey();
                }
                else
                {
                    validResponse = true;
                }
            } while (!validResponse);

            Console.WriteLine($"\tYou have entered a threshold value of {minMaxValue}.");

            return minMaxValue;
        }

        // user selects alarm range type (minimum or maximum)

        private static string LightAlarmRangeType()
        {
            string rangeType;
            bool validResponse = false;

            do
            {
                DisplayScreenHeader("Select Range Type");

                Console.Write("\tPlease select whether the alarm will trigger on a [minimum] or [maximum]: ");
                rangeType = Console.ReadLine();
                Console.WriteLine();

                switch (rangeType)
                {
                    case "minimum":
                        Console.WriteLine("\tYou have selected a minimum trigger.");
                        validResponse = true;
                        DisplayContinuePrompt();
                        break;

                    case "maximum":
                        Console.WriteLine("\tYou have selected a maximum trigger.");
                        validResponse = true;
                        DisplayContinuePrompt();
                        break;

                    default:
                        Console.WriteLine("\tThat was not a valid response. Please enter [minimum] or [maximum].");
                        validResponse = false;
                        DisplayContinuePrompt();
                        break;
                }

            } while (!validResponse);

            return rangeType;
        }

        // User selects which sensors to monitor

        private static string LightAlarmSelectSensors()
        {
            string sensorsToMonitor;
            bool validResponse = false;

            do
            {
                DisplayScreenHeader("Set Sensors to Monitor (Light Only)");

                Console.Write("\tPlease select which light sensors to monitor (left, right, or both): ");
                sensorsToMonitor = Console.ReadLine();
                Console.WriteLine();

                switch (sensorsToMonitor)
                {
                    case "left":
                        Console.WriteLine("\tYou have selected the left light sensor.");
                        validResponse = true;
                        DisplayContinuePrompt();
                        break;

                    case "right":
                        Console.WriteLine("\tYou have selected the right light sensor.");
                        validResponse = true;
                        DisplayContinuePrompt();
                        break;

                    case "both":
                        Console.WriteLine("\tYou have selected both light sensors.");
                        validResponse = true;
                        DisplayContinuePrompt();
                        break;

                    default:
                        Console.WriteLine("\tThat was not a valid response. Please enter left, right, or both.");
                        validResponse = false;
                        DisplayContinuePrompt();
                        break;
                }

            } while (!validResponse);

            return sensorsToMonitor;
        }

            #endregion

            #region TALENT SHOW

            /// <summary>
            /// *****************************************************************
            /// *                     Talent Show Menu                          *
            /// *****************************************************************
            /// </summary>
            static void TalentShowDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light Show");
                Console.WriteLine("\tb) Singing Contest");
                Console.WriteLine("\tc) Dancing Robot");
                Console.WriteLine("\td) All Together Now!");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        TalentShowDisplayLight(finchRobot);
                        break;

                    case "b":
                        TalentShowSound(finchRobot);
                        break;

                    case "c":
                        TalentShowDance(finchRobot);
                        break;

                    case "d":
                        TalentShowCombo(finchRobot);
                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light                             *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void TalentShowDisplayLight(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light Show");

            Console.WriteLine("\tThe light show will now begin!");
            DisplayContinuePrompt();

            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(500);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(500);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(500);
            finchRobot.setLED(255, 255, 0);
            finchRobot.wait(500);
            finchRobot.setLED(255, 0, 255);
            finchRobot.wait(500);
            finchRobot.setLED(0, 255, 255);
            finchRobot.wait(500);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(200);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(200);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(200);
            finchRobot.setLED(255, 255, 0);
            finchRobot.wait(200);
            finchRobot.setLED(255, 0, 255);
            finchRobot.wait(200);
            finchRobot.setLED(0, 255, 255);
            finchRobot.wait(200);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(100);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(100);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(100);
            finchRobot.setLED(255, 255, 0);
            finchRobot.wait(100);
            finchRobot.setLED(255, 0, 255);
            finchRobot.wait(100);
            finchRobot.setLED(0, 255, 255);
            finchRobot.wait(100);
            finchRobot.setLED(0, 0, 0);

            for (int redLevel = 0; redLevel < 200; redLevel++)
            {
                finchRobot.setLED(redLevel, 0, 0);
                finchRobot.wait(8);
            }

            for (int greenLevel = 0; greenLevel < 200; greenLevel++)
            {
                finchRobot.setLED(0, greenLevel, 0);
                finchRobot.wait(8);
            }

            for (int blueLevel = 0; blueLevel < 200; blueLevel++)
            {
                finchRobot.setLED(0, 0, blueLevel);
                finchRobot.wait(8);
            }

            for (int lightSoundLevel = 0; lightSoundLevel < 200; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.wait(8);
                //finchRobot.noteOn(lightSoundLevel * 100);
            }
            finchRobot.setLED(0, 0, 0);
            DisplayMenuPrompt("Talent Show Menu");
        }
        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Sound                             *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void TalentShowSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Singing Contest");

            Console.WriteLine("\tThe Finch robot will now sing \"Michael Row the Boat Ashore\" for you!");
            DisplayContinuePrompt();

            finchRobot.noteOn(261);
            finchRobot.wait(500);
            finchRobot.noteOn(330);
            finchRobot.wait(500);
            finchRobot.noteOn(392);
            finchRobot.wait(750);
            finchRobot.noteOn(330);
            finchRobot.wait(250);
            finchRobot.noteOn(392);
            finchRobot.wait(500);
            finchRobot.noteOn(440);
            finchRobot.wait(500);
            finchRobot.noteOn(392);
            finchRobot.wait(1000);
            finchRobot.noteOn(330);
            finchRobot.wait(500);
            finchRobot.noteOn(392);
            finchRobot.wait(500);
            finchRobot.noteOn(440);
            finchRobot.wait(1000);
            finchRobot.noteOn(392);
            finchRobot.wait(2000);

            finchRobot.noteOn(330);
            finchRobot.wait(500);
            finchRobot.noteOn(392);
            finchRobot.wait(1250);
            finchRobot.noteOn(330);
            finchRobot.wait(250);
            finchRobot.noteOn(349);
            finchRobot.wait(550);
            finchRobot.noteOn(330);
            finchRobot.wait(500);
            finchRobot.noteOn(294);
            finchRobot.wait(1000);
            finchRobot.noteOn(261);
            finchRobot.wait(500);
            finchRobot.noteOn(294);
            finchRobot.wait(500);
            finchRobot.noteOn(330);
            finchRobot.wait(1000);
            finchRobot.noteOn(294);
            finchRobot.wait(1000);
            finchRobot.noteOn(261);
            finchRobot.wait(1000);

            finchRobot.noteOff();

            DisplayMenuPrompt("Talent Show Menu");
        }
        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Dance                             *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void TalentShowDance(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Dancing Robot");

            Console.WriteLine("\tThe Finch robot will now dance for you!");
            DisplayContinuePrompt();

            //Back and Forth

            finchRobot.setMotors(150, 150);
            finchRobot.wait(1500);
            finchRobot.setMotors(-150, -150);
            finchRobot.wait(1500);
            
            //The Spin

            finchRobot.setMotors(150, 0);
            finchRobot.wait(3000);

            //The Waddle

            int waddle = 0;
            while (waddle < 10)
            {
                finchRobot.setMotors(60, 0);
                finchRobot.wait(333);
                finchRobot.setMotors(0, 60);
                finchRobot.wait(333);
                ++waddle;
            }

            //The Circle

            finchRobot.setMotors(200, 120);
            finchRobot.wait(3000);

            finchRobot.setMotors(0, 0);

            DisplayMenuPrompt("Talent Show Menu");
        }
        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Combo                            *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// 
        static void TalentShowCombo(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Combo Robot");

            Console.WriteLine("\tThe Finch robot will now combine its skills for you!");
            DisplayContinuePrompt();

            finchRobot.noteOn(330);
            finchRobot.setMotors(120, 20);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(2000);

            finchRobot.noteOn(350);
            finchRobot.setMotors(20, 120);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(2000);

            finchRobot.noteOn(392);
            finchRobot.setMotors(120, 120);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(2000);

            finchRobot.noteOn(523);
            finchRobot.setMotors(-120,-120);
            finchRobot.setLED(200, 200, 200);
            finchRobot.wait(4000);

            finchRobot.noteOff();
            finchRobot.setMotors(0, 0);
            finchRobot.setLED(0, 0, 0);

            DisplayMenuPrompt("Talent Show Menu");
        }


        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            // TODO test connection and provide user feedback - text, lights, sounds

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }

        #endregion

        #region DATA RECORDER

        static void DataRecorderMenuScreen(Finch finchRobot)
        {
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] temperatures = null;

            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Collect Data");
                Console.WriteLine("\td) Display Data");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderGetDataFrequency();
                        break;

                    case "c":
                        temperatures = DataRecorderGatherData(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;

                    case "d":
                        DataRecorderDisplayResults(temperatures);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);

        }

        private static void DataRecorderDisplayResults(double[] temperatures)
        {
            DisplayScreenHeader("Data Results (Celsius)");

            //display table headers

            Console.WriteLine(
                "Recording #".PadLeft(15) +
                "Temperature".PadLeft(15)
                );
            Console.WriteLine(
                "-----------".PadLeft(15) +
                "-----------".PadLeft(15)
                );

            //display table data in Celcius

            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine(
                    (index + 1).ToString().PadLeft(15) +
                    temperatures[index].ToString("n2").PadLeft(15));
            }

            DisplayContinuePrompt();

            DisplayScreenHeader("Data Results (Fahrenheit)");

            //display table headers

            Console.WriteLine(
                "Recording #".PadLeft(15) +
                "Temperature".PadLeft(15)
                );
            Console.WriteLine(
                "-----------".PadLeft(15) +
                "-----------".PadLeft(15)
                );

            //display table data in Fahrenheit

            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine(
                    (index + 1).ToString().PadLeft(15) +
                    (temperatures[index] * 9 / 5 + 32).ToString("n2").PadLeft(15));
            }

            DisplayContinuePrompt();

            DisplayScreenHeader("Sums and Averages");
            Console.WriteLine();

            //display sums

            double sum = 0;
            Array.Sort(temperatures);

            for (int index = 0; index < temperatures.Length; index++)
            {
                sum += temperatures[index];
            }

            Console.WriteLine("The sum of the temperatures in Celsius is {0}.", sum);
            Console.WriteLine();

            DisplayContinuePrompt();
            Console.WriteLine();

            double fsum = 0;

            for (int index = 0; index < temperatures.Length; index++)
            {
                fsum += (temperatures[index]*9/5+32);
            }

            Console.WriteLine("The sum of the temperatures in Fahrenheit is {0}.", fsum);
            Console.WriteLine();

            DisplayContinuePrompt();
            Console.WriteLine();

            //display averages

            Console.WriteLine("The average of the temperatures in Celsius is {0} degrees.", (sum / temperatures.Length));
            Console.WriteLine();

            DisplayContinuePrompt();
            Console.WriteLine();

            Console.WriteLine("The average of the temperatures in Fahrenheit is {0} degrees.", (fsum / temperatures.Length));
            Console.WriteLine();

            DisplayContinuePrompt();

        }



        private static double[] DataRecorderGatherData(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] temperatures = new double[numberOfDataPoints];
            int[] light = new int[numberOfDataPoints];


            DisplayScreenHeader("Gather Data");

            Console.WriteLine($"Number of data points: {numberOfDataPoints}");
            Console.WriteLine($"Data point frequency: {dataPointFrequency} seconds");
            Console.WriteLine();
            Console.WriteLine("The Finch robot is now ready to gather temperature data.");
            Console.WriteLine();
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                temperatures[index] = finchRobot.getTemperature();
                Console.WriteLine($"Reading {index + 1}: {temperatures[index].ToString("n2")}");
                int waitSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitSeconds);
            }

            DisplayContinuePrompt();

            Console.WriteLine();
            Console.WriteLine("The Finch robot is now ready to gather light data.");
            Console.WriteLine();
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                light[index] = finchRobot.getLeftLightSensor();
                Console.WriteLine($"Reading {index + 1}: {light[index].ToString("n2")}");
                int waitSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitSeconds);
            }

            DisplayContinuePrompt();

            return temperatures;
        }

        private static double DataRecorderGetDataFrequency()
        {
            double dataPointFrequency;
            string userResponse;
            bool validResponse = false;



            DisplayScreenHeader("Data Point Frequency");

            do

            {

                Console.Write("Enter desired frequency of data points: ");
                userResponse = Console.ReadLine();

                if (double.TryParse(userResponse, out dataPointFrequency))
                {
                    if (dataPointFrequency <= 0)
                    {
                        Console.WriteLine("That does not compute. Please enter a positive number.");
                    }
                    else
                    {
                        validResponse = true;
                    }
                }
                else
                {
                    Console.WriteLine("Please provide a number.");
                }



            } while (!validResponse);

            Console.WriteLine($"You have chosen a frequency of {dataPointFrequency} seconds.");

            DisplayContinuePrompt();

            return dataPointFrequency;
        }

        private static int DataRecorderGetNumberOfDataPoints()
        {
            int numberOfDataPoints;
            string userResponse;
            bool validResponse = false;



                DisplayScreenHeader("Number of Data Points");

            do

            {

                Console.Write("Enter desired number of data points: ");
                userResponse = Console.ReadLine();

                if (int.TryParse(userResponse, out numberOfDataPoints))
                {
                    if (numberOfDataPoints <= 0)
                    {
                        Console.WriteLine("That does not compute. Please enter a positive number.");
                    }
                    else
                    {
                        validResponse = true;
                    }
                }
                else
                {
                    Console.WriteLine("Please provide a number.");
                }



            } while (!validResponse);

            Console.WriteLine($"You have chosen {numberOfDataPoints} data points.");

            DisplayContinuePrompt();

            return numberOfDataPoints;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
