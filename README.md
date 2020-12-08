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
**(Visual Studio 2019 を使用したプロジェクトの作成例)**
1. 新しいプロジェクトの作成からコンソールアプリ(.NET Core) のプロジェクトを作成  
1. ```System.IO.Ports```アセンブリへの参照の追加(=NuGetパッケージのインストール)  
    1. メニューバーから [プロジェクト]>[NuGet パッケージの管理] をクリック  
    1. 上の方にあるタブを"インストール済み"から"参照"に変更  
    1. 検索欄に```System.IO.Ports```を入力し、Microsoft作成のパッケージをインストールする  
1. Test.cs と WinRPiSerial.cs を配置  
    1. 自動生成されるProgram.csを削除する（ここにTest.cs内のコードを書いても良い）  
    1. ソリューションエクスプローラーの ”プロジェクト名”（作成時に自分で決めた名前） を右クリックして [追加]>[既存の項目] をクリックする  
    1. 本リポジトリの”PC”フォルダ内にあるTest.csとWinRPiSerial.csを追加する  
1. Test.cs 内の Mainメソッド を実行  
    1. 実行ボタンをクリックするかF5キーを押す。  

**(Visual Studio Code (vscode)を使用したプロジェクトの作成例)**  
[.NET Core Consoleアプリケーションの作成方法](https://docs.microsoft.com/ja-jp/dotnet/core/tutorials/with-visual-studio-code)を参照  
1. 環境構築  
    1. [.NET SDK](https://dotnet.microsoft.com/download) をインストールする。(.NET 5.0 (recommended))  
    1. vscodeを開いて [C#拡張機能](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)をインストールする  
1. .NETコンソールアプリケーションの作成  
    1. [表示]>[ターミナル]を選択してターミナルを開き、コマンド ```dotnet new console -o Test``` を入力する  
    1. "Test"フォルダに移動する （コマンド ```cd Test``` を入力)  
    1. 自動生成された'''Program.cs'''を削除する
    1. 本リポジトリの”PC”フォルダ内にあるTest.csとWinRPiSerial.csをこのディレクトリに追加する  
1. アセンブリ参照の追加  
    1. vscodeのターミナルで'''dotnet add package System.IO.Ports''' でパッケージをインストール  
1. アプリケーションの実行  
    1. ```dotnet run```でアプリケーションを実行する
  
  
**Raspberry Pi**  
1. 必要なライブラリのインストール  
    1. ```pip install pyserial```でpySerialをインストール  
1. Raspberry Pi の設定の変更  
    1. [Preference] > [Raspberry Pi Configuration] > [Interfaces] > [Serial Port:] を Enable に設定  
1. アプリケーションの作成  
    1. 任意のディレクトリを作成し、test.py と winrpi_serial.py を同ディレクトリに配置  
1. test.py を実行する  
