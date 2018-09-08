# SWHarden-User-Controls
This repository contains a growing collection of custom user controls and standalone libraries for .NET (mostly written in C#). Projects are intended to be copy/pasted out of this repository and into whatever project you're working on.

## Tools with their own project pages
* **[ScottPlot](https://github.com/swharden/ScottPlot)** - Interactive Graphing Library for .NET
* **[SciTIF](https://github.com/swharden/SciTIF)** - a .NET library for scientific analysis of TIF files with a focus on fluorescent microscopy

## SplitDirView
**SplitDirView provides a tree-like directory browser which minimizes horizontal width.** It does this by avoiding the use of using trees and nodes (which are space ineffecient horizontally) and uses exclusively listboxes. The advantage is that very little horizontal screen space is required while navigating through highly nested folder structures. SplitDirView is intended to be used for projects which want to preserve screen space for data analysis rather than directory navigation.

![](/src/SplitDirViewDemo/demo.png)

## IconGallery
**IconGallery is a quick way to display large thumbnails of every image in a folder.** Single-clicking or double-clicking icons can do things. Right-clicking icons drops a menu to let you copy the path to clipboard or launch the image in explorer.

![](/src/IconGalleryDemo/demo.png)

## ABFbrowseLib
**ABFbrowseLib is a tool to provide organization tools to browse directories directories containing a blend of TIFs (micrographs), ABFs (electrophysiology data files), and PNGs (data analysis graphs).** This library provides directory navigation tools like grouping of ABFs into parent/children relationships. It also can read contents right out of ABF files to determine things like sweep count and comments. It can return all this data as a DataGridView DataTable. For more information about ABF files, see the [pyABF](https://github.com/swharden/pyABF) project.

![](/src/ABFbrowseLibDemo/demo.png)
