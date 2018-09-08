# SWHarden-User-Controls
This repository contains a growing collection of custom user controls and standalone libraries for .NET (mostly written in C#). Projects are intended to be copy/pasted out of this repository and into whatever project you're working on.

## TifLib
**TifLib provides an interface to microscopy data saved in TIF files.** Many TIF file reading libraries already exist, however it is not always clear how they treat uncommon TIF formats which are commonly used in _scientific_ microscopy (e.g., mult-channel multi-layer stacks of 12-bit data arranged little endian in a 16-bit TIF files). 

<img src="/data/tifs/fluo-3ch-8bitColor.png" align="right">An extremely common limitation of existing libraries is the lack of ability to scale a display LUT to fit data other than the standard (8, 16, or 32) bit depths. This is especially important considering many scientific cameras produce 10-bit, 12-bit, and 14-bit images. One example of a poor TIF library is that used by Windows to produce thumbnails in Explorer, which presents 16-bit TIFs containing 12-bit data as nearly black images.

A secondary purpose of this library is to demonstrate how to write a scientific-grade analysis routine which reads fluorescence intensity directly out of a TIF (which should be regarded scientifically as the source data). This is highly useful for fluorescennt microscopy (e.g., immunohistochemistry, colocalization assessment, image enhancement) and fluorescennt videomicroscopy (time-series calcium-sensitive fluorophore analysis such as Fluo-4 or GCaMP), especially when quantitative ratiometeric analyses are required. TifLib is specifically written with scientific analysis in mind, and was created to be easily ported to other programming languages.

* [TifLib developer notes](src/TifLib)

## SplitDirView
**SplitDirView provides a tree-like directory browser which minimizes horizontal width.** It does this by avoiding the use of using trees and nodes (which are space ineffecient horizontally) and uses exclusively listboxes. The advantage is that very little horizontal screen space is required while navigating through highly nested folder structures. SplitDirView is intended to be used for projects which want to preserve screen space for data analysis rather than directory navigation.

![](/src/SplitDirViewDemo/demo.png)

## IconGallery
**IconGallery is a quick way to display large thumbnails of every image in a folder.** Single-clicking or double-clicking icons can do things. Right-clicking icons drops a menu to let you copy the path to clipboard or launch the image in explorer.

![](/src/IconGalleryDemo/demo.png)

## ABFbrowseLib
**ABFbrowseLib is a tool to provide organization tools to browse directories directories containing a blend of TIFs (micrographs), ABFs (electrophysiology data files), and PNGs (data analysis graphs).** This library provides directory navigation tools like grouping of ABFs into parent/children relationships. It also can read contents right out of ABF files to determine things like sweep count and comments. It can return all this data as a DataGridView DataTable. For more information about ABF files, see the [pyABF](https://github.com/swharden/pyABF) project.

![](/src/ABFbrowseLibDemo/demo.png)
