using System;
using System.Collections;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MvLocalProject.Model
{

    public class IPEnumeration : IEnumerable
    {
        private string startAddress;
        private string endAddress;

        internal static Int64 AddressToInt(IPAddress addr)
        {
            byte[] addressBits = addr.GetAddressBytes();

            Int64 retval = 0;
            for (int i = 0; i < addressBits.Length; i++)
            {
                retval = (retval << 8) + (int)addressBits[i];
            }

            return retval;
        }

        internal static Int64 AddressToInt(string addr)
        {
            return AddressToInt(System.Net.IPAddress.Parse(addr));
        }

        internal static IPAddress IntToAddress(Int64 addr)
        {
            return IPAddress.Parse(addr.ToString());
        }


        public IPEnumeration(string startAddress, string endAddress)
        {
            this.startAddress = startAddress;
            this.endAddress = endAddress;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public IPEnumerator GetEnumerator()
        {
            return new IPEnumerator(startAddress, endAddress);
        }

    }

    public class IPEnumerator : IEnumerator
    {
        private string startAddress;
        private string endAddress;
        private Int64 currentIP;
        private Int64 endIP;
        bool isFirstEnumerate = true;

        public IPEnumerator(string startAddress, string endAddress)
        {
            this.startAddress = startAddress;
            this.endAddress = endAddress;

            currentIP = IPEnumeration.AddressToInt(startAddress);
            endIP = IPEnumeration.AddressToInt(endAddress);
            isFirstEnumerate = true;
        }

        public bool MoveNext()
        {
            // 在foreach 呼叫是, 需要先判斷是否為第一次呼叫
            // 避免currentIP先被++, 而忽略掉第一個值
            if (isFirstEnumerate == true)
            {
                isFirstEnumerate = false;
                return true;
            }
            currentIP++;
            return (currentIP <= endIP);
        }

        public void Reset()
        {
            isFirstEnumerate = true;
            currentIP = IPEnumeration.AddressToInt(startAddress);
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public IPAddress Current
        {
            get
            {
                try
                {
                    return IPEnumeration.IntToAddress(currentIP);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
