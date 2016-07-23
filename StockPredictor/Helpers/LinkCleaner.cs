using System.Collections.Generic;

namespace StockPredictor.Helpers
{
    class LinkCleaner
    {
        //check that values are unique in the IEnumerable list
       public bool isUnique<T>(IEnumerable<T> values)
        {
            var set = new HashSet<T>();

            foreach (T item in values)
            {
                if (!set.Add(item))
                    return false;
            }
            return true;
        }
    }
}
