/**
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

using System.Collections.Generic;
using System.Linq;
using TriangulatedPolygonAStar.BasicGeometry;

namespace TriangulatedPolygonAStar.UI.Resources
{
    /// <summary>
    /// Maps of triangulated polygons.
    /// </summary>
    public static partial class TriangleMaps
    {
        private static void SetNeighboursForAll(IEnumerable<Triangle> triangles)
        {
            foreach (var triangle in triangles)
            {
                var neighbours = triangles.Where(other => triangle.GetCommonVerticesWith(other).Count() == 2).ToArray();
                triangle.SetNeighbours(neighbours);
            }
        }

        private static void AnalyzeBoundaries(IEnumerable<Triangle> triangles)
        {
            var trianglesOfEdges = new Dictionary<Edge, int>();
            foreach (var triangle in triangles)
            {
                var e1 = new Edge(triangle.A, triangle.B);
                IncreaseNeighbourCount(e1, trianglesOfEdges);
                var e2 = new Edge(triangle.A, triangle.C);
                IncreaseNeighbourCount(e2, trianglesOfEdges);
                var e3 = new Edge(triangle.B, triangle.C);
                IncreaseNeighbourCount(e3, trianglesOfEdges);
            }
            foreach (var boundaryEdge in trianglesOfEdges.Keys.Where(edge => trianglesOfEdges[edge] == 1))
            {
                boundaryEdge.MarkNodesAsBoundary();
            }
        }

        private static void IncreaseNeighbourCount(Edge edge, Dictionary<Edge, int> trianglesOfEdges)
        {
            if (trianglesOfEdges.ContainsKey(edge))
            {
                trianglesOfEdges[edge]++;
            }
            else
            {
                trianglesOfEdges[edge] = 1;
            }
        }
    }
}