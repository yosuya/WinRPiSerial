using System;
using WinRPiSerial;

namespace Test
{
    class Test
    {
        static bool isConnected = false;
        static WinSerial serial = null;

        static void Main(string[] args)
        {
            string[] port = WinSerial.GetPortNames();
            string tgtPortName = String.Empty;

            switch (port.Length)
            {
                case 0:
                    return;

                case 1:
                    tgtPortName = port[0];
                    break;
                default:
                    while (true)
                    {
                        Console.WriteLine("");
                        for (int idx = 0; idx < port.Length; idx++)
                        {
                            Console.WriteLine($"{idx}: {port[idx]}");
                        }
                        Console.Write("SelectPort [number] >>> ");
                        string input = Console.ReadLine();

                        int num = 0;
                        bool parseOK = int.TryParse(input, out num);
                        if (parseOK)
                        {
                            if (num >= 0 && num < port.Length)
                            {
                                tgtPortName = port[num];
                                break;
                            }
                        }
                    }
                    break;
            }


            Console.WriteLine("----- Start -----");

            serial = new WinSerial(tgtPortName);
            serial.Open();
            serial.SetCallback(Callback);

            if (!isConnected)
            {
                serial.Write("hello");
                Console.WriteLine("Waiting for a message from RPi");
            }

            while (!isConnected) { } //応答待ち

            Console.WriteLine("[OK] RPi -> PC");
            serial.Close();

            Console.WriteLine("----- End -----");
        }

        static void Callback(string text)
        {
            Console.WriteLine($"<From RPi> {text}");

            if (!isConnected && text.Contains("hello"))
            {
                isConnected = true;
                serial.Write("hello");
            }
        }
    }
}
