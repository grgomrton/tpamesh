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
using System.Drawing;
 using System.Globalization;
 using System.Runtime.InteropServices;
using System.Text;
using TriangulatedPolygonAStar.BasicGeometry;

namespace TriangulatedPolygonAStar.UI
{
    /// <summary>
    /// An overlay that display poses in the left-bottom corner of the canvas.
    /// </summary>
    public class PoseDisplay : IOverlay
    {
        private static readonly Font CaptionFont;
        private static readonly Brush CaptionBrush;
        private readonly Point offset;
        private IVector currentPosition;

        /// <summary>
        /// Initializes a new instance of <see cref="PoseDisplay"/> class 
        /// which display poses in the left-bottom corner of the canvas.
        /// </summary>
        /// <param name="distanceFromLeftInPx">Offset from the left side of the canvas</param>
        /// <param name="distanceFromBottomInPx">Offset from the bottom of the canvas</param>
        public PoseDisplay(int distanceFromLeftInPx, int distanceFromBottomInPx)
        {
            SetCurrentPosition(new Vector(0.0, 0.0));
            offset = new Point(distanceFromLeftInPx, -distanceFromBottomInPx);
        }
        
        /// <summary>
        /// Sets the current position value to display.
        /// </summary>
        /// <param name="absolutePosition">The position in the coordinate system of the objects drawn onto the canvas</param>
        public void SetCurrentPosition(IVector absolutePosition)
        {
            this.currentPosition = absolutePosition;
        }

        /// <inheritdoc />
        public void Draw(Graphics canvas)
        {
            var canvasSize = canvas.ClipBounds;
            var caption = String.Format(CultureInfo.InvariantCulture, "{0:0.00}, {1:0.00}", currentPosition.X, currentPosition.Y);
            var captionSize = canvas.MeasureString(caption, CaptionFont);
            canvas.DrawString(caption, CaptionFont, CaptionBrush, offset.X, canvasSize.Bottom - captionSize.Height + offset.Y);
        }
        
        static PoseDisplay()
        {
            var captionFontSizeInPx = 11;
            CaptionFont = new Font(FontFamily.GenericMonospace, captionFontSizeInPx, GraphicsUnit.Pixel);
            CaptionBrush = Brushes.Black;
        }
    }
}