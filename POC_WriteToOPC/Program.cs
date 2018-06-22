using Opc;
using Opc.Da;
using System.Collections.Generic;

namespace POC_WriteToOPC
{
    class Program
    {
        static void Main(string[] args)
        {
            Opc.Da.Server server = new Opc.Da.Server(new OpcCom.Factory(), new URL
            {
                Path = @"Matrikon.OPC.Simulation.1",
                Scheme = Opc.UrlScheme.DA
            });
            server.Connect();

            //Write Values
            int counter = 1;
            ItemValue item = new ItemValue("Bucket Brigade.TestWrite");
            var itemList = new List<ItemValue>();
            itemList.Add(item);
            Item item_read = new Item();
            item_read.ItemName = "Bucket Brigade.TestWrite";
            item_read.Active = true;
            List<Item> readItems = new List<Item>();
            readItems.Add(item_read);
            while (true)
            {
                item.Value = "Testing" + counter;
                server.Write(itemList.ToArray());
                counter++;

               var result =  server.Read(readItems.ToArray());
                foreach (var value in result)
                {
                    System.Console.WriteLine(value.ItemName +" : " + value.Value.ToString());
                }
            }        
        }
    }
}
