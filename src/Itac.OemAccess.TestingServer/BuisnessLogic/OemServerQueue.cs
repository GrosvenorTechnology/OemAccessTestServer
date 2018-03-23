using System;
using System.Collections.Generic;
using System.Linq;
using Itac.OemAccess.TestingServer.Model;

namespace Itac.OemAccess.TestingServer.BuisnessLogic
{
    public class OemServerQueue<T> 
    {
        private readonly List<QueueEntry<T>> _queue = new List<QueueEntry<T>>();

        public void Put(T item)
        {
            _queue.Add(new QueueEntry<T>(item));
        }

        public List<QueueEntry<T>> Get(int count)
        {
            return Get(count, Enumerable.Empty<Guid>());
        }

        public List<QueueEntry<T>> Get(int count, IEnumerable<Guid> guidList)
        {
            foreach (var id in guidList)
            {
                var item = _queue.FirstOrDefault(x => x.MessageId == id);
                if (item != null)
                {
                    _queue.Remove(item);
                }
            }

            return _queue.GetRange(0, Math.Min(_queue.Count, count));
        }

        public List<QueueEntry<T>> DestructiveGet(int count)
        {
            var items = _queue.GetRange(0, Math.Min(_queue.Count, count));
            _queue.RemoveRange(0, Math.Min(_queue.Count, count));
            return items;
        }

        public bool Any()
        {
            return _queue.Any();
        }
    }
}