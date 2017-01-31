using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Net.StoreApp.Tests
{
    public class TestDbSet<T>: DbSet<T>, IQueryable, IEnumerable<T> where T: class
    {
        ObservableCollection<T> data;
        IQueryable query;

        public TestDbSet()
        {
            data = new ObservableCollection<T>();
            query = data.AsQueryable();
        }

        public override T Add(T item)
        {
            data.Add(item);
            return item;
        }

        public override T Remove(T item)
        {
            data.Remove(item);
            return item;
        }

        public override T Attach(T item)
        {
            data.Add(item);
            return item;
        }

        public override T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<T> Local
        {
            get { return new ObservableCollection<T>(data); }
        }

        Type IQueryable.ElementType
        {
            get { return query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return data.GetEnumerator();
        }

    }
}
