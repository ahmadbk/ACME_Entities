using ACME.Maintenance.Domain.DTO;
using ACME.Maintenance.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Maintenance.Persistence
{
    public class ContractRepository : IContractRepository
    {
        public ContractDto GetById(string contractId)
        {
            var contractDto = new ContractDto();


            if(contractId == "CONTRACTID")
            {
                contractDto.ExpirationDate = DateTime.Now.AddDays(1);
            }
            else if(contractId == "EXPIREDCONTRACTID")
            {
                contractDto.ExpirationDate = DateTime.Now.AddDays(-1);
            }
            // stubbed... ultimately, it will go out to the 
            // database and retrieve the given contract
            // record based on contractId

            contractDto.ContractId = contractId;
            return contractDto;
        }

    }
}
