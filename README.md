# NP Art Center VR Interactive Tour
## Project Description
This is an immersive Virtual Reality tour designed for the Meta Quest 2 headset. This application allows users to explore given 360-degree environments, transition between different rooms, and interact with points of interest. Some rooms features an introductory voice narration, and interactive buttons offer additional, context-specific audio information.

## Features
 - Immersive 360° rooms: Explore various rooms rendered from 360-degree panoramic images and videos.
 - Room Transitions: Move in different rooms with comfortable fade in/out effect
 - Voice Narration:
   - Interactive voice narration for specific artifacts or points of interest, triggered by UI buttons.

   - Text-to-Speech (TTS) powered by Meta XR Voice SDK (Wit.ai integration).
   - Intuitive VR Interaction: Uses Unity's XR Interaction Toolkit for controller-based (raycast) interaction with UI elements.
   - Optimized for Meta Quest 2

## Technologies Used
 - Unity Engine: 2022.3.55f1
 - Meta Quest 2: Target VR Headset
 - Unity XR Interaction Toolkit: For core VR interactions (controller handling, raycasting, UI interaction).
 - Meta XR Voice SDK: For Text-to-Speech (TTS) functionality.
 - Wit.ai: Backend service for Meta XR Voice SDK's TTS (requires an API Key).
 - TextMeshPro: For high-quality text rendering in UI.

## Setup and Installation
 ### Prerequisites
 - Unity Hub and Unity Editor 2022.3.55f1 or newer recommended for Meta XR SDKs).
 - Meta Quest 2 Headset configured for developer mode(Oculus).
 - Internet connection for Wit.ai TTS synthesis.

 ### Project Setup
 - Clone the Repository:

   git clone the repository
   Open in Unity

 - Install Required Unity Packages (via Package Manager):
      
   - XR Plugin Management
   - TextMeshPro 
   - XR Interaction Toolkit (Recommended to install the latest verified version for your Unity Editor)
   - Meta XR SDK(All in one SDK)

## Wit.ai API Key Configuration (Crucial for TTS)
The Meta XR Voice SDK uses Wit.ai for Text-to-Speech. You need to link your Unity project to a Wit.ai application.

 - Create a Wit.ai Account & App:
 - Go to https://wit.ai/ and sign up or log in with your Meta account.
 - Click + New App to create a new application for your project.
 - Get Your Server Access Token:
   - In your Wit.ai app dashboard, navigate to Management > Settings.
   - Copy the Server Access Token.

 - Link Wit.ai to Unity:
    - In the Unity Editor, go to Meta > Voice SDK > Voice HUB.
    - In the Voice Hub window, go to the Wit Configurations tab.
    - Click Create New App.
    - Select Custom App.
    - Paste your Server Access Token into the input field.
    - Click Create and save the new Wit Configuration asset.

  ## Usage
  - Connect Meta Quest 2: Connect your Meta Quest 2 headset to your computer via USB-C cable and ensure it's in developer mode.
 - Build and Run

## Credits
- Freepik: UI buttons
- Freesound: background music by
- Other assets from Unity Assets Store

## Note: Currently, TTS requires an internet connection for Wit.ai.

