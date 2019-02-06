using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Maintenance.Domain
{
    public class ContractService
    {
        public Contract GetById(string contractId)
        {
            //1. Call an instance of my persistence layer


            //2. Call the FindByID method of the persistence 
            //   layer and pass the contractId


            //3. Receive the data back from that function and
            //   populate my properties.... similar to this,
            //   but with real data

            var contract = new Contract();
            contract.ContractId = contractId;
            contract.ExpirationDate = DateTime.Now.AddDays(1);
            return contract;


            /*
            this.ContractId = contractId;
            this.ExpirationDate = DateTime.Now.AddDays(1);
            */
        }


    }
}
