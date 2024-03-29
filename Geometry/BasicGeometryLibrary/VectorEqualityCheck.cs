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

namespace TriangulatedPolygonAStar.BasicGeometry
{
    public static class VectorEqualityCheck
    {
        /// <summary>
        /// Defines the higher bound of the cartesian distance between two
        /// equal points. If the distance is exactly the same as the 
        /// specified value, then the equality check should not pass.
        /// </summary>
        public static double Tolerance = 0.00001;
    }
}