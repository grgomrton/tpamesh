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
        public static IEnumerable<Triangle> CreateTriangleMapOfPolygonMeshWithAddedPointsOnlyOnBoundaries()
        {
            var n1 = new Vector(0, 3);
            var n2 = new Vector(0, 4);
            var n3 = new Vector(1, 7);
            var n4 = new Vector(7, 2);
            var n5 = new Vector(6.5, 0);
            var n6 = new Vector(7, 1);
            var n7 = new Vector(8, 2);
            var n8 = new Vector(8.5, 0);
            var n9 = new Vector(7, -1);
            var n10 = new Vector(5.5, -1.5);
            var n11 = new Vector(2.5, -1);
            var n12 = new Vector(3, 1);
            var n13 = new Vector(2, 3);
            var n14 = new Vector(2, 4);
            var n15 = new Vector(5, 3);
            var n16 = new Vector(3, 2);
            var n17 = new Vector(4, 1.5);
            var n18 = new Vector(5, 2.5);
            var n19 = new Vector(6, 1);
            var n20 = new Vector(5.5, -0.5);
            var n21 = new Vector(4, 0);
            var n22 = new Vector(4, 4.5);
            var n23 = new Vector(6.75, 1);
            var n24 = new Vector(4, -1.25);
            var n25 = new Vector(1.5, 2);
            var n26 = new Vector(4, 2.5);
            var n27 = new Vector(3.5, 3.5);
            var n28 = new Vector(2.5, 5.75);
            var n29 = new Vector(5.5, 3.25);
            var n30 = new Vector(4.75, 3.875);

            var t1= new Triangle(n18, n29, n15, 1);
            var t2= new Triangle(n12, n11, n21, 2);
            var t3= new Triangle(n12, n17, n16, 3);
            var t4= new Triangle(n17, n12, n21, 4);
            var t5= new Triangle(n22, n14, n27, 5);
            var t6= new Triangle(n14, n22, n28, 6);
            var t7= new Triangle(n10, n20, n24, 7);
            var t8= new Triangle(n25, n16, n13, 8);
            var t9= new Triangle(n2, n1, n13, 9);
            var t10= new Triangle(n14, n28, n2, 10);
            var t11= new Triangle(n14, n2, n13, 11);
            var t12= new Triangle(n13, n1, n25, 12);
            var t13= new Triangle(n27, n15, n30, 13);
            var t14= new Triangle(n12, n16, n25, 14);
            var t15= new Triangle(n26, n17, n18, 15);
            var t16= new Triangle(n21, n11, n24, 16);
            var t17= new Triangle(n20, n10, n9, 17);
            var t18= new Triangle(n5, n9, n8, 18);
            var t19= new Triangle(n9, n5, n20, 19);
            var t20= new Triangle(n5, n8, n6, 20);
            var t21= new Triangle(n20, n5, n19, 21);
            var t22= new Triangle(n18, n4, n29, 22);
            var t23= new Triangle(n19, n5, n23, 23);
            var t24= new Triangle(n4, n18, n19, 24);
            var t25= new Triangle(n26, n16, n17, 25);
            var t26= new Triangle(n18, n15, n26, 26);
            var t27= new Triangle(n7, n6, n8, 27);
            var t28= new Triangle(n15, n29, n30, 28);
            var t29= new Triangle(n4, n19, n23, 29);
            var t30= new Triangle(n30, n22, n27, 30);
            var t31= new Triangle(n2, n28, n3, 31);
            var t32= new Triangle(n21, n24, n20, 32);

            var triangles = new[]
            {
                t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, t19, t20, t21, t22,
                t23, t24, t25, t26, t27, t28, t29, t30, t31, t32
            };
            SetNeighboursForAll(triangles);

            return triangles;
        }
    }
}