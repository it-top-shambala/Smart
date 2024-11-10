using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Smart.Client;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        using var udpClient = new UdpClient(8001);
        var broadcastAddress = IPAddress.Parse("235.5.5.11");
        udpClient.JoinMulticastGroup(broadcastAddress);

        var times = new List<double>();
        var temperatures = new List<int>();
        
        while (true)
        {
            var result = await udpClient.ReceiveAsync();
            var message = Encoding.UTF8.GetString(result.Buffer);

            var time = double.Parse(message[..message.IndexOf('|')]);
            var temperature = int.Parse(message[(message.IndexOf('|') + 1)..]);
            
            times.Add(time);
            temperatures.Add(temperature);
            
            Plot.Plot.Add.Scatter(times, temperatures);
            Plot.Refresh();
        }
    }
}