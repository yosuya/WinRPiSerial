import winrpi_serial

class Test():
    def __init__(self):
        self.isConnected = False
        self.requireExit = False

    def main(self):
        print('----- Start -----')

        self.serial = winrpi_serial.RPiSerial()
        self.serial.open()
        self.serial.set_callback(self.callback)

        if self.isConnected == False:
            self.serial.write('hello')
            print('Waiting for a message from PC')

        while (self.isConnected == False): pass #応答待ち

        print('[OK] PC -> RPi')
        self.serial.close()

        print('----- End -----')
        

    def callback(self, data):
        print('<From PC> ' + data.decode())

        if self.isConnected == False and ('hello' in data.decode()):
            self.serial.write('hello')
            self.isConnected = True



if __name__ == '__main__':
    test = Test()
    test.main()
