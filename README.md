# Volume Unity Plugin

1. [Usage](#usage)
1. [Releases](https://github.com/Volume-GL/Volume-Unity-Plugin/releases)
1. [Support](#support)
1. [License](#license)

![Unity scene](https://github.com/Volume-GL/Volume-Unity-Plugin/blob/master/Docs/scene.gif)

A Unity3D plugin for rendering [Volume](https://volume.gl) assets. [Volume](https://volume.gl) is a machine learning driven tool, for reconstructing 2D images and videos in 3D space. For early acsses to the beta email us at [this email](#support)

## Usage

Use [this link](https://github.com/Volume-GL/Volume-Unity-Plugin/releases) to download the latest release of the plugin as a ```.unitypackage```

Once unpacked you will have a folder structure organized in the following order:
- Volume
  - Addons
  - Perfabs
  - Scenes
  - Materials
  - Shaders
  - Sample

Use the example scene found in ```Scenes/``` or just add the ```Volume Image``` prefab to your pre-existing scene.

To load assets exported from the Make interface, import them to Unity and change the ```Volume Texture``` field in the ```Volume Image``` GameObject to reflect your image.

![Plugin texture change](https://github.com/Volume-GL/Volume-Unity-Plugin/blob/master/Docs/plugin.png)

### Example content
We provide a sample scene with a sample Volume asset and mouse/keyboard controls under ```Scenes/Volume Example```

![Unity screenshot](https://github.com/Volume-GL/Volume-Unity-Plugin/blob/master/Docs/unity.png)

## Support
For further support reach out to us at [hello@volume.gl](hello@volume.gl) 👋 

## License
[Here](https://github.com/Volume-GL/Volume-Unity-Plugin/blob/master/LICENSE)
