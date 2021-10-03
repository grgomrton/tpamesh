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

namespace TriangulatedPolygonAStar.UI
{
    public interface ILocationMarker : IDrawable
    {
        /// <summary>
        /// The position where this marker is currently put.
        /// </summary>
        IVector CurrentLocation { get; }
        
        /// <summary>
        /// Sets the position of this marker.
        /// </summary>
        /// <param name="position">The position to move this marker to</param>
        void SetLocation(IVector position);

        /// <summary>
        /// Determines, whether the specified position is under the marker sign.
        /// </summary>
        /// <param name="position">The position in the same coordinate-system as the location</param>
        /// <returns>true if the specified position falls under the marker, otherwise false</returns>
        bool IsPositionUnderMarker(IVector position);
    }
}