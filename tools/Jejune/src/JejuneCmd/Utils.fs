module Utils

let rec directoryCopy srcPath dstPath copySubDirs =

    if not <| System.IO.Directory.Exists(srcPath) then
        let msg = System.String.Format("Source directory does not exist or could not be found: {0}", srcPath)
        raise (System.IO.DirectoryNotFoundException(msg))

    if not <| System.IO.Directory.Exists(dstPath) then
        System.IO.Directory.CreateDirectory(dstPath) |> ignore

    let srcDir = new System.IO.DirectoryInfo(srcPath)

    for file in srcDir.GetFiles() do
        let temppath = System.IO.Path.Combine(dstPath, file.Name)
        file.CopyTo(temppath, true) |> ignore

    if copySubDirs then
        for subdir in srcDir.GetDirectories() do
            let dstSubDir = System.IO.Path.Combine(dstPath, subdir.Name)
            directoryCopy subdir.FullName dstSubDir copySubDirs

let rec deleteFiles srcPath pattern includeSubDirs =
    
    if not <| System.IO.Directory.Exists(srcPath) then
        let msg = System.String.Format("Source directory does not exist or could not be found: {0}", srcPath)
        raise (System.IO.DirectoryNotFoundException(msg))

    for file in System.IO.Directory.EnumerateFiles(srcPath, pattern) do
        let tempPath = System.IO.Path.Combine(srcPath, file)
        System.IO.File.Delete(tempPath)

    if includeSubDirs then
        let srcDir = new System.IO.DirectoryInfo(srcPath)
        for subdir in srcDir.GetDirectories() do
            deleteFiles subdir.FullName pattern includeSubDirs

let printJejuneLogo() =
    printfn "%s" """
    __        _                  
   \ \  ___ (_)_   _ _ __   ___ 
    \ \/ _ \| | | | | '_ \ / _ \
 /\_/ /  __/| | |_| | | | |  __/
 \___/ \___|/ |\__,_|_| |_|\___|
          |__/                  
                                                              
"""
