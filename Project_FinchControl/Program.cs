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
    // Last Modified: 6/4/21
    //
    // **************************************************

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

                        break;

                    case "d":

                        break;

                    case "e":

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
        /// *               Talent Show > Dance                             *
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
