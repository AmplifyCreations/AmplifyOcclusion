# Amplify Occlusion

  <INTRO>

  This package was for sale on the Unity Asset Store between #### and 2019 with an
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

    Unity 5.6.0+

# Quick Guide

  Standard How-to

   1) Select and apply “Image Effects/Amplify Occlusion” to your main camera.
  
   2) Adjust the Intensity and Radius.
  
   3) Adjust the blur values until you are satisfied with the results.
 
  Scriptable Render Pipeline How-to

   1) Install packages dependencies:
     Window -> Package Manager, Advanced -> Show preview packages
     Select and install:
      
      Render-Pipelines.Core
      Render-Pipelines.High-Definition
      Render-Pipelines.Lightweight
      Post Processing

   2) Go to "Assets/Import Package/Custom Package..." and select
      "Assets/AmplifyOcclusion/Packages/PostProcessingSRP_XXX.unitypackage"

   3) How to set up an SRP project example:

   3.a) Create SRP asset via Assets menu:
   
	   Create/Rendering/High Definition Render Pipeline Asset

	   OR

	   Create/Rendering/Lightweight Render Pipeline Asset
  
   3.b) Set Edit->ProjectSettings/Player/Other settings/ColorSpace to Linear (necessary for HD SRP)
  
   3.c) Edit->ProjectSettings/Graphics/Scriptable Render Pipeline Settings: select the RenderPipelineAsset 
        created in 3.a)
  
   3.d) On Camera, using Lightweight Render Pipeline, disable MSAA
  
   3.e) Camera->Add Component->Post-Process Layer
  
   3.f) Camera->Post-Process Layer->Layer: Everything (as example)
  
   3.g) Camera->Add Component->Post-Process Volume
  
   3.h) Camera->Post-Process Volume->Is Global: check (as example)
  
   3.i) Camera->Post-Process Volume->Profile: New
  
   3.j) Camera->Post-Process Volume->Add effect... AmplifyCreations->AmplifyOcclusion

# Documentation

  Please refer to the following website for an up-to-date online manual:

    http://amplify.pt/unity/amplify-occlusion/manual

# Feedback

  To file error reports, questions or suggestions, you may use our feedback form online:
	
    http://amplify.pt/contact

  Or contact us directly:

    For general inquiries - info@amplify.pt
    For technical support - support@amplify.pt (customers only)
