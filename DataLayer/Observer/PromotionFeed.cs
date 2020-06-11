using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Observer
{
    public class PromotionFeed : IObservable<PromotionEvent>
    {
        private IList<IObserver<PromotionEvent>> observers = new List<IObserver<PromotionEvent>>();

        public IDisposable Subscribe(IObserver<PromotionEvent> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new DisposeSubscription(observers, observer);
        }

        public void PublishPromotion(PromotionEvent promotion)
        {
            foreach (var observer in observers)
            {
                if (promotion == null)
                {
                    observer.OnError(new ArgumentNullException());
                }
                observer.OnNext(promotion);
            }
        }
        public void End()
        {
            foreach (var observer in observers.ToArray())
            {
                observer.OnCompleted();
            }
            observers.Clear();
        }
    }
}