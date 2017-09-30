using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;
using System.Threading.Tasks;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x412에 나와 있습니다.

namespace IOTCore_Blink
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const byte ledPin = 5;
        private GpioPin pin;

        private UInt16 duty = 1000; 
        
        public MainPage()
        {
            this.InitializeComponent();

            setup();

            loop();
        }

        public void setup()
        {
            var gpio = GpioController.GetDefault();

            if (gpio == null)
            {
                pin = null;
                return;
            }

            pin = gpio.OpenPin(ledPin);
            pin.SetDriveMode(GpioPinDriveMode.Output);
            pin.Write(GpioPinValue.High);
        }

        public async void loop()
        {
            while (true)
            {
                pin.Write(GpioPinValue.Low);
                await Task.Delay(duty);
                pin.Write(GpioPinValue.High);
                await Task.Delay(duty);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            duty = UInt16.Parse( DutyBox.Text);
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    pin.Write(GpioPinValue.Low);
        //}

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    pin.Write(GpioPinValue.High);
        //}
    }
}
