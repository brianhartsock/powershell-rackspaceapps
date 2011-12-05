==========================
RACKSPACE EMAIL API SnapIn
==========================

----- Installation - As a SnapIn -----
The Email API SnapIn needs to be registered with the computer.  Run the following script from the release package.

> .\Register-EmailApiSnapIn.ps1 [EmailApiSnapIn.dll]

InstallUtil.exe can be run manually against EmailApiSnapIn.dll if you wish.

In order to use the Snap In, after it has been registered, you have to add the Snap In in your Powershell session.  Load up Powershell.

> Add-PSSnapin EmailApiSnapIn

This loads all the Snap In cmdlet's for usage.

----- Installation - As a Module (Powershell 2.0 Only) -----

The easiest way is to just call import module on the module manifest (*.psd1 file) included in the release.

> Import-Module EmailApi.psd1

You can also copy the directory to a module directory.  Module directories are <user>\documents\WindowsPowershell\modules\EmailApi 
and <windows>\system32\windowspowershell\V1.0\modules\EmailApi.  After that, you can just import the module by name.

> Import-Module EmailApi

The module is not ready for use.

----- Cmdlet's -----

The following cmdlet's can be used by the client.

> get-resource
> post-resource
> put-resource
> delete-resource


----- More information -----
Please visit http://rackspaceappsapi.codeplex.com/wikipage?title=Powershell for complete documetation.
