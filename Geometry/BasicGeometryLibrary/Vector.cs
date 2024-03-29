﻿/**
 * Copyright 2017 Márton Gergó
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Globalization;

namespace TriangulatedPolygonAStar.BasicGeometry
{
    /// <inheritdoc />
    public class Vector : IVector
    {
        private readonly double x;
        private readonly double y;
        private bool isInternal;

        /// <summary>
        /// Initializes a new instance of <see cref="Vector"/> class by its two components.
        /// </summary>
        /// <param name="x">The horizontal component of the vector</param>
        /// <param name="y">The vertical component of the vector</param>
        public Vector(double x, double y) : this(x, y, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Vector"/> class by its two components and
        /// optionally the flag that indicates whether this vector is an internal vector of the polygon.
        /// </summary>
        /// <param name="x">The horizontal component of the vector</param>
        /// <param name="y">The vertical component of the vector</param>
        /// <param name="isInternal">The flag that indicates whether this point is an internal point in the polygon map</param>
        public Vector(double x, double y, bool isInternal)
        {
            this.x = x;
            this.y = y;
            this.isInternal = isInternal;
        }

        /// <inheritdoc />
        public double X => x;

        /// <inheritdoc />
        public double Y => y;

        /// <inheritdoc />
        public bool IsInternal
        {
            get => isInternal;
            set => isInternal = value;
        }

        /// <inheritdoc />
        public IVector Plus(IVector other)
        {
            CheckForNullArgument(other, nameof(other));

            return new Vector(X + other.X, Y + other.Y);
        }

        /// <inheritdoc />
        public IVector Minus(IVector other)
        {
            CheckForNullArgument(other, nameof(other));

            return new Vector(X - other.X, Y - other.Y);
        }

        /// <inheritdoc />
        public IVector Times(double scalar)
        {
            return new Vector(scalar * X, scalar * Y);
        }

        /// <inheritdoc />
        public double DistanceFrom(IVector other)
        {
            CheckForNullArgument(other, nameof(other));

            return other.Minus(this).Length();
        }

        /// <inheritdoc />
        public bool IsInCounterClockWiseDirectionFrom(IVector other)
        {
            CheckForNullArgument(other, nameof(other));

            return ZComponentOfCrossProductWith(other) <= 0.0;
        }

        /// <inheritdoc />
        public bool IsInClockWiseDirectionFrom(IVector other)
        {
            CheckForNullArgument(other, nameof(other));

            return ZComponentOfCrossProductWith(other) >= 0.0;
        }

        /// <summary>
        /// Determines whether the specified object represents the same point in the space as this vector.
        /// Two vectors are considered to represent the same point if the cartesian distance between them is smaller 
        /// than the tolerance set in <see cref="VectorEqualityCheck.Tolerance"/>.
        /// Please note, that since <see cref="Vector"/> instances are compared with an absolute
        /// tolerance, the <see cref="Equals"/> implementation will not be transitive, meaning
        /// a.equals(b) &amp;&amp; b.equals(c) => a.equals(c) will not necessarily hold.
        /// </summary>
        /// <param name="other">The other object to compare this vector with</param>
        /// <returns>true if the two object represent the same point, otherwise false</returns>
        public override bool Equals(object other)
        {
            if (other is Vector otherVector)
            {
                return otherVector.DistanceFrom(this) < VectorEqualityCheck.Tolerance && otherVector.IsInternal == this.IsInternal;
            }
            return false;
        }
        
        /// <summary>
        /// Returns a constant value hash code for this instance.
        /// The reason for using constant value is that this is the only hashcode which will never break
        /// the a.equals(b) => a.gethashcode() == b.gethashcode() rule for vectors compared with absolute precision, 
        /// which is a necessary requirement to ensure that the instance will be found using hash-based search.
        /// It also means that vector instances cannot be stored effectively in hash based structures. 
        /// Storing vectors in hashmap will cause the map to fall back to linear search.  
        /// </summary>
        /// <returns>An integer value that specifies a hash value for this instance</returns>
        public override int GetHashCode()
        {
            return 0;
        }

        /// <summary>
        /// Creates a textual representation of this vector with its components rounded to hundredths.
        /// The first value is the <see cref="X"/>, the second value is the <see cref="Y"/> component.
        /// </summary>
        /// <returns>The rounded components of this vector as text</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "({0:0.00}, {1:0.00})", X, Y);
        }

        private double ZComponentOfCrossProductWith(IVector other)
        {
            return X * other.Y - Y * other.X;
        }

        private static void CheckForNullArgument(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}