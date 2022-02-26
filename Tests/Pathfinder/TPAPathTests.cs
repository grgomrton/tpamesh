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

using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TriangulatedPolygonAStar.BasicGeometry;

namespace TriangulatedPolygonAStar.Tests
{
    [TestFixture]
    public class TPAPathTests
    {
        private static double AssertionPrecision = 0.0001;
        
        [OneTimeSetUp]
        public void BeforeTheseTestCases()
        {
            VectorEqualityCheck.Tolerance = 0.001;
        }

        [Test]
        public void InitiallyCurrentEdgeShouldNotBeSet()
        {
            var a = new Vector(0.0,0.0);
            var b = new Vector(1.0, 0.0);
            var c = new Vector(0.0, 1.0);
            var t = new Triangle(a, b, c, 0);
            var start = new Vector(0.1, 0.1);
            
            var initialPath = new TPAPath(start, t);

            initialPath.CurrentEdge.Should().BeNull();
        }

        [Test]
        public void InitiallyCurrentTriangleShouldBeStartTriangle()
        {
            var a = new Vector(0.0,0.0);
            var b = new Vector(1.0, 0.0);
            var c = new Vector(0.0, 1.0);
            var t = new Triangle(a, b, c, 0);
            var start = new Vector(0.1, 0.1);
            
            var initialPath = new TPAPath(start, t);

            initialPath.CurrentTriangle.Should().Be(t);            
        }

        [Test]
        public void AfterSteppingIntoNeighbourTriangleCurrentTriangleShouldMove()
        {
            var a = new Vector(0.0,0.0);
            var b = new Vector(1.0, 0.0);
            var c = new Vector(0.0, 1.0);
            var d = new Vector(-1.0, 0.0);
            var t1 = new Triangle(a, b, c, 0);
            var t2 = new Triangle(a, d, c, 1);
            t1.SetNeighbours(t2);
            var start = new Vector(0.1, 0.1);
            var goals = Enumerable.Empty<IVector>();
            var initialPath = new TPAPath(start, t1);

            var pathToT2 = initialPath.BuildPartialPathTo(t2, goals);

            initialPath.CurrentTriangle.Should().Be(t1);
            pathToT2.CurrentTriangle.Should().Be(t2);
        }

        [Test]
        public void AfterSteppingIntoNeighbourTriangleCurrentEdgeShouldBeTheCommonEdge()
        {
            var a = new Vector(0.0,0.0);
            var b = new Vector(1.0, 0.0);
            var c = new Vector(0.0, 1.0);
            var d = new Vector(-1.0, 0.0);
            var t1 = new Triangle(a, b, c, 0);
            var t2 = new Triangle(a, d, c, 1);
            var commonEdge = new Edge(a, c);
            t1.SetNeighbours(t2);
            var start = new Vector(0.1, 0.1);
            var goals = Enumerable.Empty<IVector>();
            var initialPath = new TPAPath(start, t1);

            var pathToT2 = initialPath.BuildPartialPathTo(t2, goals);

            initialPath.CurrentEdge.Should().BeNull();
            initialPath.CurrentTriangle.Should().Be(t1);
            pathToT2.CurrentEdge.Should().Be(commonEdge);
            pathToT2.CurrentTriangle.Should().Be(t2);
        }
        
        [Test]
        public void AfterSteppingIntoNeighbourTriangleMinimalPathToEdgeShouldBeTheDistanceBetweenApexAndEdgeIfEdgeIsVisibleFromApex()
        {
            var a = new Vector(0.0,0.0);
            var b = new Vector(1.0, 0.0);
            var c = new Vector(0.0, 1.0);
            var d = new Vector(-1.0, 0.0);
            var t1 = new Triangle(a, b, c, 0);
            var t2 = new Triangle(a, d, c, 1);
            t1.SetNeighbours(t2);
            var start = new Vector(0.1, 0.1);
            var distanceBetweenCommonEdgeAndStart = 0.1;
            var goals = Enumerable.Empty<IVector>();
            var initialPath = new TPAPath(start, t1);
            
            var pathToT2 = initialPath.BuildPartialPathTo(t2, goals);

            pathToT2.ShortestPathToEdgeLength.Should()
                .BeApproximately(distanceBetweenCommonEdgeAndStart, AssertionPrecision);
        }

