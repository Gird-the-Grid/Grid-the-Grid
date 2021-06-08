using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlazorServerAPI.Models.Entities
{
    public class PenetrationReportModel : BaseEntity
    {
        public string Email { get; set; }
        public int PasswordAtempts { get; set; }
        
        public Dictionary<string, int> IpList { get; set; }

        public void AddIp(string ip)
        {
            IpList ??= new Dictionary<string, int>(); //if null, make new 
            if (IpList.ContainsKey(ip))
            {
                ++IpList[ip];
            }
            else
            {
                IpList.Add(ip, 1);
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
