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

            // stubbed... ultimately, it will go out to the 
            // database and retrieve the given contract
            // record based on contractId

            contractDto.ContractId = contractId;
            contractDto.ExpirationDate = DateTime.Now.AddDays(1);
            return contractDto;
        }

    }
}
