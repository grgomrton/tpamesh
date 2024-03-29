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
    /// A pathfinder which is able to determine the euclidean shortest path between one start and multiple goal points
    /// in a triangulated polygon with polygon holes.
    /// </summary>
    public class TPAMeshPathFinder
    {
        private LinkedList<TPAPath> openSet;
        private Dictionary<IEdge, double> higherBounds;
        
        /// <summary>
        /// Initializes a new instance of a <see cref="TPAMeshPathFinder"/> class which can be used to find
        /// shortest paths in a trianglulated polygon with polygon holes.
        /// </summary>
        public TPAMeshPathFinder()
        {
            openSet = new LinkedList<TPAPath>();
            higherBounds = new Dictionary<IEdge, double>();    
        }   
        
        /// <summary>
        /// Method signature for handling triangle traversion events.
        /// </summary>
        /// <param name="triangle">The triangle which has been stepped into</param>
        /// <param name="result">Details about the resulting path after stepping into this triangle</param>
        public delegate void TriangleExploredEventHandler(ITriangle triangle, TriangleEvaluationResult result);

        /// <summary>
        /// An event raised after a triangle has been expanded through the traversal of the triangle graph.
        /// The same triangle might be reached multiple times.
        /// </summary>
        public event TriangleExploredEventHandler TriangleExplored;
        
        /// <summary>
        /// Executes a path finding on the triangle graph between the specified start and goal points. 
        /// If no goal can be reached then the result contains the start point only.
        /// </summary>
        /// <param name="startPoint">The point to originate the pathfinding from</param>
        /// <param name="startTriangle">The triangle which contains the start point</param>
        /// <param name="goals">The goal points of the pathfinding</param>
        /// <returns>The list of points that define the path between the start and goal point that can be reached with the shortest path</returns>
        public LinkedList<IVector> FindPath(IVector startPoint, ITriangle startTriangle, IEnumerable<IVector> goals)
        {
            CheckForNullArgument(startPoint, nameof(startPoint));
            CheckForNullArgument(startTriangle, nameof(startTriangle));
            CheckForNullArgument(goals, nameof(goals));
            if (!startTriangle.ContainsPoint(startPoint))
            {
                throw new ArgumentException("The specified start point does not fall into the start triangle");
            }
            
            openSet.Clear();
            higherBounds.Clear();
            
            LinkedList<IVector> bestCandidate = new LinkedList<IVector>();
            bestCandidate.AddFirst(startPoint);
            double bestCandidateLength = Double.PositiveInfinity;
            
            TPAPath initialPath = new TPAPath(startPoint, startTriangle);
            AddToOpenSet(initialPath);
            FireTriangleExploredEvent(initialPath);
            bool done = false;
            
            while ((openSet.Count > 0) && !done)
            {
                TPAPath partialPath = openSet.First.Value;
                openSet.RemoveFirst();
                
                if (partialPath.MinimalTotalCost > bestCandidateLength)    
                {                            
                    done = true;
                }
                else
                {
                    foreach (IVector goal in goals)
                    {
                        if (partialPath.CurrentTriangle.ContainsPoint(goal))
                        {
                            LinkedList<IVector> newCandidate = partialPath.BuildCompletePathTo(goal);
                            double newCandidateLength = GetLength(newCandidate);
                            if (newCandidateLength < bestCandidateLength)
                            {
                                bestCandidate = newCandidate;
                                bestCandidateLength = newCandidateLength;
                            }
                        }
                    }
                    foreach (ITriangle neighbour in partialPath.CurrentTriangle.Neighbours)
                    {
                        if (!partialPath.IsAlreadyOverstepped(neighbour.GetCommonEdgeWith(partialPath.CurrentTriangle)))
                        {
                            TPAPath pathToNeighbour = partialPath.BuildPartialPathTo(neighbour, goals);
                            if (IsGoodCandidate(pathToNeighbour))
                            {
                                AddToOpenSet(pathToNeighbour);
                                UpdateHigherBoundToReachedEdge(pathToNeighbour);
                            }
                            FireTriangleExploredEvent(pathToNeighbour);
                        }
                    }
                }
            }
            return bestCandidate;
        }

        private static double GetLength(LinkedList<IVector> path)
        {
            double length = 0.0;
            LinkedListNode<IVector> currentNode = path.First;
            while (currentNode.Next != null)
            {
                length += currentNode.Value.DistanceFrom(currentNode.Next.Value);
                currentNode = currentNode.Next;
            }
            return length;
        }
        
        private bool IsGoodCandidate(TPAPath path)
        {
            if (path.BreakingOnInternalNode)
            {
                return false;
            }
            if (higherBounds.ContainsKey(path.CurrentEdge))
            {
                if (higherBounds[path.CurrentEdge] < path.ShortestPathToEdgeLength)
                {
                    return false;
                }
            }
            return true;
        }

        private void UpdateHigherBoundToReachedEdge(TPAPath path)
        {
            if (higherBounds.ContainsKey(path.CurrentEdge))
            {
                if (higherBounds[path.CurrentEdge] > path.LongestPathToEdgeLength)
                {
                    higherBounds[path.CurrentEdge] = path.LongestPathToEdgeLength;
                }
            }
            else
            {
                higherBounds.Add(path.CurrentEdge, path.LongestPathToEdgeLength);
            }
        }

        private void AddToOpenSet(TPAPath path)
        {
            if ((openSet.First == null) || 
                (openSet.First.Value.MinimalTotalCost > path.MinimalTotalCost))
            {
                openSet.AddFirst(path);
            }
            else
            {
                LinkedListNode<TPAPath> targetNode = openSet.First;
                while ((targetNode.Next != null) && 
                       (targetNode.Next.Value.MinimalTotalCost < path.MinimalTotalCost))
                {
                    targetNode = targetNode.Next;
                }
                openSet.AddAfter(targetNode, path);
            }
        }

        private void FireTriangleExploredEvent(TPAPath resultingPath)
        {
            if (TriangleExplored != null)
            {
                TriangleEvaluationResult evaluationEventArgs = new TriangleEvaluationResult(resultingPath);
                TriangleExplored(resultingPath.CurrentTriangle, evaluationEventArgs);
            }
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
