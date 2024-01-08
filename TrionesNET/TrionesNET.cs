using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace TrionesNET
{
    public class TrionesNET
    {
        BluetoothLENet.BLE bluetoothLENet;

        private bool isOn;

        public TrionesNET(string deviceName)
        {
            bluetoothLENet = new BluetoothLENet.BLE();
            DeviceName = deviceName;
        }

        public string DeviceName { get; private set; }

        public async Task<BluetoothLENet.ConnectDeviceResult> Connect()
        {          
            bluetoothLENet.StartScanning();
            var rst = await bluetoothLENet.ConnectDevice(DeviceName);
            
            if (rst != BluetoothLENet.ConnectDeviceResult.Ok) 
                throw new Exception();

            return rst;
        }

        public  void Disconnect()
        {
            //bluetoothLENet.DisconnectDevice();
        }

        public async Task TurnOn()
        {
            var rst = await bluetoothLENet.WriteCharacteristic("65493", "65497", "CC 23 33");
            if (rst != BluetoothLENet.BLE.WriteCharacteristicResult.Write_Success) throw new Exception();
            isOn = true;
        }

        public async Task TurnOff()
        {
            var rst = await bluetoothLENet.WriteCharacteristic("65493", "65497", "CC 24 33");
            if(rst != BluetoothLENet.BLE.WriteCharacteristicResult.Write_Success) throw new Exception();
            isOn = false;
        }

        public async Task SetWhite(byte brightness)
        {
            if (isOn == false) await TurnOn();
            var rst = await bluetoothLENet.WriteCharacteristic("65493", "65497", $"56 00 00 00 {brightness:X} 0F AA");
            if (rst != BluetoothLENet.BLE.WriteCharacteristicResult.Write_Success) throw new Exception();
        }

        public async Task SetColor(Color color)
        {
            if (isOn == false) await TurnOn();
            var rst = await bluetoothLENet.WriteCharacteristic("65493", "65497", $"56 {color.Red:X} {color.Green:X} {color.Blue:X} 00 F0 AA");
            if(rst != BluetoothLENet.BLE.WriteCharacteristicResult.Write_Success) throw new Exception();
        }

        /// <summary>
        /// SetBuiltInMode
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="speed">Some operational modes take a speed parameter that controls how fast the colors are changed. 0x01 is the fastest, 0xFF is the slowest.</param>
        /// <returns></returns>
        public async Task SetPresetPatternAsync(BuiltInMode mode, byte speed)
        {
            if (isOn == false) await TurnOn();
            var rst = await bluetoothLENet.WriteCharacteristic("65493", "65497", $"BB {mode:X} {speed:X} 44");
            if (rst != BluetoothLENet.BLE.WriteCharacteristicResult.Write_Success) throw new Exception();
        }
    }
}