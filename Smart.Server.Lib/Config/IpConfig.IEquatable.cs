namespace Smart.Server.Lib.Config;

public partial class IpConfig : IEquatable<IpConfig>
{
    public bool Equals(IpConfig? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Ip == other.Ip && Port == other.Port;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((IpConfig)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Ip, Port);
    }
}