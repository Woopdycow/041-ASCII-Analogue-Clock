using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ASCII_Analogue_Clock {
    class Program {
        // ratios used in rad to deg conversion
        const double HALF_RADIAN = Math.PI / 2.0;
        const double THIRTIETH_RADIAN = Math.PI / 30.0;

        static void Main(string[] args) {

            //coords of clock frame
            bool[,] clockCirclePositions = new bool[40, 80];

            bool start = true;
            double hours;
            double minutes;
            double seconds;
            string timeString;

            double circleFunction;

            Console.SetWindowSize(83,42);
            Console.ForegroundColor = ConsoleColor.DarkCyan;


            //uses a circle graph function to select which coords are the clock frame
            for (double i = 0; i < 40; i++) {
                for (double j = 0; j < 80; j++) {
                    circleFunction = (Math.Pow(i - 20, 2) + Math.Pow(((j - 40) / 2), 2));
                    if (circleFunction >= 320.00 && circleFunction <= 380.00) {
                        Console.SetCursorPosition(Convert.ToInt32(j) + 2, Convert.ToInt32(i) + 1);
                        Console.Write("\b#");
                    }
                }
            }//end method

            DrawFace(clockCirclePositions);

            //perpetual while loop sleeping for a second every loop
            while (start) {
                ClearFace(clockCirclePositions);
                //sets time variables to current system times
                seconds = DateTime.Now.Second;
                minutes = DateTime.Now.Minute;
                hours = DateTime.Now.Hour;
                
                timeString = DateTime.Now.ToString("hh:mm:ss tt");
                //reduces 24-hour value to a number between 0 and 11
                hours = hours % 12;

                DrawHands(seconds, minutes, hours, timeString);

                Console.SetCursorPosition(0, 0);
                Thread.Sleep(1000);
            }
        }//end method

        static void DrawFace(bool[,] clockCirclePositions) {
            //clock number positions in lazy format (looks better than calculating the positions with a formula)
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(58, 6);
            Console.Write("01");
            Console.SetCursorPosition(69, 12);
            Console.Write("02");
            Console.SetCursorPosition(74, 21);
            Console.Write("03");
            Console.SetCursorPosition(70, 29);
            Console.Write("04");
            Console.SetCursorPosition(58, 36);
            Console.Write("05");
            Console.SetCursorPosition(41, 38);
            Console.Write("06");
            Console.SetCursorPosition(24, 36);
            Console.Write("07");
            Console.SetCursorPosition(11, 29);
            Console.Write("08");
            Console.SetCursorPosition(7, 21);
            Console.Write("09");
            Console.SetCursorPosition(12, 12);
            Console.Write("10");
            Console.SetCursorPosition(24, 6);
            Console.Write("11");
            Console.SetCursorPosition(41, 4);
            Console.Write("12"); 
        }//end method

        static void DrawHands(double seconds, double minutes, double hours, string timeString) {
            double angle = 0.0;
            int row = 0;
            int column = 0;

            //second hand is drawn
            angle = seconds * THIRTIETH_RADIAN - HALF_RADIAN;
            for (double i = 0; i < 30; i++) {
                double rowD = Math.Floor(i * Math.Cos(angle)) + 42;
                double columnD = Math.Floor(i/2 * Math.Sin(angle)) + 21;
                row = Convert.ToInt32(rowD);
                column = Convert.ToInt32(columnD);
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(row, column);
                Console.Write("o");
            }
            //minute hand is drawn
            angle = minutes * THIRTIETH_RADIAN - HALF_RADIAN;
            for (double i = 0; i < 24; i++) {
                double rowD = Math.Floor(i * Math.Cos(angle)) + 42;
                double columnD = Math.Floor(i / 2 * Math.Sin(angle)) + 21;
                row = Convert.ToInt32(rowD);
                column = Convert.ToInt32(columnD);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(row, column);
                Console.Write("o");
            }
            //hour hand is drawn
            angle = (hours + (minutes/60.0)) * 5 * THIRTIETH_RADIAN - HALF_RADIAN;
            for (double i = 0; i < 16; i++) {
                double rowD = Math.Floor(i * Math.Cos(angle)) + 42;
                double columnD = Math.Floor(i / 2 * Math.Sin(angle)) + 21;
                row = Convert.ToInt32(rowD);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                column = Convert.ToInt32(columnD);
                Console.SetCursorPosition(row, column);
                Console.Write("o");
            }
            //fancy frame, digital time, and centre pin locations
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(40,21);
            Console.Write("╔═╗");
            Console.SetCursorPosition(40, 22);
            Console.Write("╚═╝");
            Console.SetCursorPosition(35, 1);
            Console.Write("╔═══════════╗");
            Console.SetCursorPosition(35, 2);
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(timeString);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("║");
            Console.SetCursorPosition(35, 3);
            Console.Write("╚═══════════╝");



        }//end method

        static void ClearFace(bool[,] clockCirclePositions) {
            //designates positions for character removal and performs each second
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(0, 0);
            for (int i = 1; i < 41; i++) {
                for (int j = 2; j < 82; j++) {
                    if (!clockCirclePositions[i - 1, j - 2] && i != 1) {
                        double circleFunction = (Math.Pow(i - 20, 2) + Math.Pow(((j - 40) / 2), 2));
                        if (circleFunction <= 235.00) {
                            Console.SetCursorPosition(j+1, i+1);
                            Console.Write(".");
                        } else {
                            
                        }                        
                    }
                }
            }
        }//end method
    }
}


