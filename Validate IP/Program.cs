using System;
using System.Linq;

namespace Validate_IP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ValidIPAddress("192.168.1.0")); // valid
            Console.WriteLine(ValidIPAddress("192.168.1.00")); // invalid
            Console.WriteLine(ValidIPAddress("2001:db8:85a3:0:0:8A2E:0370:7334")); // valid
            Console.WriteLine(ValidIPAddress("2001:0db8:85a3:0000:0000:8a2e:0370:7334")); // valid
            Console.WriteLine(ValidIPAddress("02001:0db8:85a3:0000:0000:8a2e:0370:7334")); // invalid
            Console.WriteLine(ValidIPAddress("2001:db8:85a3:0::8a2E:0370:7334"));  // invalid
        }

        static string ValidIPAddress(string IP)
        {
            string validity = "Neither";
            if (IP.Count(x => x == '.') == 3) return ValidateIPV4(IP);
            if (IP.Count(x => x == ':') == 7) return ValidateIPV6(IP);
            return validity;
        }

        private static string ValidateIPV4(string iP)
        {
            var subIps = iP.Split('.');
            foreach (var subIp in subIps)
            {
                // validate the length of each substring, bw 1 and 3
                if (subIp.Length < 1 || subIp.Length > 3) return "Neither";
                // no leeding zero
                if (subIp.StartsWith('0') && subIp.Length != 1) return "Neither";
                // only digits are allowed
                int no;
                if (!int.TryParse(subIp, out no)) return "Neither";
                if (no > 255) return "Neither";
            }
            return "IPv4";
        }

        private static string ValidateIPV6(string iP)
        {
            string hex = "0123456789abcdefABCDEF";
            foreach(var subIp in iP.Split(':'))
            {
                // each sub ip should contains hex characters  only
                if (!subIp.All(x => hex.Contains(x))) return "Neither";
                // each sub ip length bw 1 and 4
                if (subIp.Length < 1 || subIp.Length > 4) return "Neither";
            }
            return "IPv6";
        }
    }
}
