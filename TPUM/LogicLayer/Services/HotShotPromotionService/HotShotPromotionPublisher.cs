using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Model;
using DataLayer.Observer;
using DataLayer.Repositories.DiscountCodes;
using PromotionEvent = DataLayer.Observer.PromotionEvent;

namespace LogicLayer.Services.HotShotPromotionService
{
    public class HotShotPromotionPublisher : IDisposable
    {
        private readonly IDiscountCodeRepository _discountCodeRepository;
        private IDisposable _subscription;
        private readonly PromotionFeed _promotionFeed = new PromotionFeed();
            
        public HotShotPromotionPublisher(TimeSpan period)
        {
            _discountCodeRepository = new DiscountCodeRepository(DataStore.Instance.State.DiscountCodes);
            Period = period;
        }

        public TimeSpan Period { get; }

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
        }

        #endregion
    }
}