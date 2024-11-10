using System.Net;
using System.Net.Sockets;
using System.Text;
using NLog;
using Smart.Server.Lib.Config;

var logger = LogManager.GetCurrentClassLogger();

logger.Info("Старт сервера");

IpConfig? config;
try
{
    config = IpConfig.Load();
    if (config is null)
        throw new ConfigException(null);
}
catch (Exception ex)
{
    logger.Error(ex, ex.Message);
    return -1;
}

var broadcastAddress = IPAddress.Parse(config.Ip);
using var udpSender = new UdpClient();

logger.Info($"Отправляем сообщение на адрес {broadcastAddress}");
logger.Debug($"Отправляем сообщение на адрес {broadcastAddress}");


return 0;

void Send(string message)
{
    var data = Encoding.UTF8.GetBytes(message);
    udpSender.Send(data, new IPEndPoint(broadcastAddress, 8001));
}