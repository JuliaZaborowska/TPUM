using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Model;

namespace DataLayer.Repositories
{
    public abstract class CrudRepository<T> : ICrudRepository<T> where T : BaseEntity
    {
        protected CrudRepository(IList<T> items)
        {
            Items = items;
        }

        protected CrudRepository()
        {
        }

        /// <summary>
        ///     Collection of items being managed by repository.
        /// </summary>
        public IList<T> Items { get; } = new List<T>();

        /// <summary>
        ///     Creates or updates item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns> Created or updated item </returns>
        public T CreateOrUpdate(T item)
        {
            return item.Id == null ? Create(item) : Update(item);
        }

        /// <summary>
        ///     Adds item to items collection.
        /// </summary>
        /// <param name="item"></param>
        /// <returns> Created item</returns>
        public T Create(T item)
        {
            item.Id = Guid.NewGuid();
            Items.Add(item);
            return item;
        }

        /// <summary>
        ///     Find item matching provided predicate.
        /// </summary>
        /// <param name="predicate"> Predicate to test against each item. </param>
        /// <returns> Returns item or null </returns>
        public T Find(Func<T, bool> predicate)
        {
            return Items.FirstOrDefault(predicate);
        }

        /// <summary>
        ///     Update existing item
        /// </summary>
        /// <param name="item"> Object with updated values </param>
        /// <returns> Return updated object. </returns>
        public T Update(T item)
        {
            var currentItem = Items.FirstOrDefault(i => i.Id.Equals(item.Id));
            if (currentItem is null) throw new ArgumentException("Provided item does not exist and can't be updated.");

            CopyProperties(item, currentItem);
            return currentItem;
        }

        /// <summary>
        ///     Delete item by reference.
        /// </summary>
        /// <param name="item"> Reference to existing item </param>
        public void Delete(T item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id.Equals(item.Id));
            if (existingItem is null) throw new ArgumentException("Provided item does not exist and can't be updated.");

            Items.Remove(existingItem);
        }

        /// <summary>
        ///     Delete item by id.
        /// </summary>
        /// <param name="id"> Unique identifier of item </param>
        public void Delete(Guid id)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id.Equals(id));
            if (existingItem is null) throw new ArgumentException("Provided item does not exist and can't be updated.");

            Items.Remove(existingItem);
        }

        /// <summary>
        ///     Copy all property values from one object to another. Source object is modified.
        /// </summary>
        /// <param name="source"> Source object which property values will be copied. </param>
        /// <param name="destination"> Destination object which properties will be modified. </param>
        private void CopyProperties(T source, T destination)
        {
            foreach (var property in typeof(T).GetProperties())
                if (property.CanWrite)
                    property.SetValue(destination, property.GetValue(source, null), null);
        }
    }
}