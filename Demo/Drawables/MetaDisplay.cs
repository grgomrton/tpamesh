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

using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TriangulatedPolygonAStar.UI
{
    /// <summary>
    /// An overlay layer that display metadata in the bottom-right corner of the canvas.
    /// </summary>
    public class MetaDisplay : IOverlay
    {
        private static readonly Font CaptionFont;
        private static readonly Brush CaptionBrush;
        private readonly Point offset;
        private readonly IEnumerable<ILocationMarker> goals;
        private readonly ILocationMarker start;
        private readonly PolyLine path;
        private DrawableTriangle selectedTriangle;
        
        /// <summary>
        /// Initializes a new instance of <see cref="MetaDisplay" /> class which 
        /// displays metadata in the bottom-right corner of the canvas.
        /// </summary>
        /// <param name="start">The start point to display</param>
        /// <param name="goals">The set of goal points to display</param>
        /// <param name="path">The path display</param>
        /// <param name="distanceFromRightInPx">The horizontal offset from the edge of the canvas</param>
        /// <param name="distanceFromBottomInPx">The vertical offset from the edge of the canvas</param>
        public MetaDisplay(ILocationMarker start, IEnumerable<ILocationMarker> goals, PolyLine path, int distanceFromRightInPx, int distanceFromBottomInPx) // TODO add start and maybe path
        {
            offset = new Point(-distanceFromRightInPx, -distanceFromBottomInPx);
            this.start = start;
            this.goals = goals;
            this.path = path;
            this.selectedTriangle = null;
        }

        /// <summary>
        /// Sets the triangle whose metadata is displayed.
        /// </summary>
        /// <param name="triangle">The triangle to display information about</param>
        public void SetSelectedTriangle(DrawableTriangle triangle)
        {
            this.selectedTriangle = triangle;
        }

        /// <summary>
        /// Removes the triangle whose details should be shown.
        /// </summary>
        public void ClearSelectedTriangle()
        {
            this.selectedTriangle = null;
        }

        /// <inheritdoc />
        public void Draw(Graphics canvas)
        {
            var canvasSize = canvas.ClipBounds;
            var builder = new StringBuilder();
            var invariantCulture = CultureInfo.InvariantCulture;

            if (selectedTriangle != null)
            {
                builder.AppendFormat(invariantCulture, "{0,-22}", selectedTriangle.DisplayName).AppendLine();
                var traversionCount = selectedTriangle.Traversions.Count();
                builder.Append("traversed: ").Append(traversionCount).AppendLine(" time(s)");
                builder.AppendLine();

                int traversionIndex = 1;
                foreach (var traversion in selectedTriangle.Traversions)
                {
                    builder.Append("traversion no. ").Append(traversionIndex).AppendLine();
                    builder.Append("       g_min: ").AppendFormat(invariantCulture, "{0:0.00}", traversion.ShortestPathToEdgeLength).AppendLine();
                    builder.Append("       g_max: ").AppendFormat(invariantCulture, "{0:0.00}", traversion.LongestPathToEdgeLength).AppendLine();
                    builder.Append("       f_min: ").AppendFormat(invariantCulture, "{0:0.00}", traversion.EstimatedMinimalCost).AppendLine();
                    builder.AppendLine();
                    traversionIndex++;
                }                
            }
            
            builder.AppendLine();
            builder.AppendLine("path:");
            foreach (var point in path.Vertices)
            {
                builder.AppendFormat(invariantCulture, "       {0:0.00}, {1:0.00}", point.X, point.Y).AppendLine();
            }
            
            builder.AppendLine();
            builder.AppendFormat(invariantCulture, "{0,-22}", "start:").AppendLine();
            builder.AppendFormat(invariantCulture, "       {0:0.00}, {1:0.00}", start.CurrentLocation.X, start.CurrentLocation.Y).AppendLine();
            builder.AppendLine();
            
            builder.AppendLine("goals:");
            foreach (var goal in goals)
            {
                builder.AppendFormat(invariantCulture, "       {0:0.00}, {1:0.00}", goal.CurrentLocation.X, goal.CurrentLocation.Y).AppendLine();
            }

            var caption = builder.ToString();
            var captionSize = canvas.MeasureString(caption, CaptionFont, canvas.ClipBounds.Size);
            canvas.DrawString(caption, CaptionFont, CaptionBrush, canvasSize.Right - captionSize.Width + offset.X, canvasSize.Bottom - captionSize.Height + offset.Y);
        }
        
        static MetaDisplay()
        {
            int captionFontSizeInPx = 11;
            CaptionFont = new Font(FontFamily.GenericMonospace, captionFontSizeInPx, GraphicsUnit.Pixel);
            CaptionBrush = Brushes.Black;
        }
    }
}