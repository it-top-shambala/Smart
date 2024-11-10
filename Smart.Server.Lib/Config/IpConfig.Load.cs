using System.Text.Json;
using NLog;

namespace Smart.Server.Lib.Config;

public partial class IpConfig
{
    public static IpConfig? Load(string path = "ipconfig.json")
    {
        var logger = LogManager.GetCurrentClassLogger();
        
        string? json = null;
        try
        {
            json = File.ReadAllText(path);
        }
        catch (Exception e)
        {
            logger.Error(e, e.Message);
            throw new FileConfigException(path, e);
        }

        try
        {
            return JsonSerializer.Deserialize<IpConfig>(json);
        }
        catch (Exception e)
        {
            logger.Error(e, e.Message);
            throw new ConfigException(e);
        }
    }
}