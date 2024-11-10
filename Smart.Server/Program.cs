using System.Reactive.Linq;
using NLog;
using Smart.Server.Lib;
using Smart.Server.Lib.Config;

var logger = LogManager.GetCurrentClassLogger();

const string filePath = @"C:\temp\data.dsv"; //FIXME Переместить в конфиг
var oldStrings = new List<string>();

logger.Info("Старт сервера");

IpConfig? config;
try
{
    config = IpConfig.Load();
    logger.Info("Загрузка конфига");
    if (config is null)
        throw new ConfigException(null);
}
catch (Exception ex)
{
    logger.Error(ex, ex.Message);
    return -1;
}

var udpMulticast = new UdpMulticast(config.Ip, config.Port);

var ticks = Observable.Timer(
    dueTime: TimeSpan.Zero,
    period: TimeSpan.FromSeconds(20));

ticks.Subscribe(
    _ =>
    {
        var newStrings = File.ReadLines(filePath).ToList();
        logger.Trace("Получение данных из файла");

        var diffStrings = newStrings
            .Where(s => !oldStrings.Contains(s))
            .ToList();
        logger.Trace("Выделение новых данных");
        
        oldStrings = newStrings;

        foreach (var s in diffStrings)
        {
            udpMulticast.Send(s);
            logger.Trace($"Отправка строки {s}");
        }
    });

Console.ReadKey();
return 0;