using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
<<<<<<< HEAD
using DataLayer;
using DataLayer.Repositories.DiscountCodes;
using LogicLayer.ModelMapper;
=======
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Model;
using DataLayer.Observer;
using DataLayer.Repositories.DiscountCodes;
using PromotionEvent = DataLayer.Observer.PromotionEvent;
>>>>>>> develop

namespace LogicLayer.Services.HotShotPromotionService
{
    public class HotShotPromotionPublisher : IDisposable
    {
<<<<<<< HEAD
        private readonly IDiscountCodeRepository _dicountCodeRepository;
        private readonly DTOModelMapper _modelMapper;

        private IDisposable _subscription;

        public HotShotPromotionPublisher(TimeSpan period)
        {
            _dicountCodeRepository = new DiscountCodeRepository(DataStore.Instance.State.DiscountCodes);
            _modelMapper = new DTOModelMapper();
=======
        private readonly IDiscountCodeRepository _discountCodeRepository;
        private IDisposable _subscription;
        private readonly PromotionFeed _promotionFeed = new PromotionFeed();
            
        public HotShotPromotionPublisher(TimeSpan period)
        {
            _discountCodeRepository = new DiscountCodeRepository(DataStore.Instance.State.DiscountCodes);
>>>>>>> develop
            Period = period;
        }

        public TimeSpan Period { get; }

<<<<<<< HEAD
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
=======
        public void Start()
        {
            IObservable<long> timerObservable = Observable.Interval(Period);
            _subscription = timerObservable.ObserveOn(Scheduler.Default).Subscribe(RaiseTick);
        }

        public void Subscribe(IObserver<PromotionEvent> observer)
        {
            _promotionFeed.Subscribe(observer);
        }

        public void End()
        {
            _promotionFeed.End();
        }

        private  void RaiseTick(long counter)
        {
            DiscountCode discountCode = _discountCodeRepository.GetRandomDiscountCode();
            PromotionEvent promotion = new PromotionEvent(discountCode);
            _promotionFeed.PublishPromotion(promotion);
        }

        #region Dispose

        public void Dispose()
        {
            _subscription?.Dispose();
>>>>>>> develop
        }

        #endregion
    }
}