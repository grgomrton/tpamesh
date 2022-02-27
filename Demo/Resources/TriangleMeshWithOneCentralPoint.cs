/**
 * Copyright 2021 Márton Gergó
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
using TriangulatedPolygonAStar.BasicGeometry;

namespace TriangulatedPolygonAStar.UI.Resources
{
    public static partial class TriangleMaps
    {
        public static IEnumerable<Triangle> CreateTriangleMapOfPolygonMeshWithOneCentralPoint()
        {
            var a = new Vector(0.0, 0.0, true);
            var b = new Vector(0.0, -2.0);
            var c = new Vector(1.0, -1.0);
            var d = new Vector(2.0, 0.0);
            var e = new Vector(1.0, 1.0);
            var f = new Vector(0.0, 2.0);
            var g = new Vector(-1.0, 1.0);
            var h = new Vector(-2.0, 0.0);
            var i = new Vector(-1.0, -1.0);
            var j = new Vector(2.0, 3.0);
            
            var t1 = new Triangle(a, b, c, 1);
            var t2 = new Triangle(a, c, d, 2);
            var t3 = new Triangle(a, d, e, 3);
            var t4 = new Triangle(a, e, f, 4);
            var t5 = new Triangle(a, f, g, 5);
            var t6 = new Triangle(a, g, h, 6);
            var t7 = new Triangle(a, h, i, 7);
            var t8 = new Triangle(a, i, b, 8);
            var t9= new Triangle(e, j, f, 9);
            var triangles = new[] { t1, t2, t3, t4, t5, t6, t7, t8, t9 };
            SetNeighboursForAll(triangles);
            
            return triangles;
        }
    }
}