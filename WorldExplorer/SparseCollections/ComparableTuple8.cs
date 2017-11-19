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
// Generic Abstract Class: ComparableTuple8
// Author: Bill Hallahan
// Date: April 22, 2010
//======================================================================

using System;
using System.Collections.Generic;

namespace SparseCollections
{
    /// <summary>
    /// This class implements a group of 8 items.
    /// </summary>
    /// <typeparam name="TItem0">The type of the first item</typeparam>
    /// <typeparam name="TItem1">The type of the second item</typeparam>
    /// <typeparam name="TItem2">The type of the third item</typeparam>
    /// <typeparam name="TItem3">The type of the fourth item</typeparam>
    /// <typeparam name="TItem4">The type of the fifth item</typeparam>
    /// <typeparam name="TItem5">The type of the sixth item</typeparam>
    /// <typeparam name="TItem6">The type of the seventh item</typeparam>
    /// <typeparam name="TItem7">The type of the eighth item</typeparam>
    [Serializable]
    public class ComparableTuple8<TItem0, TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7>
        : IComparable<ComparableTuple8<TItem0, TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7>>
        where TItem0 : IComparable<TItem0>
        where TItem1 : IComparable<TItem1>
        where TItem2 : IComparable<TItem2>
        where TItem3 : IComparable<TItem3>
        where TItem4 : IComparable<TItem4>
        where TItem5 : IComparable<TItem5>
        where TItem6 : IComparable<TItem6>
        where TItem7 : IComparable<TItem7>
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

        /// <summary>
        /// The fourth item.
        /// </summary>
        public TItem3 Item3
        {
            get;
            private set;
        }

        /// <summary>
        /// The fifth item.
        /// </summary>
        public TItem4 Item4
        {
            get;
            private set;
        }

        /// <summary>
        /// The sixth item.
        /// </summary>
        public TItem5 Item5
        {
            get;
            private set;
        }

        /// <summary>
        /// The seventh item.
        /// </summary>
        public TItem6 Item6
        {
            get;
            private set;
        }

        /// <summary>
        /// The eighth item.
        /// </summary>
        public TItem7 Item7
        {
            get;
            private set;
        }

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public ComparableTuple8()
        {
        }

        /// <summary>
        /// Constructs a new instance with the same item values as this instance.
        /// </summary>
        /// <param name="group">The group used to initialize this instance</param>
        public ComparableTuple8(ComparableTuple8<TItem0, TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7> group)
        {
            Item0 = group.Item0;
            Item1 = group.Item1;
            Item2 = group.Item2;
            Item3 = group.Item3;
            Item4 = group.Item4;
            Item5 = group.Item5;
            Item6 = group.Item6;
            Item7 = group.Item7;
        }

        /// <summary>
        /// Constructs a new instance with the specified item values.
        /// </summary>
        /// <param name="item0">The first item</param>
        /// <param name="item1">The second item</param>
        /// <param name="item2">The third item</param>
        /// <param name="item3">The fourth item</param>
        /// <param name="item4">The fifth item</param>
        /// <param name="item5">The sixth item</param>
        /// <param name="item6">The seventh item</param>
        /// <param name="item7">The eighth item</param>
        public ComparableTuple8(TItem0 item0,
                      TItem1 item1,
                      TItem2 item2,
                      TItem3 item3,
                      TItem4 item4,
                      TItem5 item5,
                      TItem6 item6,
                      TItem7 item7)
        {
            Item0 = item0;
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
        }

        #endregion

