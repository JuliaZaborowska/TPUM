using System;
using System.Collections.Generic;

namespace DataLayer.Observer
{
    class DisposeSubscription : IDisposable
    {
        private IList<IObserver<PromotionEvent>> _observers;
        private IObserver<PromotionEvent> _observer;

        public DisposeSubscription(IList<IObserver<PromotionEvent>> observers, IObserver<PromotionEvent> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            Dispose(true);
        }
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                if (_observer != null && _observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
            _disposed = true;
        }
    }
}
