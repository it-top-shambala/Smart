using Smart.Server.Lib.Config;

namespace Smart.Server.Lib.Test;

public class IpConfigTest
{
    [Fact]
    public void Load_PositiveTest()
    {
        var expected = new IpConfig()
        {
            Ip = "235.5.5.11",
            Port = 8001
        };
        
        var actual = IpConfig.Load();
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Load_NotFoundFile_NegativeTest()
    {
        Assert.Throws<FileConfigException>(() => IpConfig.Load("not_found_file.json"));
    }

    [Fact]
    public void Load_BadFile_NegativeTest()
    {
        Assert.Throws<ConfigException>(() => IpConfig.Load("ipconfig_bad.json"));
    }
}