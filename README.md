# Unity Folder Cleaner

Small utility to delete empty folder from the Unity project.

Based from a script in [Unity3D College](https://unity3d.college/2017/06/28/removing-empty-unity-folders-meta-files/)

## Usage

Open the utility at `Window > Tools > Clean Empty Folders`.

It will scan all the folders in your project and delete empty folders.

## Installation

### Adding the package to the Unity project manifest

* Navigate to the `Packages` directory of your project.
* Adjust the [project manifest file][Project-Manifest] `manifest.json` in a text editor.
  * Ensure `https://registry.npmjs.org/` is part of `scopedRegistries`.
    * Ensure `dev.bullrich` is part of `scopes`.
  * Add `dev.bullrich.folder-cleaner` to `dependencies`, stating the latest version.

  A minimal example ends up looking like this. 
  Please note that the version `X.Y.Z` stated here is to be replaced with the latest released version which is currently 
  [![openupm](https://img.shields.io/npm/v/dev.bullrich.folder-cleaner?label=openupm&registry_uri=https://package.openupm.com)][OpenUPM].
  ```json
  {
    "scopedRegistries": [
      {
        "name": "npmjs",
        "url": "https://registry.npmjs.org/",
        "scopes": [
          "dev.bullrich"
        ]
      }
    ],
    "dependencies": {
      "dev.bullrich.folder-cleaner": "X.Y.Z"
    }
  }
  ```
* Switch back to the Unity software and wait for it to finish importing the added package.

[Project-Manifest]: https://docs.unity3d.com/Manual/upm-manifestPrj.html
[OpenUPM]: https://openupm.com/packages/dev.bullrich.folder-cleaner/