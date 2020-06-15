# Unity Folder Cleaner

Small utility to delete empty folder from the Unity project.

Based from a script in [Unity3D College](https://unity3d.college/2017/06/28/removing-empty-unity-folders-meta-files/)

## Usage

Open the utility at `Window > Tools > Clean Empty Folders`.

It will scan all the folders in your project and delete empty folders.

## Installation

### Using Open UPM

You can install this package using [OpenUPM](https://openupm.com/)'s command line tool:

`openupm add dev.bullrich.folder-cleaner`

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

### Add editor test

There is a unit test to ensure that there are no empty directories in the project.

If you want to add this test to your project you can add it following this steps:

* Navigate to the `Packages` directory of your project.
* Adjust the [project manifest file][Project-Manifest] `manifest.json` in a text editor.
  * Ensure `dev.bullrich.folder-cleaner` is part of `testables`.

  A minimal example ends up looking like this.
  ```json
  {
    "scopedRegistries": [
      ...
    ],
    "testables": [
      "dev.bullrich.folder-cleaner"
    ],
    "dependencies": {
      ...
    }
  }
  ```
  
* As noted in the [official Unity documentation][Enable-Tests]:
  > **NOTE**: You may need to re-import the package, because the test framework doesn't always immediately pick up changes to the `testables` attribute.
  * Within the Unity software's `Project` window expand the `Packages` node.
  * Right-click on the `Zinnia.Unity` child node and choose `Reimport`.
  * Wait for the Unity software to finish re-importing the package.
* In the Unity software select `Main Menu -> Window -> Test Runner`.
* Within the Test Runner window click on the `EditMode` tab and the click `Run All` button.
* If all the tests pass then the installation was successful.


[Project-Manifest]: https://docs.unity3d.com/Manual/upm-manifestPrj.html
[OpenUPM]: https://openupm.com/packages/dev.bullrich.folder-cleaner/
[Enable-Tests]: https://docs.unity3d.com/Manual/cus-tests.html