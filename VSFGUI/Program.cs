using Avalonia;
using System;
using System.Net;
using Rug.Osc;

namespace VSFGUI
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
            
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                
                .LogToTrace();

        private static OscSender _sender;
        public static OscSender Sender
        {
            get
            {
                if (_sender.Port == 0)
                {
                    _sender = new OscSender(IPAddress.Any, 1234);
                }

                return _sender;
            }
            private set => _sender = value;
        }

        private static OscReceiver _receiver;
        public OscReceiver Receiver
        {
            get
            {
                if (_receiver.Port == 0)
                {
                    _receiver = new OscReceiver(IPAddress.Any, 1234);
                }

                return _receiver;
            }
            private set => _receiver = value;
        }
    }
}