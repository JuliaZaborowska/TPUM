using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Model;
using DataLayer.Repositories.DiscountCodes;
using LogicLayer.DataTransferObjects;
using LogicLayer.ModelMapper;

namespace LogicLayer.Services.DiscountCodeService
{
    public class DiscountCodeService : IDiscountCodeService
    {
        private readonly IDiscountCodeRepository _discountCodeRepository;
        private readonly DTOModelMapper _modelMapper;

        public DiscountCodeService()
        {
            _discountCodeRepository = new DiscountCodeRepository(DataStore.Instance.State.DiscountCodes);
            _modelMapper = new DTOModelMapper();
        }

        public DiscountCodeService(IDiscountCodeRepository discountCodeRepository, DTOModelMapper modelMapper)
        {
            _discountCodeRepository = discountCodeRepository;
            _modelMapper = modelMapper;
        }

        public DiscountCodeDTO AddDiscountCode(DiscountCodeDTO dto)
        {
            DiscountCode discountCode = _modelMapper.FromDiscountCodeDTO(dto);
            DiscountCode created = _discountCodeRepository.Create(discountCode);
            return _modelMapper.ToDiscountCodeDTO(created);
        }

        public IEnumerable<DiscountCodeDTO> GetAllDiscountCodes()
        {
            return _discountCodeRepository.Items.Select( item => _modelMapper.ToDiscountCodeDTO(item));
        }

        public void RemoveDiscountCode(Guid discountCodeId)
        {
            _discountCodeRepository.Delete(discountCodeId);
        }
    }
}
