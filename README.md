# Amplify Unity Products

  While this package is provided, as is, for free, we develop and maintain professionally created 
  solutions used by thousands of developers. We invite you to check our current projects, and to 
  join our growing [Discord Community](https://discord.gg/SbNs7zK).
  
  [Amplify Shader Editor](https://assetstore.unity.com/packages/tools/visual-scripting/amplify-shader-editor-68570?aid=1011lPwI&pubref=GitHub) - Award-winning node-based shader creation tool
 
  [Amplify Impostors](https://assetstore.unity.com/packages/tools/utilities/amplify-impostors-beta-119877?aid=1011lPwI&pubref=GitHub) - 1-Click Impostor Creator
 
  [Amplify LUT Pack](https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/amplify-lut-pack-50070?aid=1011lPwI&pubref=GitHub) - 200+ LUTs for Amplify Color and Unity PPS
 
# Amplify Occlusion  
  
  Amplify Occlusion was the first industry-grade, full-featured screen-space ambient occlusion 
  solution to be released on the Asset Store in 2016, at a time when Unity itself lacked a decent
  SSAO implementation. It managed to remain the fastest SSAO solution for Unity released a built-in 
  implementation of "Multi-scale Volumetric Occlusion" as part of their Post-processing Stack.
  
  <p align="center"><img src="http://files.amplify.pt/RT/2021/11/oc_1.jpg"></p>

  The first version of this plugin was using a technique known as HBAO, or "Horizon-Based
  Ambient Occlusion", based on a 2008 paper titled "Image-Space Horizon-Based Ambient Occlusion" 
  by Louis Bavoil, Miguel Sainz and Rouslan Dimitrov.  

  The second version, which improved upon the first iteration on both quality and performance was
  using a technique known as GTAO, or "Ground-Truth Ambient Occlusion", based on a 2016 paper titled 
  "Practical Realtime Strategies for Accurate Indirect Occlusion" by Jorge Jimenez, Xian-Chun Wu, 
  Angelo Pesce and Adrian Jarabo.
  
  <p align="center"><img src="http://files.amplify.pt/RT/2021/11/oc_2.jpg"></p>  

  This package was for sale on the Unity Asset Store between 2016 and 2019 with an
  average rating of 5 stars. It is now deprecated and we no longer support it, so we 
  are releasing it to open-source world under the MIT License.
	
# Description

  Amplify Occlusion sets out to deliver a new industry standard for fast, high-quality 
  Screen-Space Ambient Occlusion in Unity; delivering state of the art Ground Truth Ambient 
  Occlusion (GTAO) and bringing quality and accuracy closer to traditional raytracing. A true 
  all-in-one package, providing a highly-robust and efficient way to simulate ambient occlusion 
  and contact shadowing. Now you can attenuate reflections in occluded areas, make objects 
  actually connect to the world and add real depth to your scenes with minimal effort.
  
# Features

  * Ground Truth Ambient Occlusion
  * PS4, Xbox One and Switch compatible
  * Single and Multi-pass VR support
  * Up to 2X faster than Amplify Occlusion 1.0
  * Revamped Spatial and Temporal Filters
  * Dramatically Higher-Quality
  * Higher Flexibility
  * Under 1 ms on a mid-range GPU at Full HD
  * Accurate and fast-performing
  * Deferred and Forward Rendering
  * PBR compatible injection mode
  * Superior occlusion approximation
  * Extensive blur and intensity controls
  
# Supported Platforms

  All platforms
	
# Software Requirements

  Minimum

    Unity 2021.3 LTS

# Quick Guide

  Amplify Occlusion can be easily set on your project by selecting your game camera object and
  doing one of the following steps:

  1) Go to 'Component/Image Effects' menu and select the 'Amplify Occlusion' option. Note that
     you need to have a game object selected with a camera component, if not present it will
	 create one

  2) Hit 'Add Component' on the camera Inspector View, type 'Amplify Occlusion' on the search
     box and select the result
 
# Documentation

  Please refer to the following website for an up-to-date online manual:

    http://amplify.pt/unity/amplify-occlusion/manual

# Acknowledgements

  Version 2 was developed by the talented Mário Luzeiro:
  
    https://pt.linkedin.com/in/mluzeiro
    https://twitter.com/mluzeiro
    
