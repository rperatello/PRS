using Newtonsoft.Json;
using System;
using System.IO;

namespace PRS.Models.Models
{
    public class CommunicationPort
    {
        public int mainAPIPort { get; set; }
        public int systemAPIPort { get; set; }
        public int userAPIPort { get; set; }
        public int collectorAPIPort { get; set; }
        public int calculateAPIPort { get; set; }
    }
    public class CommunicationSetting
    {

        public CommunicationPort communicationPort;
        
        public CommunicationSetting() 
        {
            using (StreamReader file = File.OpenText(Directory.GetParent(Directory.GetCurrentDirectory()).ToString() + "\\cnsettings.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                communicationPort = (CommunicationPort)serializer.Deserialize(file, typeof(CommunicationPort));
            }
        }
    }
}
