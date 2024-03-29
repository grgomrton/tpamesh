﻿/**
 * Copyright 2022 Márton Gergó
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

namespace TriangulatedPolygonAStar
{
    /// <summary>
    /// Represents the state of a traversal through the triangle graph along a specific set of adjacent triangles.
    /// </summary>
    public class TPAPath
    {
        private ITriangle currentTriangle;
        private IEdge currentEdge;

        private FunnelStructure funnel;
        private LinkedList<IEdge> oversteppedEdges;
        private double alreadyBuiltPathLength;                 // gPart
        private double lengthOfShortestPathFromApexToEdge;     // dgMin
        private double lengthOfLongestPathFromApexToEdge;      // dgMax
        private double distanceFromClosestGoalPoint;           // h

        /// <summary>
        /// Initializes a new instance of the <see cref="TPAPath"/> class which can be used 
        /// to explore the triangle graph.
        /// </summary>
        /// <param name="startPoint">The point where the traversion originates from</param>
        /// <param name="startTriangle">The triangle which contains the start point</param>
        public TPAPath(IVector startPoint, ITriangle startTriangle)
        {
            CheckForNullArgument(startPoint, nameof(startPoint));
            CheckForNullArgument(startTriangle, nameof(startTriangle));
            if (!startTriangle.ContainsPoint(startPoint))
            {
                throw new ArgumentException("The specified point does not fall inside the specified triangle");
            }
            
            funnel = new FunnelStructure(startPoint);
            currentTriangle = startTriangle;
            alreadyBuiltPathLength = 0;
            lengthOfShortestPathFromApexToEdge = 0;
            lengthOfLongestPathFromApexToEdge = 0;
            distanceFromClosestGoalPoint = 0; 
            oversteppedEdges = new LinkedList<IEdge>();
        }

        private TPAPath(TPAPath other)
        {
            CheckForNullArgument(other, nameof(other));
            
            funnel = new FunnelStructure(other.funnel);
            currentTriangle = other.currentTriangle;
            alreadyBuiltPathLength = other.alreadyBuiltPathLength;
            lengthOfShortestPathFromApexToEdge = other.lengthOfShortestPathFromApexToEdge;
            lengthOfLongestPathFromApexToEdge = other.lengthOfLongestPathFromApexToEdge;
            distanceFromClosestGoalPoint = other.distanceFromClosestGoalPoint;
            oversteppedEdges = new LinkedList<IEdge>(other.oversteppedEdges);
        }

        /// <summary>
        /// The triangle we are currently standing on.
        /// </summary>
        public ITriangle CurrentTriangle
        {
            get { return currentTriangle; }
        }

        /// <summary>
        /// The edge where we entered the current triangle. 
        /// Until we step to the first neighbour triangle from the start triangle, this value is <code>null</code>.
        /// </summary>
        public IEdge CurrentEdge
        {
            get { return currentEdge; }
        }
        
        /// <summary>
        /// The length of the possibly shortest path from the start point to the current edge along the set of triangles
        /// that have been stepped over.
        /// </summary>
        public double ShortestPathToEdgeLength
        {
            get { return alreadyBuiltPathLength + lengthOfShortestPathFromApexToEdge; }
        }

        /// <summary>
        /// The length of the longest possible path from the start point to the current edge along the set of triangles
        /// that have been stepped over.
        /// </summary>
        public double LongestPathToEdgeLength
        {
            get { return alreadyBuiltPathLength + lengthOfLongestPathFromApexToEdge; }
        }
        
        /// <summary>
        /// The length of the possibly shortest path from the start point to the closest goal point along the set of 
        /// triangles that have been stepped over.
        /// </summary>
        public double MinimalTotalCost
        {
            get { return ShortestPathToEdgeLength + distanceFromClosestGoalPoint; }
        }

        /// <summary>
        /// Indicates whether the path has accumulated an internal node, and therefore it is known not to be the
        /// shortest path to any goal.
        /// </summary>
        public bool BreakingOnInternalNode
        {
            get
            {
                LinkedListNode<IVector> currentNode = funnel.Path.First;
                while (currentNode != null)
                {
                    if (currentNode.Value.IsInternal)
                    {
                        return true;
                    }

                    currentNode = currentNode.Next;
                }

                return false;
            }
        }
        
        /// <summary>
        /// Returns a new path which is built by proceeding to the specified neighbour triangle from the current one.
        /// </summary>
        /// <param name="neighbour">The neighbour triangle to step into</param>
        /// <param name="goals">The goal points of the pathfinding</param>
        /// <returns>The partial path which leads to the neighbour triangle</returns>
        public TPAPath BuildPartialPathTo(ITriangle neighbour, IEnumerable<IVector> goals)
        {
            CheckForNullArgument(neighbour, nameof(neighbour));
            bool isAdjacent = false;
            foreach (ITriangle triangle in CurrentTriangle.Neighbours)
            {
                if (triangle.Equals(neighbour))
                {
                    isAdjacent = true;
                }
            }
            if (!isAdjacent)
            {
                throw new ArgumentException("The triangle is not adjacent with the one this path is standing on", nameof(neighbour));
            }
            
            TPAPath newPath = this.Clone();
            newPath.StepTo(neighbour, goals);
            
            return newPath;
        }

        /// <summary>
        /// Returns the complete, final path to the specified goal point. 
        /// The goal has to be contained by the triangle this path stands on.
        /// </summary>
        /// <param name="goal">The goal to build the complete path to</param>
        /// <returns>The completed path to the reached goal point</returns>
        public LinkedList<IVector> BuildCompletePathTo(IVector goal)
        {
            CheckForNullArgument(goal, nameof(goal));
            if (!currentTriangle.ContainsPoint(goal))
            {
                throw new ArgumentException("This path has not reached the specified goal point", nameof(goal));
            }

            FunnelStructure funnelCopy = new FunnelStructure(this.funnel);
            funnelCopy.FinalizePath(goal);
            
            return funnelCopy.Path;
        }

        /// <summary>
        /// Indicates whether this path has already stepped over the specified edge.
        /// </summary>
        /// <param name="edge">The edge to test</param>
        /// <returns>Whether the path has visited this edge</returns>
        public bool IsAlreadyOverstepped(IEdge edge)
        {
            LinkedListNode<IEdge> node = oversteppedEdges.First;
            while (node != null)
            {
                if (node.Value.Equals(edge))
                {
                    return true;
                }

                node = node.Next;
            }

            return false;
        }
        
        private void UpdateEstimationToClosestGoalPoint(IEnumerable<IVector> goals)
        {
            if (currentEdge != null)
            {
                distanceFromClosestGoalPoint = 
                    MinimalDistanceBetween(currentEdge, goals);
            }
            else
            {
                distanceFromClosestGoalPoint = 
                    MinimalDistanceBetween(funnel.Apex.Value, goals);
            }
        }
        
        private void StepTo(ITriangle targetTriangle, IEnumerable<IVector> goals)
        {
            currentEdge = currentTriangle.GetCommonEdgeWith(targetTriangle);
            oversteppedEdges.AddLast(currentEdge);
            currentTriangle = targetTriangle;

            funnel.StepOver(currentEdge);
            alreadyBuiltPathLength = LengthOfBuiltPathInFunnel(funnel.Path);
            lengthOfShortestPathFromApexToEdge = LengthOfShortestPathFromApexToEdge(currentEdge, funnel.Apex);
            lengthOfLongestPathFromApexToEdge = LengthOfLongestPathFromApexToEdge(funnel.Apex);
            UpdateEstimationToClosestGoalPoint(goals);
        }
        
        private TPAPath Clone()
        {
            return new TPAPath(this);
        }
        
        private static double LengthOfShortestPathFromApexToEdge(IEdge edge, LinkedListNode<IVector> apex)
        {
            double length = 0.0;

            if ((apex.Next != null) && (apex.Previous != null)) // otherwise the apex lies on the edge, the path is already the minimal path to the edge
            {
                IVector closestPointOfEdgeToApex = edge.ClosestPointTo(apex.Value);
                IVector apexPoint = apex.Value;
                IVector apexToLeftOne = apex.Previous.Value.Minus(apex.Value);
                IVector apexToRightOne = apex.Next.Value.Minus(apex.Value);
                IVector apexToClosestPointOnEdge = closestPointOfEdgeToApex.Minus(apexPoint);

                if (apexToLeftOne.IsInCounterClockWiseDirectionFrom(apexToClosestPointOnEdge))
                {
                    if (apexToRightOne.IsInClockWiseDirectionFrom(apexToClosestPointOnEdge))
                    {
                        // easy way, closest point is visible from apex
                        length = apexPoint.DistanceFrom(closestPointOfEdgeToApex);
                    }
                    else
                    {
                        length = WalkOnRightSideOfFunnelUntilClosestPointBecomesVisible(apex, edge);
                    }
                }
                else
                {
                    length = WalkOnLeftSideOfFunnelUntilClosestPointBecomesVisible(apex, edge);
                }
            }
            return length;
        }

        private static double WalkOnRightSideOfFunnelUntilClosestPointBecomesVisible(
            LinkedListNode<IVector> startNode, IEdge edge)
        {
            double pathLength = 0;
            LinkedListNode<IVector> currentNode = startNode;

            IVector closestPointOnEdge = edge.ClosestPointTo(currentNode.Value);
            IVector currentToOneRight = currentNode.Next.Value.Minus(currentNode.Value);
            IVector currentToClosestPoint = closestPointOnEdge.Minus(currentNode.Value);

            while (currentToOneRight.IsInCounterClockWiseDirectionFrom(currentToClosestPoint) &&
                   (currentNode.Next.Next != null))
            {
                pathLength += currentNode.Value.DistanceFrom(currentNode.Next.Value);
                currentNode = currentNode.Next;

                closestPointOnEdge = edge.ClosestPointTo(currentNode.Value);
                currentToOneRight = currentNode.Next.Value.Minus(currentNode.Value);
                currentToClosestPoint = closestPointOnEdge.Minus(currentNode.Value);
            }

            pathLength += currentNode.Value.DistanceFrom(edge.ClosestPointTo(currentNode.Value));

            return pathLength;
        }

        private static double WalkOnLeftSideOfFunnelUntilClosestPointBecomesVisible(
            LinkedListNode<IVector> startNode, IEdge edge)
        {
            double pathLength = 0;
            LinkedListNode<IVector> currentNode = startNode;

            IVector closestPointOnEdge = edge.ClosestPointTo(currentNode.Value);
            IVector currentToOneLeft = currentNode.Previous.Value.Minus(currentNode.Value);
            IVector currentToClosestPoint = closestPointOnEdge.Minus(currentNode.Value);

            while (currentToOneLeft.IsInClockWiseDirectionFrom(currentToClosestPoint) &&
                   (currentNode.Previous.Previous != null))
            {
                pathLength += currentNode.Value.DistanceFrom(currentNode.Previous.Value);
                currentNode = currentNode.Previous;

                closestPointOnEdge = edge.ClosestPointTo(currentNode.Value);
                currentToOneLeft = currentNode.Previous.Value.Minus(currentNode.Value);
                currentToClosestPoint = closestPointOnEdge.Minus(currentNode.Value);
            }

            pathLength += currentNode.Value.DistanceFrom(edge.ClosestPointTo(currentNode.Value));

            return pathLength;
        }

        private static double LengthOfLongestPathFromApexToEdge(LinkedListNode<IVector> apex)
        {
            double leftPathLength = 0;
            double rightPathLength = 0;

            LinkedListNode<IVector> currentNode = apex;
            while (currentNode.Previous != null)
            {
                leftPathLength += currentNode.Value.DistanceFrom(currentNode.Previous.Value);
                currentNode = currentNode.Previous;
            }

            currentNode = apex;
            while (currentNode.Next != null)
            {
                rightPathLength += currentNode.Value.DistanceFrom(currentNode.Next.Value);
                currentNode = currentNode.Next;
            }

            return Math.Max(leftPathLength, rightPathLength);
        }

        private static double LengthOfBuiltPathInFunnel(LinkedList<IVector> path)
        {
            double length = 0;
            LinkedListNode<IVector> currentNode = path.First;
            while (currentNode.Next != null)
            {
                length += currentNode.Value.DistanceFrom(currentNode.Next.Value);    
             
                currentNode = currentNode.Next;
            }
            return length;
        }

        private static double MinimalDistanceBetween(IEdge edge, IEnumerable<IVector> targetPoints)
        {
            double minDistance = Double.PositiveInfinity;
            foreach (var targetPoint in targetPoints)
            {
                double distance = edge.DistanceFrom(targetPoint);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
            return minDistance;
        }

        private static double MinimalDistanceBetween(IVector point, IEnumerable<IVector> targetPoints)
        {
            double minDistance = Double.PositiveInfinity;
            foreach (var targetPoint in targetPoints)
            {
                double distance = point.DistanceFrom(targetPoint);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
            return minDistance;
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