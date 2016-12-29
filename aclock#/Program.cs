//
//  Author:
//    Natalia Portillo claunia@claunia.com
//
//  Copyright (c) 2016, © Claunia.com
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//     * Neither the name of the [ORGANIZATION] nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
//  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
using System;
using System.Threading;

namespace aclock_sharp
{
    class MainClass
    {
        const int FontWH_Ratio = 2;

        static void DrawCircle(int handMax, int sYcen, int sXcen)
        {
            int x, y, r;
            char c;

            for(r = 0; r < 60; r++)
            {
                x = (int)(Math.Cos(r * Math.PI / 180 * 6) * handMax * FontWH_Ratio + sXcen);
                y = (int)(Math.Sin(r * Math.PI / 180 * 6) * handMax + sYcen);
                switch(r)
                {
                    case 0:
                    case 5:
                    case 10:
                    case 15:
                    case 20:
                    case 25:
                    case 30:
                    case 35:
                    case 40:
                    case 45:
                    case 50:
                    case 55:
                        c = 'o';
                        break;
                    default:
                        c = '.';
                        break;
                }
                Console.SetCursorPosition(x, y);
                Console.Write(c);
            }
        }

        static void DrawHand(int minute, int hlength, char c, int sXcen, int sYcen)
        {
            int x, y, n;
            double r = (minute - 15) * (Math.PI / 180) * 6;

            for(n = 1; n < hlength; n++)
            {
                x = (int)(Math.Cos(r) * n * FontWH_Ratio + sXcen);
                y = (int)(Math.Sin(r) * n + sYcen);
                Console.SetCursorPosition(x, y);
                Console.Write(c);
            }
        }

        static void PrintCopyr()
        {
            Console.Clear();
            Console.WriteLine("Copyright (c) 1994-2013 Antoni Sawicki <as@tenoware.com>");
            Console.WriteLine("Copyright (c) 2016 Natalia Portillo <claunia@claunia.com>");
            Console.WriteLine("Version 2.4 (.net); Canary Islands, December 2016");
        }

        static bool KeepRunning = true;

        public static void Main()
        {
            PrintCopyr();
            Thread.Sleep(2000);

            int sXmax, sYmax, smax, handMax, sXcen, sYcen;
            DateTime ltime;

            sXmax = sYmax = handMax = sXcen = sYcen = 0;

            Console.Clear();
            Console.CursorVisible = false;
            Console.Title = ".:ACLOCK:.";

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = false;
                KeepRunning = false;
            };

            while(true)
            {
                if(!KeepRunning)
                    break;

                ltime = DateTime.Now;
                sXmax = Console.WindowWidth;
                sYmax = Console.WindowHeight;

                if(sXmax / FontWH_Ratio <= sYmax)
                    smax = sXmax / FontWH_Ratio;
                else
                    smax = sYmax;

                handMax = (smax / 2) - 1;

                sXcen = sXmax / 2;
                sYcen = sYmax / 2;

                Console.Clear();
                DrawCircle(handMax, sYcen, sXcen);

                DrawHand((ltime.Hour * 5) + (ltime.Minute / 10), 2 * handMax / 3, 'h', sXcen, sYcen);
                DrawHand(ltime.Minute, handMax - 2, 'm', sXcen, sYcen);
                DrawHand(ltime.Second, handMax - 1, '.', sXcen, sYcen);
                Console.SetCursorPosition(sXcen - 5, sYcen - (3 * handMax / 5));
                Console.Write(".:ACLOCK:.");
                Console.SetCursorPosition(sXcen - 5, sYcen + (3 * handMax / 5));
                Console.Write("{0:D2}:{1:D2}:{2:D2}", ltime.Hour, ltime.Minute, ltime.Second);

                Thread.Sleep(1000);
            }

            PrintCopyr();
            Console.CursorVisible = true;
            Console.Title = null;
        }
    }
}