        [Test]
        public void AfterSteppingIntoNeighbourTriangleMaximalPathToEdgeShouldBeTheLenghtOfTheLongerSideOfFunnelFromApexToEdge()
        {
            var a = new Vector(0.0,0.0);
            var b = new Vector(1.0, 0.0);
            var c = new Vector(0.0, 1.0);
            var d = new Vector(-1.0, 0.0);
            var t1 = new Triangle(a, b, c, 0);
            var t2 = new Triangle(a, d, c, 1);
            t1.SetNeighbours(t2);
            var start = new Vector(0.1, 0.1);
            var distanceBetweenStartAndPointC = 0.90554;
            var goals = Enumerable.Empty<IVector>();
            var initialPath = new TPAPath(start, t1);
            
            var pathToT2 = initialPath.BuildPartialPathTo(t2, goals);

            pathToT2.LongestPathToEdgeLength.Should()
                .BeApproximately(distanceBetweenStartAndPointC, AssertionPrecision);
        }
        
        [Test]
        public void AfterSteppingIntoNeighbourTriangleTheCostShouldBeTheDistanceBetweenStartAndEdgePlusTheDistanceBetweenEdgeAndClosestGoal()
        {
            var a = new Vector(0.0,0.0);
            var b = new Vector(1.0, 0.0);
            var c = new Vector(0.0, 1.0);
            var d = new Vector(-1.0, 0.0);
            var t1 = new Triangle(a, b, c, 0);
            var t2 = new Triangle(a, d, c, 1);
            t1.SetNeighbours(t2);
            var start = new Vector(0.1, 0.1);
            var goalInT2 = new Vector(-0.25, 0.5);
            var commonEdge = new Edge(a, c);
            var distanceBetweenCommonEdgeAndStart = 0.1;
            var distanceBetweenCommonEdgeAndGoal = 0.25;
            var distanceBetweenCommonEdgeAndStartPlusDistanceBetweenCommonEdgeAndGoal = 0.35;
            var goals = new[] {goalInT2};
            var initialPath = new TPAPath(start, t1);
            
            var pathToT2 = initialPath.BuildPartialPathTo(t2, goals);

            commonEdge.DistanceFrom(start).Should()
                .BeApproximately(distanceBetweenCommonEdgeAndStart, AssertionPrecision);
            commonEdge.DistanceFrom(goalInT2).Should()
                .BeApproximately(distanceBetweenCommonEdgeAndGoal, AssertionPrecision);
            pathToT2.MinimalTotalCost.Should()
                .BeApproximately(distanceBetweenCommonEdgeAndStartPlusDistanceBetweenCommonEdgeAndGoal,
                    AssertionPrecision);
        }

        [Test]
        public void DuringEstimatingShortestPathRightSideOfFunnelShouldBeTraversedIfClosestPointIsNotVisibleFromApex()
        {
            var origin = new Vector(0.0, 0.0);
            var a = new Vector(1.0, 2.0);
            var b = new Vector(1.0, 3.0);
            var t1 = new Triangle(origin, a, b, 1);
            var c = new Vector(2.0, 2.5);
            var t2 = new Triangle(a, b, c, 2);
            var d = new Vector(3.0, 9.0);
            var t3 = new Triangle(b, c, d, 3);
            var e = new Vector(3.0, 0.0);
            var t4 = new Triangle(c, e, d, 4);
            var f = new Vector(4.0, 0.0);
            var t5 = new Triangle(d, e, f, 5);
            t1.SetNeighbours(t2);
            t2.SetNeighbours(t1, t3);
            t3.SetNeighbours(t2, t4);
            t4.SetNeighbours(t3, t5);
            t5.SetNeighbours(t4);
            var start = origin;
            var goals = Enumerable.Empty<IVector>();
            var initialPath = new TPAPath(start, t1);
            var onePlusSquareRootFivePlusSquareRootOnePointTwentyFive = 4.35410;
            
            var pathToT2 = initialPath.BuildPartialPathTo(t2, goals);
            var pathToT3 = pathToT2.BuildPartialPathTo(t3, goals);
            var pathToT4 = pathToT3.BuildPartialPathTo(t4, goals);
            var pathToT5 = pathToT4.BuildPartialPathTo(t5, goals);

            var edgeBetweenDE = new Edge(d, e);
            pathToT5.CurrentEdge.Should().Be(edgeBetweenDE);
            pathToT5.ShortestPathToEdgeLength.Should()
                .BeApproximately(onePlusSquareRootFivePlusSquareRootOnePointTwentyFive, AssertionPrecision);
        }

