using System;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WinRPiSerial
{
    class WinSerial
    {
        private SerialPort serialPort = null;

        public WinSerial(string portName, int baudRate = 115200)
        {
            serialPort = new SerialPort
            {
                PortName = portName,
                BaudRate = baudRate,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                Encoding = Encoding.UTF8
            };
        }


        /// <summary>
        /// シリアルポートを開ける
        /// </summary>
        public void Open()
        {
            if (serialPort == null || serialPort.IsOpen) return;

            //データを受け取ったときのハンドラを設定
            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

            //接続を試みる
            try
            {
                //シリアルポートを開ける
                serialPort.Open();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
            }
        }


        /// <summary>
        /// シリアルポートを閉じる
        /// </summary>
        public void Close()
        {
            if (serialPort == null || !serialPort.IsOpen) return;

            serialPort.Close();
            serialPort = null;
        }


        /// <summary>
        /// シリアルポートの送信バッファに送信データを書き込む
        /// </summary>
        public void Write(string text)
        {
            if (serialPort == null || !serialPort.IsOpen) return;

            if (!text.EndsWith("\n")) text += "\n";
            serialPort.Write(text);
        }


        /// <summary>
        /// 認識できるCOMポート名を返す
        /// </summary>
        /// <returns>COMポート名のリスト</returns>
        public static string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }


        /// <summary>
        /// シリアルポートからデータを受信した時に実行するメソッドを設定する
        /// </summary>
        public void SetCallback(DataReceivedMethod method)
        {
            callbackMethod = method;
        }
        public delegate void DataReceivedMethod(string data);

        private DataReceivedMethod callbackMethod;


        /// <summary>
        /// シリアルポートからデータを受信したときの処理
        /// </summary>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort == null || !serialPort.IsOpen) return;

            try
            {
                string data = serialPort.ReadLine();
                callbackMethod?.Invoke(data); //設定したメソッドを実行
            }
            catch
            {
                Console.WriteLine($"[Error] cannot connect {serialPort.PortName}");
            }
        }
    }
}