        #region IComparable<ComparableTuple8> implementation
        /// <summary>
        /// This methods implements the IComparable<ComparableTuple8<TItem0, TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7>> interface.
        /// </summary>
        /// <param name="group">The group being compared to this group</param>
        /// <returns>
        /// The value -1 if this groups is less than the passed group.
        /// The value 1 if this group is greater than the passed group.
        /// The value 0 if this group and the passed groups are equal.
        /// </returns>
        public int CompareTo(ComparableTuple8<TItem0, TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7> group)
        {
            int result = this.Item0.CompareTo(group.Item0);

            if (result == 0)
            {
                result = this.Item1.CompareTo(group.Item1);

                if (result == 0)
                {
                    result = this.Item2.CompareTo(group.Item2);

                    if (result == 0)
                    {
                        result = this.Item3.CompareTo(group.Item3);

                        if (result == 0)
                        {
                            result = this.Item4.CompareTo(group.Item4);

                            if (result == 0)
                            {
                                result = this.Item5.CompareTo(group.Item5);

                                if (result == 0)
                                {
                                    result = this.Item6.CompareTo(group.Item6);

                                    if (result == 0)
                                    {
                                        result = this.Item7.CompareTo(group.Item7);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        #endregion
    }

    /// <summary>
    /// This class implements the IEqualityComparer<ComparableTuple8<TItem0, TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7>> interface
    /// to allow using ComparableTuple8<ComparableTuple8<TItem0, TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7> class instances as keys in a dictionary.
    /// </summary>
    /// <typeparam name="TItem0">The type of the first item</typeparam>
    /// <typeparam name="TItem1">The type of the second item</typeparam>
    /// <typeparam name="TItem2">The type of the third item</typeparam>
    /// <typeparam name="TItem3">The type of the fourth item</typeparam>
    /// <typeparam name="TItem4">The type of the fifth item</typeparam>
    /// <typeparam name="TItem5">The type of the sixth item</typeparam>
    /// <typeparam name="TItem6">The type of the seventh item</typeparam>
    /// <typeparam name="TItem7">The type of the eighth item</typeparam>
    [Serializable]
    public class ComparableTuple8EqualityComparer<TItem0, TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7>
        : IEqualityComparer<ComparableTuple8<TItem0, TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7>>
        where TItem0 : IComparable<TItem0>
        where TItem1 : IComparable<TItem1>
        where TItem2 : IComparable<TItem2>
        where TItem3 : IComparable<TItem3>
        where TItem4 : IComparable<TItem4>
        where TItem5 : IComparable<TItem5>
        where TItem6 : IComparable<TItem6>
        where TItem7 : IComparable<TItem7>
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ComparableTuple8EqualityComparer()
        {
        }

        #endregion

        /// IEqualityComparer.Equals compares the items in this group for equality.
        public bool Equals(ComparableTuple8<TItem0, TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7> groupA,
                           ComparableTuple8<TItem0, TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7> groupB)
        {
            return ((groupA.Item0.Equals(groupB.Item0))
                && (groupA.Item1.Equals(groupB.Item1))
                && (groupA.Item2.Equals(groupB.Item2))
                && (groupA.Item3.Equals(groupB.Item3))
                && (groupA.Item4.Equals(groupB.Item4))
                && (groupA.Item5.Equals(groupB.Item5))
                && (groupA.Item6.Equals(groupB.Item6))
                && (groupA.Item7.Equals(groupB.Item7)));
        }

        /// <summary>
        /// Returns a hash code for an object.
        /// </summary>
        /// <param name="obj">An object of type ComparableTuple8</param>
        /// <returns>A hash code for the object.</returns>
        public int GetHashCode(ComparableTuple8<TItem0, TItem1, TItem2, TItem3, TItem4, TItem5, TItem6, TItem7> group)
        {
            int hash0 = group.Item0.GetHashCode();
            int hash1 = group.Item1.GetHashCode();
            int hash2 = group.Item2.GetHashCode();
            int hash3 = group.Item3.GetHashCode();
            int hash4 = group.Item4.GetHashCode();
            int hash5 = group.Item5.GetHashCode();
            int hash6 = group.Item6.GetHashCode();
            int hash7 = group.Item7.GetHashCode();

            int hash = 577 * hash0 + 599 * hash1 + 619 * hash2 + 661 * hash3
                     + 743 * hash4 + 829 * hash5 + 971 * hash6 + 1117 * hash7;
            return hash.GetHashCode();
        }
    }
}