        [Test]
        public void DuringEstimatingShortestPathLeftSideOfFunnelShouldBeTraversedIfClosestPointIsNotVisibleFromApex()
        {
            var origin = new Vector(0.0, 0.0);
            var a = new Vector(1.0, -2.0);
            var b = new Vector(1.0, -3.0);
            var t1 = new Triangle(origin, a, b, 1);
            var c = new Vector(2.0, -2.5);
            var t2 = new Triangle(a, b, c, 2);
            var d = new Vector(3.0, -9.0);
            var t3 = new Triangle(b, c, d, 3);
            var e = new Vector(3.0, 0.0);
            var t4 = new Triangle(c, e, d, 4);
            var f = new Vector(4.0, 0.0);
            var t5 = new Triangle(d, e, f, 5);
            t1.SetNeighbours(t2);
            t2.SetNeighbours(t1, t3);
            t3.SetNeighbours(t2, t4);
            t4.SetNeighbours(t3, t5);
            t5.SetNeighbours(t4);
            var start = origin;
            var goals = Enumerable.Empty<IVector>();
            var initialPath = new TPAPath(start, t1);
            var onePlusSquareRootFivePlusSquareRootOnePointTwentyFive = 4.35410;
            
            var pathToT2 = initialPath.BuildPartialPathTo(t2, goals);
            var pathToT3 = pathToT2.BuildPartialPathTo(t3, goals);
            var pathToT4 = pathToT3.BuildPartialPathTo(t4, goals);
            var pathToT5 = pathToT4.BuildPartialPathTo(t5, goals);

            var edgeBetweenDE = new Edge(d, e);
            pathToT5.CurrentEdge.Should().Be(edgeBetweenDE);
            pathToT5.ShortestPathToEdgeLength.Should()
                .BeApproximately(onePlusSquareRootFivePlusSquareRootOnePointTwentyFive, AssertionPrecision);
        }
        
        [Test]
        public void AfterApexHasBeenMovedPartialPathShouldBeCalculatedIntoTheShortestPathLength()
        {
            var origin = new Vector(0.0, 0.0);
            var a = new Vector(1.0, 2.0);
            var b = new Vector(1.0, 3.0);
            var t1 = new Triangle(origin, a, b, 1);
            var c = new Vector(2.0, 2.5);
            var t2 = new Triangle(a, b, c, 2);
            var d = new Vector(3.0, 9.0);
            var t3 = new Triangle(b, c, d, 3);
            var e = new Vector(3.0, 1.5);
            var t4 = new Triangle(c, e, d, 4);
            var f = new Vector(4.0, 1.5);
            var t5 = new Triangle(d, e, f, 5);
            var g = new Vector(3.5, 0.0);
            var t6 = new Triangle(e, f, g, 6);
            t1.SetNeighbours(t2);
            t2.SetNeighbours(t1, t3);
            t3.SetNeighbours(t2, t4);
            t4.SetNeighbours(t3, t5);
            t5.SetNeighbours(t4, t6);
            t6.SetNeighbours(t5);
            var start = origin;
            var goals = Enumerable.Empty<IVector>();
            var initialPath = new TPAPath(start, t1);
            var sqaureRootTwoPlusSquareRootFivePlusSquareRootOnePointTwentyFive = 4.76832;
            
            var pathToT2 = initialPath.BuildPartialPathTo(t2, goals);
            var pathToT3 = pathToT2.BuildPartialPathTo(t3, goals);
            var pathToT4 = pathToT3.BuildPartialPathTo(t4, goals);
            var pathToT5 = pathToT4.BuildPartialPathTo(t5, goals);
            var pathToT6 = pathToT5.BuildPartialPathTo(t6, goals);

            var edgeBetweenEF = new Edge(e, f);
            pathToT6.CurrentEdge.Should().Be(edgeBetweenEF);
            pathToT6.ShortestPathToEdgeLength.Should()
                .BeApproximately(sqaureRootTwoPlusSquareRootFivePlusSquareRootOnePointTwentyFive, AssertionPrecision);
        }

