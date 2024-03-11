# Folder Object Component

Provides a simple component which turns a GameObject into a "folder" for organising your scene.

Forces the transform to:

- Pos 0, 0, 0
- Rot 0, 0, 0
- Scale 1, 1, 1

To avoid local offsets impacting child objects, and hides the transform component in the inspector.

To disable this behaviour, and reshow the transform, just remove the FolderObject component.

## Install

You can install via the following methods:

### Via Git URL

In the package manager, select "Add package from git URL"

![Package manager screenshot](https://user-images.githubusercontent.com/46207/79450714-3aadd100-8020-11ea-8aae-b8d87fc4d7be.png)

Use the path: `https://github.com/r0bbie/unity-folder-object.git`

### Unity Asset Store

For convenience, or if you would like to support the developer, the package is also available via the Asset Store for a small fee.

https://assetstore.unity.com/packages/tools/utilities/hierarchy-folder-object-10966

## License

MIT.