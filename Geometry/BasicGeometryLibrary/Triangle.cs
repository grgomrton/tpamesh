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
using System.Collections.Generic;
using System.Linq;

namespace TriangulatedPolygonAStar.BasicGeometry
{
    /// <inheritdoc />
    public class Triangle : ITriangle
    {
        private readonly int id;
        private readonly Vector[] vertices;
        private IEnumerable<Triangle> neighbours;

        /// <summary>
        /// Initializes a new instance of <see cref="Triangle"/> class by its three corner points.
        /// No specific order of the points is expected. 
        /// Distorted triangles which has two or more identical corner are not supported.
        /// Triangles that lie between the same points should have identical ids,
        /// otherwise edges acquired from <see cref="GetCommonEdgeWith"/> might produce different hashes for equal edges.
        /// </summary>
        /// <param name="a">The first corner point</param>
        /// <param name="b">The second corner point</param>
        /// <param name="c">The third corner point</param>
        /// <param name="id">The identifier of the triangle</param>
        public Triangle(Vector a, Vector b, Vector c, int id)
        {
            CheckForNullArgument(a, nameof(a));
            CheckForNullArgument(b, nameof(b));
            CheckForNullArgument(c, nameof(c));
            if (a.Equals(b) || a.Equals(c) || b.Equals(c))
            {
                throw new ArgumentOutOfRangeException("One or more of the specified vertices overlap each other");
            }

            this.id = id;
            vertices = new[]{ a, b, c };
            neighbours = Enumerable.Empty<Triangle>();
        }

        /// <summary>
        /// The first corner of the triangle.
        /// </summary>
        public Vector A => vertices[0];

        /// <summary>
        /// The second corner of the triangle.
        /// </summary>
        public Vector B => vertices[1];

        /// <summary>
        /// The third corner of the triangle.
        /// </summary>
        public Vector C => vertices[2];

        /// <summary>
        /// The identifier of this triangle.
        /// </summary>
        public int Id => id;

        /// <inheritdoc />
        public IEnumerable<ITriangle> Neighbours => neighbours;

        /// <summary>
        /// Sets the neighbours of this triangle. 
        /// Every neighbour triangle is expected to share exactly two vertices with this one. 
        /// The maximum allowed number of neighbours is three.
        /// </summary>
        /// <param name="neighbours">The neighbours to set</param>
        public void SetNeighbours(params Triangle[] neighbours)
        {
            CheckForNullArgument(neighbours, nameof(neighbours));
            if (neighbours.Any(item => item == null))
            {
                throw new ArgumentException("One or more of the specified neighbours is null", nameof(neighbours));
            }
            if (neighbours.Length > 3)
            {
                throw new ArgumentOutOfRangeException(
                    "The amount of the specified neighbours exceeds the maximal amount of three", nameof(neighbours));
            }
            if (neighbours.Any(triangle => triangle.GetCommonVerticesWith(this).Count() != 2))
            {
                throw new ArgumentException(
                    "One or more of the specified triangles are not adjacent with this one", nameof(neighbours));
            }
            
            this.neighbours = neighbours;
        }

        /// <inheritdoc />
        public IEdge GetCommonEdgeWith(ITriangle other)
        {
            CheckForNullArgument(other, nameof(other));
            if (!neighbours.Any(triangle => triangle.Equals(other)))
            {
                throw new ArgumentException("The specified triangle cannot be found among the neighbours",
                    nameof(other));
            }
            
            var neighbour = neighbours.First(triangle => triangle.Equals(other));
            return new Edge(this, neighbour);
        }

        /// <summary>
        /// Determines the set of vertices shared by this triangle and the specified one.
        /// If no shared vertex exists, an empty set is returned.
        /// </summary>
        /// <param name="other">The other triangle to compare this triangle with</param>
        /// <returns>The set of common vertices</returns>
        public IEnumerable<Vector> GetCommonVerticesWith(Triangle other)
        {
            CheckForNullArgument(other, nameof(other));
            
            return vertices.Where(vertex => other.vertices.Any(otherVertex => otherVertex.Equals(vertex)));
        }

        // Source: http://www.blackpawn.com/texts/pointinpoly/default.html
        /// <inheritdoc />
        public bool ContainsPoint(IVector point)
        {
            CheckForNullArgument(point, nameof(point));

            // Compute vectors
            IVector v0 = C.Minus(A); // v0 = C - A
            IVector v1 = B.Minus(A); // v1 = B - A
            IVector v2 = point.Minus(A); // v2 = P - A

            // Lower bounds taking into consideration vector equality check parameters
            double boundaryWidth = VectorEqualityCheck.Tolerance;
            double lowU = boundaryWidth / v0.Length();
            double lowV = boundaryWidth / v1.Length();

            // Compute dot products
            double dot00 = v0.DotProduct(v0); // dot00 = dot(v0, v0)
            double dot01 = v0.DotProduct(v1); // dot01 = dot(v0, v1)
            double dot02 = v0.DotProduct(v2); // dot02 = dot(v0, v2)
            double dot11 = v1.DotProduct(v1); // dot11 = dot(v1, v1)
            double dot12 = v1.DotProduct(v2); // dot12 = dot(v1, v2)

            // Compute barycentric coordinates
            double invDenom = 1 / (dot00 * dot11 - dot01 * dot01); // invDenom = 1 / (dot00 * dot11 - dot01 * dot01)
            double u = (dot11 * dot02 - dot01 * dot12) * invDenom; // u = (dot11 * dot02 - dot01 * dot12) * invDenom
            double v = (dot00 * dot12 - dot01 * dot02) * invDenom; // v = (dot00 * dot12 - dot01 * dot02) * invDenom

            // Check if point is in triangle
            // The higher bound is increased by the applicable border size for the u and v weights
            return (u > -lowU) && (v > -lowV) &&
                   (u + v < 1.0 + lowU * u + lowV * v); // return (u >= 0) && (v >= 0) && (u + v < 1)
        }

        /// <summary>
        /// Determines whether the specified object represents the same triangle as this one.
        /// Two triangles are considered to be equal if their endpoints are closer than the value 
        /// specified in <see cref="VectorEqualityCheck.Tolerance"/>.
        /// Please note, that since <see cref="Vector"/> instances are compared with an absolute
        /// tolerance, the <see cref="Equals"/> implementation will not be transitive, meaning
        /// a.equals(b) &amp;&amp; b.equals(c) => a.equals(c) will not necessarily hold.
        /// </summary>
        /// <param name="other">The other object to compare with</param>
        /// <returns>true if the specified object is equal to the current object, otherwise false</returns>
        public override bool Equals(object other)
        {
            if (other is Triangle otherTriangle)
            {
                return GetCommonVerticesWith(otherTriangle).Count() == 3;
            }
            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance. 
        /// <see cref="GetHashCode"/> provides a useful distribution only if the uniqueness requirement holds in the system
        /// described in 
        /// <see cref="Triangle(TriangulatedPolygonAStar.BasicGeometry.Vector,TriangulatedPolygonAStar.BasicGeometry.Vector,TriangulatedPolygonAStar.BasicGeometry.Vector,int)"/>.
        /// </summary>
        /// <returns>An integer value that specifies a hash value for this instance</returns>
        public override int GetHashCode()
        {
            return id;
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