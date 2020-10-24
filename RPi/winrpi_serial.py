import serial
import serial.tools.list_ports as ports
import threading

class RPiSerial():
    def __init__(self):
        self.__serial = None
        self.__eventhandler = None 

    #シリアルポートを開く
    def open(self, port_name='/dev/ttyS0', baud_rate=115200):
        if self.__serial == None:
            try:
                self.__serial = serial.Serial(port_name, baud_rate, timeout=None)
                
                #受信ハンドラの設定
                self.__eventhandler = threading.Thread(target=self.__receive_eventhandler)
                self.__eventhandler.setDaemon(True)
                self.__eventhandler.start()

            except Exception as e:
                print(e)
            
    #シリアルポートを閉じる
    def close(self):
        if self.__serial == None or self.__serial.isOpen() == False: return
        
        self.__serial.close()

    #送信
    def write(self, text):
        if self.__serial == None or self.__serial.isOpen() == False: return

        if text[-1] != '\n': text += '\n'

        try:
            self.__serial.write(bytes(text, 'UTF-8'))
        except Exception as e:
            print(e)

    #シリアルポートがオープンしているか
    def isOpen(self):
        if self.__serial == None: return False
        else: return self.__serial.isOpen()

    #接続されているデバイスを返す
    @staticmethod
    def get_ports():
        coms = ports.comports()
        port_list = []
        for com in coms:
            port_list.append(com.device)
        return port_list


    #データを受信したときに実行する関数を設定
    def set_callback(self, func):
        self.__callback = func


    #データの受信をハンドルする
    def __receive_eventhandler(self):
        while self.__serial.isOpen():
            buf = self.__serial.readline()
            if self.__callback != None:
                self.__callback(buf)
