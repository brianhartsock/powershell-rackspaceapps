
param
(
	$zipFile = "EmailApiSnapIn.zip",
    $files =
        (
            (gci EmailApiSnapIn\bin\Release -exclude "*install*","*.pdb" | select -expand fullname), 
            (gi samples | select -expand fullname)
        ),
    $tmpFolder = "tmp",
    $7zip = (gi tools\7za.exe)
)

function Cleanup($files){
    $files | 
        %{
            if(test-path $_) { remove-item $_ -recurse -force }
        }
}

function StripSvn($folder){
    gci $folder -recurse -filter ".svn" | remove-item -recurse -force
}

function CreateTmpDirectory($folder){
    new-item $folder -type Directory | out-null
}

function CopyFiles($files, $folder){
    $files | 
        %{
            cp $_ $folder -recurse
        }
}

function CompressFiles($folder, $archive){
    push-location $folder
    try
    {
        $filesToZip = "'" + ((gci | select -expand name) -join "' '") + "'"
        "'$7zip' a '..\$archive' $fileToZip"
        & $7zip a ..\$zipFile "$fileToZip"
        
    }
    finally
    {
        pop-location
    }

}

Cleanup $tmpFolder, $zipFile
CreateTmpDirectory $tmpFolder
CopyFiles $files $tmpFolder
StripSvn $tmpFolder
CompressFiles $tmpFolder $zipFile
Cleanup $tmpFolder