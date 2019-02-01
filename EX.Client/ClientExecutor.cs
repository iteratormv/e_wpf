using EX.Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX.Client
{
    public class ClientExecutor
    {
        VisitorContractClient client;

        public ClientExecutor()
        {
            client = new VisitorContractClient();
        }

        public VisitorContractClient GetClient() { return client; }
    }
}
