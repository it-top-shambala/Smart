namespace Smart.Server.Lib.Config;

public class FileConfigException : Exception
{
    public FileConfigException(string path, Exception? innerException) 
        : base($"Ошибка чтения файла {path}", innerException)
    { }
}

public class ConfigException : Exception
{
    public ConfigException(Exception? innerException) 
        : base($"Ошибка получения конфигурации", innerException)
    { }
}