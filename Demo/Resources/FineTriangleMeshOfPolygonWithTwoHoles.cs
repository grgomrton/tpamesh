﻿/**
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
        public static IEnumerable<Triangle> CreateTriangleMapOfFinePolygonMeshWithTwoPolygonHoles()
        {
            var n1 = new Vector(0, 3, false);
            var n2 = new Vector(0, 4, false);
            var n3 = new Vector(1, 7, false);
            var n4 = new Vector(7, 2, false);
            var n5 = new Vector(6.5, 0, false);
            var n6 = new Vector(7, 1, false);
            var n7 = new Vector(8, 2, false);
            var n8 = new Vector(8.5, 0, false);
            var n9 = new Vector(7, -1, false);
            var n10 = new Vector(5.5, -1.5, false);
            var n11 = new Vector(2.5, -1, false);
            var n12 = new Vector(3, 1, false);
            var n13 = new Vector(2, 3, false);
            var n14 = new Vector(2, 4, false);
            var n15 = new Vector(5, 3, false);
            var n16 = new Vector(3, 2, false);
            var n17 = new Vector(4, 1.5, false);
            var n18 = new Vector(5, 2.5, false);
            var n19 = new Vector(6, 1, false);
            var n20 = new Vector(5.5, -0.5, false);
            var n21 = new Vector(4, 0, false);
            var n22 = new Vector(4, 4.5, false);
            var n23 = new Vector(4, 2.5, false);
            var n24 = new Vector(5.5, 3.25, false);
            var n25 = new Vector(4.75, 3.875, false);
            var n26 = new Vector(6.375, 1.2554328304597702, true);
            var n27 = new Vector(6.25, 2.625, false);
            var n28 = new Vector(3.375, 1.5, true);
            var n29 = new Vector(3.6875, 2, true);
            var n30 = new Vector(3.75, 0.75, true);
            var n31 = new Vector(1.5, 2, false);
            var n32 = new Vector(1, 3.5, true);
            var n33 = new Vector(6.166666666666667, -1, true);
            var n34 = new Vector(5.6458333333333339, -1, true);
            var n35 = new Vector(6.5833333333333339, -0.58333333333333326, true);
            var n36 = new Vector(6.0555555555555562, -0.36111111111111155, true);
            var n37 = new Vector(5.8765432098765427, 0.31327160493827128, true);
            var n38 = new Vector(7.82287724206844, 0.10049891058853283, true);
            var n39 = new Vector(7.75, -0.5, false);
            var n40 = new Vector(7.1275510204081636, 0.38307823129251728, true);
            var n41 = new Vector(8.294107768307768, 0.47977694207694183, true);
            var n42 = new Vector(8.25, 1, false);
            var n43 = new Vector(7.5, 1.5, false);
            var n44 = new Vector(7.1583333333333341, -0.42500000000000038, true);
            var n45 = new Vector(7.6893556095523623, 0.69048366266248129, true);
            var n46 = new Vector(7.8045566839718425, 1.1443350259577638, true);
            var n47 = new Vector(6.75, 1, false);
            var n48 = new Vector(6.375, 0.57812283257039754, true);
            var n49 = new Vector(5.5, 1.75, false);
            var n50 = new Vector(6.875, 1.5, false);
            var n51 = new Vector(6.1961206896551726, 1.6724137931034484, true);
            var n52 = new Vector(2.9356060606060606, 0.48484848484848475, true);
            var n53 = new Vector(3.375, 0.20833333333333334, true);
            var n54 = new Vector(4.5, 2, false);
            var n55 = new Vector(5.8988656849915779, 2.1670436985786479, true);
            var n56 = new Vector(5.6774323334791728, 2.7004188001750071, true);
            var n57 = new Vector(5.4122625436850589, 2.2331750291233723, true);
            var n58 = new Vector(6.4765573584741656, 2.1343688301689991, true);
            var n59 = new Vector(2.25, 1.5, false);
            var n60 = new Vector(2.7916666666666665, 1.5, true);
            var n61 = new Vector(2.2749999999999999, 2.2749999999999999, true);
            var n62 = new Vector(1.7607843137254902, 2.4946078431372549, true);
            var n63 = new Vector(0.75, 2.5, false);
            var n64 = new Vector(1.6760355067073738, 3.5, true);
            var n65 = new Vector(1.2551140475745419, 2.4451710713618127, true);
            var n66 = new Vector(1.0549240282829067, 2.9550189929292734, true);
            var n67 = new Vector(0.53600990345012267, 3.1779801930997547, true);
            var n68 = new Vector(0.49277845508870843, 3.7355569101774169, true);
            var n69 = new Vector(0.5, 5.5, false);
            var n70 = new Vector(1.9958758070561353, 5.9075509684673628, true);
            var n71 = new Vector(1.9728947368421053, 1.8968421052631579, true);
            var n72 = new Vector(1.6520829228554552, 5.0458334506127267, true);
            var n73 = new Vector(1.3380177533536868, 3.9893406828688338, true);
            var n74 = new Vector(0.75, 6.25, false);
            var n75 = new Vector(2.5, 5.75, false);
            var n76 = new Vector(0.25, 4.75, false);
            var n77 = new Vector(0.61417790751603885, 4.3008831050468066, true);
            var n78 = new Vector(1.1100634542702981, 5.3592202124485064, true);
            var n79 = new Vector(1.66138500345051, 4.4681405144681614, true);
            var n80 = new Vector(0.98306580201289973, 4.7461394974195956, true);
            var n81 = new Vector(2.0851269259301333, 5.3869765286837268, true);
            var n82 = new Vector(0.61809118153198483, 5.0439696061560051, true);
            var n83 = new Vector(2.432737567650519, 4.6695409794220994, true);
            var n84 = new Vector(1.75, 6.375, false);
            var n85 = new Vector(1.5919797404860943, 5.5703658951639348, true);
            var n86 = new Vector(1.1611957293452149, 5.8980492595390013, true);
            var n87 = new Vector(3.4399255597324112, 4.155037220133794, true);
            var n88 = new Vector(1.2255434782608696, 6.5081521739130439, true);
            var n89 = new Vector(2.4225933703644724, 4.2014834636566629, true);
            var n90 = new Vector(3.5, 3.5, false);
            var n91 = new Vector(2.8157852584733769, 3.9473557754201303, true);
            var n92 = new Vector(3.2679317967297923, 5.0614267724545234, true);
            var n93 = new Vector(2.9795504490585838, 4.4968940061310869, true);
            var n94 = new Vector(3.5268541364822052, 4.6410458126134309, true);
            var n95 = new Vector(4.2532894736842106, 3.2598684210526314, true);
            var n96 = new Vector(4.7180799220272904, 3.3926656920077973, true);
            var n97 = new Vector(4.1170646047724535, 3.8779775257269438, true);
            var n98 = new Vector(2.6954826738148956, 5.1955073318936327, true);
            var n99 = new Vector(6.9480251331525071, -0.028385904855267818, true);
            var n100 = new Vector(4.75, -0.25, false);
            var n101 = new Vector(4.75, -1.375, false);
            var n102 = new Vector(3.25, -1.125, false);
            var n103 = new Vector(4, -1.25, false);
            var n104 = new Vector(3.7529269972451789, -0.42957988980716233, true);
            var n105 = new Vector(3.171325481088255, -0.22230977659809795, true);
            var n106 = new Vector(2.75, 0, false);
            var n107 = new Vector(4.208333333333333, -0.625, true);
            var n108 = new Vector(5.0455419167974167, -0.613374249607749, true);
            var n109 = new Vector(5.1595109140084636, -1.0957529242054027, true);
            var n110 = new Vector(4.6307059272465949, -0.89055497847005161, true);
            var n111 = new Vector(3.3422986062036406, -0.66218208385982069, true);

            var t1 = new Triangle(n50, n26, n47, 1);
            var t2 = new Triangle(n104, n21, n53, 2);
            var t3 = new Triangle(n28, n16, n60, 3);
            var t4 = new Triangle(n28, n30, n17, 4);
            var t5 = new Triangle(n55, n51, n58, 5);
            var t6 = new Triangle(n49, n51, n55, 6);
            var t7 = new Triangle(n109, n20, n108, 7);
            var t8 = new Triangle(n62, n71, n61, 8);
            var t9 = new Triangle(n29, n28, n17, 9);
            var t10 = new Triangle(n67, n68, n1, 10);
            var t11 = new Triangle(n77, n2, n68, 11);
            var t12 = new Triangle(n64, n73, n32, 12);
            var t13 = new Triangle(n24, n25, n96, 13);
            var t14 = new Triangle(n83, n72, n79, 14);
            var t15 = new Triangle(n12, n60, n59, 15);
            var t16 = new Triangle(n23, n16, n29, 16);
            var t17 = new Triangle(n23, n54, n18, 17);
            var t18 = new Triangle(n13, n66, n62, 18);
            var t19 = new Triangle(n57, n18, n49, 19);
            var t20 = new Triangle(n34, n33, n36, 20);
            var t21 = new Triangle(n9, n39, n44, 21);
            var t22 = new Triangle(n33, n35, n36, 22);
            var t23 = new Triangle(n21, n17, n30, 23);
            var t24 = new Triangle(n99, n35, n44, 24);
            var t25 = new Triangle(n66, n13, n64, 25);
            var t26 = new Triangle(n5, n37, n36, 26);
            var t27 = new Triangle(n15, n18, n56, 27);
            var t28 = new Triangle(n18, n15, n23, 28);
            var t29 = new Triangle(n51, n26, n50, 29);
            var t30 = new Triangle(n16, n28, n29, 30);
            var t31 = new Triangle(n26, n51, n19, 31);
            var t32 = new Triangle(n40, n99, n38, 32);
            var t33 = new Triangle(n30, n28, n12, 33);
            var t34 = new Triangle(n48, n26, n19, 34);
            var t35 = new Triangle(n77, n73, n80, 35);
            var t36 = new Triangle(n82, n78, n69, 36);
            var t37 = new Triangle(n10, n109, n101, 37);
            var t38 = new Triangle(n12, n106, n52, 38);
            var t39 = new Triangle(n32, n66, n64, 39);
            var t40 = new Triangle(n54, n29, n17, 40);
            var t41 = new Triangle(n10, n9, n33, 41);
            var t42 = new Triangle(n35, n33, n9, 42);
            var t43 = new Triangle(n34, n109, n10, 43);
            var t44 = new Triangle(n10, n33, n34, 44);
            var t45 = new Triangle(n35, n99, n5, 45);
            var t46 = new Triangle(n36, n35, n5, 46);
            var t47 = new Triangle(n26, n48, n47, 47);
            var t48 = new Triangle(n36, n20, n34, 48);
            var t49 = new Triangle(n19, n20, n37, 49);
            var t50 = new Triangle(n20, n36, n37, 50);
            var t51 = new Triangle(n38, n8, n41, 51);
            var t52 = new Triangle(n41, n45, n38, 52);
            var t53 = new Triangle(n38, n44, n39, 53);
            var t54 = new Triangle(n40, n38, n45, 54);
            var t55 = new Triangle(n8, n42, n41, 55);
            var t56 = new Triangle(n5, n40, n6, 56);
            var t57 = new Triangle(n5, n99, n40, 57);
            var t58 = new Triangle(n45, n41, n42, 58);
            var t59 = new Triangle(n9, n44, n35, 59);
            var t60 = new Triangle(n45, n46, n6, 60);
            var t61 = new Triangle(n43, n46, n7, 61);
            var t62 = new Triangle(n6, n40, n45, 62);
            var t63 = new Triangle(n8, n38, n39, 63);
            var t64 = new Triangle(n45, n42, n46, 64);
            var t65 = new Triangle(n7, n46, n42, 65);
            var t66 = new Triangle(n6, n46, n43, 66);
            var t67 = new Triangle(n48, n19, n37, 67);
            var t68 = new Triangle(n37, n5, n48, 68);
            var t69 = new Triangle(n5, n47, n48, 69);
            var t70 = new Triangle(n58, n50, n4, 70);
            var t71 = new Triangle(n27, n24, n56, 71);
            var t72 = new Triangle(n58, n51, n50, 72);
            var t73 = new Triangle(n27, n56, n55, 73);
            var t74 = new Triangle(n51, n49, n19, 74);
            var t75 = new Triangle(n53, n106, n105, 75);
            var t76 = new Triangle(n109, n34, n20, 76);
            var t77 = new Triangle(n20, n100, n108, 77);
            var t78 = new Triangle(n106, n53, n52, 78);
            var t79 = new Triangle(n29, n54, n23, 79);
            var t80 = new Triangle(n15, n56, n24, 80);
            var t81 = new Triangle(n57, n56, n18, 81);
            var t82 = new Triangle(n49, n55, n57, 82);
            var t83 = new Triangle(n56, n57, n55, 83);
            var t84 = new Triangle(n4, n27, n58, 84);
            var t85 = new Triangle(n55, n58, n27, 85);
            var t86 = new Triangle(n59, n60, n61, 86);
            var t87 = new Triangle(n28, n60, n12, 87);
            var t88 = new Triangle(n61, n60, n16, 88);
            var t89 = new Triangle(n16, n13, n61, 89);
            var t90 = new Triangle(n62, n61, n13, 90);
            var t91 = new Triangle(n14, n64, n13, 91);
            var t92 = new Triangle(n71, n62, n31, 92);
            var t93 = new Triangle(n63, n65, n66, 93);
            var t94 = new Triangle(n66, n32, n67, 94);
            var t95 = new Triangle(n78, n82, n80, 95);
            var t96 = new Triangle(n63, n66, n67, 96);
            var t97 = new Triangle(n31, n62, n65, 97);
            var t98 = new Triangle(n63, n31, n65, 98);
            var t99 = new Triangle(n65, n62, n66, 99);
            var t100 = new Triangle(n68, n67, n32, 100);
            var t101 = new Triangle(n63, n67, n1, 101);
            var t102 = new Triangle(n14, n89, n79, 102);
            var t103 = new Triangle(n1, n68, n2, 103);
            var t104 = new Triangle(n73, n77, n68, 104);
            var t105 = new Triangle(n77, n82, n76, 105);
            var t106 = new Triangle(n81, n70, n85, 106);
            var t107 = new Triangle(n14, n79, n73, 107);
            var t108 = new Triangle(n31, n59, n71, 108);
            var t109 = new Triangle(n59, n61, n71, 109);
            var t110 = new Triangle(n72, n85, n78, 110);
            var t111 = new Triangle(n81, n85, n72, 111);
            var t112 = new Triangle(n14, n73, n64, 112);
            var t113 = new Triangle(n79, n80, n73, 113);
            var t114 = new Triangle(n75, n84, n70, 114);
            var t115 = new Triangle(n80, n72, n78, 115);
            var t116 = new Triangle(n78, n85, n86, 116);
            var t117 = new Triangle(n82, n69, n76, 117);
            var t118 = new Triangle(n73, n68, n32, 118);
            var t119 = new Triangle(n81, n83, n98, 119);
            var t120 = new Triangle(n79, n89, n83, 120);
            var t121 = new Triangle(n84, n85, n70, 121);
            var t122 = new Triangle(n81, n75, n70, 122);
            var t123 = new Triangle(n84, n3, n88, 123);
            var t124 = new Triangle(n82, n77, n80, 124);
            var t125 = new Triangle(n78, n86, n69, 125);
            var t126 = new Triangle(n87, n22, n94, 126);
            var t127 = new Triangle(n87, n91, n90, 127);
            var t128 = new Triangle(n72, n83, n81, 128);
            var t129 = new Triangle(n80, n79, n72, 129);
            var t130 = new Triangle(n69, n86, n74, 130);
            var t131 = new Triangle(n97, n87, n90, 131);
            var t132 = new Triangle(n15, n96, n95, 132);
            var t133 = new Triangle(n84, n86, n85, 133);
            var t134 = new Triangle(n88, n86, n84, 134);
            var t135 = new Triangle(n2, n77, n76, 135);
            var t136 = new Triangle(n96, n97, n95, 136);
            var t137 = new Triangle(n3, n74, n88, 137);
            var t138 = new Triangle(n86, n88, n74, 138);
            var t139 = new Triangle(n22, n75, n92, 139);
            var t140 = new Triangle(n14, n91, n89, 140);
            var t141 = new Triangle(n83, n89, n93, 141);
            var t142 = new Triangle(n14, n90, n91, 142);
            var t143 = new Triangle(n93, n89, n91, 143);
            var t144 = new Triangle(n93, n91, n87, 144);
            var t145 = new Triangle(n75, n98, n92, 145);
            var t146 = new Triangle(n94, n93, n87, 146);
            var t147 = new Triangle(n98, n93, n92, 147);
            var t148 = new Triangle(n22, n92, n94, 148);
            var t149 = new Triangle(n93, n94, n92, 149);
            var t150 = new Triangle(n97, n25, n22, 150);
            var t151 = new Triangle(n90, n15, n95, 151);
            var t152 = new Triangle(n24, n96, n15, 152);
            var t153 = new Triangle(n97, n96, n25, 153);
            var t154 = new Triangle(n87, n97, n22, 154);
            var t155 = new Triangle(n90, n95, n97, 155);
            var t156 = new Triangle(n81, n98, n75, 156);
            var t157 = new Triangle(n93, n98, n83, 157);
            var t158 = new Triangle(n38, n99, n44, 158);
            var t159 = new Triangle(n11, n111, n105, 159);
            var t160 = new Triangle(n11, n105, n106, 160);
            var t161 = new Triangle(n103, n101, n110, 161);
            var t162 = new Triangle(n53, n30, n52, 162);
            var t163 = new Triangle(n103, n104, n111, 163);
            var t164 = new Triangle(n21, n107, n100, 164);
            var t165 = new Triangle(n104, n53, n105, 165);
            var t166 = new Triangle(n105, n111, n104, 166);
            var t167 = new Triangle(n104, n107, n21, 167);
            var t168 = new Triangle(n30, n53, n21, 168);
            var t169 = new Triangle(n111, n11, n102, 169);
            var t170 = new Triangle(n103, n107, n104, 170);
            var t171 = new Triangle(n110, n109, n108, 171);
            var t172 = new Triangle(n101, n109, n110, 172);
            var t173 = new Triangle(n110, n100, n107, 173);
            var t174 = new Triangle(n100, n110, n108, 174);
            var t175 = new Triangle(n52, n30, n12, 175);
            var t176 = new Triangle(n103, n110, n107, 176);
            var t177 = new Triangle(n103, n111, n102, 177);

            var triangles = new[]
            {
                t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, t19, t20, t21, t22,
                t23, t24, t25, t26, t27, t28, t29, t30, t31, t32, t33, t34, t35, t36, t37, t38, t39, t40, t41, t42, t43,
                t44, t45, t46, t47, t48, t49, t50, t51, t52, t53, t54, t55, t56, t57, t58, t59, t60, t61, t62, t63, t64,
                t65, t66, t67, t68, t69, t70, t71, t72, t73, t74, t75, t76, t77, t78, t79, t80, t81, t82, t83, t84, t85,
                t86, t87, t88, t89, t90, t91, t92, t93, t94, t95, t96, t97, t98, t99, t100, t101, t102, t103, t104,
                t105, t106, t107, t108, t109, t110, t111, t112, t113, t114, t115, t116, t117, t118, t119, t120, t121,
                t122, t123, t124, t125, t126, t127, t128, t129, t130, t131, t132, t133, t134, t135, t136, t137, t138,
                t139, t140, t141, t142, t143, t144, t145, t146, t147, t148, t149, t150, t151, t152, t153, t154, t155,
                t156, t157, t158, t159, t160, t161, t162, t163, t164, t165, t166, t167, t168, t169, t170, t171, t172,
                t173, t174, t175, t176, t177
            };
            SetNeighboursForAll(triangles);

            return triangles;
        }
    }
}