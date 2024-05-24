using System.Collections.Concurrent;

namespace EventHubExplorer.Helpers
{
    public class EventBus
    {
        private static EventBus _instance;
        public static EventBus Instance => _instance ??= new EventBus();

        public delegate void EventHandler<in T>(T payload);

        public delegate Task AsyncEventHandler<in T>(T payload);

        public delegate TResult EventHandler<in T, out TResult>(T payload);

        public delegate Task<TResult> AsyncEventHandler<in T, TResult>(T payload);

        private readonly ConcurrentDictionary<Type, Delegate> _handlers = new ConcurrentDictionary<Type, Delegate>();
        // Generate different ConcurrentDictionary for async and non-async 



        public EventBus()
        {
            _instance = this;
        }

        public TResult Send<TResult, T>(T payload) where T : class
        {
            var eventType = typeof(T);
            if (!_handlers.TryGetValue(eventType, out var handler)) return default;
            return ((EventHandler<T, TResult>)handler).Invoke(payload);
        }

        public async Task<TResult> SendAsync<TResult, T>(T payload) where T : class
        {
            var eventType = typeof(T);
            if (!_handlers.TryGetValue(eventType, out var handler)) return default;
            var result = ((EventHandler<T, TResult>)handler).Invoke(payload);
            if (result != null)
            {
                return result;
            }
            result = await ((AsyncEventHandler<T, TResult>)handler).Invoke(payload);
            return result;

        }

        public void Publish<T>(T payload) where T : class
        {
            var eventType = typeof(T);
            if (!_handlers.TryGetValue(eventType, out var handler)) return;
            // handler is async or non-async
            if (handler is AsyncEventHandler<T> asyncHandler)
            {
                // The handler is asynchronous
                Task.Run(() => asyncHandler.Invoke(payload));
            }
            else if (handler is EventHandler<T> syncHandler)
            {
                // The handler is non-asynchronous
                syncHandler.Invoke(payload);
            }

        }
        public async Task PublishAsync<T>(T payload) where T : class
        {
            var eventType = typeof(T);
            if (_handlers.TryGetValue(eventType, out var handler))
            {
                await ((AsyncEventHandler<T>)handler).Invoke(payload);
            }
        }

        public void Register<T>(EventHandler<T> handler) where T : class
        {
            var eventType = typeof(T);
            _handlers.AddOrUpdate(eventType, handler, (_, existingHandler) => Delegate.Combine(existingHandler, handler));
        }

        public void Register<T, TResult>(EventHandler<T, TResult> handler) where T : class
        {
            var eventType = typeof(T);
            _handlers.AddOrUpdate(eventType, handler, (_, existingHandler) => Delegate.Combine(existingHandler, handler));
        }

        public Task RegisterAsync<T>(AsyncEventHandler<T> handler) where T : class
        {
            var eventType = typeof(T);
            _handlers.AddOrUpdate(eventType, handler, (_, existingHandler) => Delegate.Combine(existingHandler, handler));
            return Task.CompletedTask;
        }

        public Task RegisterAsync<T, TResult>(AsyncEventHandler<T, TResult> handler) where T : class
        {
            var eventType = typeof(T);
            _handlers.AddOrUpdate(eventType, handler, (_, existingHandler) => Delegate.Combine(existingHandler, handler));
            return Task.CompletedTask;
        }

        public void Remove<T>(EventHandler<T> handler)
        {
            var eventType = typeof(T);
            _handlers.AddOrUpdate(eventType, handler, (_, existingHandler) => Delegate.Remove(existingHandler, handler));
        }

        public void RemoveAsync<T>(AsyncEventHandler<T> handler)
        {
            var eventType = typeof(T);
            _handlers.AddOrUpdate(eventType, handler, (_, existingHandler) => Delegate.Remove(existingHandler, handler));
        }

        public void Remove<T, TResult>(EventHandler<T, TResult> handler)
        {
            var eventType = typeof(T);
            _handlers.AddOrUpdate(eventType, handler, (_, existingHandler) => Delegate.Remove(existingHandler, handler));
        }

        public void RemoveAsync<T, TResult>(AsyncEventHandler<T, TResult> handler)
        {
            var eventType = typeof(T);
            _handlers.AddOrUpdate(eventType, handler, (_, existingHandler) => Delegate.Remove(existingHandler, handler));
        }
    }
}