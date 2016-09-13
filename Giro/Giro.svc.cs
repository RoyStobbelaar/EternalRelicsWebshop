using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;

namespace Giro
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Giro : IGiro
    {
        public int GetGiroNumber(string name, string address, string zipcode, string residence, decimal price)
        {
            System.Diagnostics.Debug.WriteLine("Name:    " + name);
            System.Diagnostics.Debug.WriteLine("Address: " + address);
            System.Diagnostics.Debug.WriteLine("Zipcode: " + zipcode);
            System.Diagnostics.Debug.WriteLine("Residence:    " + residence);
            System.Diagnostics.Debug.WriteLine("Price:   " + price);

            Thread.Sleep(30000);

            var random = new Random();

            return random.Next(10000, 99999);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
