using ACME.Maintenance.Domain.DTO;
using ACME.Maintenance.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Maintenance.Domain
{
    public class PartService
    {
        private readonly IPartServiceRepository _partServiceRepository;

        public PartService(IPartServiceRepository partServiceRepository)
        {
            _partServiceRepository = partServiceRepository;
        }

        public Part GetById(string partId)
        {
            var partDto = _partServiceRepository.GetById(partId);

            //The auto mapper is giving issues thus mapped it manually
            Part part = new Part();
            part.PartId = partDto.PartId;
            part.Price = partDto.Price;
            

            //var part = AutoMapper.Mapper.Map<PartDto, Part>(partDto);
            //AutoMapper.Mapper.Reset();

            return part;
        }
    }
}
