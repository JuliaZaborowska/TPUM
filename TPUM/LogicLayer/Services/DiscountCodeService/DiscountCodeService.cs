using System;
using DataLayer;
using DataLayer.Model;
using DataLayer.Repositories.DiscountCodeRepository;
using LogicLayer.DataTransferObjects;
using LogicLayer.ModelMapper;

namespace LogicLayer.Services.DiscountCodeService
{
    class DiscountCodeService : IDiscountCodeService
    {
        private readonly IDiscountCodeRepository _discountCodeRepository;
        private readonly DTOModelMapper _modelMapper;

        public DiscountCodeService()
        {
            _discountCodeRepository = new DiscountCodeRepository(DataStore.Instance.State.DiscountCodes);
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

        public void RemoveDiscountCode(Guid discountCodeId)
        {
            _discountCodeRepository.Delete(discountCodeId);
        }
    }
}