        [Test]
        public void IfBuiltPathContainsInternalNodeBreakingOnInternalNodeShouldBeTrue()
        {
            var origin = new Vector(0.0, 0.0, true);
            var a = new Vector(2.0, 0.0);
            var b = new Vector(1.0, 1.0);
            var c = new Vector(0.0, 2.0);
            var d = new Vector(-1.0, 1.0);
            var e = new Vector(-2.0, 0.0);
            var f = new Vector(-1.0, -1.0);
            var g = new Vector(0.0, -2.0);
            var h = new Vector(2.0, -3.0);
            var i = new Vector(1.0, -1.0);
            var t1 = new Triangle(origin, a, b, 1);
            var t2 = new Triangle(origin, b, c, 2);
            var t3 = new Triangle(origin, c, d, 3);
            var t4 = new Triangle(origin, d, e, 4);
            var t5 = new Triangle(origin, e, f, 5);
            var t6 = new Triangle(origin, f, g, 6);
            var t7 = new Triangle(origin, g, i, 7);
            var t8 = new Triangle(origin, i, a, 8);
            var t9 = new Triangle(g, h, i, 9);
            t9.SetNeighbours(t7);
            t7.SetNeighbours(t6, t8);
            t8.SetNeighbours(t7, t1);
            t1.SetNeighbours(t8, t2);
            t2.SetNeighbours(t1, t3);
            t3.SetNeighbours(t2, t4);
            t4.SetNeighbours(t3, t5);
            t5.SetNeighbours(t4, t6);
            t6.SetNeighbours(t5, t7);
            var start = new Vector(1.8, -2.8);
            var goals = Enumerable.Empty<IVector>();
            var initialPath = new TPAPath(start, t9);

            var afterFirstStep = initialPath.BuildPartialPathTo(t7, goals);
            var afterSecondStep = afterFirstStep.BuildPartialPathTo(t8, goals);
            var afterThirdStep = afterSecondStep.BuildPartialPathTo(t1, goals);
            var afterFourthStep = afterThirdStep.BuildPartialPathTo(t2, goals);
            var afterFifthStep = afterFourthStep.BuildPartialPathTo(t3, goals);
            var afterSixthStep = afterFifthStep.BuildPartialPathTo(t4, goals);

            afterSixthStep.BreakingOnInternalNode.Should().BeTrue();
        }
        
        [Test]
        public void PathShouldNotProceedToNotAdjacentTriangle()
        {
            var a = new Vector(0.0,0.0);
            var b = new Vector(1.0, 0.0);
            var c = new Vector(0.0, 1.0);
            var d = new Vector(-1.0, 0.0);
            var e = new Vector(0.0, -1.0);
            var t1 = new Triangle(a, b, c, 0);
            var t2 = new Triangle(a, d, e, 1);
            var start = new Vector(0.1, 0.1);
            var goals = Enumerable.Empty<IVector>();
            var initialPath = new TPAPath(start, t1);

            Action buildPathToT2 = () => initialPath.BuildPartialPathTo(t2, goals);

            buildPathToT2.ShouldThrow<ArgumentException>().And.Message.Should().Contain("triangle");
        }

        [Test]
        public void PathShouldNotInitializeIfStartPointIsNotInStartTriangle()
        {
            var a = new Vector(0.0,0.0);
            var b = new Vector(1.0, 0.0);
            var c = new Vector(0.0, 1.0);
            var t1 = new Triangle(a, b, c, 0);
            var start = new Vector(-0.1, -0.1);

            Action init = () => new TPAPath(start, t1);

            init.ShouldThrow<ArgumentException>().And.Message.Should().Contain("fall");
        }

        [Test]
        public void AfterSteppingIntoNeighbourTriangleTheCostShouldBeTheDistanceBetweenStartAndEdgePlusTheDistanceBetweenEdgeAndClosestGoalChoosenFromMultipleOnes()
        {
            var a = new Vector(0.0,0.0);
            var b = new Vector(1.0, 0.0);
            var c = new Vector(0.0, 1.0);
            var d = new Vector(-1.0, 0.0);
            var t1 = new Triangle(a, b, c, 0);
            var t2 = new Triangle(a, d, c, 1);
            t1.SetNeighbours(t2);
            var start = new Vector(0.1, 0.1);
            var goalInT2 = new Vector(-0.25, 0.5);
            var furtherGoal = new Vector(-1.0, 0.5);
            var commonEdge = new Edge(a, c);
            var distanceBetweenCommonEdgeAndStart = 0.1;
            var distanceBetweenCommonEdgeAndGoal = 0.25;
            var distanceBetweenCommonEdgeAndStartPlusDistanceBetweenCommonEdgeAndGoal = 0.35;
            var goals = new[] {goalInT2, furtherGoal};
            var initialPath = new TPAPath(start, t1);
            
            var pathToT2 = initialPath.BuildPartialPathTo(t2, goals);

            commonEdge.DistanceFrom(start).Should()
                .BeApproximately(distanceBetweenCommonEdgeAndStart, AssertionPrecision);
            commonEdge.DistanceFrom(goalInT2).Should()
                .BeApproximately(distanceBetweenCommonEdgeAndGoal, AssertionPrecision);
            pathToT2.MinimalTotalCost.Should()
                .BeApproximately(distanceBetweenCommonEdgeAndStartPlusDistanceBetweenCommonEdgeAndGoal,
                    AssertionPrecision);
        }
    }
}
