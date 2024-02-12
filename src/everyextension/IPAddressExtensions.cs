using System.Net;

namespace EveryExtension;

public static class IPAddressExtensions
{
    public static bool IsLocal(this IPAddress ipAddress)
    {
        var bytes = ipAddress.GetAddressBytes();
        return bytes[0] == 127;
    }

    public static bool IsIPv4(this IPAddress ipAddress)
        => ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;

    public static bool IsIPv6(this IPAddress ipAddress)
        => ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6;

    public static bool IsPrivate(this IPAddress ipAddress)
    {
        var bytes = ipAddress.GetAddressBytes();
        return (bytes[0] == 10) ||
               (bytes[0] == 172 && (bytes[1] >= 16 && bytes[1] <= 31)) ||
               (bytes[0] == 192 && bytes[1] == 168);
    }

    public static long ToLong(this IPAddress ipAddress)
    {
        var bytes = ipAddress.GetAddressBytes();
        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);
        return BitConverter.ToInt64(bytes, 0);
    }

    public static string ToBinaryString(this IPAddress ipAddress)
    {
        var bytes = ipAddress.GetAddressBytes();
        return string.Join(".", bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
    }

    public static string ToShortString(this IPAddress ipAddress)
        => ipAddress.ToString().Split('%')[0];

    public static string GetHostName(this IPAddress ipAddress)
    {
        try
        {
            var hostEntry = Dns.GetHostEntry(ipAddress);
            return hostEntry.HostName;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public static bool IsMulticast(this IPAddress ipAddress)
    {
        var bytes = ipAddress.GetAddressBytes();
        return bytes[0] >= 224 && bytes[0] <= 239;
    }

    public static bool IsInRange(this IPAddress ipAddress, IPAddress startRange, IPAddress endRange)
    {
        var ipAddressBytes = ipAddress.GetAddressBytes();
        var startRangeBytes = startRange.GetAddressBytes();
        var endRangeBytes = endRange.GetAddressBytes();

        for (int i = 0; i < ipAddressBytes.Length; i++)
        {
            if (ipAddressBytes[i] < startRangeBytes[i] || ipAddressBytes[i] > endRangeBytes[i])
                return false;
        }
        return true;
    }

    public static bool IsLocalhost(this IPAddress ipAddress)
        => IPAddress.IsLoopback(ipAddress);

    public static bool IsLinkLocal(this IPAddress ipAddress)
    {
        var bytes = ipAddress.GetAddressBytes();
        return bytes[0] == 169 && bytes[1] == 254;
    }

    public static IPAddress GetNetworkAddress(this IPAddress ipAddress, IPAddress subnetMask)
    {
        var ipBytes = ipAddress.GetAddressBytes();
        var maskBytes = subnetMask.GetAddressBytes();
        var networkBytes = new byte[ipBytes.Length];
        for (int i = 0; i < ipBytes.Length; i++)
            networkBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
        return new IPAddress(networkBytes);
    }

    public static IPAddress GetBroadcastAddress(this IPAddress ipAddress, IPAddress subnetMask)
    {
        var ipBytes = ipAddress.GetAddressBytes();
        var maskBytes = subnetMask.GetAddressBytes();
        var broadcastBytes = new byte[ipBytes.Length];
        for (int i = 0; i < ipBytes.Length; i++)
            broadcastBytes[i] = (byte)(ipBytes[i] | ~maskBytes[i]);
        return new IPAddress(broadcastBytes);
    }

    public static bool SubnetContains(this IPAddress subnetAddress, IPAddress subnetMask, IPAddress ipAddress)
    {
        var networkAddress = subnetAddress.GetNetworkAddress(subnetMask);
        var broadcastAddress = subnetAddress.GetBroadcastAddress(subnetMask);
        var ipBytes = ipAddress.GetAddressBytes();
        var networkBytes = networkAddress.GetAddressBytes();
        var broadcastBytes = broadcastAddress.GetAddressBytes();
        for (int i = 0; i < ipBytes.Length; i++)
        {
            if (ipBytes[i] < networkBytes[i] || ipBytes[i] > broadcastBytes[i])
                return false;
        }
        return true;
    }

    public static bool IsZero(this IPAddress ipAddress)
    {
        var bytes = ipAddress.GetAddressBytes();
        foreach (byte b in bytes)
        {
            if (b != 0)
            {
                return false;
            }
        }
        return true;
    }
}
