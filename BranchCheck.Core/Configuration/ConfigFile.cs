using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace BranchCheck.Core.Configuration
{
    public class ConfigFile
    {
        private class ConfigElement : IDisposable
        {
            private readonly XmlTextWriter writer = null;

            public ConfigElement(XmlTextWriter writer, string elementName)
            {
                this.writer = writer;
                writer.WriteStartElement(elementName);
            }
            
            public static string TryGetElementValue(XElement element)
            {
                return element == null ? null : element.Value;
            }

            public void Dispose()
            {
                if (writer != null)
                    writer.WriteEndElement();
            }
        }

        private readonly string configFile = null;

        public ConfigFile(string configFile)
        {
            this.configFile = configFile;
        }

        public void Read(out ManagerConfig config)
        {
            string configXml = File.ReadAllText(configFile);
            XDocument xmlDoc = XDocument.Parse(configXml);

            config = (from m in xmlDoc.Descendants("Properties")
                      select new ManagerConfig
                      (
                          ConfigElement.TryGetElementValue(m.Element("GitLocation")),
                          ConfigElement.TryGetElementValue(m.Element("CommandPromptLocation")),
                          ConfigElement.TryGetElementValue(m.Element("RepositoryLocation")),
                          ConfigElement.TryGetElementValue(m.Element("RemoteRepositoryName")),
                          ConfigElement.TryGetElementValue(m.Element("User")),
                          ConfigElement.TryGetElementValue(m.Element("YouTrackBaseURL"))
                      )
                     ).First();
        }
    }
}
