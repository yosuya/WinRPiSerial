# WinRPiSerial
Serial communication between Windows PC and Raspberry Pi  
Windows PC と Raspberry Pi 間のシリアル通信

System requirements
-------------------
**Cable**

RaspberryPi と PC を接続するケーブルが必要です。


**PC**

 ```.NET Core```の場合は```System.IO.Ports```アセンブリへの参照の追加(=NuGetパッケージのインストール)が必要です。

**Raspberry Pi**

 ```pySerial```パッケージが必要です。  
 インストールされていない場合は```pip install pyserial```等でインストールしてください。



Test environment
-------------------
**Raspberry Pi 3 Model B (GPIO Pins) - PC (USB Type-A Port)**

**必要物**
- USB Type-A to Micro USB Type-B
- USBシリアル変換モジュール FT-232RQ (https://akizukidenshi.com/catalog/g/gM-11007/)
- ジャンパーワイヤー（オス-メス）

**USBシリアル変換モジュールのスイッチ状態**

| SW1 | SW2 |
| --- | --- |
| OFF (LED消灯) | OFF (TTL=3.3V) |

**USBシリアル変換モジュールとRaspberry Pi の結線**

| 変換モジュール | Raspberry Pi |
| --- | --- |
| TXD | GPIO15(UART RXD) (pin 10) |
| RXD | GPIO14(UART TXD) (pin 08) |
| GND | GND (pin 14 ) |

**PC**
1. Visual Studio 2019 で コンソールアプリ(.NET Core) プロジェクトを作成
1. ```System.IO.Ports```アセンブリへの参照の追加(=NuGetパッケージのインストール)
1. Test.cs と WinRPiSerial.cs を配置
1. Test.cs 内の Mainメソッド を実行

**Raspberry Pi**
1. ```pip install pyserial```でpySerialをインストール
1. Preference > Raspberry Pi Configuration > Interfaces > Serial Port: を Enable に設定
1. test.py と winrpi_serial.py を同ディレクトリに配置
1. test.py を実行する
