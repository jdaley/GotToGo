using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace GotToGo
{
    [XmlRoot("gotToGo")]
    public class GotToGoConfig
    {
        public static GotToGoConfig Current { get; set; }

        static GotToGoConfig()
        {
            string configPath;

            if (HttpRuntime.AppDomainAppId == null)
            {
                configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GotToGo.config");
            }
            else
            {
                configPath = Path.Combine(HttpRuntime.AppDomainAppPath, "GotToGo.config");
            }

            if (!File.Exists(configPath))
            {
                throw new Exception("GotToGo.config does not exist.");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(GotToGoConfig));

            using (StreamReader reader = new StreamReader(configPath))
            {
                Current = (GotToGoConfig)serializer.Deserialize(reader);
            }
        }

        [XmlElement("connectionString")]
        public string ConnectionString { get; set; }
    }
}