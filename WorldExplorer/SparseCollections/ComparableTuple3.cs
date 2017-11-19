//=======================================================================
// Copyright (C) 2010-2013 William Hallahan
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software,
// and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//=======================================================================

﻿//======================================================================
// Generic Abstract Class: ComparableTuple3
// Author: Bill Hallahan
// Date: April 22, 2010
//======================================================================

using System;
using System.Collections.Generic;

namespace SparseCollections
{
    /// <summary>
    /// This class implements a group of 3 items.
    /// </summary>
    /// <typeparam name="TItem0">The type of the first item</typeparam>
    /// <typeparam name="TItem1">The type of the second item</typeparam>
    /// <typeparam name="TItem2">The type of the third item</typeparam>
    [Serializable]
    public class ComparableTuple3<TItem0, TItem1, TItem2>
        : IComparable<ComparableTuple3<TItem0, TItem1, TItem2>>
        where TItem0 : IComparable<TItem0>
        where TItem1 : IComparable<TItem1>
        where TItem2 : IComparable<TItem2>
    {
        /// <summary>
        /// The first item
        /// </summary>
        public TItem0 Item0
        {
            get;
            private set;
        }

        /// <summary>
        /// The second item.
        /// </summary>
        public TItem1 Item1
        {
            get;
            private set;
        }

        /// <summary>
        /// The third item.
        /// </summary>
        public TItem2 Item2
        {
            get;
            private set;
        }

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public ComparableTuple3()
        {
        }

        /// <summary>
        /// Constructs a new instance with the same item values as this instance.
        /// </summary>
        /// <param name="group">The group used to initialize this instance</param>
        public ComparableTuple3(ComparableTuple3<TItem0, TItem1, TItem2> group)
        {
            Item0 = group.Item0;
            Item1 = group.Item1;
            Item2 = group.Item2;
        }

        /// <summary>
        /// Constructs a new instance with the specified item values.
        /// </summary>
        /// <param name="item0">The first item</param>
        /// <param name="item1">The second item</param>
        /// <param name="item2">The third item</param>
        public ComparableTuple3(TItem0 item0,
                      TItem1 item1,
                      TItem2 item2)
        {
            Item0 = item0;
            Item1 = item1;
            Item2 = item2;
        }

        #endregion

        #region IComparable<ComparableTuple3> implementation
        /// <summary>
        /// This methods implements the IComparable<ComparableTuple3<TItem0, TItem1, TItem2>> interface.
        /// </summary>
        /// <param name="group">The group being compared to this group</param>
        /// <returns>
        /// The value -1 if this groups is less than the passed group.
        /// The value 1 if this group is greater than the passed group.
        /// The value 0 if this group and the passed groups are equal.
        /// </returns>
        public int CompareTo(ComparableTuple3<TItem0, TItem1, TItem2> group)
        {
            int result = this.Item0.CompareTo(group.Item0);

            if (result == 0)
            {
                result = this.Item1.CompareTo(group.Item1);

                if (result == 0)
                {
                    result = this.Item2.CompareTo(group.Item2);
                }
            }

            return result;
        }

        #endregion
    }

    /// <summary>
    /// This class implements the IEqualityComparer<ComparableTuple3<TItem0, TItem1, TItem2>> interface
    /// to allow using ComparableTuple3<ComparableTuple3<TItem0, TItem1, TItem2> class instances as keys in a dictionary.
    /// </summary>
    /// <typeparam name="TItem0">The type of the first item</typeparam>
    /// <typeparam name="TItem1">The type of the second item</typeparam>
    /// <typeparam name="TItem2">The type of the third item</typeparam>
    [Serializable]
    public class ComparableTuple3EqualityComparer<TItem0, TItem1, TItem2>
        : IEqualityComparer<ComparableTuple3<TItem0, TItem1, TItem2>>
        where TItem0 : IComparable<TItem0>
        where TItem1 : IComparable<TItem1>
        where TItem2 : IComparable<TItem2>
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ComparableTuple3EqualityComparer()
        {
        }

        #endregion

        /// IEqualityComparer.Equals compares the items in this group for equality.
        public bool Equals(ComparableTuple3<TItem0, TItem1, TItem2> groupA,
                           ComparableTuple3<TItem0, TItem1, TItem2> groupB)
        {
            return ((groupA.Item0.Equals(groupB.Item0))
                && (groupA.Item1.Equals(groupB.Item1))
                && (groupA.Item2.Equals(groupB.Item2)));
        }

        /// <summary>
        /// Returns a hash code for an object.
        /// </summary>
        /// <param name="obj">An object of type ComparableTuple3</param>
        /// <returns>A hash code for the object.</returns>
        public int GetHashCode(ComparableTuple3<TItem0, TItem1, TItem2> group)
        {
            int hash0 = group.Item0.GetHashCode();
            int hash1 = group.Item1.GetHashCode();
            int hash2 = group.Item2.GetHashCode();

            int hash = 577 * hash0 + 599 * hash1 + 619 * hash2;
            return hash.GetHashCode();
        }
    }
}
