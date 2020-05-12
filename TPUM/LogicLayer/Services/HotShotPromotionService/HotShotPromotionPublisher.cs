using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using DataLayer;
using DataLayer.Repositories.DiscountCodes;
using LogicLayer.ModelMapper;

namespace LogicLayer.Services.HotShotPromotionService
{
    public class HotShotPromotionPublisher : IDisposable
    {
        private readonly IDiscountCodeRepository _dicountCodeRepository;
        private readonly DTOModelMapper _modelMapper;

        private IDisposable _subscription;

        public HotShotPromotionPublisher(TimeSpan period)
        {
            _dicountCodeRepository = new DiscountCodeRepository(DataStore.Instance.State.DiscountCodes);
            _modelMapper = new DTOModelMapper();
            Period = period;
        }

        public TimeSpan Period { get; }

        public event EventHandler<HotShotMessage> HotShotMessage;

        public void Start()
        {
            var timerObservable = Observable.Interval(Period);
            _subscription = timerObservable.ObserveOn(Scheduler.Default).Subscribe(RaiseTick);
        }

        private void RaiseTick(long counter)
        {
            var discountCode = _dicountCodeRepository.GetRandomDiscountCode();
            HotShotMessage?.Invoke(this, new HotShotMessage(_modelMapper.ToDiscountCodeDTO(discountCode)));
        }

        #region Dispose

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}