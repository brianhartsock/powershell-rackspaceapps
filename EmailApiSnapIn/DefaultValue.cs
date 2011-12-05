using System;
using System.Collections.Generic;
using System.Linq;

namespace EmailApiSnapIn
{
    /// <summary>
    /// This class provides a means to set a list of default values for a given object, and retrieve them in order if 
    /// no non-null value is ever set.
    /// </summary>
    /// <typeparam name="T">Class type object to provide default values for.</typeparam>
    public class DefaultValue<T> where T: class
    {
        T innerValue;
        IList<Func<T>> defaultValues;

        public DefaultValue(params Func<T>[] _defaultValues)
        {
            defaultValues = _defaultValues.ToList();
            defaultValues.Insert(0, () => this.innerValue);
        }

        public T Value
        {
            get
            {
                return defaultValues.Select(v => v.Invoke())
                    .Where(v => v != null)
                    .FirstOrDefault();
            }
            set
            {
                innerValue = value;
            }
        }
    }
}
