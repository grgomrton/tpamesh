# tpamesh

_Triangulated Polygon A-star for 2D Triangle Meshes_ is an extension of my pathfinder [tpastar](https://github.com/grgomrton/tpastar/). It is now be able to find shortest paths on triangle meshes containing points inside the polygon boundaries.

<p align="center"><img src="./Documentation/exploration-one-start-multiple-goals-cropped.png" alt="The result of an exploration between one start and multiple goals" /></p>  

The image shows the result of a pathfinding between one starting point and three target points. The shade of the triangles indicate the number of times the given triangle was expanded. 

The algorithm works in various arrangements:

| <img src="./Documentation/exploration-one-start-one-goal-lowpoly-mesh.png" alt="Result on a lowpoly mesh" /> | <img src="./Documentation/exploration-one-start-one-goal-delaunay-mesh.png" alt="Result on a Delaunay triangulated mesh" /> | <img src="./Documentation/exploration-one-start-one-goal-fine-mesh.png" alt="Result on a fine polygon mesh" /> |
|:-:|:-:|:-:|
| The result of a pathfinding on a lowpoly triangulated polygon | The result of a pathfinding on a Delaunay triangulated polygon where added points lie only on boundaries | The result of a pathfinding on a polygon mesh where added points lie inside the polygon boundaries |

The performance of the algorithm depends on the amount of triangles in the search space, as well as on the length of the path.

## Licensing

This repository contains the implementation of the algorithm in C#.

The project is licensed under [![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

## Acknowledgements
- _Jonathan Shewchuk_, whose library, Triangle was used to generate polygon meshes for the demo application
