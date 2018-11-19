## Description

*RoboBackup* is a Windows service for orchestrating file backups via `robocopy` utility. The GUI aims to provide user-friendly interface for the backup tasks management without the need to understand the `robocopy` utility itself. The service runs with *Local System* privileges, allowing it to work independently on the GUI user or on any interactive session. The service manages scheduling, logging and retention of the backups.

The goal of *RoboBackup* is not to be robust, secure and enterprise-ready, but to be small, simple and lower the difficulty of creating and scheduling `robocopy` commands.

## Usage

Backups can be created in 3 modes:

- **Incremental** means that there is a single destination directory. When the backup runs, files which are new or modified in the source directory are copied to the destination directory. Files which were deleted from the source directory are kept (i.e. not deleted) in the destination directory.
 - **Differential** creates timestamped subdirectories in `yyyy-MM-dd_HH-mm-ss` format under the destination directory for each backup run. Files are hardlinked from the subdirectory of the previous backup (if there is one) and then only the differences between the source directory and the previous backup are synchronized. This feature speeds up the whole backup process and reduces required filesystem space. In linux world, similar effect can be achieved using `rsync` tool with `--link-dest` parameter.
 - **Full** also creates timestamped subdirectories under the destination directory for each backup run, but the files are always fully copied from the source directory with no regard to the previous backups.

Backup retention is set as a number of timestamped directories with previous backups. When the retention limit is exceeded, excessive subdirectories with the oldest timestamps are deleted. With retention set to 1, differential and full methods behave the same. They do not create timestamped directories and only differentially synchronize the contents of the source directory to the destination directory.

**Note about network shares:** By default, Windows map drives only for interactive user sessions and open the mapping only when the drive is first used. This means that the mapped drives are not visible for the service account. GUI automatically resolves the mapped drives to UNC network paths, which *are* reachable for the service, but still requires you to enter the network credentials, so the service can open the resource to successfully run the backup. Both source and destination can be entered as UNC network paths and the service figures out when to use the credentials automatically, however they should not be network shares *both at the same time*.

## Acknowledgments
- Toolbar icons are [Mark James' Silk icons](http://famfamfam.com/lab/icons/silk/), licensed under [CC BY 3.0](https://creativecommons.org/licenses/by/3.0/).
- Main form icon is OpenIcon's one from [Pixabay](https://pixabay.com/en/hard-drive-disk-saving-data-add-97582/), licensed under [CC0 1.0](https://creativecommons.org/publicdomain/zero/1.0/).
- [Simon Mourier](https://stackoverflow.com/a/15386992) for most of the folder selection dialog.
- [PInvoke.net](https://pinvoke.net/) and [Microsoft](https://docs.microsoft.com/en-us/windows/desktop/api/index) for references on black magic with unmanaged API.
- The idea for the tool is loosely based on [Create Synchronicity](https://sourceforge.net/projects/synchronicity/).
