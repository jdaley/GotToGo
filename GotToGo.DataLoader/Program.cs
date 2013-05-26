using GotToGo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GotToGo.DataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Toilet> toilets = LoadFromXml();
            Console.WriteLine(string.Format(
                "About to load {0} toilets from XML to database. Press enter to proceed.", toilets.Count));

            Console.ReadLine();
            Console.WriteLine("Connecting...");

            using (SqlConnection connection = new SqlConnection(GotToGoConfig.Current.ConnectionString))
            using (SqlCommand command = new SqlCommand("TRUNCATE TABLE Toilets", connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }

            Console.WriteLine("Loading toilet data...");

            GotToGoContext context = new GotToGoContext();

            try
            {
                for (int i = 0; i < toilets.Count; i++)
                {
                    context.Toilets.Add(toilets[i]);

                    if (i % 200 == 199 || i == toilets.Count - 1)
                    {
                        context.SaveChanges();
                        Console.WriteLine(string.Format("  {0:0.0}%", (i + 1) * 100.0 / toilets.Count));

                        context.Dispose();
                        context = new GotToGoContext();
                    }
                }
            }
            finally
            {
                context.Dispose();
            }

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        static List<Toilet> LoadFromXml()
        {
            List<Toilet> list = new List<Toilet>();

            using (XmlTextReader reader =
                new XmlTextReader(@"C:\Users\Joe\GovHack2013\Toiletmap\ToiletmapExport.xml"))
            {
                Toilet toilet = null;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "ToiletDetails" &&
                        !reader.IsEmptyElement)
                    {
                        toilet = new Toilet()
                        {
                            Lat = decimal.Parse(reader.GetAttribute("Latitude")),
                            Lng = decimal.Parse(reader.GetAttribute("Longitude"))
                        };
                    }
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Name")
                    {
                        toilet.Name = reader.ReadString();
                    }
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "ToiletDetails")
                    {
                        list.Add(toilet);
                        toilet = null;
                    }
                }
            }

            return list;
        }
    }
}
