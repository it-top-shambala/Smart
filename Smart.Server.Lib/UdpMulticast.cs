using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Smart.Server.Lib;

public class UdpMulticast
{
    private readonly IPAddress _broadcastAddress;
    private readonly int _port;
    private readonly UdpClient _udpSender;

    public UdpMulticast(string ip, int port)
    {
        _broadcastAddress = IPAddress.Parse(ip);
        _port = port;
        
        _udpSender = new UdpClient();
    }
    
    public void Send(string message)
    {
        var data = Encoding.UTF8.GetBytes(message);
        _udpSender.Send(data, new IPEndPoint(_broadcastAddress, _port));
    }
}