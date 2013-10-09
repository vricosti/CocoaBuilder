/*
* XibParser.
* Copyright (C) 2013 Smartmobili SARL
* 
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License as published by the Free Software Foundation; either
* version 2 of the License, or (at your option) any later version.
* 
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
* 
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, write to the
* Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
* Boston, MA  02110-1301, USA. 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Smartmobili.Cocoa.Utils
{
    public class KeyedObjectList : ICollection<object>
    {
        protected List<object> _innerArray;
       
        protected bool _IsReadOnly;


         // Default constructor
        public KeyedObjectList()
        {
            //_innerArray = new ArrayList();
            _innerArray = new List<object>();
        }


        // Default accessor for the collection 
        public virtual object this[int index]
        {
            get
            {
                return (object)_innerArray[index];
            }
            set
            {
                _innerArray[index] = value;
            }
        }

        public int Count 
        {
            get
            {
                return _innerArray.Count;
            }
        }
        
        public bool IsReadOnly 
        {
            get
            {
                return _IsReadOnly;
            }
        }

        public void Add(object item)
        {
            _innerArray.Add(item);
        }

        public void Clear()
        {
            _innerArray.Clear();
        }

        public  bool Contains(object item)
        {
            return _innerArray.Contains(item);
        }

        public void CopyTo(object[] array, int arrayIndex)
        {
            _innerArray.CopyTo(array, arrayIndex);
        }

        public bool Remove(object item)
        {
            bool result = false;

            //loop through the inner array's indices
            for (int i = 0; i < _innerArray.Count; i++)
            {
                //store current index being checked
                object obj = (object)_innerArray[i];

                //compare the BusinessObjectBase UniqueId property
                if (obj.Equals(item))
                {
                    //remove item from inner ArrayList at index i
                    _innerArray.RemoveAt(i);
                    result = true;
                    break;
                }
            }

            return result;
        }

        // Returns custom generic enumerator for this BusinessObjectCollection
        public virtual IEnumerator<object> GetEnumerator()
        {
            //return a custom enumerator object instantiated
            //to use this BusinessObjectCollection 
            return _innerArray.GetEnumerator();
        }

        // Explicit non-generic interface implementation for IEnumerable
        // extended and required by ICollection (implemented by ICollection<T>)
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerArray.GetEnumerator();
        }

    }
}
