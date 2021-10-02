using System.Collections.Generic;
using TriangulatedPolygonAStar.BasicGeometry;

namespace TriangulatedPolygonAStar.UI.Resources
{
    public static partial class TriangleMaps
    {
        public static IEnumerable<Triangle> CreateTriangleMapOfPolygonMeshWithTwoPolygonHoles()
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
            var n23 = new Vector(1.5, 2);
            var n24 = new Vector(5.5, 3.25);
            var n25 = new Vector(6.25, 2.625);
            var n26 = new Vector(4, 2.5);
            var n27 = new Vector(4.75, 3.875);
            var n28 = new Vector(5.5, 1.75);
            var n29 = new Vector(4.5, 2.75);
            var n30 = new Vector(5.875, 2.9375);
            var n31 = new Vector(4.5, 2);
            var n32 = new Vector(5.25, 2.125);
            var n33 = new Vector(5.8203125, 2.234375);
            var n34 = new Vector(5.4660773026315788, 2.5398848684210527);
            var n35 = new Vector(6.1700246710526319, 1.655016447368421);
            var n36 = new Vector(6.75, 1);
            var n37 = new Vector(3.5, 3.5);
            var n38 = new Vector(1, 3.5);
            var n39 = new Vector(4, -1.25);
            var n40 = new Vector(2.5, 5.75);
            var n41 = new Vector(0.36754446796632401, 5.102633403898972);
            var n42 = new Vector(3.25, 5.125);
            var n43 = new Vector(2.3762019230769229, 4.8389423076923075);
            var n44 = new Vector(1.4323072803947681, 5.4311423268775529);
            var n45 = new Vector(2.75, 3.75);
            var n46 = new Vector(3.3214285714285716, 4.2142857142857144);

            var t1 = new Triangle(n33, n32, n28, 1);
            var t2 = new Triangle(n12, n11, n21, 2);
            var t3 = new Triangle(n12, n17, n16, 3);
            var t4 = new Triangle(n17, n12, n21, 4);
            var t5 = new Triangle(n29, n26, n31, 5);
            var t6 = new Triangle(n15, n24, n27, 6);
            var t7 = new Triangle(n10, n20, n39, 7);
            var t8 = new Triangle(n23, n16, n13, 8);
            var t9 = new Triangle(n34, n24, n15, 9);
            var t10 = new Triangle(n2, n1, n38, 10);
            var t11 = new Triangle(n14, n43, n44, 11);
            var t12 = new Triangle(n41, n38, n14, 12);
            var t13 = new Triangle(n28, n35, n33, 13);
            var t14 = new Triangle(n37, n15, n27, 14);
            var t15 = new Triangle(n12, n16, n23, 15);
            var t16 = new Triangle(n28, n19, n35, 16);
            var t17 = new Triangle(n31, n18, n29, 17);
            var t18 = new Triangle(n25, n35, n4, 18);
            var t19 = new Triangle(n33, n25, n30, 19);
            var t20 = new Triangle(n20, n10, n9, 20);
            var t21 = new Triangle(n5, n9, n8, 21);
            var t22 = new Triangle(n9, n5, n20, 22);
            var t23 = new Triangle(n33, n30, n34, 23);
            var t24 = new Triangle(n5, n8, n6, 24);
            var t25 = new Triangle(n34, n30, n24, 25);
            var t26 = new Triangle(n20, n5, n19, 26);
            var t27 = new Triangle(n18, n32, n34, 27);
            var t28 = new Triangle(n38, n23, n13, 28);
            var t29 = new Triangle(n18, n15, n29, 29);
            var t30 = new Triangle(n26, n16, n17, 30);
            var t31 = new Triangle(n15, n18, n34, 31);
            var t32 = new Triangle(n7, n6, n8, 32);
            var t33 = new Triangle(n25, n33, n35, 33);
            var t34 = new Triangle(n35, n36, n4, 34);
            var t35 = new Triangle(n27, n22, n37, 35);
            var t36 = new Triangle(n41, n2, n38, 36);
            var t37 = new Triangle(n32, n33, n34, 37);
            var t38 = new Triangle(n21, n39, n20, 38);
            var t39 = new Triangle(n36, n35, n19, 39);
            var t40 = new Triangle(n26, n17, n31, 40);
            var t41 = new Triangle(n19, n5, n36, 41);
            var t42 = new Triangle(n43, n40, n44, 42);
            var t43 = new Triangle(n13, n14, n38, 43);
            var t44 = new Triangle(n23, n38, n1, 44);
            var t45 = new Triangle(n42, n40, n43, 45);
            var t46 = new Triangle(n21, n11, n39, 46);
            var t47 = new Triangle(n37, n46, n45, 47);
            var t48 = new Triangle(n41, n14, n44, 48);
            var t49 = new Triangle(n45, n46, n43, 49);
            var t50 = new Triangle(n37, n22, n46, 50);
            var t51 = new Triangle(n40, n3, n44, 51);
            var t52 = new Triangle(n3, n41, n44, 52);
            var t53 = new Triangle(n22, n42, n46, 53);
            var t54 = new Triangle(n43, n14, n45, 54);
            var t55 = new Triangle(n43, n46, n42, 55);

            var triangles = new[]
            {
                t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, t19, t20, t21, t22,
                t23, t24, t25, t26, t27, t28, t29, t30, t31, t32, t33, t34, t35, t36, t37, t38, t39, t40, t41, t42, t43,
                t44, t45, t46, t47, t48, t49, t50, t51, t52, t53, t54, t55
            };
            SetNeighboursForAll(triangles);

            return triangles;
        }
    }
}