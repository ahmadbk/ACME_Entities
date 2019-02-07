using ACME.Maintenance.Domain.DTO;
using ACME.Maintenance.Domain.Exceptions;
using ACME.Maintenance.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ACME.Maintenance.Persistence
{
    public class PartRepository : IPartServiceRepository
    {
        public PartDto GetById(string partId)
        {
            var partDto = new PartDto();

            if (partId == "VALIDPARTID")
            {
                partDto.Price = 50.0;
            }
            else if (partId == "INVALIDPARTID")
            {
                throw new PartNotFoundException();
            }

            partDto.PartId = partId;
            return partDto;
        }
    }
}
