using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace MvLocalProject.Controller
{
    public class MvNetworker
    {

        private static int pingTimeout = 100;
        public static bool isInRange(string startIpAddr, string endIpAddr, string address)
        {
            long ipStart = BitConverter.ToInt32(IPAddress.Parse(startIpAddr).GetAddressBytes().Reverse().ToArray(), 0);
            long ipEnd = BitConverter.ToInt32(IPAddress.Parse(endIpAddr).GetAddressBytes().Reverse().ToArray(), 0);
            long ip = BitConverter.ToInt32(IPAddress.Parse(address).GetAddressBytes().Reverse().ToArray(), 0);

            return ip >= ipStart && ip <= ipEnd;
        }

        public static bool isPingAlive(string ipName)
        {
            IPAddress ipAddress;
            try
            {
                ipAddress = IPAddress.Parse(ipName);
                return isPingAlive(ipAddress);
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            catch(SocketException)
            {
                return false;
            }

        }

        public static bool isPingAlive(IPAddress ipAddress)
        {
            Ping p = new Ping();
            PingReply rep;
            string hostName = string.Empty;

            try
            {
                rep = p.Send(ipAddress, pingTimeout);
                //rep = p.Send(ipAddress);
                if (rep.Status != IPStatus.Success) { return false; }
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
            catch (PingException)
            {
                return false;
            }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
